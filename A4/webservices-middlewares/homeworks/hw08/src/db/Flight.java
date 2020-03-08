package db;

public class Flight {
	private int id;
	private String arrivalAirport;
	private String departureAirport;
	private String arrivalDate;
	private String departureDate;
	private int capacity;
	private boolean availability;
	
	public Flight() {
		
	}
	
	public Flight(int id, String arrivalAirport, String departureAirport, String arrivalDate, String departureDate,
			int capacity, boolean availability) {
		super();
		this.id = id;
		this.arrivalAirport = arrivalAirport;
		this.departureAirport = departureAirport;
		this.arrivalDate = arrivalDate;
		this.departureDate = departureDate;
		this.capacity = capacity;
		this.availability = availability;
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public String getArrivalAirport() {
		return arrivalAirport;
	}
	public void setArrivalAirport(String arrivalAirport) {
		this.arrivalAirport = arrivalAirport;
	}
	public String getDepartureAirport() {
		return departureAirport;
	}
	public void setDepartureAirport(String departureAirport) {
		this.departureAirport = departureAirport;
	}
	public String getArrivalDate() {
		return arrivalDate;
	}
	public void setArrivalDate(String arrivalDate) {
		this.arrivalDate = arrivalDate;
	}
	public String getDepartureDate() {
		return departureDate;
	}
	public void setDepartureDate(String departureDate) {
		this.departureDate = departureDate;
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
	public boolean isAvailability() {
		return availability;
	}
	public void setAvailability(boolean availability) {
		this.availability = availability;
	}
	
}
