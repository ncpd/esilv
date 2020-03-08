mapFunction = function () {
    sum = 0; nb = 0;
    for(i=0; i < this.salaries.length;i++) {
        sum += this.salaries[i].salary
        nb++
    }
    emit(this.current_dept.dept_no, {"sum": sum, "nb": nb})
}
reduceFunction = function (key,values) {
    sum = 0; nb = 0;
    for (i=0; i < values.length; i++) {
        sum += values[i].sum
        nb += values[i].nb
    }
    return {"avg": sum/nb, "sum": sum, "nb": nb};
};
queryParam = {"query":{}, "out":{"inline":true}};
db.employees.mapReduce(mapFunction, reduceFunction, queryParam);