#!/usr/bin/python3

from utils import uploader
from utils import dumper
from bson import json_util
from multiprocessing.pool import ThreadPool as Pool
import random
import json
import time

pool_size = 256
logfile = 'employees.log'

# Parallélisation des tâches de dump
def worker(item):
    try:
        result = dumper.dump_employee(int(item))
        uploader.insert(result)
        # Utilitaire afin de mesurer l'avancée du programme à l'aide du script percentage.sh
        # watch -n 0.5 ./percentage.sh
        with open(logfile, 'a+') as f:
            f.write("Dumped employee no.{}\n".format(item))
    except:
        print('error with employee {}'.format(item))

pool = Pool(pool_size)

start = time.time()
# On boucle sur les ids récupérés auparavant avec dumper.dump_employees_ids()
for line in open('ids.txt', 'r'):
    pool.apply_async(worker, (int(line.strip()),))

pool.close()
pool.join()
end = time.time()

print('Time elapsed: ' + str(end - start))
