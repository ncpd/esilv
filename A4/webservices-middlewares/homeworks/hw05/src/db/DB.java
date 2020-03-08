package db;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Set;

public class DB {
	private static DB instance = null;
    private Hashtable<Integer, Customer> ht = new Hashtable<Integer, Customer>() {{
    	put(1, new Customer(1, "Jean", new ArrayList<Order>() {{ add(new Order(1, "test item C")); }}));
    	put(2, new Customer(2, "Jul", new ArrayList<Order>() {{ add(new Order(1, "test item A")); add(new Order(2, "test item B"));}}));
    }};
    private Hashtable<Integer, Customer> deleteQueue = new Hashtable<Integer, Customer>();
 
    public static DB getInstance() {
        if (instance == null)
            instance = new DB();
        return instance;
    }
    public void addObject(Customer t){
        ht.put(t.getId(), t);
    }
    public Customer getObject(int id) {
        return ht.get(id);
    }
    public ArrayList<Customer> getAllObjects() {
    	ArrayList<Customer> list = new ArrayList<>();
    	Set<Integer> keys = ht.keySet();
    	for (Integer key : keys) {
			list.add(ht.get(key));
		}
    	return list;
    }
    
    public void removeObject(int id) {
    	deleteQueue.put(id, getObject(id));
    	ht.remove(id);
    }
    
    public Customer getFromQueue(int id) {
    	return deleteQueue.get(id);
    }
    
    public void removeFromQueue(int id) {
    	deleteQueue.remove(id);
    }

}
