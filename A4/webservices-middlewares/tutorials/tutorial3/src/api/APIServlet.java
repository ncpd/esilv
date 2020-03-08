package api;

import java.io.IOException;
import java.io.StringWriter;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;
import javax.xml.bind.Unmarshaller;

import java.util.ArrayList;
import java.util.stream.Collectors;
import com.google.gson.Gson;
import db.*;

@WebServlet("/*")
public class APIServlet extends HttpServlet {

	private String API_KEY = "3e670a86-8480-43f6-bcf4-045c4c26e9b0";

	protected void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String[] path = request.getPathInfo().split("/");
		// /order/{id}
		if (path.length > 2 && path[1].equalsIgnoreCase("order")) {
			String accept = request.getHeader("Accept");
			switch (accept) {
			case "application/json":
				DB db = DB.getInstance();
				response.setHeader("Content-Type", "application/json");
				Gson gson = new Gson();
				Order order = db.getObject(Integer.valueOf(path[2]));
				Links links;
				if (db.getObject(Integer.valueOf(path[2])) != null && !db.isLast((Integer.valueOf(path[2])))) {
					links = new Links(order,
							new Link[] { new Link("http://127.0.0.1:7001/tutorial3/order/" + order.getId(), "self"),
									new Link("http://127.0.0.1:7001/tutorial3/order/" + db.getNext(order.getId()).getId(), "next"),
									new Link("http://127.0.0.1:7001/tutorial3/order/" + order.getId(), "remove") });
					response.setStatus(200);
					response.getWriter().println(gson.toJson(links));
				} else if (db.getObject(Integer.valueOf(path[2])) != null && db.isLast(Integer.valueOf(path[2]))) {
					System.out.println("im in");
					links = new Links(order,
							new Link[] { new Link("http://127.0.0.1:7001/tutorial3/order/" + order.getId(), "self"),
									new Link("http://127.0.0.1:7001/tutorial3/order/" + order.getId(), "remove") });
					response.setStatus(200);
					response.getWriter().println(gson.toJson(links));
				} else {
					response.setStatus(400);
				}
				break;
			case "application/xml":
				DB dbxml = DB.getInstance();
				response.setHeader("Content-Type", "application/json");
				Order o = dbxml.getObject(Integer.valueOf(path[2]));
				Links linksxml;
				if (dbxml.getObject(Integer.valueOf(path[2]) + 1) != null) { // Check if last
					linksxml = new Links(o,
							new Link[] { new Link("http://127.0.0.1:7001/tutorial3/order/" + o.getId(), "self"),
									new Link("http://127.0.0.1:7001/tutorial3/order/" + dbxml.getNext(o.getId()).getId(), "next"),
									new Link("http://127.0.0.1:7001/tutorial3/order/" + o.getId(), "remove") });
					response.setStatus(200);
					response.getWriter().println(linksToXML(linksxml));
				} else if (dbxml.getObject(Integer.valueOf(path[2])) != null) {
					linksxml = new Links(o,
							new Link[] { new Link("http://127.0.0.1:7001/tutorial3/order/" + o.getId(), "self"),
									new Link("http://127.0.0.1:7001/tutorial3/order/" + o.getId(), "remove") });
					response.setStatus(200);
					response.getWriter().println(linksToXML(linksxml));
				} else {
					response.setStatus(400);
				}
				break;
			default:
				response.setStatus(400);
				break;
			}
		} else if (path.length > 1 && path[1].equalsIgnoreCase("order")) { // /order/
			DB db = DB.getInstance();
			response.setStatus(200);
			response.setHeader("Content-Type", "application/json");
			Gson gson = new Gson();
			ArrayList<Order> list = db.getAllObjects();
			ArrayList<Links> linksList = new ArrayList<>();
			for (Order order : list) {
				linksList.add(new Links(order,
						new Link[] { new Link("http://127.0.0.1:7001/tutorial3/order/", "list"),
								new Link("http://127.0.0.1:7001/tutorial3/order/", "add"),
								new Link("http://127.0.0.1:7001/tutorial3/order/" + order.getId(), "self") }));
			}
			response.getWriter().println(gson.toJson(linksList));
		} else {
			response.setStatus(400);
		}
	}

	private String linksToXML(Links links) {
		String xml;
		try {
			JAXBContext context = JAXBContext.newInstance(Links.class);
			Marshaller m = context.createMarshaller();
			m.setProperty(Marshaller.JAXB_FORMATTED_OUTPUT, true);

			StringWriter writer = new StringWriter();
			m.marshal(links, writer);
			xml = writer.toString();
		} catch (JAXBException e) {
			//e.printStackTrace();
			xml = null;
		}
		return xml;
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		String[] path = request.getPathInfo().split("/");
		String key = request.getParameter("api_key");

		if (path.length == 2 && path[1].equalsIgnoreCase("order") && (key != null && key.equalsIgnoreCase(API_KEY))) {
			switch (request.getHeader("Content-Type")) {
			case "application/json":
				String data = request.getReader().lines().collect(Collectors.joining());
				Gson gson = new Gson();
				Order c = gson.fromJson(data, Order.class);
				if (c.getCustomer() != null && c.getItem() != null) {
					DB db = DB.getInstance();
					c.setId(db.getAllObjects().size() + 1);
					db.addObject(c);
					Links links = new Links(c,
							new Link[] { new Link("http://127.0.0.1:7001/tutorial3/order/" + c.getId(), "self") });
					response.setStatus(201);
					response.getWriter().println(gson.toJson(links));
				} else {
					response.setStatus(400);
				}
				break;
			case "application/xml":
				// Parse XML
				String dataxml = request.getReader().lines().collect(Collectors.joining());
				try {
					JAXBContext context = JAXBContext.newInstance(Order.class);
					Unmarshaller unmarshaller = context.createUnmarshaller();

					Gson gsonxml = new Gson();
					java.io.StringReader reader = new java.io.StringReader(dataxml);
					Order o = (Order) unmarshaller.unmarshal(reader);
					if (o.getCustomer() != null && o.getItem() != null) {
						DB db = DB.getInstance();
						o.setId(db.getAllObjects().size() + 1);
						db.addObject(o);
						Links links = new Links(o,
								new Link[] { new Link("http://127.0.0.1:7001/tutorial3/order/" + o.getId(), "self") });
						response.setStatus(201);
						response.getWriter().println(gsonxml.toJson(links));
					} else {
						response.setStatus(400);
					}
				} catch (JAXBException e) {
					e.printStackTrace();
					response.setStatus(400);
				}
				break;
			default:
				response.setStatus(400);
				break;
			}
		} else {
			response.setStatus(400);
		}
	}

	protected void doDelete(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
		String[] path = req.getPathInfo().split("/");
		String key = req.getParameter("api_key");
		// /order/{id}
		if (path.length > 2 && path[1].equalsIgnoreCase("order") && (key != null && key.equalsIgnoreCase(API_KEY))) {
			DB db = DB.getInstance();
			resp.setHeader("Content-Type", "application/json");
			Gson gson = new Gson();
			if (db.getObject(Integer.valueOf(path[2])) != null) {
				db.removeObject(Integer.valueOf(path[2]));
				Links links = new Links(null,
						new Link[] { new Link("http://127.0.0.1:7001/tutorial3/order/", "list") });
				resp.getWriter().println(gson.toJson(links));
				resp.setStatus(200);
			} else {
				resp.setStatus(401);
			}

		} else {
			resp.setStatus(400);
		}
	}
}
