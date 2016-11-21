## Mediatonic code test 

It is a basic implementation of the game using webapi and localdb. By deploying to Azure we can scale webapp/sql server. 
Depending on requirements, the whole thing can go under Service Fabric.

##### API - The game
##### IISHost - Implementation for IIS hosting
##### SelfHost - Implementation for Self hosting
##### Api.Test - Api tests

##### Usage:
Run: Mediatonic/SelfHost/SelfHost.exe
Api endpoints:
- User: localhost:9001/api/user/1
- Animals: 
-- Get animals for the user (Get) - localhost:9001/api/animal/user/1
-- Pet (Post) - localhost:9001/api/animal/pet
-- Feed (Post) - localhost:9001/api/animal/feed
