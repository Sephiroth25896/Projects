using UnityEngine;
using System.Collections;

public class WordGenerator
{
	public static string Generate(int numLetter)
	{
		string res = "";
		string alpha = "abcdefghijklmnopqrstuvwxyz";
		int rand = 0;
		for (int i = 0; i< numLetter; i++) {
			rand = (int)(Random.value * 25);
			res += alpha[rand];
		}
		return (res);
	}
}
