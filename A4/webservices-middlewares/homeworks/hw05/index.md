# Homework 05 - RESTfull - Asynchronous operation

## HttpServlet Async

First, to make our Servlet async compatible, we need to add this line above the class declaration.
```java
@WebServlet(urlPatterns = { "/*" }, asyncSupported = true)
```

Next, we can create the **doDelete** method to handle the **DELETE** request on our API.  
```java
@Override
	protected void doDelete(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		String[] path = req.getPathInfo().split("/");
		int customerid = Integer.valueOf(path[2]);
		if (path.length > 2 && path[1].equalsIgnoreCase("customer") && customerid > 0) { // /customer/id
			final javax.servlet.AsyncContext acontext = req.startAsync();
			acontext.start(new Runnable() {
				
				@Override
				public void run() {
					// TODO Auto-generated method stub
					HttpServletResponse response = (HttpServletResponse) acontext.getResponse();
					try {
						DB db = DB.getInstance();
						Customer c = db.getObject(customerid);
						if(c != null) {
							response.setStatus(202);
							response.setHeader("Location", "/request/status/" + customerid);
							acontext.complete();
							db.removeObject(customerid);
							Thread.sleep(10000);
							db.removeFromQueue(customerid);
							System.out.println("It's been 10 seconds, customer no." + customerid + " has been deleted.");
						} else {
							response.setStatus(204);
							acontext.complete();
						}
					} catch (Exception e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			});
			
		} else {
			resp.setStatus(400);
		}
	}
```

Above you can see a new Thread is created for an asynchronous operation (This is made with **AsyncContext**).  
Inside, the **run** method is putting the customer in the deletion queue, deleting him from the database, waiting 10 seconds to simulate a human confirmation within the asynchronous thread, and finally deleting him from the queue.  
The queue is needed to check the deletion status.  
If no customer has been found to delete, `204 No Content` is sent, otherwise `202 Accepted` is sent if the operation is successful and being processed.  

Below you can see a typical cURL log example of a customer deletion.

## cUrl Log

First, we can try to check the status of a customer not yet being deleted. If we try the following command:
```bash
curl -v http://localhost:7001/hw05/request/status/2
```
the server will send us the following:
```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw05/request/status/2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> 
< HTTP/1.1 404 Not Found
< Date: Thu, 22 Nov 2018 02:11:22 GMT
< Content-Length: 26
< 
Customer no. 2 not found.
* Connection #0 to host localhost left intact
```
Indeed, we get `404 Not Found` because customer n°2 is not in the delete queue.

We can try to delete the customer n°2 now:
```bash
curl -v -X DELETE http://localhost:7001/hw05/customer/2
```

The server responds the following:
```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> DELETE /hw05/customer/2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> 
< HTTP/1.1 202 Accepted
< Date: Thu, 22 Nov 2018 02:12:13 GMT
< Location: /request/status/2
< Content-Length: 0
< 
* Connection #0 to host localhost left intact
```
The server has accepted the deletion of customer n°2 and it is now being processed.

If we check for the status:
```bash
curl -v http://localhost:7001/hw05/request/status/2
```
We get `200 OK` as the customer has not yet been deleted and is still being processed.
```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw05/request/status/2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> 
< HTTP/1.1 200 OK
< Date: Thu, 22 Nov 2018 02:12:18 GMT
< Content-Length: 58
< 
Customer no. 2 is being processed. Please wait and retry.
* Connection #0 to host localhost left intact
```

Ten seconds after deletion, if we still check the status we get `404 Not Found` because customer n°2 doesn't exists anymore, neither in the database nor the deletion queue.
```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> GET /hw05/request/status/2 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> 
< HTTP/1.1 404 Not Found
< Date: Thu, 22 Nov 2018 02:15:16 GMT
< Content-Length: 26
< 
Customer no. 2 not found.
* Connection #0 to host localhost left intact
```

Finally, it isn't possible to delete a non-existing customer of course. If we execute:
```bash
curl -v -X DELETE http://localhost:7001/hw05/customer/5
```

We get a `204 No Content` as no customer with the id 5 is existing so there is no deletable content.
```bash
*   Trying 127.0.0.1...
* Connected to localhost (127.0.0.1) port 7001 (#0)
> DELETE /hw05/customer/5 HTTP/1.1
> Host: localhost:7001
> User-Agent: curl/7.47.0
> Accept: */*
> 
< HTTP/1.1 204 No Content
< Date: Thu, 22 Nov 2018 02:18:39 GMT
< 
* Connection #0 to host localhost left intact
```
