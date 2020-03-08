#!/usr/bin/python3

from pymongo import MongoClient
import sys
import time

host = 'devincimdb1046.westeurope.cloudapp.azure.com' # routeur mongos
port = 30000
dbName = 'employee'

def connect():
    client = MongoClient(host, port)
    return client

def stats():
    # Renvoie des stats sur les shards à propos d'une collection
    client = connect()
    db = client[dbName]
    collection = db.employees
    result = db.command("collstats", "employees")
    print(result["shards"])

def insert(document):
    # Insère un document en base MongoDB
    try:
        client = connect()
        db = client[dbName]
        collection = db.employees
        doc = collection.insert_one(document)
        client.close()
    except:
        print('[-] Exception occured while inserting document')
        print("[-] Unexpected error: {}", sys.exc_info()[0])

def find(filter, projection):
    # Find générique sur la base MongoDB
    try:
        client = connect()
        db = client[dbName]
        collection = db.employees
        doc = collection.find(filter, projection)
        l = list(doc)
        return l
    except:
        print('[-] Exception occured while finding document')
        print("[-] Unexpected error: {}", sys.exc_info()[0])
