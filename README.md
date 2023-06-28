# CQRS and Event Sourcing with Kafka

This repository contains the course materials for learning how to create .NET microservices that adhere to the CQRS (Command Query Responsibility Segregation) and Event Sourcing patterns. Unlike other courses, you won't be using any pre-existing CQRS framework. Instead, you will write every line of code necessary to build your own CQRS and Event Sourcing framework using C# and Apache Kafka. Don't worry, you'll be guided step by step, gaining the knowledge and confidence to become an expert in CQRS and Event Sourcing.

## Course Description
In this course, you will learn the following key concepts and techniques:

- Handle commands and raise events to separate write and read concerns.
- Implement the mediator pattern to create command and query dispatchers.
- Create and modify the state of an aggregate using event messages.
- Set up an event store / write database using MongoDB.
- Create a read database using MS SQL to store denormalized data for efficient querying.
- Apply event versioning to handle changes in event structures.
- Implement optimistic concurrency control to handle concurrent updates.
- Produce events to Apache Kafka for scalable and fault-tolerant event-driven architectures.
- Consume events from Apache Kafka to update and manipulate records in the read database.
- Replay the event store to rebuild the state of an aggregate at any point in time.
- Separate read and write concerns for better scalability and maintainability.
- Structure your code using Domain-Driven Design (DDD) best practices.
- Replay the event store to rebuild the entire read database.
- Migrate the read database from one database type (MS SQL) to another (PostgreSQL) using event replay.

By the end of this course, you will have a comprehensive understanding of CQRS and Event Sourcing, enabling you to design and build highly decoupled and scalable microservices.
