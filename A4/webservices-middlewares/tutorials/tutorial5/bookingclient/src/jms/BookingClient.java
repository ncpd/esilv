package jms;

public class BookingClient {

    
    public static void main(String[] args) throws Exception {
    	// input arguments
        String msg = "booking" ;
        String bookingQueue = "jms/mdw-bqueue" ;
        
        // create the producer object and send the message
        BookingClientProducer producer = new BookingClientProducer();
        producer.send(bookingQueue, msg);
    	
        // input arguments
        String confirmationQueue = "jms/mdw-cqueue" ;
        
        // create the producer object and receive the message
        BookingClientConsumer consumer = new BookingClientConsumer();
        consumer.receive(confirmationQueue);
    }
}
