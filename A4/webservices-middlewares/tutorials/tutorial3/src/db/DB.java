package db;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Set;
 
public class DB {
    private static DB instance = null;
    private Hashtable<Integer, Order> ht = new Hashtable<Integer, Order>() {{
    	put(1, new Order(1, "Jean", "yololol"));
    	put(2, new Order(2, "Baptiste", "azerty"));
    	put(3, new Order(3, "Poquelin", "qwerty"));
    }};
 
    public static DB getInstance() {
        if (instance == null)
            instance = new DB();
        return instance;
    }
    public void addObject(Order t){
        ht.put(t.getId(), t);
    }
    public Order getObject(int id) {
        return ht.get(id);
    }
    public ArrayList<Order> getAllObjects() {
    	ArrayList<Order> list = new ArrayList<>();
    	Set<Integer> keys = ht.keySet();
    	for (Integer key : keys) {
			list.add(ht.get(key));
		}
    	return list;
    }
    
    public Order getNext(int id) {
    	id = id + 1;
    	while(this.getObject(id) == null) {
    		id++;
    	}
    	return this.getObject(id);
    }
    
    public void removeObject(int id) {
    	ht.remove(id);
    }
    
    public boolean isLast(int id) {
    	ArrayList<Order> arr = this.getAllObjects();
    	if(id == arr.size()) {
    		return true; // is last
    	} else {
    		return false;
    	}
    }
}