package db;

public class FlightBooking {
	private int id;
	private Flight flight;
	private Person passenger;
	
	public FlightBooking() {
		super();
	}
	public FlightBooking(int id, Flight flight, Person passenger) {
		super();
		this.id = id;
		this.flight = flight;
		this.passenger = passenger;
	}
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public Flight getFlight() {
		return flight;
	}
	public void setFlight(Flight flight) {
		this.flight = flight;
	}
	public Person getPassenger() {
		return passenger;
	}
	public void setPassenger(Person passenger) {
		this.passenger = passenger;
	}
	
	
	
	
}
