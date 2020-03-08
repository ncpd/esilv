package db;

public class Order {
	
    private String item;
    private int id;
 
    public Order(int id, String item) {
        this.id = id;
        this.item = item;
    }
    
    public Order() {
    	
    }

    public String getItem() {
        return item;
    }
 
    public void setItem(String item) {
        this.item = item;
    }
    
    public int getId() {
        return id;
    }
 
    public void setId(int id) {
        this.id = id;
    }
}