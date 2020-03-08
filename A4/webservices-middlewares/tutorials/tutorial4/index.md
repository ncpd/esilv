# Tutorial 4

## SoapUI

### List all trips
Request
```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ser="http://services/">
   <soapenv:Header/>
   <soapenv:Body>
      <ser:listTrips/>
   </soapenv:Body>
</soapenv:Envelope>
```

Response
```xml
<S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
   <S:Body>
      <ns0:listTripsResponse xmlns:ns0="http://services/">
         <return>
            <capacity>30</capacity>
            <id>3</id>
            <occupied>false</occupied>
            <title>Stockholm</title>
         </return>
         <return>
            <capacity>30</capacity>
            <id>2</id>
            <occupied>false</occupied>
            <title>Hong Kong</title>
         </return>
         <return>
            <capacity>30</capacity>
            <id>1</id>
            <occupied>false</occupied>
            <title>New York</title>
         </return>
      </ns0:listTripsResponse>
   </S:Body>
</S:Envelope>
```

### Add a trip
Request
```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ser="http://services/">
   <soapenv:Header/>
   <soapenv:Body>
      <ser:addTrip>
         <!--Optional:-->
         <arg0>San Francisco</arg0>
         <arg1>17</arg1>
      </ser:addTrip>
   </soapenv:Body>
</soapenv:Envelope>
```

Response
```xml
<S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
   <S:Body>
      <ns0:addTripResponse xmlns:ns0="http://services/">
         <return>
            <capacity>17</capacity>
            <id>4</id>
            <occupied>false</occupied>
            <title>San Francisco</title>
         </return>
      </ns0:addTripResponse>
   </S:Body>
</S:Envelope>
```

### Book a trip
Request
```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ser="http://services/">
   <soapenv:Header/>
   <soapenv:Body><ser:bookTrip>
         <!--Optional:-->
         <arg0>Hong Kong</arg0>
         <!--Optional:-->
         <arg1>Jean Bon</arg1>
      </ser:bookTrip>
   </soapenv:Body>
</soapenv:Envelope>
```

Response
```xml
<S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
   <S:Body>
      <ns0:bookTripResponse xmlns:ns0="http://services/">
         <return>
            <person>Jean Bon</person>
            <trip>
               <capacity>30</capacity>
               <id>2</id>
               <occupied>false</occupied>
               <title>Hong Kong</title>
            </trip>
         </return>
      </ns0:bookTripResponse>
   </S:Body>
</S:Envelope>
```

## Test Console

### List all trips
Request
```xml
<?xml version="1.0" encoding="UTF-8"?><soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Header/>
    <soap:Body>
        <ns1:listTrips xmlns:ns1="http://services/"/>
    </soap:Body>
</soap:Envelope>
```

Response
```xml
<?xml version="1.0" encoding="UTF-8"?><S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
    <S:Body>
        <ns0:listTripsResponse xmlns:ns0="http://services/">
            <return>
                <capacity>17</capacity>
                <id>4</id>
                <occupied>false</occupied>
                <title>San Francisco</title>
            </return>
            <return>
                <capacity>30</capacity>
                <id>3</id>
                <occupied>false</occupied>
                <title>Stockholm</title>
            </return>
            <return>
                <capacity>30</capacity>
                <id>2</id>
                <occupied>false</occupied>
                <title>Hong Kong</title>
            </return>
            <return>
                <capacity>30</capacity>
                <id>1</id>
                <occupied>false</occupied>
                <title>New York</title>
            </return>
        </ns0:listTripsResponse>
    </S:Body>
</S:Envelope>
```

### Add a trip

Request
```xml
<?xml version="1.0" encoding="UTF-8"?><soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Header/>
    <soap:Body>
        <ns1:addTrip xmlns:ns1="http://services/">
            <arg0>Moscou</arg0>
            <arg1>42</arg1>
        </ns1:addTrip>
    </soap:Body>
</soap:Envelope>
```

Response
```xml
<?xml version="1.0" encoding="UTF-8"?><S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
    <S:Body>
        <ns0:addTripResponse xmlns:ns0="http://services/">
            <return>
                <capacity>42</capacity>
                <id>5</id>
                <occupied>false</occupied>
                <title>Moscou</title>
            </return>
        </ns0:addTripResponse>
    </S:Body>
</S:Envelope>
```

### Book a trip

Request
```xml
<?xml version="1.0" encoding="UTF-8"?><soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Header/>
    <soap:Body>
        <ns1:bookTrip xmlns:ns1="http://services/">
            <arg0>Moscou</arg0>
            <arg1>Alexis Laniel</arg1>
        </ns1:bookTrip>
    </soap:Body>
</soap:Envelope>
```

Response
```xml
<?xml version="1.0" encoding="UTF-8"?><S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
    <S:Body>
        <ns0:bookTripResponse xmlns:ns0="http://services/">
            <return>
                <person>Alexis Laniel</person>
                <trip>
                    <capacity>42</capacity>
                    <id>5</id>
                    <occupied>false</occupied>
                    <title>Moscou</title>
                </trip>
            </return>
        </ns0:bookTripResponse>
    </S:Body>
</S:Envelope>
```

## cURL

### List all trips

Request

```bash
curl -H "Content-Type: text/xml; charset=utf-8" -d@soap-request.xml http://localhost:7001/tutorial4/TripServiceService > listTripsoutput.xml
```

soap-request.xml
```xml
<?xml version="1.0" encoding="utf-8"?>
<env:Envelope xmlns:env="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ser="http://services/">
  <env:Header />
    <env:Body>
    	<ser:listTrips/>
  </env:Body>
</env:Envelope>
```

listTripsoutput.xml
```xml
<?xml version="1.0" encoding="UTF-8"?>
<S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
    <S:Body>
        <ns0:listTripsResponse xmlns:ns0="http://services/">
            <return>
                <capacity>42</capacity>
                <id>5</id>
                <occupied>false</occupied>
                <title>Moscou</title>
            </return>
            <return>
                <capacity>17</capacity>
                <id>4</id>
                <occupied>false</occupied>
                <title>San Francisco</title>
            </return>
            <return>
                <capacity>30</capacity>
                <id>3</id>
                <occupied>false</occupied>
                <title>Stockholm</title>
            </return>
            <return>
                <capacity>30</capacity>
                <id>2</id>
                <occupied>false</occupied>
                <title>Hong Kong</title>
            </return>
            <return>
                <capacity>30</capacity>
                <id>1</id>
                <occupied>false</occupied>
                <title>New York</title>
            </return>
        </ns0:listTripsResponse>
    </S:Body>
</S:Envelope>
```

### Add a trip

Request
```bash
curl -H "Content-Type: text/xml; charset=utf-8" -d@soap-request.xml http://localhost:7001/tutorial4/TripServiceService > addTripoutput.xml
```
soap-request.xml
```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ser="http://services/">
   <soapenv:Header/>
   <soapenv:Body>
      <ser:addTrip>
         <arg0>Vienne</arg0>
         <arg1>42</arg1>
      </ser:addTrip>
   </soapenv:Body>
</soapenv:Envelope>
```

addTripoutput.xml
```xml
<?xml version="1.0" encoding="UTF-8"?>
<S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
    <S:Body>
        <ns0:addTripResponse xmlns:ns0="http://services/">
            <return>
                <capacity>42</capacity>
                <id>6</id>
                <occupied>false</occupied>
                <title>Vienne</title>
            </return>
        </ns0:addTripResponse>
    </S:Body>
</S:Envelope>
```

### Book a trip

```bash
curl -H "Content-Type: text/xml; charset=utf-8" -d@soap-request.xml http://localhost:7001/tutorial4/TripServiceService > bookTripoutput.xml
```

soap-request.xml
```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ser="http://services/">
   <soapenv:Header/>
   <soapenv:Body><ser:bookTrip>
         <arg0>Paris</arg0>
         <arg1>Danny Flamby</arg1>
      </ser:bookTrip>
   </soapenv:Body>
</soapenv:Envelope>
```

bookTripoutput.xml
```xml
<?xml version="1.0" encoding="UTF-8"?>
<S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
    <S:Body>
        <ns0:bookTripResponse xmlns:ns0="http://services/">
            <return>
                <person>Danny Flamby</person>
                <trip>
                    <capacity>30</capacity>
                    <id>2</id>
                    <occupied>false</occupied>
                    <title>Hong Kong</title>
                </trip>
            </return>
        </ns0:bookTripResponse>
    </S:Body>
</S:Envelope>
```