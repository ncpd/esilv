package db;

import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name="booking")
public class Booking {
	private int id;
	private String person;
	private String arrivalDate;
	private String departureDate;
	private String arrivalAirportID;
	private String departureAirportID;
	
	public Booking() {
		
	}
	
	public Booking(int id, String person, String arrivalDate, String departureDate, String arrivalAirportID, String departureAirportID) {
		super();
		this.id = id;
		this.person = person;
		this.arrivalDate = arrivalDate;
		this.departureDate = departureDate;
		this.arrivalAirportID = arrivalAirportID;
		this.departureAirportID = departureAirportID;
	}
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	
	public String getPerson() {
		return person;
	}
	public void setPerson(String person) {
		this.person = person;
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

	public String getArrivalAirportID() {
		return arrivalAirportID;
	}

	public void setArrivalAirportID(String arrivalAirportID) {
		this.arrivalAirportID = arrivalAirportID;
	}

	public String getDepartureAirportID() {
		return departureAirportID;
	}

	public void setDepartureAirportID(String departureAirportID) {
		this.departureAirportID = departureAirportID;
	}

	
}
