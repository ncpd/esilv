package jms;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Timer;
import java.util.TimerTask;
import java.util.stream.Collectors;

public class Program {

	public final static String STATS_OUTPUT = "data_"+(new SimpleDateFormat("yyyy-MM-dd-HH-mm-ss").format(new Date()))+ ".csv";
	
	public static void main(String[] args) throws Exception {
		// TODO Auto-generated method stub
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
