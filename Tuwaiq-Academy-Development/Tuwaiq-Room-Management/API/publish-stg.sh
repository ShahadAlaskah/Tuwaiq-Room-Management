
cd ..

docker image rm docker.tuwaiqdev.com/tuwaiq-forms-service:latest
docker image rm docker.tuwaiqdev.com/tuwaiq-forms-service:v1.0
docker image rm tuwaiq-forms-service:latest
docker image rm tuwaiq-forms-service:v1.0

docker build -t docker.tuwaiqdev.com/tuwaiq-forms-service:v1.0 -f API/Dockerfile . 
#--no-cache

docker tag docker.tuwaiqdev.com/tuwaiq-forms-service:v1.0 docker.tuwaiqdev.com/tuwaiq-forms-service:latest

docker push docker.tuwaiqdev.com/tuwaiq-forms-service:v1.0
docker push docker.tuwaiqdev.com/tuwaiq-forms-service:latest