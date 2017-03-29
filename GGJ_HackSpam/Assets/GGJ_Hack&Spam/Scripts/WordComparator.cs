using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public static class WordComparator
{
	public static int Comparator(string chat, string key)
	{
		int i = 0;
		for (; i < chat.Length && i < key.Length; i++)
		{
			if (chat[i] != key[i])
			{
				return i;
			}
		}

		return i;
	}
}
