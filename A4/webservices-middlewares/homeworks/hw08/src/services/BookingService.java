package services;

import javax.jws.WebParam;
import javax.jws.WebService;
import javax.xml.ws.WebServiceRef;


import db.*;

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
