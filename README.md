# ProductCategorySolution 🎉

ProductCategorySolution contains four projects 🏗️
* **Infrastructure** -> Contains models about connection database, cache etc. If we had RabbitMQ, Kafka or other third party app integrations, this project would be the place.
* **Service** -> Contains business models where presentation and infrastructure projects meet. This layer consumes all resources from Infrastructure.
* **Presentation** -> Contains endpoint where users connect the application. Also holds sensitive configurations of the application.
* **ProductCategoryTests** -> Project that contains unit tests. xUnit has been used.

Some features of the solution ✨

* Project uses MongoDB as database.
* Project uses Redis for caching.
* Project has healthcheck feature.
* API has swagger documentation.
* API has JWT authentication.
* API using a middleware for exception handling.
* Project using SeriLog for logging.
* Solution contains unit tests.

How to Run 🚀

* Run MongoDB and Redis at your local. Assign their connection strings and endpoints under Presentation/appsetings.json.
* Run the project. See if everything is clear by checking _/hc_ endpoint.
* Call _token/get-token_ by swagger. Password is **2021Hepsiexpress2021** You should use this jwt token for all other endpoints. If you want to disable token feature, you can simply remove 'Authorize' attribute class from HepsiController. HepsiController is the base controller for all other controllers.
* Now you are ready to create/fetch/filter/update/delete all product and category data.
* You can get errors via bad parameters on purpose and check the error logs under Presentation/Logs/log.txt.

## Diagrams 📸
_Desing of Solution_

![Screenshot_2](https://user-images.githubusercontent.com/47561392/135548140-62e9c222-01db-421f-a084-24988144b828.png)

## What could be better ? 💡
* Encrypting and decrypting productId and categoryId in GET requests would be better since they would be considered as sensitive data.
* A message queue system for POST request could be used. An orchestrator application would adress requests to right queues. I am limiting this design with only POST calls, since GET expects a model right away and also PUT, DELETE are not suitable for working eventually consistent in Product/Category domain. A price change or removing a product are real-time events and can't wait to be consumed in a queue.

_Diagram of EventBus Design_

![MessageQueue](https://user-images.githubusercontent.com/47561392/135656897-01fb181f-f96b-4320-905b-032cfb2f58ac.png)
