# Aggregate Framework
A lightweight framework enabling rapid development of C# applications using DDD

## Moving parts
The framework attempts to simplify the implementation of a [hexagonal architecture](http://alistair.cockburn.us/Hexagonal+architecture) that is well-aligned with DDD principles. You "plug in" your data access code by extending a few simple methods in the AbstractRepository, then you can get right down to writing your business logic which consists of Aggregates and Services.

###AbstractRepository
The AbstractRepository contains a few methods that must be implemented to provide the application with a simple transactional key/value store. The sample app includes an Entity Framework implementation to demonstrate how little code is needed to get up and running with the data access method of your choice.

###Aggregates
Aggregates are simple POCOs that represent entities in a domain model. In an e-commerce application you may have a ShoppingCart aggregate and a Product aggregate for example. The aggregates don't know anything about the state of the world around them. They don't know how they come in to being or what happens to them after they've done their job.

###Services
Services handle the imperative infrastructure tasks of fielding commands from the UI layer, fetching the aggregate from the repository, providing the aggregate with any other collaborators it needs to do its job, calling the correct method on the aggregate, then persisting the updated aggregate.