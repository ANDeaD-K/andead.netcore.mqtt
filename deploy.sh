#!/bin/bash
set -ev

echo "dotnet andead.netcore.mqtt.dll --mqtt-server=$MQTT_SERVER --mqtt-topic=$MQTT_TOPIC" >> ./publish/entrypoint.sh

docker build -t andead/netcore.mqtt:latest publish/.

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker push andead/netcore.mqtt:latest