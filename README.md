# min-api-with-mongo

docker run -d --name mongodb -p 27017:27017 -v mongodb-data:/data/db -e MONGO_INITDB_ROOT_USERNAME=gbs -e MONGO_INITDB_ROOT_PASSWORD=geheim mongo