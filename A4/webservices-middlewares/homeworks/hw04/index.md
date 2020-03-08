# Homework 04 - RESTful API Design - resources, URIs, operations

## Design

> /owners - all owners  
> /owners/{owner-id} - owner details  
> /owners/{owner-id}/cars - owner's cars  
> /owners/{owner-id}/cars/{car-id} - owner's car details  

### Data Models

#### Owner
```json
{
    "id": 1,
    "name": "",
    "cars": [
        {
            "id": 1,
            "brand": "",
            "model": "",
            "plate": {
                "country": "",
                "id": ""
            }
        },...
    ],
    "place": {
        "id": 1,
        "lat": 50.104048,
        "long": 14.3889023,
        "street" : "",
        "streetNumber": 1,
        "city": "",
        "zipcode": "",
        "country": ""
    }
}
```

#### Car
```json
{
    "id": 1,
    "brand": "",
    "model": "",
    "plate": {
        "country": "",
        "id": ""
    }
}
```

#### Place

```json
{
    "id": 1,
    "lat": 50.104048,
    "long": 14.3889023,
    "street" : "",
    "streetNumber": 1,
    "city": "",
    "zipcode": "",
    "country": ""
}
```


## Operations

**URI** | **Method** | **Description** | **Status Codes**
--- | --- | --- | --- | 
/owners | `GET` | Get list of owners | `200 OK` `400 BAD REQUEST`
/owners | `POST` | Create a new owner | `201 CREATED` `400 BAD REQUEST` `401 UNAUTHORIZED`
/owners/{owner-id} | `GET` | Get owner details | `200 OK` `400 BAD REQUEST` `404 NOT FOUND`
/owners/{owner-id} | `PUT` | Modify owner details | `200 OK` `204 NO CONTENT` `400 BAD REQUEST` `401 UNAUTHORIZED`
/owners/{owner-id} | `DELETE` | Delete an owner | `200 OK` `204 NO CONTENT` `400 BAD REQUEST` `401 UNAUTHORIZED`
/owners/{owner-id}/cars | `GET` | Get owner's cars | `200 OK` `400 BAD REQUEST`
/owners/{owner-id}/cars | `POST` | Create a new car | `201 CREATED` `400 BAD REQUEST` `401 UNAUTHORIZED`
/owners/{owner-id}/cars/{car-id} | `GET` | Get an owner's car details | `200 OK` `400 BAD REQUEST` `404 NOT FOUND`
/owners/{owner-id}/cars/{car-id} | `PUT` | Modify an owner's car details | `200 OK` `204 NO CONTENT` `400 BAD REQUEST` `401 UNAUTHORIZED`
/owners/{owner-id}/cars/{car-id} | `DELETE` | Delete an owner's car | `200 OK` `204 NO CONTENT` `400 BAD REQUEST` `401 UNAUTHORIZED`

