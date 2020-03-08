package services;

import java.util.ArrayList;

import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;
import javax.xml.bind.annotation.XmlElement;

import db.*;

@WebService
public class TripService {
	
	 @WebResult(name="booking")
	 public ArrayList<Booking> listBookings() {
		 DB db = DB.getInstance();
		 return db.getAllObjects();
	 }
	
	 @WebResult(name="booking")
	 public Booking addBooking(@XmlElement(required=true) @WebParam(name="passenger") String passenger, @XmlElement(required=true) @WebParam(name="arrivalDate") String arrivalDate,
			 @XmlElement(required=true) @WebParam(name="departureDate") String departureDate, @XmlElement(required=true) @WebParam(name="arrivalAirportID") String arrivalAirportID,
			 @XmlElement(required=true) @WebParam(name="departureAirportID") String departureAirportID) {
		 if(!passenger.isEmpty() && !arrivalDate.isEmpty() && !departureDate.isEmpty() && !arrivalAirportID.isEmpty() && !departureAirportID.isEmpty()) {
			 DB db = DB.getInstance();
			 Booking t = new Booking(db.getAllObjects().size() + 1, passenger, arrivalDate, departureDate, arrivalAirportID, departureAirportID);
			 db.addObject(t);
			 return t;
		 } else {
			 return null;
		 }
	 }
	 
	 @WebResult(name="message")
	 public String removeBooking(@XmlElement(required=true) @WebParam(name="bookingID") int bookingID) {
		 DB db = DB.getInstance();
		 if(db.getObject(bookingID) != null) {
			 db.removeObject(bookingID);
			 return "Booking removed";
		 } else {
			 return "Booking not found";
		 }
	 }
	 
	 @WebResult(name="booking")
	 public Booking updateBooking(@XmlElement(required=true) @WebParam(name="bookingID")int id, @XmlElement(required=true) @WebParam(name="passenger") String passenger,
			 @XmlElement(required=true) @WebParam(name="arrivalDate") String arrivalDate, @XmlElement(required=true) @WebParam(name="departureDate") String departureDate,
			 @XmlElement(required=true) @WebParam(name="arrivalAirportID") String arrivalAirportID, @XmlElement(required=true) @WebParam(name="departureAirportID") String departureAirportID) {
		 if(!passenger.isEmpty() && !arrivalDate.isEmpty() && !departureDate.isEmpty() && !arrivalAirportID.isEmpty() && !departureAirportID.isEmpty()) {
			 DB db = DB.getInstance();
			 Booking b = db.getObject(id);
			 if(b != null) {
				 b.setPerson(passenger);
				 b.setArrivalDate(arrivalDate);
				 b.setDepartureDate(departureDate);
				 b.setArrivalAirportID(arrivalAirportID);
				 b.setDepartureAirportID(departureAirportID);
			 }
			 return b;
		 } else {
			 return null;
		 }
	 }

}
