cd ..
docker build -t ghcr.io/tuwaiq-academy-development/tuwaiq-forms/tuwaiq-forms-staging:latest -f WebUI/Dockerfile .
docker push ghcr.io/tuwaiq-academy-development/tuwaiq-forms/tuwaiq-forms-staging:latest
#ssh ubuntu@65.1.97.146 'cd docker/tuwaiq-forms/ && bash pull.sh'
#ssh ubuntu@65.1.97.146 'sudo systemctl restart nginx'