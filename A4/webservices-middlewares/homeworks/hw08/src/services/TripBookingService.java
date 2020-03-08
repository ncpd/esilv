package services;

import java.util.ArrayList;
import db.*;

import javax.jws.WebParam;
import javax.jws.WebService;

@WebService
public class TripBookingService {
	public ArrayList<TripBooking> listBookings() {
		 TripBookingDB db = TripBookingDB.getInstance();
		 return db.getAllObjects();
	 }
	 
	 public TripBooking bookTrip(@WebParam(name="first_name") String fName, @WebParam(name="last_name") String lName, @WebParam(name="destination") String tripTitle) {
		 if(fName != null && lName != null && tripTitle != null) {
			 TripBookingDB db = TripBookingDB.getInstance();
			 TripBooking tb = null;
			 for (Trip trip : db.getAllTrips()) {
					if(trip.getDestination().equalsIgnoreCase(tripTitle) && trip.isAvailability()) {
						tb = new TripBooking(db.getAllObjects().size() + 1, trip, new Person(fName, lName));
						trip.setCapacity(trip.getCapacity() - 1);
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
