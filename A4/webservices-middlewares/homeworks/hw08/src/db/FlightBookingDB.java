package db;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Set;

public class FlightBookingDB {
	private static FlightBookingDB instance = null;
	private Hashtable<Integer, Flight> flights = new Hashtable<Integer, Flight>() {{
    	put(1, new Flight(1, "PRG", "CDG", "18-10-2018", "12-10-2018", 30, true));
    	put(2, new Flight(2, "FAR", "LAX", "26-09-2018", "14-09-2018", 54, true));
    }};
    private Hashtable<Integer, FlightBooking> ht = new Hashtable<Integer, FlightBooking>() {{
    	put(1, new FlightBooking(1, flights.get(1), new Person("Rami", "Malek")));
    	put(2, new FlightBooking(2, flights.get(2), new Person("Charlie", "Hunnam")));
    }};

    public static FlightBookingDB getInstance() {
        if (instance == null)
            instance = new FlightBookingDB();        
        return instance;
    }
    
    public ArrayList<Flight> getAllFlights() {
    	ArrayList<Flight> list = new ArrayList<>();
    	Set<Integer> keys = flights.keySet();
    	for (Integer key : keys) {
			list.add(flights.get(key));
		}
    	return list;
    }
    
    public Flight getFlight(int id) {
    	return flights.get(id);
    }
    
    public void addObject(FlightBooking t){
        ht.put(t.getId(), t);
    }
    public FlightBooking getObject(int id) {     
        return ht.get(id);
    }
    
    public void removeObject(int id) {
    	ht.remove(id);
    }
    
    public ArrayList<FlightBooking> getAllObjects() {
    	ArrayList<FlightBooking> list = new ArrayList<>();
    	Set<Integer> keys = ht.keySet();
    	for (Integer key : keys) {
			list.add(ht.get(key));
		}
    	return list;
    }
}
