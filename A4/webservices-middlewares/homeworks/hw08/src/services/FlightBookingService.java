package services;

import java.util.ArrayList;

import javax.jws.WebParam;
import javax.jws.WebService;

import db.*;

@WebService
public class FlightBookingService {
	public ArrayList<FlightBooking> listBookings() {
		 FlightBookingDB db = FlightBookingDB.getInstance();
		 return db.getAllObjects();
	 }
	 
	 public FlightBooking bookFlight(@WebParam(name="first_name") String fName, @WebParam(name="last_name") String lName, @WebParam(name="destination_airport_id") String arrivalAirport) {
		 if(fName != null && lName != null && arrivalAirport != null) {
			 FlightBookingDB db = FlightBookingDB.getInstance();
			 FlightBooking tb = null;
			 for (Flight flight : db.getAllFlights()) {
					if(flight.getArrivalAirport().equalsIgnoreCase(arrivalAirport) && flight.isAvailability()) {
						tb = new FlightBooking(db.getAllObjects().size() + 1, flight, new Person(fName, lName));
						flight.setCapacity(flight.getCapacity() - 1);
					}
				 }
			 if(tb != null) {
				 db.addObject(tb);
			 }
			 return tb;
		 } else {
			 return null;
		 }
	 }
}
