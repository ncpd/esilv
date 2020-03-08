package db;

public enum TripState {
	NEW ("NEW"),
	WFPAYMENT ("WAITING FOR PAYMENT"),
	COMPLETED ("COMPLETED");
	
	private String description = "";
	
	TripState(String description) {
		this.description = description;
	}
	
	public String toString() {
		return description;
	}
}
