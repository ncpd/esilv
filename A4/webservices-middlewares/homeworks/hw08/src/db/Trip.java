package db;

public class Trip {
	private int id;
	private String destination;
	private int capacity;
	private boolean availability;
	
	public Trip() {
		super();
	}
	public Trip(int id, String destination, int capacity, boolean availability) {
		super();
		this.id = id;
		this.destination = destination;
		this.capacity = capacity;
		this.availability = availability;
	}
	public int getCapacity() {
		return capacity;
	}
	
	public void setCapacity(int capacity) {
		if(capacity <= 0) {
			this.availability = false;
			this.capacity = 0;
		} else {
			this.capacity = capacity;
		}
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public String getDestination() {
		return destination;
	}
	public void setDestination(String destination) {
		this.destination = destination;
	}
	public boolean isAvailability() {
		return availability;
	}
	public void setAvailability(boolean availability) {
		this.availability = availability;
	}
	
	
}
