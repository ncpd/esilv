# Homework 08 - Web Service Composition

## Trip Booking Service & Flight Booking Service

This services are perfectly working as they have been tested with SoapUI and on the weblogic console. You can check the booking methods below:  

### TripBookingService

Request
```xml
<?xml version="1.0" encoding="UTF-8"?><soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Header/>
    <soap:Body>
        <ns1:bookTrip xmlns:ns1="http://services/">
            <first_name>Jean</first_name>
            <last_name>Bon</last_name>
            <destination>Moscow</destination>
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
                <id>4</id>
                <passenger>
                    <fName>Jean</fName>
                    <lName>Bon</lName>
                </passenger>
                <trip>
                    <availability>true</availability>
                    <capacity>21</capacity>
                    <destination>Moscow</destination>
                    <id>1</id>
                </trip>
            </return>
        </ns0:bookTripResponse>
    </S:Body>
</S:Envelope>
```

### FlightBookingService

Request
```xml
<?xml version="1.0" encoding="UTF-8"?><soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Header/>
    <soap:Body>
        <ns1:bookFlight xmlns:ns1="http://services/">
            <first_name>Karl</first_name>
            <last_name>Olive</last_name>
            <destination_airport_id>PRG</destination_airport_id>
        </ns1:bookFlight>
    </soap:Body>
</soap:Envelope>
```

Response
```xml
<?xml version="1.0" encoding="UTF-8"?><S:Envelope xmlns:S="http://schemas.xmlsoap.org/soap/envelope/">
    <S:Body>
        <ns0:bookFlightResponse xmlns:ns0="http://services/">
            <return>
                <flight>
                    <arrivalAirport>PRG</arrivalAirport>
                    <arrivalDate>18-10-2018</arrivalDate>
                    <availability>true</availability>
                    <capacity>29</capacity>
                    <departureAirport>CDG</departureAirport>
                    <departureDate>12-10-2018</departureDate>
                    <id>1</id>
                </flight>
                <id>3</id>
                <passenger>
                    <fName>Karl</fName>
                    <lName>Olive</lName>
                </passenger>
            </return>
        </ns0:bookFlightResponse>
    </S:Body>
</S:Envelope>
```

## Booking Service

Unfortunately, I couldn't find a way to make this web service call other ones with their respective WSDLs. I have tested by creating a new project for this web service, with the `@WebServiceRef` annotation
thinking that it would work but the server has to republish and thus, gets a 503 on the WSDL urls. I think I understand the logic behind web service composition but I did not find a way to implement it, sorry about that.  
This Web Service is the following:  
```java
@WebService
public class BookingService {
	
	// Can't make it work
	//@WebServiceRef(wsdlLocation = "http://127.0.1.1:7001/hw08/TripBookingServiceService?WSDL")
	TripBookingService tripBookingService;
	//@WebServiceRef(wsdlLocation = "http://127.0.1.1:7001/hw08/FlightBookingServiceService?WSDL")
	FlightBookingService flightBookingService;

	public Object makeBooking(@WebParam(name="type") String type, @WebParam(name="firstname") String firstName, @WebParam(name="lastname") String lastName, @WebParam(name="destination") String destination) {
		if(!type.isEmpty() && !firstName.isEmpty() && !lastName.isEmpty() && !destination.isEmpty()) {
			switch(type) {
			case "trip":
				tripBookingService = new TripBookingService();
				TripBooking tb = tripBookingService.bookTrip(firstName, lastName, destination);
				if(tb != null) {
					return tb;
				} else {
					return "No trip available";
				}
			case "flight":
				flightBookingService = new FlightBookingService();
				FlightBooking fb = flightBookingService.bookFlight(firstName, lastName, destination);
				if(fb != null) {
					return fb;
				} else {
					return "No flight available";
				}
			default:
				return null;
			}
		} else {
			return null;
		}
	}
}

```