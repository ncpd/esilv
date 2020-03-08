package db;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Set;

public class DB {
    private static DB instance = null;
    private Hashtable<Integer, Trip> ht = new Hashtable() {{
    	put(1, new Trip(1, "New York", 30, false));
    	put(2, new Trip(2, "Hong Kong", 30, false));
    	put(3, new Trip(3, "Stockholm", 30, false));
    }};

    public static DB getInstance() {
        if (instance == null)
            instance = new DB();        
        return instance;
    }
    public void addObject(Trip t){
        ht.put(t.getId(), t);
    }
    public Trip getObject(int id) {     
        return ht.get(id);
    }
    
    public ArrayList<Trip> getAllObjects() {
    	ArrayList<Trip> list = new ArrayList<>();
    	Set<Integer> keys = ht.keySet();
    	for (Integer key : keys) {
			list.add(ht.get(key));
		}
    	return list;
    }
}