package db;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlTransient;

@XmlRootElement(name="order")
public class Order {
	@XmlElement(name="customer")
    private String customer;
	@XmlElement(name="item")
    private String item;
	
    private int id;
 
    public Order(int id, String customer, String item) {
        this.id = id;
        this.customer = customer;
        this.item = item;
    }
    
    public Order() {
    	
    }
 
    public String getCustomer() {
        return customer;
    }
 
    public void setCustomer(String customer) {
        this.customer = customer;
    }

    public String getItem() {
        return item;
    }
 
    public void setItem(String item) {
        this.item = item;
    }
    @XmlTransient
    public int getId() {
        return id;
    }
 
    public void setId(int id) {
        this.id = id;
    }
}