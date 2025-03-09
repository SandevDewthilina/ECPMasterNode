# Monitoring Configurations
## Loki & Promtail installation

1. Install loki docker driver

```shell
docker plugin install grafana/loki-docker-driver:latest --alias loki --grant-all-permissions
```

2. Add following to docker daemon.json at `/etc/docker/daemon.json`

```json
{
    "log-driver": "loki",
    "log-opts": {
        "loki-url": "http://<loki address>:3100/loki/api/v1/push",
        "loki-batch-size": "400"
    }
}
```
Finally restart the docker service

```shell
 sudo systemctl restart docker
```