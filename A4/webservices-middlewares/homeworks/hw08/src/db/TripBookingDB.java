package db;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Set;

public class TripBookingDB {
	private static TripBookingDB instance = null;
	private Hashtable<Integer, Trip> trips = new Hashtable<Integer, Trip>() {{
    	put(1, new Trip(1, "Moscow", 22, true));
    	put(2, new Trip(2, "Prague", 35, true));
    	put(3, new Trip(3, "San Francisco", 15, true));
    }};
    private Hashtable<Integer, TripBooking> ht = new Hashtable<Integer, TripBooking>() {{
    	put(1, new TripBooking(1, trips.get(1), new Person("Jean", "Bon")));
    	put(2, new TripBooking(2, trips.get(2), new Person("Rami", "Pear")));
    	put(3, new TripBooking(3, trips.get(3), new Person("Gael", "Kight")));
    }};

    public static TripBookingDB getInstance() {
        if (instance == null)
            instance = new TripBookingDB();        
        return instance;
    }
    
    public ArrayList<Trip> getAllTrips() {
    	ArrayList<Trip> list = new ArrayList<>();
    	Set<Integer> keys = trips.keySet();
    	for (Integer key : keys) {
			list.add(trips.get(key));
		}
    	return list;
    }
    
    public Trip getTrip(int id) {
    	return trips.get(id);
    }
    
    public void addObject(TripBooking t){
        ht.put(t.getId(), t);
    }
    public TripBooking getObject(int id) {     
        return ht.get(id);
    }
    
    public void removeObject(int id) {
    	ht.remove(id);
    }
    
    public ArrayList<TripBooking> getAllObjects() {
    	ArrayList<TripBooking> list = new ArrayList<>();
    	Set<Integer> keys = ht.keySet();
    	for (Integer key : keys) {
			list.add(ht.get(key));
		}
    	return list;
    }
}
