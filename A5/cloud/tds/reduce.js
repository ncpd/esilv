mapFunction = function () {
    for(i=0; i<this.reviews.length; i++)
        emit(this.reviews[i].source, this.reviews[i].wordsCount);
};
reduceFunction = function (key,values) {
    return Array.avg(values);
};
queryParam = {"query":{}, "out":{"inline":true}};
db.tourPedia_paris.mapReduce(mapFunction, reduceFunction, queryParam);

/* ========================================= */

mapFunction = function () {
    for(i=0; i<this.reviews.length; i++)
        emit(this.reviews[i].source,
             {"sum": this.reviews[i].wordsCount, "nb": 1});
};
reduceFunction = function (key,values) {
    sum = 0; nb = 0;
    for (i=0; i < values.length; i++) {
        sum += values[i].sum
        nb += values[i].nb
    }
    return {"avg": sum/nb, "sum": sum, "nb": nb};
};
queryParam = {"query":{}, "out":{"inline":true}};
db.tourPedia_paris.mapReduce(mapFunction, reduceFunction, queryParam);

/* ========================================= */

mapFunction = function () {
    for(i=0; i<this.reviews.length; i++)
        emit(this.reviews[i].source,
             {"sum": this.reviews[i].rating, "nb": 1,
              "min": this.reviews[i].rating,
              "max": this.reviews[i].rating});
};
reduceFunction = function (key,values) {
    sum = 0; nb = 0;min = values[0].min; max = values[0].max;
    for (i=0; i < values.length; i++) {
        sum += values[i].sum
        nb += values[i].nb
        if(min >values[i].min) min = values[i].min
        if(max <values[i].max) max = values[i].max
    }
    return {"avg": sum/nb, "sum": sum, "nb": nb, "min": min, "max": max};
};
queryParam = {"query":{}, "out":{"inline":true}};
db.tourPedia_paris.mapReduce(mapFunction, reduceFunction, queryParam);

/* ========================================= */

mapFunction = function() {
    if(this.services && this.services.length > 0)
        emit(null, {"services": this.services})
}
reduceFunction = function (key,values) {
    output = new Array();
    for(i=0;i < values.length ; i++) {
        for(j=0; j < values[i].services.length; j++) {
            service = values[i].services[j]
            if(!Array.contains(output, service))
                output.push(service)
        } 
    }
    return {"services": output}
};
queryParam = {"query":{}, "out":{"inline":true}};
db.tourPedia_paris.mapReduce(mapFunction, reduceFunction, queryParam);

/* ========================================= */

mapFunction = function() {
    sources = {}
    for(var i=0; i < this.reviews.length; i++) {
        review = this.reviews[i]
        if(sources[review.source]) {
            src = sources[review.source];
            if(!Array.contains(src.langs, review.language)) {
                src.langs.push(review.language)
                src.nb++;
            } 
        } else src = {"langs": [review.language], "nb": 1};
        sources[review.source] = src
    }
    for(key in sources) emit(key, sources[key])
}
reduceFunction = function (key,values) {
    var distinct = values[0].nb;
    var langs = values[0].langs;
    for(i=1;i<values.length; i++) {
        l = values[i]
        for(j=0;j<l.langs.length;j++) {
            if(!Array.contains(langs, l.langs[j])) {
                langs.push(l.langs[j])
                distinct++
            }
        }
    }
    return {"langs": langs, "nb": distinct};
};
queryParam = {"query":{}, "out":{"inline":true}};
db.tourPedia_paris.mapReduce(mapFunction, reduceFunction, queryParam);

/* ========================================= */

mapFunction = function() {
    for(i=0; i<this.reviews.length; i++) {
        review = this.reviews[i];
        if(review.time) annee = review.time.substring(0, 4)
        else annee = null
        emit({"src": review.source, "y": annee}, 1)
    }
}
reduceFunction = function (key,values) {
    return Array.sum(values)
};
queryParam = {"query":{}, "out":{"inline":true}};
db.tourPedia_paris.mapReduce(mapFunction, reduceFunction, queryParam);
db.result.aggregate([
    {$group: {"_id": "$_id.src",
              "moy": {$avg: "$value"}}}
])

/* ========================================= */
/* noy yet complete */

mapFunction = function() {
    for(i=0; i<this.reviews.length; i++) {
        review = this.reviews[i];
        if(review.time) annee = review.time.substring(0, 4)
        else annee = null
        annees = {};annees[annee] = 1; annees["avg"] = 1;
        emit(review.source, annees)
    }
}
reduceFunction = function (key,values) {
    annees = {}
    for(i=0; i<values.length; i++) {
        v = values[i]
        for(key in v) {
            if(key !== "avg") {
                if(annees[key]) {
                    annees[key] += v[key]
                } else {
                    annees[key] = v[key]
                }
            }
        }
    }
    nb_values = 0
    sum_values = 0
    for(key in annees) {
        sum_values += annees[key]
        nb_values++
    }
};
queryParam = {"query":{}, "out":{"inline":true}};
db.tourPedia_paris.mapReduce(mapFunction, reduceFunction, queryParam);
db.result.aggregate([
    {$group: {"_id": "$_id.src",
              "moy": {$avg: "$value"}}}
])