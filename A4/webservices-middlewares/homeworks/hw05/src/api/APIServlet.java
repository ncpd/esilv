package api;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.tangosol.coherence.component.net.requestContext.AsyncContext;

import db.*;

@WebServlet(urlPatterns = { "/*" }, asyncSupported = true)
public class APIServlet extends HttpServlet {

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String[] path = request.getPathInfo().split("/");
		if (path.length > 3 && path[1].equalsIgnoreCase("request") && path[2].equalsIgnoreCase("status")) { // request/status/123
			int customerid = -1;
			
			try {
				if(path[3] != null) {
					customerid = Integer.valueOf(path[3]);
				} else {
					response.setStatus(400);
				}
			} catch (Exception e) {
				e.printStackTrace();
			}
			
			if(customerid != -1) {
				DB db = DB.getInstance();
				Customer c = db.getFromQueue(customerid);
				if(c != null) {
					response.setStatus(200);
					response.getWriter().println("Customer no. " + customerid + " is being processed. Please wait and retry.");
				} else {
					response.setStatus(404);
					response.getWriter().println("Customer no. " + customerid + " not found.");
				}
			} else {
				response.setStatus(400);
			}
		} else {
			response.setStatus(400);
		}
	}

	@Override
	protected void doDelete(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		String[] path = req.getPathInfo().split("/");
		int customerid = Integer.valueOf(path[2]);
		if (path.length > 2 && path[1].equalsIgnoreCase("customer") && customerid > 0) { // /customer/id
			final javax.servlet.AsyncContext acontext = req.startAsync();
			acontext.start(new Runnable() {
				
				@Override
				public void run() {
					// TODO Auto-generated method stub
					HttpServletResponse response = (HttpServletResponse) acontext.getResponse();
					try {
						DB db = DB.getInstance();
						Customer c = db.getObject(customerid);
						if(c != null) {
							response.setStatus(202);
							response.setHeader("Location", "/request/status/" + customerid);
							acontext.complete();
							db.removeObject(customerid);
							Thread.sleep(10000);
							db.removeFromQueue(customerid);
							System.out.println("It's been 10 seconds, customer no." + customerid + " has been deleted.");
						} else {
							response.setStatus(204);
							acontext.complete();
						}
					} catch (Exception e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			});
			
		} else {
			resp.setStatus(400);
		}
	}
}
