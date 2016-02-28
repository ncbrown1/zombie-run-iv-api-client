using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace zombierunivapiclient
{
	public class ApiClient
	{
//		static string api = "https://zombie-run-iv.herokuapp.com";
		static string api = "http://localhost:5000";

		public static Player getPlayer(string name) {
			Player player = null;
			string endpoint = api + "/players/find?name=" + name + "&device_id=" + Util.getDeviceID ();
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create (endpoint);
			HttpWebResponse resp = (HttpWebResponse)req.GetResponse ();

			if (resp.StatusCode == HttpStatusCode.NotFound) {
				req = (HttpWebRequest)WebRequest.Create (endpoint);
				req.Method = "POST";
				resp = (HttpWebResponse)req.GetResponse ();
			} else if (resp.StatusCode != HttpStatusCode.OK) {
				Console.WriteLine ("Could not connect to server");
				return null;
			}
			Stream dataStream = resp.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);
			string data = reader.ReadToEnd();
			Console.WriteLine (data);

			JObject json = JObject.Parse (data);
			player = new Player(
				(int)json["id"],
				(string)json["name"],
				(int)json["hifive_count"],
				(int)json["characters"],
				(int)json["powerup_lvl"]
			);
			return player;
		}

		public static List<Score> getScores() {
			List<Score> scores;

			WebClient client = new WebClient();
			string scores_json = client.DownloadString(api + "/scores");
			//Console.WriteLine(scores);
			JObject joresp = JObject.Parse(scores_json);
			Console.WriteLine(joresp["scores"][0].ToString());
			JArray jscores = (JArray)joresp["scores"];

			scores = jscores.Select(
				o => new Score((int)o["score"],(string)o["player"]["name"], (string)o["time"])
			).ToList();
			return scores;
		}

		public static List<Score> getScores(string name) {
			Player player = ApiClient.getPlayer (name);
			return ApiClient.getScores (player);
		}

		public static List<Score> getScores(Player player) {
			List<Score> scores;
			if (player == null) {
				Console.WriteLine ("Not Found");
				return new List<Score> ();
			}

			WebClient client = new WebClient();
			string scores_json = client.DownloadString(api + "/players/" + player.getID() + "/scores");
			//Console.WriteLine(scores);
			JObject joresp = JObject.Parse(scores_json);
			Console.WriteLine(joresp["scores"][0].ToString());
			JArray jscores = (JArray)joresp["scores"];

			scores = jscores.Select(
				o => new Score((int)o["score"],(string)o["player"]["name"], (string)o["time"])
			).ToList();
			return scores;
		}

		public static int newScore(int score, string name)
		{
			Score s = new Score(score, name, null);
			return ApiClient.newScore(s);
		}

		public static int newScore(Score score)
		{
			string endpoint = api + "/scores?name=" + score.name +
			                  "&device_id=" + Util.getDeviceID () +
			                  "&score=" + score.score;
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create (endpoint);
			req.Method = "POST";
			HttpWebResponse resp = (HttpWebResponse)req.GetResponse ();

			string data = new StreamReader (resp.GetResponseStream ()).ReadToEnd ();
			JObject jdata = JObject.Parse (data);

			if (resp.StatusCode == HttpStatusCode.OK) {
				return (int)jdata ["rank"];
			} else {
				return -1;
			}
		}
	}
}

