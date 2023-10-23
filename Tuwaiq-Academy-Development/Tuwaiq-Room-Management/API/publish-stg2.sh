
cd ..

docker build -t ghcr.io/tuwaiq-academy-development/tuwaiq-forms/tuwaiq-service-forms-staging:latest -f API/Dockerfile .
docker push ghcr.io/tuwaiq-academy-development/tuwaiq-forms/tuwaiq-service-forms-staging:latest