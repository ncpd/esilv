package api;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.util.stream.Collectors;
import com.google.gson.Gson;
import db.*;

@WebServlet("/*")
public class APIServlet extends HttpServlet {
	
	// api/country/:id
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String[] path = request.getPathInfo().split("/");
	    if(path.length>2 && path[1].equalsIgnoreCase("country")) {
	            DB db = DB.getInstance();
	            response.setStatus(200);
	            response.setHeader("Content-Type" , "application/json");
	            Gson gson = new Gson();
	            response.getWriter().println(gson.toJson(db.getObject(Integer.valueOf(path[2]))));
	    } else {
	            response.setStatus(400);
	    }
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String[] path = request.getPathInfo().split("/");
	    if(path.length==2 && path[1].equalsIgnoreCase("country")) {
	            String data = request.getReader().lines().collect(Collectors.joining());
	            Gson gson = new Gson();
	            Country c = gson.fromJson(data, Country.class);
	            c.setId(c.getId());
	            DB db = DB.getInstance();
	            db.addObject(c);
	            response.setStatus(201);
	            System.out.println("Created");
	    } else {
	            response.setStatus(400);
	    }
	}
	
	@Override
	protected void doPut(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		String[] path = req.getPathInfo().split("/");
	    if(path.length==2 && path[1].equalsIgnoreCase("country")) {
	            DB db = DB.getInstance();
	            String data = req.getReader().lines().collect(Collectors.joining());
	            Gson gson = new Gson();
	            Country c = gson.fromJson(data, Country.class);
	            try {
	            Country toChange = db.getObject(c.getId());
	            toChange.setId(c.getId());
	            toChange.setName(c.getName());
	            resp.setStatus(201);
	            System.out.println("Modified");
	            } catch(Exception e) {
	            	resp.setStatus(404);
	            }
	    } else {
	            resp.setStatus(400);
	    }
	}
	
	@Override
	protected void doDelete(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		String[] path = req.getPathInfo().split("/");
	    if(path.length>2 && path[1].equalsIgnoreCase("country")) {
	            DB db = DB.getInstance();
	            try {
	            db.deleteObject(Integer.valueOf(path[2]));
	            resp.setStatus(200);
	            System.out.println("Deleted");
	            } catch(Exception e) {
	            	resp.setStatus(404);
	            }
	    } else {
	            resp.setStatus(400);
	    }
	}

}
