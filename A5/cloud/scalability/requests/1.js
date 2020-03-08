opMatch = { $match: {"emp_no": 10001} }
opUnwind = { $unwind: "$salaries" }
opProject = { $project: {emp_no: 1, toDateYear: { $year: "$salaries.to_date" }, amount: "$salaries.salary"} }
opMatch2 = { $match: {"toDateYear" : { $eq: 9999 }} }

db.employees.aggregate([
    opMatch, opUnwind, opProject, opMatch2
])