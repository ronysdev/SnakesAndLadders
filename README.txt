Multiplayer Snakes and Ladders game

Swagger available at - [domain]/swagger/index.html


All services/providers are registered in the .NET Core IoC container

Players data are saved in a concurrent dictionary,
could alternatively implement using DistCache with DI (production grade),
Since Memory/DistributedCache does not allow key enumartion - chose ConcurrentDictionary for simplicity.

Main Objects - 
ConfigModel - defines game policy (snakes and ladders), using levelConfig.json.
Dice - returns random dice throw - registered as transient for thread safety
Level - defines players movement and validity using ConfigModel
SnakesAndLadders - Facade for our API, responsible for adding user / acquiring his status.

When running locally please make sure you have levelConfig.json in your current domain base directory.

Unit Testing with xUnit




