package api;

import java.io.IOException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.xml.bind.annotation.adapters.HexBinaryAdapter;

import java.util.ArrayList;
import com.google.gson.Gson;
import db.*;

@WebServlet("/*")
public class APIServlet extends HttpServlet {

	private static final String CACHE_CONTROL_HEADER = "Cache-Control";
	private static final String LAST_MODIFIED_HEADER = "Last-Modified";
	private static final String IF_MODIFIED_SINCE_HEADER = "If-Modified-Since";
	private static final String ETAG_HEADER = "ETag";
	private static final String IF_NONE_MATCH_HEADER = "If-None-Match";
	private static final String CONTENT_TYPE_HEADER = "Content-Type";
	
	private static long LAST_MODIFIED_MILLIS = System.currentTimeMillis();
	
	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String[] path = request.getPathInfo().split("/");
		if (path.length > 2 && path[1].equalsIgnoreCase("customers") && path[2].equalsIgnoreCase("v1")) { // /customers/v1 => STRONG ETAG
			
			DB db = DB.getInstance();
			ArrayList<Customer> list = db.getAllObjects();
			
			long lastModifiedBrowser = request.getDateHeader(IF_MODIFIED_SINCE_HEADER);
			String browserETag = request.getHeader(IF_NONE_MATCH_HEADER);
						
			response.setHeader(CACHE_CONTROL_HEADER, "private, no-store, max-age=500");
			response.setHeader(ETAG_HEADER, computeStrongETag(list));
			response.setDateHeader(LAST_MODIFIED_HEADER, LAST_MODIFIED_MILLIS);
			
			boolean uptodateEtag = false;
			boolean uptodateLastModified = false;
			if(browserETag != null || lastModifiedBrowser != -1) {
				// At least one header is present
				if(browserETag != null) { // ETag header is present
					if(!browserETag.equalsIgnoreCase(computeStrongETag(list))) {
						// Send 200 OK
						uptodateEtag = false;
					} else {
						// Send 304 Not Modified
						uptodateEtag = true;
					}
				}
				if(lastModifiedBrowser != -1) { // Last-Modified header is present
					if(compareLastModified(lastModifiedBrowser)) {
						// Send 200 OK
						uptodateLastModified = false;
					} else {
						// Send 304 Not Modified
						uptodateLastModified = true;
					}
				}
			}
			
			if (uptodateEtag || uptodateLastModified) {
				// Send 304 Not Modified
				response.setStatus(304);
			} else {
				// Send 200 OK
				response.setHeader(CONTENT_TYPE_HEADER, "application/json");
				response.addDateHeader(LAST_MODIFIED_HEADER, LAST_MODIFIED_MILLIS);
				Gson gson = new Gson();
				response.getWriter().println(gson.toJson(list));
				response.setStatus(200);
			}
			
		} else if(path.length > 2 && path[1].equalsIgnoreCase("customers") && path[2].equalsIgnoreCase("v2")) { // /customers/v2 => WEAK ETAG
			
			DB db = DB.getInstance();
			ArrayList<Customer> list = db.getAllObjects();
			
			long lastModifiedBrowser = request.getDateHeader(IF_MODIFIED_SINCE_HEADER);
			String browserETag = request.getHeader(IF_NONE_MATCH_HEADER);
			
			response.setHeader(CACHE_CONTROL_HEADER, "private, no-store, max-age=500");
			response.setHeader(ETAG_HEADER, computeWeakETag(list));
			response.setDateHeader(LAST_MODIFIED_HEADER, LAST_MODIFIED_MILLIS);
						
			boolean uptodateEtag = false;
			boolean uptodateLastModified = false;
			if(browserETag != null || lastModifiedBrowser != -1) {
				// At least one header is present
				if(browserETag != null) { // ETag header is present
					if(!browserETag.equalsIgnoreCase(computeWeakETag(list))) {
						// Send 200 OK
						uptodateEtag = false;
					} else {
						// Send 304 Not Modified
						uptodateEtag = true;
					}
				}
				if(lastModifiedBrowser != -1) { // Last-Modified header is present
					if(compareLastModified(lastModifiedBrowser)) {
						// Send 200 OK
						uptodateLastModified = false;
					} else {
						// Send 304 Not Modified
						uptodateLastModified = true;
					}
				}
			}
			
			if (uptodateEtag || uptodateLastModified) {
				// Send 304 Not Modified
				response.setStatus(304);
			} else {
				// Send 200 OK
				response.setHeader(CONTENT_TYPE_HEADER, "application/json");
				response.addDateHeader(LAST_MODIFIED_HEADER, LAST_MODIFIED_MILLIS);
				Gson gson = new Gson();
				response.getWriter().println(gson.toJson(list));
				response.setStatus(200);
			}
		} else {
			response.setStatus(400);
		}
	}
	
	private boolean compareLastModified(long browserIfModifiedSince) {
		if(browserIfModifiedSince != -1 && LAST_MODIFIED_MILLIS <= browserIfModifiedSince) {
			return false; // 304 : No need to refresh
		} else {
			return true;
		}
	}
	
	private String computeStrongETag(ArrayList<Customer> customers) {
		String content = "";
		for (Customer customer : customers) {
			String ordersContent = "";
			for (Order order : customer.getOrders()) {
				ordersContent += order.getId() + order.getItem();
			}
			content += customer.getId() + customer.getName() + ordersContent;
		}
		
		MessageDigest md;
		String weakETag;
		try {
			md = MessageDigest.getInstance("MD5");
			weakETag = (new HexBinaryAdapter()).marshal(md.digest(content.getBytes()));
		} catch (NoSuchAlgorithmException e) {
			weakETag = "";
			e.printStackTrace();
		}
		
		return "\"" + weakETag + "\"";
	}
	
	private String computeWeakETag(ArrayList<Customer> customers) {
		String content = "";
		for (Customer customer : customers) {
			content += customer.getId() + customer.getName();
		}
		
		MessageDigest md;
		String weakETag;
		try {
			md = MessageDigest.getInstance("MD5");
			weakETag = (new HexBinaryAdapter()).marshal(md.digest(content.getBytes()));
		} catch (NoSuchAlgorithmException e) {
			weakETag = "";
			e.printStackTrace();
		}
		
		return "W/\"" + weakETag + "\"";
	}

}
