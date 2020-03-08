mapFunction = function () {
    emp_no = this.emp_no
    for(i=0; i < this.titles.length;i++) {
        if(this.titles[i].to_date.getYear() + 1900 === 9999) {
            emit(this.titles[i].title, {"emp_no": [emp_no] })
        }
    }
}
reduceFunction = function (key,values) {
    emps = []
    for(i=0;i<values.length; i++) {
        for(j=0;j<values[i].emp_no.length;j++) {
            emps.push(values[i].emp_no[j])
        }
    }
    return {"emp_no": emps};
};
queryParam = {"query":{}, "out":{"inline":true}};
db.employees.mapReduce(mapFunction, reduceFunction, queryParam);