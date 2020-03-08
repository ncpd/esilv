package services;

import java.util.ArrayList;
import javax.jws.WebService;
import db.*;

@WebService
public class TripService {
	
	 public ArrayList<Trip> listTrips() {
		 DB db = DB.getInstance();
		 return db.getAllObjects();
	 }
	 
	 public Trip addTrip(String title, int capacity) {
		 if(title != null && capacity >= 0) {
			 DB db = DB.getInstance();
			 Trip t = new Trip(db.getAllObjects().size() + 1, title, capacity, false);
			 db.addObject(t);
			 return t;
		 } else {
			 return null;
		 }
	 }
	 
	 public Booking bookTrip(String tripTitle, String name) {
		 DB db = DB.getInstance();
		 Booking b = null;
		 for (Trip trip : db.getAllObjects()) {
			if(trip.getTitle().equalsIgnoreCase(tripTitle) && name != null) {
				b = new Booking(trip, name);
				return b;
			}
		 }
		 return null;
	 }

}
