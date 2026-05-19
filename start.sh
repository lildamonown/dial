#!/bin/bash
docker compose -f "$(dirname "$0")/docker-compose.yml" up --build -d && echo "App: http://localhost:8000" && open http://localhost:8000
