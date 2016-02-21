using System;

namespace zombierunivapiclient
{
	public class Player
	{
		int id { get; }
		string name { get; set; }
		string device_id { get; set; }
		int hifive_count { get; set; }
		int characters { get; set; }
		int powerup_lvl { get; set; }

		public Player (int id, string name, int hifive_count, int characters, int powerup_lvl) {
			this.id = id;
			this.device_id = Util.getDeviceID ();
			this.name = name;
			this.hifive_count = hifive_count;
			this.characters = characters;
			this.powerup_lvl = powerup_lvl;
		}

		public int getID() {
			return this.id;
		}

		public string getName() {
			return this.name;
		}

		public int getHiFives() {
			return this.hifive_count;
		}

		public int getCharacters() {
			return this.characters;
		}

		public int getPowerupLvl() {
			return this.powerup_lvl;
		}
	}
}

