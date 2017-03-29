using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TwitchIRCForm : MonoBehaviour
{
	public Image StreamNameUI = null;
	public Image UsernameUI = null;
	public Image OAuthTokenUI = null;
	public MainMenuManager MainMenuManager = null;
	public Menu ConnectErrorMenu = null;
	public Menu ConnectSuccessMenu = null;

	private TwitchIRC m_TwitchIRC = null;
	private Menu m_MenuToShow = null;
	private string m_StreamName = "";
	private string m_Username = "";
	private string m_OAuthToken = "";

	private void Start()
	{
		if (PlayerPrefs.HasKey("StreamName"))
		{
			m_StreamName = PlayerPrefs.GetString("StreamName");
			StreamNameUI.GetComponentInChildren<InputField>().text = m_StreamName;
		}
		if (PlayerPrefs.HasKey("Username"))
		{
			m_Username = PlayerPrefs.GetString("Username");
			UsernameUI.GetComponentInChildren<InputField>().text = m_Username;
		}
		if (PlayerPrefs.HasKey("OAuthToken"))
		{
			m_OAuthToken = PlayerPrefs.GetString("OAuthToken");
			OAuthTokenUI.GetComponentInChildren<InputField>().text = m_OAuthToken;
		}
	}

	private void Update()
	{
		if (m_MenuToShow != null)
		{
			MainMenuManager.ShowMenu(m_MenuToShow);
			m_MenuToShow = null;
		}
	}

	public void OpenOAuth2Webpage()
	{
		Application.OpenURL("https://twitchapps.com/tmi/");
	}
	
	public void UpdateTwitchIRCStreamName(string streamname)
	{
		m_StreamName = streamname;
		PlayerPrefs.SetString("StreamName", streamname);
		PlayerPrefs.Save();
		if (!string.IsNullOrEmpty(streamname))
		{
			StreamNameUI.CrossFadeColor(Color.white, 0.5f, true, false);
		}
		else
		{
			StreamNameUI.CrossFadeColor(Color.red, 0.5f, true, false);
		}
		UsernameUI.GetComponentInChildren<Selectable>().Select();
	}
	
	public void UpdateTwitchIRCUsername(string username)
	{
		m_Username = username;
		PlayerPrefs.SetString("Username", username);
		PlayerPrefs.Save();
		if (!string.IsNullOrEmpty(username))
		{
			UsernameUI.CrossFadeColor(Color.white, 0.5f, true, false);
		}
		else
		{
			UsernameUI.CrossFadeColor(Color.red, 0.5f, true, false);
		}
		OAuthTokenUI.GetComponentInChildren<Selectable>().Select();
	}
	
	public void UpdateTwitchIRCOAuthToken(string oauth)
	{
		m_OAuthToken = oauth;
		PlayerPrefs.SetString("OAuthToken", oauth);
		PlayerPrefs.Save();
		if (!string.IsNullOrEmpty(oauth))
		{
			OAuthTokenUI.CrossFadeColor(Color.white, 0.5f, true, false);
		}
		else
		{
			OAuthTokenUI.CrossFadeColor(Color.red, 0.5f, true, false);
		}
	}

	public void ConnectTwitchIRC()
	{
		bool error = false;
		if (string.IsNullOrEmpty(m_StreamName))
		{
			error = true;
			StreamNameUI.CrossFadeColor(Color.red, 0.5f, true, false);
		}
		if (string.IsNullOrEmpty(m_Username))
		{
			error = true;
			UsernameUI.CrossFadeColor(Color.red, 0.5f, true, false);
		}
		if (string.IsNullOrEmpty(m_OAuthToken))
		{
			error = true;
			OAuthTokenUI.CrossFadeColor(Color.red, 0.5f, true, false);
		}

		if (!error)
		{
			if (m_TwitchIRC != null)
			{
				DestroyImmediate(m_TwitchIRC.gameObject);
			}
			GameObject twitchObj = new GameObject("TwitchIRC");
			m_TwitchIRC = twitchObj.AddComponent<TwitchIRC>();
			m_TwitchIRC.ConnectedEvent += HandleTwitchIRCConnected;
			m_TwitchIRC.ConnectErrorEvent += HandleConnectErrorEvent;
			m_TwitchIRC.StreamName = m_StreamName;
			m_TwitchIRC.Username = m_Username;
			m_TwitchIRC.OAuthToken = m_OAuthToken;
			
			Object.DontDestroyOnLoad(twitchObj);

			m_TwitchIRC.Connect();
		}
	}

	public void HandleTwitchIRCConnected()
	{
		m_MenuToShow = ConnectSuccessMenu;
	}
	
	private void HandleConnectErrorEvent(MessageIRC message)
	{
		m_MenuToShow = ConnectErrorMenu;
	}
}
