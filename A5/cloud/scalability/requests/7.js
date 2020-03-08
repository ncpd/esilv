mapFunction = function () {
    birthyear = this.birth_date.getFullYear()
    todayyear = new Date().getFullYear()
    age = todayyear - birthyear
    emit(this.current_dept.dept_no, {"age": age, "nb": 1})
}
reduceFunction = function (key,values) {
    sum = 0; nb = 0;
    for (i=0; i < values.length; i++) {
        sum += values[i].age
        nb += values[i].nb
    }
    return {"avg": sum/nb, "age": sum, "nb": nb};
};
queryParam = {"query":{}, "out":{"inline":true}};
db.employees.mapReduce(mapFunction, reduceFunction, queryParam);