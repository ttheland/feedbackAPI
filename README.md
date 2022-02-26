# feedbackAPI

Asynchronous RESTful API built with .NET, complete with persistent entities via MongoDB running in a Docker container.

This example API made for demonstration purposes is for collecting person-project feedback, but not all functionality is implemented.

Project is configured to run from VSCode with one press of `f5`.

to run the Docker container: In VSCode terminal: `docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo`

As usual when running, the SwaggerUI API documentation is available at `http://localhost:5001/swagger/index.html`

Tested with Postman.

This work is based on Julio Casal's wonderful [.NET 5 REST API Tutorial - Build From Scratch With C#](https://youtu.be/ZXdFisA_hOY)

