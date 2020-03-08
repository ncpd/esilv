package jms;

public class OrderProcessor {

    
    public static void main(String[] args) throws Exception {
    	// input arguments
        String allOrdersQueue = "jms/mdw-aoqueue";
        
        // create the producer object and receive the message
        OrderProcessorConsumer consumer = new OrderProcessorConsumer();
        consumer.receive(allOrdersQueue);
    }
}
