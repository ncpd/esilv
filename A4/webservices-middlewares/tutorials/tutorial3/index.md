## GET /order
```bash
curl -v http://127.0.0.1:7001/tutorial3/order/
```

## POST /order
### JSON version
```bash
curl -v -d "{\"customer\":\"customer-1\",\"item\":\"item-1\"}" -H "Content-Type:application/json" http://127.0.0.1:7001/tutorial3/order?api_key=3e670a86-8480-43f6-bcf4-045c4c26e9b0  
```
### XML version
```bash
curl -v -d "<order><customer>customer-xml</customer><item>item-xml</item></order>" -H "Content-Type:application/xml" http://127.0.0.1:7001/tutorial3/order?api_key=3e670a86-8480-43f6-bcf4-045c4c26e9b0  
```

## GET /order/{order-id}
### JSON version
```bash
curl -v -H "Accept:application/json" http://127.0.0.1:7001/tutorial3/order/2  
```
### XML version
```bash
curl -v -H "Accept:application/xml" http://127.0.0.1:7001/tutorial3/order/2  
```

## DELETE /order/{order-id}
```bash
curl -v -X DELETE http://127.0.0.1:7001/tutorial3/order/4?api_key=3e670a86-8480-43f6-bcf4-045c4c26e9b0
```