# Homework 03 - Booking Protocol & Session Management

## POST /
This command creates a new session with the **NEW** state. The **JSESSIONID** cookie is saved in *cookie.txt*.
```bash
curl -v -d "" http://127.0.0.1:7001/hw03 -c cookie.txt
```

We send the server a POST request with our **JSESSIONID** cookie saved in *cookie.txt*. If the session is already completed or invalidated, a new session is created. If not, a state change is processed.
```bash
curl -v -d "" http://127.0.0.1:7001/hw03 -b cookie.txt
```

## Full trip booking example
Our session is null for the moment as no cookie with **JSESSIONID** was sent to the server. The server will create a session for us.
```bash
curl -v -d "" http://127.0.0.1:7001/hw03 -c cookie.txt
```
Response
```
*   Trying 127.0.0.1...
* Connected to 127.0.0.1 (127.0.0.1) port 7001 (#0)
> POST /hw03 HTTP/1.1
> Host: 127.0.0.1:7001
> User-Agent: curl/7.47.0
> Accept: */*
> Content-Length: 0
> Content-Type: application/x-www-form-urlencoded
> 
< HTTP/1.1 200 OK
< Date: Mon, 29 Oct 2018 19:26:30 GMT
< Content-Length: 0
* Added cookie JSESSIONID="d6bBSrXvkh4IUROiVRj7VGWKKPLYVwaaGxt34e6Xi-vL_Nbp59LO!-1087261712" for domain 127.0.0.1, path /, expire 0
< Set-Cookie: JSESSIONID=d6bBSrXvkh4IUROiVRj7VGWKKPLYVwaaGxt34e6Xi-vL_Nbp59LO!-1087261712; path=/; HttpOnly
< 
* Connection #0 to host 127.0.0.1 left intact
```

Now we're using the cookie attributed by the server for our next POST request.  
```bash
curl -v -d "" http://127.0.0.1:7001/hw03 -b cookie.txt
```
Response
```
*   Trying 127.0.0.1...
* Connected to 127.0.0.1 (127.0.0.1) port 7001 (#0)
> POST /hw03 HTTP/1.1
> Host: 127.0.0.1:7001
> User-Agent: curl/7.47.0
> Accept: */*
> Cookie: JSESSIONID=d6bBSrXvkh4IUROiVRj7VGWKKPLYVwaaGxt34e6Xi-vL_Nbp59LO!-1087261712
> Content-Length: 0
> Content-Type: application/x-www-form-urlencoded
> 
< HTTP/1.1 200 OK
< Date: Mon, 29 Oct 2018 19:28:52 GMT
< Content-Length: 134
< 
d6bBSrXvkh4IUROiVRj7VGWKKPLYVwaaGxt34e6Xi-vL_Nbp59LO!-1087261712!1540841190896 TRIP STATE HAS CHANGED FROM NEW TO WAITING FOR PAYMENT
* Connection #0 to host 127.0.0.1 left intact
```
Trip state has been changed to **WAITING FOR PAYMENT** as written in the reponse.  
We make the same request as before to process to the last state change.
```bash
curl -v -d "" http://127.0.0.1:7001/hw03 -b cookie.txt
```
Response
```
*   Trying 127.0.0.1...
* Connected to 127.0.0.1 (127.0.0.1) port 7001 (#0)
> POST /hw03 HTTP/1.1
> Host: 127.0.0.1:7001
> User-Agent: curl/7.47.0
> Accept: */*
> Cookie: JSESSIONID=d6bBSrXvkh4IUROiVRj7VGWKKPLYVwaaGxt34e6Xi-vL_Nbp59LO!-1087261712
> Content-Length: 0
> Content-Type: application/x-www-form-urlencoded
> 
< HTTP/1.1 200 OK
< Date: Mon, 29 Oct 2018 19:30:37 GMT
< Content-Length: 123
< 
Booking is COMPLETED. d6bBSrXvkh4IUROiVRj7VGWKKPLYVwaaGxt34e6Xi-vL_Nbp59LO!-1087261712!1540841190896 has been invalidated.
* Connection #0 to host 127.0.0.1 left intact
```
Trip state has been changed to **COMPLETED** as written in the reponse.  
Now that the booking is complete, session has been invalidated. If we use the same command as before, our session is not existing anymore so the server will create a new session for us.
```bash
curl -v -d "" http://127.0.0.1:7001/hw03 -b cookie.txt
```
Response
```
*   Trying 127.0.0.1...
* Connected to 127.0.0.1 (127.0.0.1) port 7001 (#0)
> POST /hw03 HTTP/1.1
> Host: 127.0.0.1:7001
> User-Agent: curl/7.47.0
> Accept: */*
> Cookie: JSESSIONID=d6bBSrXvkh4IUROiVRj7VGWKKPLYVwaaGxt34e6Xi-vL_Nbp59LO!-1087261712
> Content-Length: 0
> Content-Type: application/x-www-form-urlencoded
> 
< HTTP/1.1 200 OK
< Date: Mon, 29 Oct 2018 19:32:24 GMT
< Content-Length: 0
* Replaced cookie JSESSIONID="quXBUBuM5xlLyZH-Hdz8OGw1v183FvhPWJEwfqnT5zjUQy1nzOkQ!-1087261712" for domain 127.0.0.1, path /, expire 0
< Set-Cookie: JSESSIONID=quXBUBuM5xlLyZH-Hdz8OGw1v183FvhPWJEwfqnT5zjUQy1nzOkQ!-1087261712; path=/; HttpOnly
< 
* Connection #0 to host 127.0.0.1 left intact
```