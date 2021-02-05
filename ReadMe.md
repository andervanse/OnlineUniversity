## Arquitecture overview

#### Description
To achieve the goals of this project, it would take two microservices subscribing a queue in the cloud, one to receive signUp data, persist it, and send e-mail to the student with the result.
After the success of the students subscription to a course, another microservice would be necessary to update course statistics, updating data in a exclusive statistics database.
As an alternative the statistics database could be syncronized by a scheduled job.

#### What to test
A postman collection is available to test the API in the "Assets" folder and the application will initialize with some data.
Pick a course id from "Courses" endpoint and associate it to a student to do a sign-up request.
As a result calling "CourseStatistics" endpoint will list an updated view of courses.
There are unit tests in the source code for more details.

#### About the source code
The project was developed with DDD concepts in mind resulting on the following separation of  layers:

- ##### Web API
   Responsible for exposing the funcionalities through HTTP using RESTFull concepts.
- ##### Application
   Responsible for coordinating entities, services and repositories from the domain Layer and taking care of mapping between DTO's and Domain entities.
- ##### Domain
  Home of all domain logic in the application. 
- ##### Infra/Data
  Repositories Implementation of Interfaces in the Domain.  
- ##### Infrastructure
  Logging and Email Implementation.
  
  #### Technologies
  - ##### asp.net core 3.0
  - ##### swagger
  - ##### Automapper
  - ##### FluentValidator
  - ##### Entity Framework Core
  - ##### xUnit
  
  #### Observations
  It took more time that I expected, but it was fun implementing this from the ground up.
  I wish I had implemented at least 1 microservice subscribing a queue and put it all on a docker compose.

  
   
