package api;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;

import javax.servlet.ServletException;
import javax.servlet.ServletOutputStream;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import weblogic.net.http.HttpURLConnection;

@WebServlet("/*")
public class APIServlet extends HttpServlet {
	
	static AtomicInteger SERVER_INDEX = new AtomicInteger(1);
	
	private static List<Integer> healthy = Arrays.asList(1, 2, 3);
	private List<Integer> down = Arrays.asList();
				
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		int server = getServer();
		monitor();
		if(!healthy.isEmpty() && healthy.contains(server)) {
			String url = String.format("http://147.32.233.18:8888/MI-MDW-LastMinute%d/list", server);
			HttpURLConnection connection = (HttpURLConnection) (new URL(url)).openConnection();
			// HTTP method
			connection.setRequestMethod("GET");
			// copy headers
			Collections.list(request.getHeaderNames()).forEach(head -> connection.setRequestProperty(head, request.getHeader(head)));
			// copy body
			BufferedReader inputStream = new BufferedReader(new InputStreamReader(connection.getInputStream()));
			String inputLine;
			ServletOutputStream sout = response.getOutputStream();
			while ((inputLine = inputStream.readLine()) != null) {
				sout.write(inputLine.getBytes());
			}
			// close
			inputStream.close();
			System.out.println("Forwarding to " + url);
			sout.flush();
		} else {
			response.setStatus(500);
		}
	}
	
	private static int getServer() {
		if(healthy != null && !healthy.isEmpty()) {
			Integer[] servs = healthy.toArray(new Integer[healthy.size()]);
			return servs[SERVER_INDEX.getAndAccumulate(servs.length, (cur, n)->cur >= n-1 ? 0 : cur+1)];
		} else {
			return -1;
		}
	}
	
	private void addToHealthy(int service) {
		if(healthy != null && down != null && !healthy.contains(service) && down.contains(service)) {
			healthy.add(service);
			down.remove(service);
		}
	}
	
	private void addToDown(int service) {
		if(down != null && healthy != null && !down.contains(service) && healthy.contains(service)) {
			down.add(service);
			healthy.remove(service);
		}
	}
	
	private void monitor() {
		for(int i = 1; i <= 3; i++) {
			try {
				String url = String.format("http://147.32.233.18:8888/MI-MDW-LastMinute%d/list", i);
				HttpURLConnection connection = (HttpURLConnection) (new URL(url)).openConnection();
				connection.setRequestMethod("GET");
				int code = connection.getResponseCode();
				System.out.println(code + " " + url);
				if(code == 200) {
					// add to healthy
					addToHealthy(i);
				} else {
					// add to down
					addToDown(i);
				}
			} catch(Exception e) {
				e.printStackTrace();
			}
		}
	}
}
