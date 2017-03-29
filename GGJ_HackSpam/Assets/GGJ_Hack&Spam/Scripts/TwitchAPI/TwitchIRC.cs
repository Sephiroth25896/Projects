using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;

using Ping = System.Net.NetworkInformation.Ping;

public class TwitchIRC : MonoBehaviour
{
	public string Hostname = "irc.twitch.tv";
	public int Port = 6667;
	public string Username = "";
	public string StreamName = "";
	public string OAuthToken = "";

	private const int m_ReadBufferLength = 1024;
	private const string m_EndPacketString = "\r\n";
	private TcpClient m_TcpClient = null;
	private Encoding m_Encoding = Encoding.UTF8;
	private byte[] m_ReadBuffer = null;
	private string m_PartialMessage = "";
	private Queue<string> m_MessageQueue = null;
	private bool m_IsWriting = false;
	private delegate void CommandHandler(MessageIRC message);
	private Dictionary<string, CommandHandler> m_CommandHandlers = null;
	private bool m_FirstMessage = true;

	private void Start()
	{
		if (m_ReadBuffer == null)
		{
			m_ReadBuffer = new byte[m_ReadBufferLength];
		}
		if (m_MessageQueue == null)
		{
			m_MessageQueue = new Queue<string>();
		}
		if (m_CommandHandlers == null)
		{
			m_CommandHandlers = new Dictionary<string, CommandHandler>();
			m_CommandHandlers["ping"] = HandlePingCommand;
			m_CommandHandlers["privmsg"] = HandlePrivateMessage;
		}
	}

	private void OnDestroy()
	{
		Disconnect();
	}

	private void Update()
	{
		if (!m_IsWriting && m_MessageQueue.Count > 0)
		{
			string message = m_MessageQueue.Dequeue();
			SendRawMessage(message);
		}
	}

	public void Connect()
	{
		// We first try to ping twitch.tv to make sure it's reachable
		string data = "abcdefghijklmopqrstuvwxyz012345";
		byte[] buffer = Encoding.ASCII.GetBytes(data);
		int timeout = 5000;
		Ping ping = new Ping();
		ping.PingCompleted += HandleConnectionPingCompleted;
		PingOptions pingOption = new PingOptions(64, true);
		ping.SendAsync(Hostname, timeout, buffer, pingOption);
	}
	
	private void HandleConnectionPingCompleted(object sender, PingCompletedEventArgs e)
	{
		if (m_TcpClient != null)
		{
			Disconnect();
		}
		m_TcpClient = new TcpClient();
		m_TcpClient.BeginConnect(Hostname, Port, ConnectComplete, null);
	}

	private void ConnectComplete(IAsyncResult result)
	{
		m_TcpClient.EndConnect(result);
		if (!m_TcpClient.Connected)
		{
			Debug.LogError("Could not connect to TwitchTV");
			Disconnect(false);
			return;
		}

		m_TcpClient.GetStream().BeginRead(m_ReadBuffer, 0, m_ReadBufferLength, HandleData, null);
		
		if (!string.IsNullOrEmpty(OAuthToken))
		{
			SendRawMessage("PASS {0}", OAuthToken);
		}
		SendRawMessage("NICK {0}", Username.ToLower());
		SendRawMessage("JOIN #{0}", StreamName.ToLower());
	}

	private void HandleData(IAsyncResult result)
	{
		if (m_TcpClient == null)
		{
			return;
		}

		int length = m_TcpClient.GetStream().EndRead(result);
		if (length == 0)
		{
			Debug.Log("Connection dropped by server");
			Disconnect(false);
			return;
		}

		int readOffset = 0;
		while (length > 0)
		{
			int messageLength = Array.IndexOf(m_ReadBuffer, (byte)'\n', readOffset, length);
			messageLength++;
			int bytesToRead = messageLength;
			if (bytesToRead == 0)
			{
				bytesToRead = m_ReadBufferLength;
			}
			bytesToRead -= readOffset;

			StringBuilder rawMessage = new StringBuilder(m_PartialMessage);
			m_PartialMessage = "";
			rawMessage.Append(m_Encoding.GetString(m_ReadBuffer, readOffset, bytesToRead));
			if (rawMessage.Length > 0)
			{
				if (messageLength == 0)
				{
					// raw message is not complete
					m_PartialMessage = rawMessage.ToString();
					break;
				}
				else
				{
					// raw message complete
					MessageIRC ircMessage = new MessageIRC(rawMessage.ToString());
					string commandToLower = ircMessage.Command.ToLower();
					if (m_CommandHandlers.ContainsKey(commandToLower))
					{
						m_CommandHandlers[commandToLower](ircMessage);
					}
					else
					{
						if (m_FirstMessage)
						{
							if (ircMessage.Parameters[0] == "*" && ircMessage.Parameters[1].Contains("Error"))
							{
								Debug.Log(string.Format("Connection error: {0}", ircMessage.Parameters[1]));
								Disconnect(true);
								OnConnectError(ircMessage);
							}
							else
							{
								Debug.Log(string.Format("Connected to TwitchTV as {0}, on {1}'s channel", Username, StreamName));
								OnConnectSuccess();
							}
							m_FirstMessage = false;
						}
						else
						{
#if DEBUG
							Debug.Log(string.Format("Unknown message: {0}", ircMessage.RawMessage));
#endif
						}
					}
				}
			}
			else
			{
				Debug.LogError("Raw message is empty");
			}

			length -= bytesToRead;
			readOffset += bytesToRead;
		}

		m_TcpClient.GetStream().BeginRead(m_ReadBuffer, 0, m_ReadBufferLength, new AsyncCallback(HandleData), null);
	}

	public void SendRawMessage(string message, params object[] format)
	{
		if (m_TcpClient == null)
		{
			return;
		}

		message = string.Concat(string.Format(message, format), "\r\n");
		byte[] data = m_Encoding.GetBytes(message);
		if (!m_IsWriting)
		{
			m_IsWriting = true;
			m_TcpClient.GetStream().BeginWrite(data, 0, data.Length, (IAsyncResult result) =>
			{
				m_TcpClient.GetStream().EndWrite(result);
				m_IsWriting = false;
			}, null);
		}
		else
		{
			m_MessageQueue.Enqueue(message);
		}
	}

	public void Disconnect(bool sendDisconnectMessage = true)
	{
		if (sendDisconnectMessage)
		{
			SendRawMessage("QUIT");
		}

		if (m_TcpClient != null)
		{
			m_TcpClient.Client.BeginDisconnect(false, (IAsyncResult result) =>
	        {
				m_TcpClient.Client.EndDisconnect(result);
				m_TcpClient.GetStream().Close();
				m_TcpClient.Close();
			}, null);
		}
	}

	public delegate void ConnectedHandler();
	public ConnectedHandler ConnectedEvent;
	internal void OnConnectSuccess()
	{
		if (ConnectedEvent != null) ConnectedEvent();
	}

	public delegate void MessageRecievedHandler(MessageIRC message);
	public event MessageRecievedHandler MessageRecievedEvent;
	internal void OnMessageRecieved(MessageIRC message)
	{
		if (MessageRecievedEvent != null) MessageRecievedEvent(message);
	}
	
	public delegate void ConnectErrorHandler(MessageIRC message);
	public event ConnectErrorHandler ConnectErrorEvent;
	internal void OnConnectError(MessageIRC message)
	{
		if (ConnectErrorEvent != null) ConnectErrorEvent(message);
	}

#region Command handlers
	void HandlePingCommand(MessageIRC message)
	{
#if DEBUG
		Debug.Log("got pinged: " + message.Parameters[0]);
#endif
		SendRawMessage("PONG :{0}", message.Parameters[0]);
	}

	void HandlePrivateMessage(MessageIRC message)
	{
#if DEBUG
		string log = string.Format("Message from {0}: {1}", message.Parameters[0], message.Parameters[1]);
		Debug.Log(log);
#endif
		OnMessageRecieved(message);
	}
#endregion Command handlers
}
