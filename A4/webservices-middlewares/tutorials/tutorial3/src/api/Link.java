package api;

import javax.xml.bind.annotation.XmlAttribute;

public class Link {

	@XmlAttribute
    private String href;
	@XmlAttribute
    private String rel;
 
    public Link(String href, String rel) {
        this.href = href;
        this.rel = rel;
    }
    
    public Link() {
    	
    }
 
    public String getHref() {
        return href;
    }
 
    public void setHref(String href) {
        this.href = href;
    }

    public String getLinks() {
        return rel;
    }
 
    public void setRel(String rel) {
        this.rel = rel;
    }

}