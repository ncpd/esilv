# Homework 1  
## Transformation of formats  

I used the original data from the example on [GitLab](https://gitlab.fit.cvut.cz/mi-mdw/mi-mdw/blob/master/hw/01/index.adoc).

### Data Format 1 (Plain Text)

#### Input for the Java application in a txt file

```
Dear Sir or Madam,

please find the details about my booking bellow:

===
Trip id: "1"
Location: "Bohemian Switzerland"
Person: "Jan Novak"
===

Regards,
Jan Novak
```

### Data Format 2 (JSON)  

#### Output from the Java application in a JSON file  

```json
{
    "person": {
        "surname": "Novak",
        "name": "Jan"
    },
    "location": "Bohemian Switzerland",
    "id": "1"
}
```

### Source code explanation  

I decided to use Maven for this project as there is no default JSON processing in Java, I needed to add a library to it.

At first, the input txt file (source.txt) is located in Maven's main/resources and loaded into a File type variable.

```java
ClassLoader classLoader = new App().getClass().getClassLoader();
		 
File file = new File(classLoader.getResource(fileName).getFile());
```

Afterwards, we scan the file with a regular expression to get essential information (which is always in quotation marks), and then append each information with a semicolon.

```java
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
```

The final string from the parsing function looks like :

```
1;Bohemian Switzerland;Jan Novak;
```

I choosed to split this String into a String array.
Data is processed into Objects (Trip and Person classes) to work in an efficient Object-Oriented Programming way.

```java
String concatenated = finalStr.toString().replaceAll("\"", "");
			String[] splitString = concatenated.split(";"); // First split
			String[] personSplit = splitString[2].split(" "); // Name split
			return new Trip(splitString[0], splitString[1], personSplit[0], personSplit[1]);
		}
```

The Trip constructor being :

```java
public Trip(String id, String location, String fName, String lName) {
		this.id = id;
		this.location = location;
		this.person = new Person(fName, lName);
	}
```

A function makes the processing of Object to JSON easily with JSON in Java and Objects getters.  

```java
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
		
        // Prettify JSON String with 4 spaces
		return tripJson.toString(4);
	}
```

A final function writes the JSON String to an output JSON file (named output.json).

```java
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
```

### Dependencies
[JSON in Java](https://mvnrepository.com/artifact/org.json/json) to use JSON Objects easily.