
	public static void Exo2()
	{
		XmlDocument docXml = new XmlDocument();
		
		// création de l'en-tête XML (no <=> pas de DTD associée)
		docXml.CreateXmlDeclaration("1.0", "UTF-8", "no"); 
		
		XmlElement racine = docXml.CreateElement("a");
		docXml.AppendChild(racine);
		
		XmlElement autreBalise = docXml.CreateElement("b");
		autreBalise.InnerText = "coucou";
		racine.AppendChild(autreBalise);
		
		// enregistrement du document XML   ==> à retrouver dans le dossier bin\Debug de Visual Studio
		docXml.Save("test.xml");
	}