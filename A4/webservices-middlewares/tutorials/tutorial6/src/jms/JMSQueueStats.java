package jms;

import java.io.FileOutputStream;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Hashtable;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;
import java.util.stream.Collectors;

import javax.management.MBeanServerConnection;
import javax.management.ObjectName;
import javax.management.remote.JMXConnector;
import javax.management.remote.JMXConnectorFactory;
import javax.management.remote.JMXServiceURL;

public class JMSQueueStats {
	
	public final static String WEBLOGIC_USER = "weblogic";
	public final static String WEBLOGIC_PASSWORD = "welcome1";
	public final static String STATS_OUTPUT = "data_"+(new SimpleDateFormat("yyyy-MM-dd-HH-mm-ss").format(new Date()))+ ".csv";
	
	public JMXConnector jmxCon = null;
	public MBeanServerConnection connection = null;
	
	public void open() throws Exception {
		
		JMXServiceURL serviceUrl =  new JMXServiceURL("service:jmx:t3://127.0.0.1:7001/jndi/weblogic.management.mbeanservers.runtime");
		 
        System.out.println("Connecting to: " + serviceUrl);

        Hashtable<String, String> env = new Hashtable<>();
        env.put(JMXConnectorFactory.PROTOCOL_PROVIDER_PACKAGES, "weblogic.management.remote");
        env.put(javax.naming.Context.SECURITY_PRINCIPAL, WEBLOGIC_USER);
        env.put(javax.naming.Context.SECURITY_CREDENTIALS, WEBLOGIC_PASSWORD);

        jmxCon = JMXConnectorFactory.newJMXConnector(serviceUrl, env);
        jmxCon.connect();
        connection = jmxCon.getMBeanServerConnection();
		
	}
	
	public void close() throws Exception {
		if (jmxCon != null) {
            jmxCon.close();
		}
	}
	
	public List<List<String>> readStats() throws Exception {
		ObjectName service = new ObjectName("com.bea:Name=RuntimeService,Type=weblogic.management.mbeanservers.runtime.RuntimeServiceMBean");
        ObjectName serverRuntimeMBean = (ObjectName)connection.getAttribute(service, "ServerRuntime");
        System.out.println("serverRuntimeMBean: " + serverRuntimeMBean);
        String serverName = (String)connection.getAttribute(serverRuntimeMBean, "Name");
        System.out.println("Server name: " + serverName);

        ObjectName jmsRuntimeMbean = (ObjectName)connection.getAttribute(serverRuntimeMBean, "JMSRuntime");
        System.out.println("JMSRuntime: " + jmsRuntimeMbean);
         
        ObjectName[] jmsServerRuntimeMbeans = (ObjectName[])connection.getAttribute(jmsRuntimeMbean, "JMSServers");
        
        List<List<String>> out = new ArrayList<>(); 
        
        for (ObjectName jmsServerRuntime : jmsServerRuntimeMbeans) {
            String jmsServerName = (String)connection.getAttribute(jmsServerRuntime, "Name");
            System.out.println("JMSServer name: " + jmsServerName);
            
            ObjectName[] jmsDestinationRuntimeMbeans = (ObjectName[])connection.getAttribute(jmsServerRuntime, "Destinations");
            for (ObjectName jmsDestinationRuntime : jmsDestinationRuntimeMbeans) {
                String destinationName = (String)connection.getAttribute(jmsDestinationRuntime, "Name");
                System.out.println(destinationName);    
                 
                if (destinationName.split("@").length == 2) {
                    destinationName = destinationName.split("@")[1];
                }
                if (destinationName.split("!").length == 2) {
                    destinationName = destinationName.split("!")[1];
                }

                System.out.println(destinationName);                    

                long messagesCurrentCount = (Long)connection.getAttribute(jmsDestinationRuntime, "MessagesCurrentCount");
                System.out.println("messagesCurrentCount " + messagesCurrentCount);
                 
                long messagesPendingCount = (Long)connection.getAttribute(jmsDestinationRuntime, "MessagesPendingCount");
                System.out.println("messagesPendingCount " + messagesPendingCount);
                 
                long messagesReceivedCount = (Long)connection.getAttribute(jmsDestinationRuntime, "MessagesReceivedCount");
                System.out.println("messagesReceivedCount " + messagesReceivedCount);
                 
                long messagesHighCount = (Long)connection.getAttribute(jmsDestinationRuntime, "MessagesHighCount");
                System.out.println("messagesHighCount " + messagesHighCount);
                 
                System.out.println("--------------------------------------------------");  
                
                // build an entry
                ArrayList<String> entry = new ArrayList<>();
                entry.add(new SimpleDateFormat("yyyy.MM.dd HH:mm:ss").format(new Date()));
                entry.add(jmsServerName);
                entry.add(destinationName);
                entry.add(String.valueOf(messagesCurrentCount));
                entry.add(String.valueOf(messagesPendingCount));
                entry.add(String.valueOf(messagesReceivedCount));
                entry.add(String.valueOf(messagesHighCount));
                out.add(entry);
            }
        }
        return(out);
	}
	
	public static void main(String[] args) throws Exception {
		
		FileOutputStream fos = new FileOutputStream(STATS_OUTPUT, false);
	    fos.write("timestamp,server,queue,messagesCurrentCount,messagesPendingCount,messagesReceivedCount,messagesHighCount\n".getBytes());
	    fos.close();
		
		JMSQueueStats stats = new JMSQueueStats();
		stats.open();
		
		Timer t = new Timer();
		t.schedule(new TimerTask() {
		    @Override
		    public void run() {
		    	try {
		    		FileOutputStream fos = new FileOutputStream(STATS_OUTPUT, true);
		    		String csvStats = stats.readStats().stream().map(entry -> String.join(",", entry)).collect(Collectors.joining("\n"))+"\n";
		    	    fos.write(csvStats.getBytes());
		    	    fos.close();
				} catch (Exception e) {
					e.printStackTrace();
				}
		    }
		}, 0, 5000);
		
//		stats.close();
        
    }

}
