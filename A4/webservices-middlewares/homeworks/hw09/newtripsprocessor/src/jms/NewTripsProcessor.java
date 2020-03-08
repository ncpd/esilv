package jms;

public class NewTripsProcessor {

    
    public static void main(String[] args) throws Exception {
        String newtripsQueue = "jms/mdw-ntqueue" ;
        
        // create the producer object and receive the message
        NewTripsProcessorConsumer consumer = new NewTripsProcessorConsumer();
        consumer.receive(newtripsQueue);
    }
}
