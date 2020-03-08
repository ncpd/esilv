package jms;

public class BookingProcessor {

    
    public static void main(String[] args) throws Exception {
    	// input arguments
        String msg = "confirmation" ;
        String bookingQueue = "jms/mdw-bqueue" ;
        // input arguments
        String confirmationQueue = "jms/mdw-cqueue" ;
        
        // create the producer object and receive the message
        BookingProcessorConsumer consumer = new BookingProcessorConsumer();
        //BookingProcessor bp = new BookingProcessor();
        consumer.receive(bookingQueue);
    }
}
