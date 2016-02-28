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
    * `Player getPlayer(string name)`
      * Preconditions:
        * `name` is some defined string
        * `Util.getDeviceID()` has been defined to return a device UUID
        * The zombie-run-iv-api is online and referenced in `ApiClient`
      * Postconditions:
        * If a player with `name` on the current device already exists, that player is returned; otherwise the player is created and sent to the API
        * A Player class object corresponding to the database record is returned
    * `List<Score> getScores()`
      * Preconditions:
        * zombie-run-iv-api is online and referenced in `ApiClient`
      * Postconditions:
        * returns a list of high scores from the API server
    * `List<Score> getScores(string name)`:
      * Preconditions:
        * `name` is some defined string
        * `Util.getDeviceID()` has been defined to return a device UUID
        * zombie-run-iv-api is online and referenced in `ApiClient`
      * Postconditions:
        * returns a list of high scores earned by player with name `name` on current device from the API server
    * `List<Score> getScores(Player player)`:
      * Preconditions:
        * `player` is some defined Player object
        * `Util.getDeviceID()` has been defined to return a device UUID
        * zombie-run-iv-api is online and referenced in `ApiClient`
      * Postconditions:
        * returns a list of high scores earned by player `player` from API server
    * `int newScore(int score, string name)`:
      * Preconditions:
        * `name` is some defined string
        * `Util.getDeviceID()` has been defined to return a device UUID
        * zombie-run-iv-api is online and referenced in `ApiClient`
      * Postconditions:
        * A new score corresponding to the parameters has been created on the API server
        * Returns the rank of the new score
    * `int newScore(Score score)`:
      * Preconditions:
        * `score` is some defined Score object
        * `Util.getDeviceID()` has been defined to return a device UUID
        * zombie-run-iv-api is online and referenced in `ApiClient`
      * Postconditions:
        * A new score corresponding to the parameter has been created on the API server
        * Returns the rank of the new score
    * `bool saveHiFives(int pid, int val)`:
      * Preconditions:
        * `pid` corresponds to an existing Player on the API server
        * `Util.getDeviceID()` has been defined to return a device UUID
        * zombie-run-iv-api is online and referenced in `ApiClient`
      * Postconditions:
        * The `hifive_count` field of the player has been updated on the API server and database
        * Returns a boolean of whether the operation was a success
    * `bool saveCharacters(int pid, int val)`:
      * Preconditions:
        * `pid` corresponds to an existing Player on the API server
        * `Util.getDeviceID()` has been defined to return a device UUID
        * zombie-run-iv-api is online and referenced in `ApiClient`
      * Postconditions:
        * The `characters` field of the player has been updated on the API server and database
        * Returns a boolean of whether the operation was a success
    * `bool savePowerupLvl(int pid, int val)`:
      * Preconditions:
        * `pid` corresponds to an existing Player on the API server
        * `Util.getDeviceID()` has been defined to return a device UUID
        * zombie-run-iv-api is online and referenced in `ApiClient`
      * Postconditions:
        * The `powerup_lvl` field of the player has been updated on the API server and database
        * Returns a boolean of whether the operation was a success

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
    * `Player (int id, string name, int hifive_count, int characters, int powerup_lvl)`:
      * Preconditions: None
      * Postconditions: an object instance of `Player` is returned
    * `int getID()`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the `id` attribute value is returned
    * `string getName()`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the `name` attribute value is returned
    * `string getDeviceID()`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the `device_id` attribute value is returned
    * `int getHiFives()`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the `hifive_count` attribute value is returned
    * `void setHiFives(int val)`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the `hifive_count` attribute value is updated to be `val`
    * `void saveHiFives()`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the API server's database has been updated to reflect the current `hifive_count` value
    * `int getCharacters()`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the `characters` attribute value is returned
    * `void setCharacters(int val)`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the `characters` attribute value is updated to be `val`
    * `void saveCharacters()`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the API server's database has been updated to reflect the current `characters` value
    * `int getPowerupLvl()`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the `powerup_lvl` attribute value is returned
    * `void setPowerupLvl(int val)`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the `powerup_lvl` attribute value is updated to be `val`
    * `void savePowerupLvl()`
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: the API server's database has been updated to reflect the current `powerup_lvl` value
    * `List<Score> getScores()`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: returns a list of all the scores achieved by the player record this object refers to
    * `int addScore(int score)`:
      * Preconditions: the calling object has been properly instantiated
      * Postconditions:
        * A new score corresponding to the parameter has been created on the API server
        * The rank of the new score is returned

#### Score
  * Fields:
    * `int score` - the score in reference by this object
    * `string name` - the name of the owner of this score
    * `DateTime time` - the time at which this score was created

  * Methods:
    * `Score (int score, string name, string time)`:
      * Preconditions: None
      * Postconditions: an object instance of `Score` is returned
    * `string toJson()` - returns the json representation of this score
      * Preconditions: the calling object has been properly instantiated
      * Postconditions: a string is returned which represents this object in JSON format

## License
Licensed under the [MIT License](https://opensource.org/licenses/MIT).

## Support
Please use GitHub issues for support requests.
