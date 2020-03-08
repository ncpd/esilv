# Homework 06 - RESTfull - Conditional GET

In order to easily understand which status codes need to be returned depending on sent headers, you can look at the following table.  
Some scenarios wouldn't be possible with a browser but with cURL you can forge commands and thus, set an up-to-date ETag but an old If-Modified-Since date.  
I decided to handle this by returning `304 Not Modified` if one of the headers was up-to-date.  

  
**If-None-Match** | **Last-Modified** | **Status Code**
--- | --- | --- | 
`Not provided` | `Not provided` | `200 OK`
`Up-to-date` | `Up-to-date` | `304 Not Modified`
`Up-to-date` | `Old` | `304 Not Modified`
`Old` | `Up-to-date` | `304 Not Modified`
`Old` | `Old` | `200 OK`
`Old` | `Not provided` | `200 OK`
`Up-to-date` | `Not provided` | `304 Not Modified`
`Not provided` | `Old` | `200 OK`
`Not provided` | `Up-to-date` | `304 Not Modified`

## V1 - Strong ETag

The Strong ETag implemented version can be found at ***/customers/v1***.
The function to compute the strong ETag is the following :  
```java
private String computeStrongETag(ArrayList<Customer> customers) {
		String content = "";
		for (Customer customer : customers) {
			String ordersContent = "";
			for (Order order : customer.getOrders()) {
				ordersContent += order.getId() + order.getItem();
			}
			content += customer.getId() + customer.getName() + ordersContent;
		}
		
		MessageDigest md;
		String weakETag;
		try {
			md = MessageDigest.getInstance("MD5");
			weakETag = (new HexBinaryAdapter()).marshal(md.digest(content.getBytes()));
		} catch (NoSuchAlgorithmException e) {
			weakETag = "";
			e.printStackTrace();
		}
		
		return "\"" + weakETag + "\"";
	}
```

### cURL log

Next you can see a full cURL log to test all the possibilities with **If-None-Match** and **If-Modified-Since** headers.  
**If-None-Match** refers to the ETag and **If-Modified-Since** to the **Last-Modified** date.

### No ETag neither Last-Modified headers provided
```bash
curl -v http://localhost:7001/hw06/customers/v1
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v1 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> 
< HTTP/1.1 200 OK
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:10:46 GMT
< Content-Length: 97
< Content-Type: application/json
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: "EDEEA8260C11340EA51682BD3172152E"
< 
[{"id":2,"name":"bbb","orders":[]},{"id":1,"name":"aaa","orders":[{"item":"item test","id":1}]}]
* Connection #0 to host localhost left intact
```

### Up-to-date ETag & Up-to-date Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v1 -H "If-None-Match:\"EDEEA8260C11340EA51682BD3172152E\"" -H "If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v1 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:"EDEEA8260C11340EA51682BD3172152E"
> If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:12:33 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: "EDEEA8260C11340EA51682BD3172152E"
< 
* Connection #0 to host localhost left intact
```

### Up-to-date ETag & Old Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v1 -H "If-None-Match:\"EDEEA8260C11340EA51682BD3172152E\"" -H "If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v1 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:"EDEEA8260C11340EA51682BD3172152E"
> If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:13:57 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: "EDEEA8260C11340EA51682BD3172152E"
< 
* Connection #0 to host localhost left intact
```

### Old ETag & Up-to-date Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v1 -H "If-None-Match:\"EDEEA8260C11340EA51682BD3172152F\"" -H "If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v1 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:"EDEEA8260C11340EA51682BD3172152F"
> If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:14:46 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: "EDEEA8260C11340EA51682BD3172152E"
< 
* Connection #0 to host localhost left intact
```

### Old ETag & Old Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v1 -H "If-None-Match:\"EDEEA8260C11340EA51682BD3172152F\"" -H "If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v1 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:"EDEEA8260C11340EA51682BD3172152F"
> If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT
> 
< HTTP/1.1 200 OK
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:15:50 GMT
< Content-Length: 97
< Content-Type: application/json
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: "EDEEA8260C11340EA51682BD3172152E"
< 
[{"id":2,"name":"bbb","orders":[]},{"id":1,"name":"aaa","orders":[{"item":"item test","id":1}]}]
* Connection #0 to host localhost left intact
```

### Old ETag & No Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v1 -H "If-None-Match:\"EDEEA8260C11340EA51682BD317A152F\""
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v1 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:"EDEEA8260C11340EA51682BD317A152F"
> 
< HTTP/1.1 200 OK
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:16:21 GMT
< Content-Length: 97
< Content-Type: application/json
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: "EDEEA8260C11340EA51682BD3172152E"
< 
[{"id":2,"name":"bbb","orders":[]},{"id":1,"name":"aaa","orders":[{"item":"item test","id":1}]}]
* Connection #0 to host localhost left intact
```

### Up-to-date ETag & No Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v1 -H "If-None-Match:\"EDEEA8260C11340EA51682BD3172152E\""
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v1 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:"EDEEA8260C11340EA51682BD3172152E"
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:17:03 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: "EDEEA8260C11340EA51682BD3172152E"
< 
* Connection #0 to host localhost left intact
```

### No ETag & Old Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v1 -H "If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v1 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT
> 
< HTTP/1.1 200 OK
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:18:44 GMT
< Content-Length: 97
< Content-Type: application/json
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: "EDEEA8260C11340EA51682BD3172152E"
< 
[{"id":2,"name":"bbb","orders":[]},{"id":1,"name":"aaa","orders":[{"item":"item test","id":1}]}]
* Connection #0 to host localhost left intact
```

### No ETag & Up-to-date Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v1 -H "If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v1 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:19:12 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: "EDEEA8260C11340EA51682BD3172152E"
< 
* Connection #0 to host localhost left intact
```

## V2 - Weak ETag

The Weak ETag implemented version can be found at ***/customers/v2***.
The function to compute the weak ETag is the following : 
```java
private String computeWeakETag(ArrayList<Customer> customers) {
		String content = "";
		for (Customer customer : customers) {
			content += customer.getId() + customer.getName();
		}
		
		MessageDigest md;
		String weakETag;
		try {
			md = MessageDigest.getInstance("MD5");
			weakETag = (new HexBinaryAdapter()).marshal(md.digest(content.getBytes()));
		} catch (NoSuchAlgorithmException e) {
			weakETag = "";
			e.printStackTrace();
		}
		
		return "W/\"" + weakETag + "\"";
	}
```

### cURL log

Next you can see a full cURL log to test all the possibilities with **If-None-Match** and **If-Modified-Since** headers.  
**If-None-Match** refers to the ETag and **If-Modified-Since** to the **Last-Modified** date.

### No ETag neither Last-Modified headers provided
```bash
curl -v http://localhost:7001/hw06/customers/v2
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> 
< HTTP/1.1 200 OK
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:40:39 GMT
< Content-Length: 97
< Content-Type: application/json
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: W/"55A52119A79DFDDD9C50E527114CC2DA"
< 
[{"id":2,"name":"bbb","orders":[]},{"id":1,"name":"aaa","orders":[{"item":"item test","id":1}]}]
* Connection #0 to host localhost left intact
```

### Up-to-date ETag & Up-to-date Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v2 -H "If-None-Match:W/\"55A52119A79DFDDD9C50E527114CC2DA\"" -H "If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:W/"55A52119A79DFDDD9C50E527114CC2DA"
> If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:45:01 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: W/"55A52119A79DFDDD9C50E527114CC2DA"
< 
* Connection #0 to host localhost left intact
```

### Up-to-date ETag & Old Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v2 -H "If-None-Match:W/\"55A52119A79DFDDD9C50E527114CC2DA\"" -H "If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:W/"55A52119A79DFDDD9C50E527114CC2DA"
> If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:46:54 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: W/"55A52119A79DFDDD9C50E527114CC2DA"
< 
* Connection #0 to host localhost left intact
```

### Old ETag & Up-to-date Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v2 -H "If-None-Match:W/\"55A52119A79DFDDD9C50E527114CC2DB\"" -H "If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:W/"55A52119A79DFDDD9C50E527114CC2DB"
> If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:48:23 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: W/"55A52119A79DFDDD9C50E527114CC2DA"
< 
* Connection #0 to host localhost left intact
```

### Old ETag & Old Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v2 -H "If-None-Match:W/\"55A52119A79DFDDD9C50E527114CC2DB\"" -H "If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:W/"55A52119A79DFDDD9C50E527114CC2DB"
> If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT
> 
< HTTP/1.1 200 OK
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:49:20 GMT
< Content-Length: 97
< Content-Type: application/json
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: W/"55A52119A79DFDDD9C50E527114CC2DA"
< 
[{"id":2,"name":"bbb","orders":[]},{"id":1,"name":"aaa","orders":[{"item":"item test","id":1}]}]
* Connection #0 to host localhost left intact
```

### Old ETag & No Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v2 -H "If-None-Match:W/\"55A52119A79DFDDD9C50E527114CC2DB\""
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:W/"55A52119A79DFDDD9C50E527114CC2DB"
> 
< HTTP/1.1 200 OK
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:50:15 GMT
< Content-Length: 97
< Content-Type: application/json
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: W/"55A52119A79DFDDD9C50E527114CC2DA"
< 
[{"id":2,"name":"bbb","orders":[]},{"id":1,"name":"aaa","orders":[{"item":"item test","id":1}]}]
* Connection #0 to host localhost left intact
```

### Up-to-date ETag & No Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v2 -H "If-None-Match:W/\"55A52119A79DFDDD9C50E527114CC2DA\""
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-None-Match:W/"55A52119A79DFDDD9C50E527114CC2DA"
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:51:04 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: W/"55A52119A79DFDDD9C50E527114CC2DA"
< 
* Connection #0 to host localhost left intact
```

### No ETag & Old Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v2 -H "If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-Modified-Since: Wed, 21 Oct 2015 07:28:00 GMT
> 
< HTTP/1.1 200 OK
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:51:47 GMT
< Content-Length: 97
< Content-Type: application/json
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: W/"55A52119A79DFDDD9C50E527114CC2DA"
< 
[{"id":2,"name":"bbb","orders":[]},{"id":1,"name":"aaa","orders":[{"item":"item test","id":1}]}]
* Connection #0 to host localhost left intact
```

### No ETag & Up-to-date Last-Modified
```bash
curl -v http://localhost:7001/hw06/customers/v2 -H "If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT"
```

```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw06/customers/v2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> If-Modified-Since: Wed, 21 Oct 2019 07:28:00 GMT
> 
< HTTP/1.1 304 Not Modified
< Cache-Control: private, no-store, max-age=500
< Date: Thu, 22 Nov 2018 00:52:53 GMT
< Last-Modified: Wed, 21 Nov 2018 23:15:22 GMT
< ETag: W/"55A52119A79DFDDD9C50E527114CC2DA"
< 
* Connection #0 to host localhost left intact
```