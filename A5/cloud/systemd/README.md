# Installation

## Shards

```bash
vim ~/cloud/init/mongod-shard.service
```

Décommenter la ligne du shard correspondant :
```
[...]
# ExecStart=/usr/bin/mongod -f /home/administrateur/cloud/config/mongo_RS1.conf
# ExecStart=/usr/bin/mongod -f /home/administrateur/cloud/config/mongo_RS2.conf
# ExecStart=/usr/bin/mongod -f /home/administrateur/cloud/config/mongo_RS3.conf
# ExecStart=/usr/bin/mongod -f /home/administrateur/cloud/config/mongo_RS4.conf
# ExecStart=/usr/bin/mongod -f /home/administrateur/cloud/config/mongo_RS5.conf
# ExecStart=/usr/bin/mongod -f /home/administrateur/cloud/config/mongo_RS6.conf
[...]
```

```bash
sudo cp mongod-shard.service /etc/systemd/system/mongod-shard.service
sudo chmod 644 /etc/systemd/system/mongod-shard.service
```

```bash
sudo systemctl start mongod-shard # Starts the service
sudo systemctl status mongod-shard # Service status
sudo systemctl enable mongod-shard # Start the service at boot
```

## Config Server

```bash
sudo cp mongod-cfg.service /etc/systemd/system/mongod-cfg.service
sudo chmod 644 /etc/systemd/system/mongod-cfg.service
```

```bash
sudo systemctl start mongod-cfg # Starts the service
sudo systemctl status mongod-cfg # Service status
sudo systemctl enable mongod-cfg # Start the service at boot
```

## Router

```bash
sudo cp mongos.service /etc/systemd/system/mongos.service
sudo chmod 644 /etc/systemd/system/mongos.service
```

```bash
sudo systemctl start mongos # Starts the service
sudo systemctl status mongos # Service status
sudo systemctl enable mongos # Start the service at boot
```