package db;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Set;
 
public class DB {
    private static DB instance = null;
    private Hashtable<Integer, Customer> ht = new Hashtable<Integer, Customer>() {{
    	put(1, new Customer(1, "aaa", new ArrayList<Order>() {{ add(new Order(1, "item test")); }}));
    	//put(1, new Customer(1, "aaa", new ArrayList<Order>())); // Test
    	put(2, new Customer(2, "bbb", new ArrayList<Order>()));
    }};
 
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
    	ht.remove(id);
    }
}