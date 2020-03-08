### /authors - all authors  
### /authors/:id - specific author + his books  
### /books - all books   
### /book/:isbn - specific book + author + publisher  
### /publishers - all publishers  
### /publishers/:id - specific publisher + books  
### /shops - all shops  
### /shops/:id - specific shop + publishers + books  
### /countries - all countries  
### /countries/:name - specific country + publishers  

  

```json
/authors
[
    {
        firstName: "",
        lastName: ""
    }, ...
]
```
```json
/authors/:id
{
    firstName: "",
    lastName: "",
    books: [
        {
            name: "",
            ISBN: 45498,
            year: 2018,
            publisher: ""
        },...
    ]
}
```
```json
/books
[
    {
        name: "",
        ISBN: 45498,
        year: 2018,
        publisher: "",
        author: ""
    },...
]
```
```json
/books/:isbn
{
    name: "",
    ISBN: 45498,
    year: 2018,
    publisher: "",
    author: ""
}
```
```json
/publishers
[
    {
        name: "",
        country: ""
    },...
]
```
```json
/publishers/:id
{
    name: "",
    country: ""
}
```
```json
/shops
[
    {
        name: "",
        city: "",
        publishers: [
            {
                name: "",
                country: ""
            },...
        ]
    },...
]
```
```json
/shops/:id
{
    name: "",
    city: "",
    publishers: [
        {
            name: "",
            country: ""
        },...
    ]
}
```
```json
/countries
[
    {
        name: "",
        publishers: [
            {
                name: ""
            },...
        ]
    }
]
```
```json
/countries/:id
{
    name: "",
    publishers: [
        {
            name: ""
        },...
    ]
}
```

## /authors - get all authors  
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request  

## /authors/:id - specific author + his books  
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request, 404 - not found  
HTTP method: POST  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: PUT  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: DELETE  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  

## /books - get all books
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request  

## /book/:isbn - specific book + author + publisher
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request, 404 - not found  
HTTP method: POST  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: PUT  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: DELETE  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found 

## /publishers - get all publishers  
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request  

## /publishers/:id - specific publisher + books  
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request, 404 - not found  
HTTP method: POST  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: PUT  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: DELETE  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found 

## /shops - get all shops  
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request  

## /shops/:id - specific shop + publishers + books  
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request, 404 - not found  
HTTP method: POST  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: PUT  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: DELETE  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found 

## /countries - get all countries  
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request  

## /countries/:name - specific country + publishers  
HTTP method: GET  
Status codes: 200 - ok, 400 - bad request, 404 - not found  
HTTP method: POST  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: PUT  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found  
HTTP method: DELETE  
Status codes: 200 - ok, 400 - bad request, 401 - Unauthorized, 404 - not found 
