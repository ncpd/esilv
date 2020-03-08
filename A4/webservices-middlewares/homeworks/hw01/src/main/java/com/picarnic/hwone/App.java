package com.picarnic.hwone;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.json.JSONObject;

public class App {
	public static void main(String[] args) {
		try {
			Trip trip = parseFile("source.txt");
			saveJSONtoFile(TripToJSON(trip));

		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public static String TripToJSON(Trip trip) {
		// Base object
		JSONObject tripJson = new JSONObject();
		tripJson.put("id", trip.getId());
		tripJson.put("location", trip.getLocation());

		// Person object
		JSONObject personJson = new JSONObject();
		personJson.put("name", trip.getPerson().getfName());
		personJson.put("surname", trip.getPerson().getlName()); 
		
		// Put person object in trip object
		tripJson.put("person", personJson);
		
		return tripJson.toString(4);
	}

	public static void saveJSONtoFile(String json) {
	    BufferedWriter writer;
		try {
			writer = new BufferedWriter(new FileWriter("output.json"));
			
			writer.write(json);
		     
		    writer.close();
		    System.out.println("JSON file saved successfully !");
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    
	}
	
	public static Trip parseFile(String fileName) throws FileNotFoundException {
		ClassLoader classLoader = new App().getClass().getClassLoader();
		 
        File file = new File(classLoader.getResource(fileName).getFile());
        
		Scanner scan = new Scanner(file);
		StringBuilder finalStr = new StringBuilder();
		while (scan.hasNext()) {
			String line = scan.nextLine().toString();
			Pattern pattern = Pattern.compile("\"(.*?)\"");
			Matcher matcher = pattern.matcher(line);

			List<String> listMatches = new ArrayList<String>();

			while (matcher.find()) {
				listMatches.add(matcher.group());
			}

			for (String s : listMatches) {
				finalStr.append(s).append(";");
			}
		}
		
		try {
			String concatenated = finalStr.toString().replaceAll("\"", "");
			String[] splitString = concatenated.split(";"); // First split
			String[] personSplit = splitString[2].split(" "); // Name split
			return new Trip(splitString[0], splitString[1], personSplit[0], personSplit[1]);
		} catch(Exception e) {
			e.printStackTrace();
		}
		
		return null;
	}
}
