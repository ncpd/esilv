opMatch = { $match: { "current_dept.dept_no": "d004", 
                      "current_dept.from_date" : {"$gte": new Date("2002-01-13T00:00:00.000+0000")} } };
opProject = { $project: { "first_name": 1, "last_name": 1 } }
opSort = { $sort: { "current_dept.from_date": -1 } }
         
db.employees.aggregate([
    opMatch, opProject, opSort
])