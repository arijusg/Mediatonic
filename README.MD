# Mediatonic code test
A simple virtual pet style game, where:
* Users have animals
* Petting animals makes them happy
* Feeing animals makes them less hungry
* Animals start "neutral" on both metrics
* Happiness decreases over time / hunger increases over time
* Users can own multiple animals of different types
* Different animal types have metrics which increase/decrease at different rates

#### Description
It is a basic implementation of the game using WebApi and LocalDB. By deploying to Azure we can scale webapp/sql server. 
Depending on requirements, the whole thing can go under Service Fabric.

* API - The game
* IISHost - Implementation for IIS hosting
* SelfHost - Implementation for Self hosting
* Api.Test - Api tests

#### Usage:
Run: Mediatonic/SelfHost/SelfHost.exe
Api endpoints:
* Get User (Get): localhost:9001/api/user/1 
* Get animals for the user (Get) - localhost:9001/api/animal/user/1
* Pet (Post) - localhost:9001/api/animal/pet
* Feed (Post) - localhost:9001/api/animal/feed