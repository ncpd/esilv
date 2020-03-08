opMatch = { $match: {"current_dept.dept_no": "d007"} }
opUnwind = { $unwind: "$titles" }
opProject = { $project: {emp_no: 1, currentDept: "$current_dept", firstName: "$first_name", lastName: "$last_name", toDateYear: { $year: "$titles.to_date" }, isManager: "$titles.isManager"} }
opMatch2 = { $match: {"toDateYear" : { $eq: 9999 }, "isManager": true} }

db.employees.aggregate([
    opMatch, opUnwind, opProject, opMatch2
])