# Zombie Run IV API Client (C#)

This code, written in C#, is not meant to be used on its own. Instead, it should be imported or copied into an existing project (originally meant for a Unity game project) and used as a library.

Before you use this library, you should have an instance of the [zombie-run-iv-api](https://github.com/ncbrown1/zombie-run-iv-api) running. Put the URL it is running on into the field at the top of zombie-run-api-client/ApiClient.cs. Ideally, you would have this URL in some global configuration variable, which you would place there instead.

In addition to having the actual API up and running, you should probably come up with a way to uniquely identify devices through the zombie-run-api-client/Util.cs "getDeviceID()" function. This could return the device's MAC address, the Processor ID, or some other UUID you can retrieve from the machine the program will be running on.

## Usage

The usage as thought out when developing this library is as follows:
  1. Retrieve name from user
  1. Retrieve Player object via `ApiClient.getPlayer(name)`
  1. ...Do other system things with app...
  1. Use other operations on Player object and ApiClient as needed.
    * to get list of high scores: `ApiClient.getScores()`
    * to add a new high score: `player.addScore(score_int)`
    * to get players's high score: `player.getScores()`
    * to update players' hifive_count value:
      1. `player.setHiFives(count)`
      1. `player.saveHiFives()`
    * to update players' characters value:
      1. `player.setCharacters(value)`
      1. `player.saveCharacters()`
    * to update players' powerup_lvl:
      1. `player.setPowerupLvl(level)`
      1. `player.savePowerupLvl()`

## Classes

#### ApiClient
  * Fields
    * `string URL` - the URL the API is running on

  * Methods: (all statically called, i.e. `ApiClient.getPlayer("nick")`)
    * `Player getPlayer(string name)` - gets or creates a Player object corresponding to `name` and the current device's `Util.getDeviceID()` value from the API
    * `List<Score> getScores()` - returns a list of high scores from the API server
    * `List<Score> getScores(string name)` - returns a list of high scores earned by player with name `name` on current device from the API server
    * `List<Score> getScores(Player player)` - returns a list of high scores earned by player `player` from API server
    * `int newScore(int score, string name)` - sends a new score to the API server, which adds the score to the database and returns the rank of the new score
    * `int newScore(Score score)` - same as above, but with a Score object
    * `bool saveHiFives(int pid, int val)` - Updates the `hifive_count` value of the player with id `pid` on the API server, and returns true if successful, false otherwise
    * `bool saveCharacters(int pid, int val)` - Updates the `characters` value of the player with id `pid` on the API server, and returns true if successful, false otherwise
    * `bool savePowerupLvl(int pid, int val)` - Updates the `powerup_lvl` value of the player with id `pid` on the API server, and returns true if successful, false otherwise

#### Player

The Player class corresponds to an individual `player` object from the API and its database. Most operations required can be completed through an object of this class type.

  * Fields:
    * `int id` - the unique ID of this player in the database
    * `string name` - the name of this player
    * `string device_id` - the UUID of the device this player is registered on
    * `int hifive_count` - the number of high fives this player has obtained over their entire career
    * `int characters` - the number of characters this player has unlocked
    * `int powerup_lvl` - the powerup level this player has reached

  * Methods:
    * `Player (int id, string name, int hifive_count, int characters, int powerup_lvl)` - constructor
    * `int getID()` - returns the player's `id` field value
    * `string getName()` - returns the player's `name` field value
    * `string getDeviceID()` - returns the player's `device_id` field value
    * `int getHiFives()` - returns the player's `hifive_count` field value
    * `void setHiFives(int val)` - sets the player's `hifive_count` field to `val`
    * `void saveHiFives()` - uploads the player's `hifive_count` value to the server for persistence
    * `int getCharacters()` - returns the player's `characters` field value
    * `void setCharacters(int val)` - sets the player's `characters` field to `val`
    * `void saveCharacters()` - uploads the player's `characters` value to the server for persistence
    * `int getPowerupLvl()` - returns the player's `powerup_lvl` field value
    * `void setPowerupLvl(int val)` - sets the player's `powerup_lvl` field to `val`
    * `void savePowerupLvl()` - uploads the player's `powerup_lvl` value to the server for persistence
    * `List<Score> getScores()` - gets the list of scores achieved by this player
    * `int addScore(int score)` - adds the new score to the API and its database, and returns the rank of the new score

#### Score
  * Fields:
    * `int score` - the score in reference by this object
    * `string name` - the name of the owner of this score
    * `DateTime time` - the time at which this score was created

  * Methods:
    * `Score (int score, string name, string time)` - constructor
    * `string toJson()` - returns the json representation of this score

## License
Licensed under the [MIT License](https://opensource.org/licenses/MIT).

## Support
Please use GitHub issues for support requests.
