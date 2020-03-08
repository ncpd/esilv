#!/bin/bash

# Script pour simplifier le lancement d'un shard

if [ $# -ne 1 ]; then
    printf "Usage: %s <mongo_RSX.conf>\n" $0
    exit 2
fi

printf "[+] Starting mongod with %s...\n" $1
if mongod -f $1; then
    printf "[+] Done !\n"
    exit 0
else
    printf "[-] mongod exited with code %d\n" $?
    exit 2
fi