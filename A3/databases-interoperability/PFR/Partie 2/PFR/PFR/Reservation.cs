using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PFR
{
    class Reservation
    {
        private const string jsonfile = "AirBNPfinal.json";

        private bool confirme;
        private string id;
        private Sejour sejour;
        private User client;
        private Voiture voiture;
        private Appartement appartement;

        #region Accesseurs

        internal Sejour Sejour { get => sejour; set => sejour = value; }
        public string Id { get => id; set => id = value; }
        public bool Confirme { get => confirme; set => confirme = value; }
        internal Voiture Voiture { get => voiture; set => voiture = value; }
        internal Appartement Appartement { get => appartement; set => appartement = value; }

        #endregion

        #region Constructeurs

        public Reservation(User client, Voiture voiture, Appartement appartement, int arrondissement, int semaine)
        {
            id = GetNextLocationId();
            confirme = false;
            this.client = client;
            this.voiture = voiture;
            this.appartement = appartement;
            sejour = new Sejour(arrondissement, semaine);
        }

        public Reservation(string id, string immat)
        {
            this.id = id;
            voiture = RetrieveCarFromDatabase(immat);
        }

        public Reservation(string id, string immat, string flatHostId, string flatRoomId)
        {
            this.id = id;
            voiture = RetrieveCarFromDatabase(immat);
            appartement = RetrieveFlatFromJSON(flatHostId, flatRoomId);
        }

        #endregion

        /// <summary>
        /// Récupère un appartement dans le fichier JSON à l'aide du HostID et RoomID
        /// </summary>
        /// <param name="flatHostId">Id de l'hôte RBNP</param>
        /// <param name="flatRoomId">Id du logement RBNP</param>
        /// <returns>Appartement correspondant</returns>
        private Appartement RetrieveFlatFromJSON(string flatHostId, string flatRoomId)
        {
            // Récupération de l'appart avec JSONPath (sinon on désérialisera)
            try
            {
                int flatHostIdcast = Int32.Parse(flatHostId);
                int flatRoomIdcast = Int32.Parse(flatRoomId);
                JArray array = JArray.Parse(File.ReadAllText(jsonfile));
                JToken obj = array.Children<JObject>().FirstOrDefault(o => o.First != null
                                                                      && o.SelectToken("﻿host_id").Value<int>() == flatHostIdcast
                                                                      && o.SelectToken("room_id").Value<int>() == flatRoomIdcast);

                Appartement flat = obj.ToObject<Appartement>();

                return flat;

            } catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère une voiture dans la BDD depuis son immatriculation
        /// </summary>
        /// <param name="immat">Immatriculation du véhicule</param>
        /// <returns>Voiture correspondante</returns>
        private Voiture RetrieveCarFromDatabase(string immat)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Parameters.AddWithValue("@Immat", immat);
            commande.CommandText = "SELECT immat, id_controleur FROM voiture WHERE immat = @Immat;";

            Query query = new Query();
            query.Select(commande);
            string result = query.QueryResult;

            if (result != "")
            {
                String[] firstSplitted = result.Split('\n');
                String[] fSplitted = firstSplitted[0].Split('-');
                Voiture v = new Voiture(fSplitted[0], fSplitted[1], true);
                return v;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Crée l'id de location suivant
        /// Exemple : la dernière location était L003, alors la nouvelle aura pour id L004
        /// </summary>
        /// <returns>Nouvel identifiant</returns>
        public string GetNextLocationId()
        {
            MySqlCommand commande = new MySqlCommand();
            commande.CommandText = "SELECT id FROM location ORDER BY id DESC LIMIT 1;";

            Query query = new Query();
            query.Select(commande);
            string result = query.QueryResult;
            int nbId = 0;

            if (result != "")
            {
                String[] fSplitted = result.Split('-');
                String[] fSplitted2 = fSplitted[0].Split('L');
                Int32.TryParse(fSplitted2[1], out nbId);
            }
            result = "L" + (nbId + 1).ToString("000");
            //Console.WriteLine(result);
            query = null;
            return result;
        }

        /// <summary>
        /// Génère le message de confirmation de séjour XML (M2)
        /// </summary>
        public void EnvoiConfirmationSejour()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlDocumentType doctype = doc.CreateDocumentType("reservation", null, "m2.dtd", null);
            doc.AppendChild(doctype);

            XmlElement reservation = (XmlElement)doc.AppendChild(doc.CreateElement("reservation"));
            doc.InsertBefore(declaration, doctype);
            reservation.AppendChild(doc.CreateElement("noSejour")).InnerText = id;
            XmlElement client = (XmlElement)reservation.AppendChild(doc.CreateElement("client"));
            client.AppendChild(doc.CreateElement("nom")).InnerText = this.client.Nom;
            client.AppendChild(doc.CreateElement("codeC")).InnerText = this.client.Id;
            XmlElement sejour = (XmlElement)reservation.AppendChild(doc.CreateElement("sejour"));
            sejour.SetAttribute("statut", "non_confirmé");
            sejour.AppendChild(doc.CreateElement("theme")).InnerText = Sejour.Theme.ToString();
            sejour.AppendChild(doc.CreateElement("date")).InnerText = Sejour.Date.ToString();
            XmlElement parking = (XmlElement)reservation.AppendChild(doc.CreateElement("parking"));
            parking.AppendChild(doc.CreateElement("nom")).InnerText = voiture.NomParking;
            parking.AppendChild(doc.CreateElement("place")).InnerText = voiture.PlaceParking;
            parking.AppendChild(doc.CreateElement("immatriculation")).InnerText = voiture.Immatriculation;

            XmlElement appartement = (XmlElement)reservation.AppendChild(doc.CreateElement("appartement"));
            appartement.AppendChild(doc.CreateElement("host_id")).InnerText = this.appartement.HostId.ToString();
            appartement.AppendChild(doc.CreateElement("room_id")).InnerText = this.appartement.RoomId.ToString();
            appartement.AppendChild(doc.CreateElement("room_type")).InnerText = this.appartement.Room_type;
            appartement.AppendChild(doc.CreateElement("arrondissement")).InnerText = this.appartement.Arrondissement.ToString();
            appartement.AppendChild(doc.CreateElement("bedrooms")).InnerText = this.appartement.NbChambres.ToString();
            appartement.AppendChild(doc.CreateElement("price")).InnerText = this.appartement.Price.ToString();
            appartement.AppendChild(doc.CreateElement("accomodates")).InnerText = this.appartement.Accommodates.ToString();
            appartement.AppendChild(doc.CreateElement("overall_satisfaction")).InnerText = this.appartement.Evaluation.ToString();

            doc.Save("m2.xml");
            bool b = SaveReservationInDatabase();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (b) Console.WriteLine("\nRéservation non confirmée enregistrée.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Réalise la notation d'un séjour
        /// </summary>
        /// <param name="noLocation">Numéro de location (LXXX)</param>
        /// <param name="note">Note du client</param>
        /// <param name="commentaire">Commentaire du client</param>
        /// <returns>True si la notation s'est bien effectuée</returns>
        public bool Noter(string noLocation, double note, string commentaire)
        {
            bool success = false;
            if (note >= 0 && note <= 5 && commentaire.Length >= 0 && commentaire.Length <= 120)
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Parameters.AddWithValue("@Note", note);
                commande.Parameters.AddWithValue("@Commentaire", commentaire);
                commande.Parameters.AddWithValue("@NoLocation", noLocation);
                commande.CommandText = "UPDATE location SET note = @Note, appreciation = @Commentaire WHERE id = @NoLocation;";

                Query query = new Query();
                query.Update(commande);
                success = (query.Success);
                query = null;
            }
            return success;
        }

        /// <summary>
        /// Contrôle en fin de séjour
        /// </summary>
        /// <returns>Validation ou non si le contrôle s'est bien effectué</returns>
        public bool Controler()
        {
            return voiture.Controler();
        }

        /// <summary>
        /// Enregistre la réservation en BDD
        /// </summary>
        /// <returns>True si la réservation a bien été enregistrée</returns>
        private bool SaveReservationInDatabase()
        {
            bool success = false;
            MySqlCommand commande = new MySqlCommand();
            commande.Parameters.AddWithValue("@Id", this.id); // id de la loc Lxxx
            commande.Parameters.AddWithValue("@IdSejour", sejour.IdSejour); // id du séjour Sxxx
            commande.Parameters.AddWithValue("@Immat", voiture.Immatriculation);
            commande.Parameters.AddWithValue("@CodeC", client.Id);
            commande.Parameters.AddWithValue("@Confirme", false);
            commande.CommandText = "INSERT INTO location VALUES (@Id, @IdSejour, @Immat, @CodeC, NULL, NULL, @Confirme);";

            Query query = new Query();
            query.Insert(commande);
            success = (query.Success);
            query = null;
            return success;
        }

        /// <summary>
        /// Remise de la voiture à l'id parking et à la place de parking indiqués
        /// </summary>
        /// <param name="idParking">Id du parking indiqué par le client</param>
        /// <param name="placeParking">Place de parking indiquée par le client</param>
        /// <returns>True si la voiture a bien été rendue</returns>
        public bool RendreVoiture(string idParking, string placeParking)
        {
            bool success = false;
            Regex regexId = new Regex(@"^P\d{1,2}$");
            bool IdisValid = regexId.IsMatch(idParking);
            Regex regexPlace = new Regex(@"^A\d{1}$");
            bool PlaceisValid = regexPlace.IsMatch(placeParking);

            if (IdisValid && PlaceisValid)
            {
                int noParking = Int32.Parse(idParking.Replace("P", ""));
                int noPlace = Int32.Parse(placeParking.Replace("A", ""));
                if (noParking > 0 && noParking < 23 && noPlace > -1 && noPlace < 10)
                {
                    MySqlCommand commande = new MySqlCommand();
                    commande.Parameters.AddWithValue("@IdParking", idParking);
                    commande.Parameters.AddWithValue("@PlaceParking", placeParking);
                    commande.Parameters.AddWithValue("@Immat", voiture.Immatriculation);
                    commande.CommandText = "UPDATE voiture SET id_parking = @IdParking, place_parking = @PlaceParking WHERE immat = @Immat AND disponibilite = 0;";

                    Query query = new Query();
                    query.Update(commande);
                    success = (query.Success);
                    query = null;
                }
            }
            return success;
        }

        /// <summary>
        /// Réserve un logement en créant un message JSON
        /// normalement envoyé à l'API RBNP
        /// </summary>
        /// <returns>True si la réservation s'est bien effectuée</returns>
        public bool ReserverLogement()
        {
            try
            {
                string monFichier = "je5.json";

                //instanciation des "writer"
                StreamWriter writer = new StreamWriter(monFichier);
                JsonTextWriter jwriter = new JsonTextWriter(writer);

                //debut du fichier Json
                jwriter.WriteStartObject();

                jwriter.WritePropertyName("host_id");
                jwriter.WriteValue(appartement.HostId);
                jwriter.WritePropertyName("room_id");
                jwriter.WriteValue(appartement.RoomId);
                jwriter.WritePropertyName("week");
                jwriter.WriteValue(appartement.Week);
                jwriter.WritePropertyName("availability");
                jwriter.WriteValue("no");

                jwriter.WriteEndObject();

                //femeture de "writer"
                jwriter.Close();
                writer.Close();

                string jsonFormatted = JToken.Parse(File.ReadAllText(monFichier)).ToString(Newtonsoft.Json.Formatting.Indented);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Confirmation à l'API de AirBNP (fichier " + monFichier + "):\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(jsonFormatted + "\n");

                return true;
            } catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Réserve une voiture en BDD
        /// </summary>
        /// <returns>True si la réservation s'est bien effectuée</returns>
        public bool ReserverVoiture()
        {
            bool success = false;
            MySqlCommand commande = new MySqlCommand();
            commande.Parameters.AddWithValue("@Immat", voiture.Immatriculation);
            commande.Parameters.AddWithValue("@Motif", "Réservée");
            commande.CommandText = "UPDATE voiture SET disponibilite = 0, motif_indisponibilite = @Motif WHERE immat = @Immat;";

            Query query = new Query();
            query.Update(commande);
            success = (query.Success);
            query = null;
            return success;
        }

        /// <summary>
        /// Parse le fichier de confirmation M3 envoyé par le mail client
        /// </summary>
        /// <param name="fileName">Nom du fichier (M3)</param>
        /// <returns>Chaine de caractère concaténée avec les informations de location / appartement</returns>
        public string ReceptionConfirmationSejour(string fileName)
        {
            if (Escapade.XmlIsValid(fileName))
            {
                try
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    XPathDocument doc = new XPathDocument(fileName);
                    XPathNavigator nav = doc.CreateNavigator();
                    XPathExpression expr = nav.Compile("reservation");

                    XPathNodeIterator nodes = nav.Select(expr);
                    while (nodes.MoveNext())
                    {
                        nodes.Current.MoveToFirstChild();
                        string noSejour = nodes.Current.Value; 
                        stringBuilder.Append(noSejour).Append(";");

                        nodes.Current.MoveToNext();
                        string statutSejour = nodes.Current.GetAttribute("statut", "");
                        stringBuilder.Append(statutSejour).Append(";");

                        nodes.Current.MoveToNext();
                        nodes.Current.MoveToFirstChild();
                        string hostId = nodes.Current.Value;
                        nodes.Current.MoveToNext();
                        string roomId = nodes.Current.Value;
                        stringBuilder.Append(hostId).Append(";").Append(roomId);

                        //Console.WriteLine(stringBuilder.ToString());
                    }

                    String[] splitted = stringBuilder.ToString().Split(';');
                    if (splitted[1].Equals("confirmé"))
                    {
                        return stringBuilder.ToString();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }

        public override string ToString()
        {
            string conf = (confirme) ? "confirmée" : "non confirmée";
            return "Réservation " + conf + " au nom de " + client.Nom + " (" + client.Id + ").\n"
                + "Votre voiture : " + voiture.ToString() + "\n"
                + "Votre appartement : " + appartement.ToString() + "\n"
                + "Votre séjour vous revient donc à " + (voiture.PrixJ * 2 + appartement.Price) + " euros."; 
        }
    }
}
