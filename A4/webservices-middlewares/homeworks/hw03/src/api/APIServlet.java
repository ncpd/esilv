package api;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import db.*;

@WebServlet("/*")
public class APIServlet extends HttpServlet {
				
	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		response.setStatus(400);
	}
	
	@Override
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		HttpSession session = request.getSession(false);
		if(session == null || !request.isRequestedSessionIdValid()) {
			// Create session
			session = request.getSession();
			session.setMaxInactiveInterval(30*60);
			session.setAttribute("state", TripState.NEW);
			System.out.println("New Session " + session.getId());
		} else {
			// Change session state
			TripState state = (TripState) session.getAttribute("state");
			switch(state) {
			case NEW:
				state = TripState.WFPAYMENT;
				session.setAttribute("state", state);
				System.out.println(session.getId() + " TRIP STATE HAS CHANGED FROM " + TripState.NEW + " TO " + state.toString());
				response.getWriter().println(session.getId() + " TRIP STATE HAS CHANGED FROM " + TripState.NEW + " TO " + state.toString());
				response.setStatus(200);
				break;
			case WFPAYMENT:
				state = TripState.COMPLETED;
				session.setAttribute("state", state);
				System.out.println(session.getId() + " TRIP STATE HAS CHANGED FROM " + TripState.WFPAYMENT + " TO " + state.toString());
				session.invalidate();
				response.setStatus(200);
				response.getWriter().println("Booking is COMPLETED. " + session.getId() + " has been invalidated.");
				break;
			default:
				session.invalidate();
				response.setStatus(400);
				break;
			}
		}
	}
}
