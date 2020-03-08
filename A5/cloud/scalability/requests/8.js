mapFunction = function () {
    if(this.gender === "M") {
        emit(this.current_dept.dept_no, {"M": 1, "F": 0, "nb": 1})
    } else {
        emit(this.current_dept.dept_no, {"M": 0, "F": 1, "nb": 1})
    }
}
reduceFunction = function (key,values) {
    sumM = 0; sumF = 0; nb = 0;
    for (i=0; i < values.length; i++) {
        sumM += values[i].M
        sumF += values[i].F
        nb += values[i].nb
    }
    return {"M": sumM, "F": sumF, "nb": nb};
};
queryParam = {"query":{}, "out":{"inline":true}};
db.employees.mapReduce(mapFunction, reduceFunction, queryParam);