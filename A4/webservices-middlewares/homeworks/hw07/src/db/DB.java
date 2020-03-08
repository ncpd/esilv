package db;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Set;

public class DB {
    private static DB instance = null;
    private Hashtable<Integer, Booking> ht = new Hashtable<Integer, Booking>() {{
    	put(1, new Booking(1, "Jean Bon", "12-10-2018", "18-10-2018", "PRG", "CDG"));
    	put(2, new Booking(2, "Rami Malek", "23-12-2018", "28-12-2018", "NYC", "FAR"));
    }};

    public static DB getInstance() {
        if (instance == null)
            instance = new DB();        
        return instance;
    }
    public void addObject(Booking t){
        ht.put(t.getId(), t);
    }
    public Booking getObject(int id) {     
        return ht.get(id);
    }
    
    public void removeObject(int id) {
    	ht.remove(id);
    }
    
    public ArrayList<Booking> getAllObjects() {
    	ArrayList<Booking> list = new ArrayList<>();
    	Set<Integer> keys = ht.keySet();
    	for (Integer key : keys) {
			list.add(ht.get(key));
		}
    	return list;
    }
}