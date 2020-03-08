package api;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

import db.*;

@XmlRootElement(name="hateoas")
public class Links {

    private Order order;
    private Link[] links;
 
    public Links(Order order, Link[] links) {
        this.order = order;
        this.links = links;
    }
    
    public Links() {
    	
    }
 
    public Order getOrder() {
        return order;
    }
 
    public void setOrder(Order order) {
        this.order = order;
    }

    @XmlElement(name="link")
    public Link[] getLinks() {
        return links;
    }
 
    public void setLinks(Link[] links) {
        this.links = links;
    }

}