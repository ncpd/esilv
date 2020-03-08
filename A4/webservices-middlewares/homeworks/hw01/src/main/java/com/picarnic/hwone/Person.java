package com.picarnic.hwone;

public class Person {
	private String fName;
	private String lName;

	public Person(String fName, String lName) {
		this.setfName(fName);
		this.setlName(lName);
	}

	public String getfName() {
		return fName;
	}

	public void setfName(String fName) {
		this.fName = fName;
	}

	public String getlName() {
		return lName;
	}

	public void setlName(String lName) {
		this.lName = lName;
	}
}
