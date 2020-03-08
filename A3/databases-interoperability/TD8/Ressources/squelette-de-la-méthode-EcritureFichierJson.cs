using System;
using Newtonsoft.Json;
using System.IO;

/// <summary>
/// exercice 7: 
/// ecriture de fichier Json
/// </summary>
static void EcritureFichierJson()
{
	Console.WriteLine("ecriture de chats.json\n-----------------------");
	string monFichier = "chats.json";
	
	//informations sur les chats
	string[] nom = { "Bambou", "Taz", "Leloo" };
	string[] race = { "europeen", "europeen", "siamois" };
	string[] sexe = { "femelle", "male", "femelle" };
	string[] proprietaire = { "Jules", "Alain", "Luc" };

	//instanciation des "writer"
	// -----------
	// à compléter
	// -----------
	
	//debut du fichier Json
	jwriter.WriteStartObject();
	jwriter.WritePropertyName("chats");
	
	//ecriture du tableau Json
	// -----------
	// à compléter
	// -----------
	
	//fin du fichier Json
	jwriter.WriteEndObject();

	//fermeture des "writer"
	jwriter.Close();
	writer.Close();

	//relecture du fichier créé
	//-----------------------------
	Console.WriteLine("\nlecture des informations de chats.json");
	Console.WriteLine("--------------------------------------\n");
	AfficherPrettyJson("chats.json");
}


