all:
	@echo "Running API..."; \
	cd API/ && dotnet run --urls=https://+:7208 & \
	echo "Running WebUI..."; \
	cd WebUI/ && dotnet watch run --no-hot-reload --urls=https://+:8601 & \
	echo "Running Yarn DEV..."; \
	cd WebUI/ClientApp && yarn dev &