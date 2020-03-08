package com.picarnic.hwone;

public class Trip {
	private String id;
	private String location;
	private Person person;

	public Trip(String id, String location, String fName, String lName) {
		this.id = id;
		this.location = location;
		this.person = new Person(fName, lName);
	}

	public String getLocation() {
		return location;
	}

	public void setLocation(String location) {
		this.location = location;
	}

	public Person getPerson() {
		return person;
	}

	public void setPerson(Person person) {
		this.person = person;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}
}
