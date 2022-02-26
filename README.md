# feedbackAPI

Asynchronous RESTful API built with .NET, complete with persistent entities via MongoDB running in a Docker container.

This example API made for demonstration purposes is for collecting person-project feedback, but not all functionality is implemented.

Project is configured to run from VSCode with one press of `f5`.

to run the Docker container (from dockerhub): In VSCode terminal: `docker run -it --rm -p 8080:80 -e MongoDbSettings:Host=mongo --network=feedbackapidemo ttheland/feedbackapi:v1`

As usual when running locally, the SwaggerUI API documentation is available at `https://localhost:5001/swagger/index.html` OR `http://localhost:5000`.

Tested with Postman, unit testing using Xunit.

This work is based on Julio Casal's wonderful [.NET 5 REST API Tutorial - Build From Scratch With C#](https://youtu.be/ZXdFisA_hOY)

