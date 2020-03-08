package jms;

public class BookingProcessor {

    
    public static void main(String[] args) throws Exception {
        String bookingQueue = "jms/mdw-bqueue" ;
        
        // create the producer object and receive the message
        BookingProcessorConsumer consumer = new BookingProcessorConsumer();
        consumer.receive(bookingQueue);
    }
}
