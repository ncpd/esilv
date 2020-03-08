package db;
import java.util.Hashtable;
 
public class DB {
    private static DB instance = null;
    private Hashtable<Integer, Country> ht = new Hashtable<Integer, Country>() {
    	{
    		put(1, new Country(1, "Czech Republic"));
    		put(2, new Country(2, "France"));
    		put(3, new Country(3, "United Kingdom"));
    	}
    };
 
    public static DB getInstance() {
        if (instance == null)
            instance = new DB();
        return instance;
    }
    public void addObject(Country t){
        ht.put(t.getId(), t);
        System.out.println(ht.size());
    }
    public Country getObject(int id) {
        return ht.get(id);
    }
    public void deleteObject(int id) {
    	ht.remove(id);
    }
}