package db;

public class Booking {
	private Trip trip;
	private String person;
	
	public Booking() {
		this(null,"N/A");
	}
	
	public Booking(Trip trip, String person) {
		super();
		this.trip = trip;
		this.person = person;
	}
	public Trip getTrip() {
		return trip;
	}
	public void setTrip(Trip trip) {
		this.trip = trip;
	}
	public String getPerson() {
		return person;
	}
	public void setPerson(String person) {
		this.person = person;
	}
	
	
}
