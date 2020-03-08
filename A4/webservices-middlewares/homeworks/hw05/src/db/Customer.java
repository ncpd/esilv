package db;

import java.util.ArrayList;

public class Customer {
	private int id;
	private String name;
	private ArrayList<Order> orders;
	
	public Customer(int id, String name, ArrayList<Order> orders) {
		super();
		this.id = id;
		this.name = name;
		this.orders = orders;
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public ArrayList<Order> getOrders() {
		return orders;
	}
	public void setOrders(ArrayList<Order> orders) {
		this.orders = orders;
	}
	
	
}
