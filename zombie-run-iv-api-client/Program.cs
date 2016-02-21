using System;
using System.Collections.Generic;

namespace zombierunivapiclient
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ApiClient.getPlayer ("nick");

			List<Score> scores = ApiClient.getScores("nick");
			foreach (Score score in scores)
			{
			    Console.WriteLine(score);
			}

			Console.WriteLine (Util.getDeviceID ());
			Console.WriteLine ("Hello World!");
		}
	}
}
