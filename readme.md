### Running The Project

To get up and running you will need docker and docker compose.

The database schemas will automatically be applied.

Build the containers
```shell
docker compose build
```

Run the containers

```shell
docker compose up
```

### Testing The API

To test the API you will need to open http://localhost:5001/swagger after the containers have started up.

The first time you create an account on the identity service you will need to manually verify your email address by clicking on the link on the "Confirm Email" page.
