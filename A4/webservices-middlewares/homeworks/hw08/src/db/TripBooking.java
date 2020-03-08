package db;

public class TripBooking {
	private int id;
	private Trip trip;
	private Person passenger;
	
	public TripBooking() {
		super();
	}
	public TripBooking(int id, Trip trip, Person passenger) {
		super();
		this.id = id;
		this.trip = trip;
		this.passenger = passenger;
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public Trip getTrip() {
		return trip;
	}
	public void setTrip(Trip trip) {
		this.trip = trip;
	}
	public Person getPassenger() {
		return passenger;
	}
	public void setPassenger(Person passenger) {
		this.passenger = passenger;
	}
	
}
