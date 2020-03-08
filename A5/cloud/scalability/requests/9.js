mapFunction = function () {
    emp_no = this.emp_no
    for(i=0; i < this.titles.length;i++) {
        if(this.titles[i].to_date.getYear() + 1900 === 9999 && this.titles[i].isManager) {
            emit(this.gender, {"nb": 1})
        }
    }
}
reduceFunction = function (key,values) {
    return {"nb": values.length};
};
queryParam = {"query":{}, "out":{"inline":true}};
db.employees.mapReduce(mapFunction, reduceFunction, queryParam);