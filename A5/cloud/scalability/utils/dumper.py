#!/usr/bin/python3

import mysql.connector
import datetime

def connect():
    # Connextion à la base de données locale
    db = mysql.connector.connect(
        host="localhost",
        user="root",
        passwd="V8eOFR%_",
        database="employee"
    )
    return db
    
def execute(query):
    # Execution d'une requête générique et renvoi des resultats
    try:
        db = connect()
        cursor = db.cursor()
        cursor.execute(query)
        result = cursor.fetchall()
        cursor.close()
        db.close()
        return result
    except mysql.connector.Error as err:
        print('[-] An error occured while executing the following query: {} - {}'.format(query, type(err)))
        # On log dans un fichier si jamais une erreur arrive
        f = open('errors.log', 'a')
        f.write('ERROR: {}\nQuery : {}\n'.format(err, query))
        f.close()
        return None

def castDate(date):
    # On cast le type date qui provient de MySQL en datetime pour MongoDB
    dt = datetime.datetime.combine(date, datetime.datetime.min.time())
    return dt

def castDateFields(document):
    # Cast tous les champs de type date en datetime
    for key in document:
        if type(document[key]) is datetime.date:
            document[key] = castDate(document[key])
    return document

def getManagerTitle(deptManager, fromDate, toDate):
    # Utilitaire pour mapper les dates des titres aux dates de dept_manager,
    # afin de savoir si l'employé était manager lorsqu'il occupait ce poste
    for row in deptManager:
        if(row[2] == fromDate and row[3] == toDate):
            return row
        
def dump_employee(empNo):
    result = execute('SELECT * from employees WHERE emp_no = {}'.format(empNo))
    result = result[0]
    employee = {
        "_id": result[0],
        "emp_no": result[0],
        "birth_date": result[1],
        "first_name": result[2],
        "last_name": result[3],
        "gender": result[4],
        "hire_date": result[5]
    }
    titles = dump_titles(empNo)
    if titles is not None:
        employee["titles"] = titles
    salaries = dump_salaries(empNo)
    if salaries is not None:
        employee["salaries"] = salaries
    employee = dump_depts(empNo, employee)
    return castDateFields(employee)

def dump_titles(empNo):
    try:
        # Récupération des titres d'un employé
        result = execute('SELECT * FROM titles WHERE emp_no = {}'.format(empNo))
        # Récupération des dates où un employé a été manager
        mgmt = execute('SELECT * FROM dept_manager WHERE emp_no = {}'.format(empNo))
        titles = []
        for row in result:
            title = {
                "title": row[1],
                "from_date": row[2],
                "to_date": row[3]
            }
            title["isManager"] = True if getManagerTitle(mgmt, row[2], row[3]) is not None else False
            titles.append(castDateFields(title))
            return titles
    except:
        return []

def dump_salaries(empNo):
    # Récupération des salaires
    try:
        result = execute('SELECT * FROM salaries WHERE emp_no = {}'.format(empNo))
        salaries = []
        for row in result:
            salary = {
                "salary": row[1],
                "from_date": row[2],
                "to_date": row[3]
            }
            salaries.append(castDateFields(salary))
        return salaries
    except:
        return []

def dump_depts(empNo, document):
    # Récupération de l'historique des départements d'un employé, classés par date
    result = execute('SELECT * FROM dept_emp dpe INNER JOIN departments d ON dpe.dept_no = d.dept_no WHERE emp_no = {} ORDER BY to_date'.format(empNo))
    dept_history = []
    for row in result:
        dept = {
            "dept_name": row[5],
            "dept_no": row[1],
            "from_date": row[2],
            "to_date": row[3]
        }
        # On regarde si l'année est 9999 = année actuelle, sinon on l'ajoute à l'historique
        if(row[3].year == 9999):
            # On met uniquement les champs necéssaires dans current_dept
            dept.pop("to_date", None)
            document["current_dept"] = dept
        else:
            dept_history.append(castDateFields(dept))
    if(len(dept_history) > 0):
        document["dept_history"] = dept_history
    if not "current_dept" in document:
        # Si on a pas trouvé de current_dept alors l'employé ne travaille plus dans l'entreprise
        document["current_dept"] = {
            "dept_name": "No longer employed",
            "dept_no": "d000",
            "from_date": result[-1][3] # date de fin d'embauche (dernière ligne, champ to_date)
        }
    document["current_dept"] = castDateFields(document["current_dept"]) # On cast les types date en datetime
    return document

def dump_employees_ids():
    # Utilitaire pour récupérer une liste des ids des employés pour les parcourir par la suite
    result = execute('SELECT emp_no FROM employees')
    print(result)
    output = open('ids.txt', 'w')
    ids = []
    for row in result:
        output.write("%s\n" % row[0])
    return ids
