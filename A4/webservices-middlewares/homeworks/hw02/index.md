# Homework 2
## HTTP Application Protocol - Telnet, cURL  

### Telnet

```bash
telnet 1-dot-mi-mdw-1071.appspot.com 80
```

#### GET

Request
```
GET /httpTelnet1 HTTP/1.1
Host:1-dot-mi-mdw-1071.appspot.com
User-Agent:fit-telnet
Accept:text/html
Accept-Charset:UTF-8
Accept-Language:en-US
```

Answer
```
HTTP/1.1 200 OK
Content-Type: text/html; charset=utf-8
X-Cloud-Trace-Context: beed8431ce8b5f941a59662461a4710c;o=1
Date: Sat, 13 Oct 2018 16:00:45 GMT
Server: Google Frontend
Content-Length: 3

OK
```

#### POST

Request
```
POST /httpTelnet2 HTTP/1.1
Host:1-dot-mi-mdw-1071.appspot.com
Referer:mi-mdw
Content-Type:application/x-www-form-urlencoded
Content-Length:8

data=fit
```

Answer
```
HTTP/1.1 200 OK
Content-Type: text/html; charset=utf-8
X-Cloud-Trace-Context: 4ecb5df8655ea97599b3da9b6e0c2f98;o=1
Date: Sat, 13 Oct 2018 16:12:37 GMT
Server: Google Frontend
Content-Length: 3

OK
```

### cURL

#### Welcome
```bash
curl http://1-dot-mi-mdw-1071.appspot.com:80/protocol/welcome
```

```bash
OK
Your next page is /protocol/get
send GET parameter "name" with value "version"
set Header "Accept" to "text/plain"
```

#### GET
```bash
curl -H "Accept: text/plain" http://1-dot-mi-mdw-1071.appspot.com:80/protocol/get?name=version
```

```bash
OK
Your next page is /protocol/post
send POST parameter "name" with value "allowing"
and set Header "Accept" is "text/plain"
and set Header "Accept-Language" is "en-US"
```

#### POST
```bash
curl -H "Accept: text/plain" -H "Accept-Language: en-US" --data="name=allowing" http://1-dot-mi-mdw-1071.appspot.com/protocol/post
```

```bash
OK
Your next page is /protocol/referer
change referer to value "pick"
set Header "Accept" is "text/html"
```

#### Referer
```bash
curl -H "Accept: text/html" --referer pick http://1-dot-mi-mdw-1071.appspot.com/protocol/referer
```

```bash
OK
Your next page is /protocol/useragent
and change User-Agent to value "lippard"
and set Header "Accept-Language" is "en-US"
```

#### User-Agent
```bash
curl --user-agent "lippard" -H "Accept-Language: en-US" http://1-dot-mi-mdw-1071.appspot.com/protocol/useragent
```

```bash
OK
Your next page is /protocol/cookie
send cookie called "name" with value "data"
```

#### Cookie
```bash
curl --cookie "name=data" http://1-dot-mi-mdw-1071.appspot.com/protocol/cookie
```

```bash
OK
Your next page is /protocol/auth
authenticate by "student:revealing"
set Header "Accept" is "text/html"
```

#### Auth
```bash
curl --user student:revealing -H "Accept: text/html" http://1-dot-mi-mdw-1071.appspot.com/protocol/auth
```

```bash
OK
Your final result is: focus
```
