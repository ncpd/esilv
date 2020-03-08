package db;
 
public class Trip {
	
    private int id;
    private String title;
    private int capacity;
    private boolean occupied;

    public Trip() {
    	this(0, "N/A", 0, false);
    }
    
	public Trip(int id, String title, int capacity, boolean occupied) {
		super();
		this.id = id;
		this.title = title;
		this.capacity = capacity;
		this.occupied = occupied;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public int getCapacity() {
		return capacity;
	}

	public void setCapacity(int capacity) {
		this.capacity = capacity;
	}

	public boolean isOccupied() {
		return occupied;
	}

	public void setOccupied(boolean occupied) {
		this.occupied = occupied;
	}

    
}