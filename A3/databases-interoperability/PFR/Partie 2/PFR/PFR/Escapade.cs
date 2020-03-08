using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Schema;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;

namespace PFR
{
    class Escapade
    {
        private const string jsonfile = "AirBNPfinal.json";

        #region Constructeur

        public Escapade()
        {

        }

        #endregion

        /// <summary>
        /// Sélectionne les voitures disponibles dans le parking d'un arrondissement donné
        /// </summary>
        /// <param name="noArrondissement">Arrondissement</param>
        /// <returns>Liste de voitures disponibles</returns>
        public List<Voiture> SelectAvailableCars(int noArrondissement)
        {
            MySqlCommand commande = new MySqlCommand();
            // Empêche l'injection SQL 
            commande.Parameters.AddWithValue("@Arrondissement", "%" + noArrondissement);
            commande.CommandText = "SELECT immat, marque, modele, id_controleur, categorie, places, p.nom, place_parking, prixJ FROM voiture v INNER JOIN parking p ON v.id_parking = p.id WHERE id_parking LIKE @Arrondissement AND disponibilite = 1;";

            Query query = new Query();
            query.Select(commande);

            if (query.QueryResult != "" && query.QueryResult != null)
            {
                //List de List
                List<List<string>> data = new List<List<string>>();
                List<Voiture> voituresDispo = new List<Voiture>();
                // tant qu'il y a des \n on cut
                String[] splittedFirst = query.QueryResult.Split('\n');
                for (int i = 0; i < splittedFirst.Length; i++)
                {
                    if (splittedFirst[i] != "")
                    {
                        List<string> splittedSecond = splittedFirst[i].Split('-').ToList();
                        Voiture v = new Voiture(splittedSecond.ElementAt(0), splittedSecond.ElementAt(1), splittedSecond.ElementAt(2),
                            splittedSecond.ElementAt(3), splittedSecond.ElementAt(4), Int32.Parse(splittedSecond.ElementAt(5)), splittedSecond.ElementAt(6), splittedSecond.ElementAt(7), Int32.Parse(splittedSecond.ElementAt(8)));
                        voituresDispo.Add(v);
                        data.Add(splittedSecond);
                    }
                }

                return voituresDispo;
            }

            query = null;

            return null;
        }

        /// <summary>
        /// Réalise le Checkout d'un client
        /// </summary>
        /// <param name="noSejour">Numéro de location (LXXX)</param>
        /// <param name="idParking">Id du parking où la voiture a été laissée</param>
        /// <param name="placeParking">Place de parking où la voiture a été laissée</param>
        /// <param name="note">Note du séjour (0 à 5)</param>
        /// <param name="commentaire">Commentaire du client</param>
        public void CheckOut(string noSejour, string idParking, string placeParking, double note, string commentaire)
        {
            Reservation reservation = GetReservation(noSejour);
            if (reservation != null)
            {
                bool problem = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("========== Remise de la voiture au parking " + idParking + ", place " + placeParking + " ==========\n");
                if (reservation.RendreVoiture(idParking, placeParking))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("La voiture a bien été rendue.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Une erreur est survenue lors de la remise au parking. Merci de vérifier que vous avez entré correctement le parking / la place de la voiture.");
                    problem = true;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n========== Notation du séjour ==========\n");
                if (reservation.Noter(noSejour, note, commentaire))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Votre note ainsi que votre commentaire ont bien été pris en compte. Merci et à bientôt !");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Une erreur est survenue lors de la notation du séjour. Merci de vérifier que vous avez entré une note entre 0 et 5 et un commentaire de moins de 120 caractères.");
                    problem = true;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n========== Controle de la voiture ==========\n");
                if (reservation.Controler())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Contrôle et intervention effectués correctement.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Une erreur est survenue lors du contrôle de la voiture.");
                    problem = true;
                }
                if (problem)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nMerci de réitérer votre checkout.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Réservation introuvable. Merci de vérifier que vous avez entré le bon numéro de location.");

            }
        }

        /// <summary>
        /// Récupère une réservation à partir d'un numéro de location
        /// </summary>
        /// <param name="noLocation">Numéro de location (LXXX)</param>
        /// <returns>Réservation si elle est trouvée</returns>
        public Reservation GetReservation(string noLocation)
        {
            // Récupération de la voiture

            MySqlCommand commande = new MySqlCommand();

            commande.Parameters.AddWithValue("@NoLocation", noLocation);
            commande.CommandText = "SELECT id, immat FROM location WHERE id = @NoLocation LIMIT 1";

            Query query = new Query();
            query.Select(commande);

            if (query.QueryResult != "" && query.QueryResult != null)
            {
                String[] splitted = query.QueryResult.Split('\n'); // Liste des places prises
                List<String> result = splitted[0].Split('-').ToList();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Réservation trouvée au n°" + noLocation);
                Console.ForegroundColor = ConsoleColor.White;
                Reservation r = new Reservation(noLocation, result.ElementAt(1));
                query = null;
                return r;
            }

            query = null;
            return null;
        }

        /// <summary>
        /// Récupère une réservation à partir d'un numéro de location et des données d'un appartement
        /// </summary>
        /// <param name="noLocation">Numéro de location (LXXX)</param>
        /// <param name="hostId">Id hôte RBNP</param>
        /// <param name="roomId">Id logement RBNP</param>
        /// <returns>Réservation si elle est trouvée</returns>
        public Reservation GetReservation(string noLocation, string hostId, string roomId)
        {
            // Récupération de la voiture

            MySqlCommand commande = new MySqlCommand();
            string queryResult = "";
            commande.Parameters.AddWithValue("@NoLocation", noLocation);
            commande.CommandText = "SELECT id, immat FROM location WHERE id = @NoLocation LIMIT 1";

            Query query = new Query();
            query.Select(commande);

            if (query.QueryResult != "" && query.QueryResult != null)
            {
                String[] splitted = query.QueryResult.Split('\n'); // Liste des places prises
                List<String> result = splitted[0].Split('-').ToList();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Réservation trouvée au n°" + noLocation);
                Console.ForegroundColor = ConsoleColor.White;
                queryResult = result.ElementAt(1);
                query = null;
            }

            query = null;

            Reservation r = new Reservation(noLocation, queryResult, hostId, roomId);

            return r;
        }

        /// <summary>
        /// Sélectionne une voiture aléatoire à partir d'une liste de voitures
        /// </summary>
        /// <param name="voituresDispo">Liste de voitures disponibles</param>
        /// <returns>Une voiture choisie au hasard</returns>
        public Voiture GetRandomCar(List<Voiture> voituresDispo)
        {
            if (voituresDispo != null && voituresDispo.Count > 0)
            {
                Random rand = new Random();
                return voituresDispo.ElementAt(rand.Next(0, voituresDispo.Count));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Déplace une voiture dans un arrondissement renseigné
        /// </summary>
        /// <param name="nouvelArrondissement">Nouvel arrondissement où déplacer la voiture</param>
        private void MoveCar(int nouvelArrondissement)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.CommandText = "SELECT immat FROM voiture WHERE disponibilite IS TRUE ORDER BY RAND() LIMIT 1"; // Trouve une voiture dispo aléatoirement

            Query query = new Query();
            query.Select(commande);

            if (query.QueryResult != "" && query.QueryResult != null) // Il y a au moins une voiture dispo
            {
                String[] immat = query.QueryResult.Split('-'); // Immatriculation de la voiture

                commande = new MySqlCommand();
                commande.Parameters.AddWithValue("@NouvelArrondissement", "%" + nouvelArrondissement);
                commande.CommandText = "SELECT id FROM parking WHERE code_postal LIKE @NouvelArrondissement";
                query = new Query();
                query.Select(commande);
                String[] idParking = query.QueryResult.Split('-'); // Id du nouveau parking de la voiture

                commande = new MySqlCommand();
                query = new Query();
                commande.Parameters.AddWithValue("@Immat", immat[0]);
                commande.Parameters.AddWithValue("@IdParking", idParking[0]);
                commande.Parameters.AddWithValue("@PlaceParking", "A0");
                commande.CommandText = "UPDATE voiture SET id_parking = @IdParking, place_parking = @PlaceParking WHERE immat = @Immat;";
                query.Update(commande);
            }

            query = null;
        }

        /// <summary>
        /// Retourne l'identifiant d'un client s'il existe dans la BDD, null sinon
        /// </summary>
        /// <param name="nomClient">Nom du client</param>
        /// <returns>Identifiant du cliant ou NULL sinon</returns>
        public string SearchUserId(string nomClient)
        {
            MySqlCommand commande = new MySqlCommand();
            // Empêche l'injection SQL
            commande.Parameters.AddWithValue("@NomClient", nomClient);
            commande.CommandText = "SELECT codeC, prenom, nom FROM client WHERE nom = @NomClient";

            Query query = new Query();
            query.Select(commande);

            if (query.QueryResult != "" && query.QueryResult != null)
            {
                String[] splitted = query.QueryResult.Split('-');
                return splitted[0];
            }

            query = null;

            return null;
        }

        /// <summary>
        /// Récupère un utilisateur existant depuis la BDD
        /// </summary>
        /// <param name="codeC">Identifiant client</param>
        /// <returns>Utilisateur</returns>
        public User GetUser(string codeC)
        {
            User client = null;
            MySqlCommand commande = new MySqlCommand();
            commande.Parameters.AddWithValue("@CodeClient", codeC);
            commande.CommandText = "SELECT codeC, prenom, nom FROM client WHERE codeC = @CodeClient";

            Query query = new Query();
            query.Select(commande);

            if (query.QueryResult != "" && query.QueryResult != null)
            {
                String[] splitted = query.QueryResult.Split('-');
                if (splitted.Length == 4)
                {
                    client = new User(splitted[2], splitted[1], splitted[0]);
                }
            }

            query = null;

            return client;
        }

        /// <summary>
        /// Crée un client dans la BDD
        /// </summary>
        /// <param name="nom">Nom du client</param>
        /// <returns>Utilisateur</returns>
        public User CreateUser(string nom)
        {
            User nouveauClient = new User(nom);
            if (nouveauClient.AddToDataBase())
            {
                Console.WriteLine("Nouvel utilisateur créé : " + nouveauClient.ToString());
            }
            return nouveauClient;
        }

        /// <summary>
        /// Parse le fichier de demande de séjour (M1)
        /// </summary>
        /// <param name="fileName">Nom du fichier</param>
        /// <returns>Informations du client et du séjour sous forme d'un stringbuilder</returns>
        private string ReceptionDemandeSejour(string fileName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            XPathDocument doc = new XPathDocument(fileName);
            XPathNavigator nav = doc.CreateNavigator();
            XPathExpression expr = nav.Compile("reservation");

            XPathNodeIterator nodes = nav.Select(expr);
            while (nodes.MoveNext())
            {
                nodes.Current.MoveToFirstChild(); // On rentre à l'intérieur
                string client = nodes.Current.Value; // nom du client
                stringBuilder.Append(client).Append(";");

                nodes.Current.MoveToNext();
                nodes.Current.MoveToFirstChild();
                string ville = nodes.Current.Value;
                stringBuilder.Append(ville).Append(";");

                nodes.Current.MoveToParent();
                nodes.Current.MoveToNext();
                string date = nodes.Current.Value;
                stringBuilder.Append(date).Append(";");

                nodes.Current.MoveToNext();
                string sejour = nodes.Current.Value;
                stringBuilder.Append(sejour).Append(";");

                //Console.WriteLine(stringBuilder.ToString());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Crée une réservation (en local) puis en BDD en mode "non confirmé" et génère un fichier XML (M2)
        /// de résumé des informations concernant le séjour.
        /// </summary>
        /// <param name="client">Client</param>
        /// <param name="voituresDispo">liste de voitures disponibles</param>
        /// <param name="appartementsDispo">Liste d'appartements disponibles</param>
        /// <param name="arrondissement">Arrondissement du séjour</param>
        /// <param name="semaine">Semaine du séjour</param>
        /// <returns>Réservation</returns>
        private Reservation CreerReservation(User client, List<Voiture> voituresDispo, List<Appartement> appartementsDispo, int arrondissement, int semaine)
        {
            Voiture voitureReservee = GetRandomCar(voituresDispo);
            Appartement appartement = GetRandomFlat(appartementsDispo);

            Reservation reservation = new Reservation(client, voitureReservee, appartement, arrondissement, semaine);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("N° de réservation : ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(reservation.Id + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(reservation.ToString());

            reservation.EnvoiConfirmationSejour();
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("========== La création de séjour (non confirmé) (M2 => m2.xml) a été réalisée avec succès ==========");
                Console.ForegroundColor = ConsoleColor.White;
                Program.AfficheContenuXML("m2.xml");
                XmlIsValid("m2.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return reservation;
        }

        /// <summary>
        /// Vérifie si le fichier XML est conforme à sa DTD
        /// </summary>
        /// <param name="xmlFile">Nom du fichier XML</param>
        /// <returns>Validation ou non</returns>
        public static bool XmlIsValid(string xmlFile)
        {
            var messages = new StringBuilder();
            var settings = new XmlReaderSettings { ValidationType = ValidationType.DTD, DtdProcessing = DtdProcessing.Parse, XmlResolver = new XmlUrlResolver() };
            settings.ValidationEventHandler += (sender, args) => messages.AppendLine(args.Message);
            var reader = XmlReader.Create(xmlFile, settings);

            try
            {
                while (reader.Read()) { }
            }
            catch (XmlException e)
            {
                messages.AppendLine(e.Message.ToString());
            }

            if (messages.Length > 0)
            {
                Console.WriteLine("Le document " + xmlFile + " n'est pas valide conformément à sa DTD." + messages);
                return false;
            }
            else
            {
                Console.WriteLine("Le document " + xmlFile + " est valide conformément à sa DTD.");
                return true;
            }

        }

        /// <summary>
        /// Réalise une demande de séjour complète
        /// </summary>
        /// <param name="filename">Nom du fichier XML (M1)</param>
        public void DemandeSejour(string filename)
        {
            bool reservationPossible = true;
            bool validXmlDocument = XmlIsValid(filename);

            if (validXmlDocument)
            {
                string xml = ReceptionDemandeSejour(filename);

                if (xml != null && xml != "")
                {
                    string[] formattedXml = xml.Split(';');
                    string nom = formattedXml[0];
                    int semaine = Int32.Parse(formattedXml[2]);
                    int arrondissement = Int32.Parse(formattedXml[3]);

                    string identifiantClient = SearchUserId(nom);

                    if (identifiantClient != null && identifiantClient != "")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Client existant dans la base (" + identifiantClient + ")");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else // On crée le nouveau client
                    {
                        User newUser = CreateUser(nom);
                        identifiantClient = SearchUserId(newUser.Nom);
                    }

                    /* ============================================================================
                    *
                    * ON RECUPERE LES VOITURES DISPO DANS LE PARKING, SI AUCUNE N'EST DISPO
                    *               ON DEPLACE UNE VOITURE D'UN AUTRE PARKING
                    * SI CE N'EST TOUJOURS PAS POSSIBLE, LA RESERVATION EST IMPOSSIBLE A REALISER
                    * 
                    ==============================================================================*/

                    List<Voiture> voituresDispo = SelectAvailableCars(arrondissement);
                    if (voituresDispo == null || voituresDispo.Count <= 0)
                    {
                        MoveCar(arrondissement);
                        voituresDispo = SelectAvailableCars(arrondissement);
                        if (voituresDispo == null) // Le séjour n'est pas possible
                        {
                            reservationPossible = false;
                        }
                    }

                    // get appartements in json
                    List<Appartement> appartementsDispo = SelectAvailableFlats(jsonfile, arrondissement, 1, 4.5);
                    if (appartementsDispo == null || appartementsDispo.Count <= 0)
                    {
                        reservationPossible = false;
                    }

                    // Si la réservation est possible, alors on la réalise (en mode "non confirmé")
                    if (reservationPossible)
                    {
                        Reservation reservation = CreerReservation(GetUser(identifiantClient), voituresDispo, appartementsDispo, arrondissement, semaine);

                        // ETAPE DE CONFIRMATION
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n========== Le message de confirmation (M3 => m3.xml) est envoyé à ce moment ==========\n");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Vous devez manuellement modifier les champs du fichier à l'aide d'un éditeur de texte en y mettant les informations nécéssaires affichées ci dessus dans le message m2.xml," +
                            " et ensuite appuyer sur Entrée.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();

                        Program.AfficheContenuXML("m3.xml");
                        string resStr = reservation.ReceptionConfirmationSejour("m3.xml");
                        string noLoc = "";
                        try
                        {
                            String[] spl = resStr.Split(';');
                            noLoc = spl[0];
                        }
                        catch (Exception e)
                        {

                        }

                        if (!AlreadyConfirmed(noLoc))
                        {
                            if (!ConfirmerReservation(resStr))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Impossible de confirmer la réservation. Merci de recommencer plus tard.");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Votre réservation a déjà été confirmée.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("La réservation n'est pas possible. Aucune voiture / appartement n'est actuellement disponible. Merci de réessayer plus tard.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        /// <summary>
        /// Vérifie si une réservation a déjà été confirmée
        /// </summary>
        /// <param name="noLoc">Numéro de location (LXXX)</param>
        /// <returns>Validation ou non</returns>
        private bool AlreadyConfirmed(string noLoc)
        {
            MySqlCommand commande = new MySqlCommand();
            // Empêche l'injection SQL
            commande.Parameters.AddWithValue("@noLoc", noLoc);
            commande.CommandText = "SELECT confirme FROM location WHERE id = @noLoc";

            Query query = new Query();
            query.Select(commande);

            if (query.QueryResult != "" && query.QueryResult != null)
            {
                String[] splitted = query.QueryResult.Split('-');
                return (splitted[0].Equals("True")) ? true : false;
            }
            return false;
        }

        /// <summary>
        /// Confirme une réservation à partir d'une chaine de caractère formatée
        /// </summary>
        /// <param name="reservationStr">Chaine de caractère formatée comprenant les informations de M3</param>
        /// <returns>Validation ou non de si la réservation a bien été confirmée</returns>
        public bool ConfirmerReservation(string reservationStr)
        {
            if (reservationStr != null)
            {
                String[] splittedInfos = reservationStr.Split(';');
                if (splittedInfos.Length == 4)
                {
                    string noLoc = splittedInfos[0];
                    string status = splittedInfos[1];
                    string appartmentHostId = splittedInfos[2];
                    string appartmentRoomId = splittedInfos[3];

                    if (status.Equals("confirmé"))
                    {

                        Reservation res = GetReservation(noLoc, appartmentHostId, appartmentRoomId);
                        if (res != null && res.Appartement != null && res.Voiture != null)
                        {
                            res.Confirme = true;
                            bool b = res.ReserverVoiture();
                            bool a = res.ReserverLogement();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            if (a && b) Console.WriteLine("Réservation confirmée. Votre voiture et appartement sont réservés.");
                            Console.ForegroundColor = ConsoleColor.White;



                            bool success = false;
                            MySqlCommand commande = new MySqlCommand();
                            commande.Parameters.AddWithValue("@Immat", res.Voiture.Immatriculation);
                            commande.Parameters.AddWithValue("@Id", res.Id);
                            commande.CommandText = "UPDATE location SET confirme = 1 WHERE immat = @Immat AND id = @Id;";

                            res.Voiture.DoitEtreNettoyee = true;

                            Query query = new Query();
                            query.Update(commande);
                            success = (query.Success);
                            query = null;
                            return success;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Récupère un appartement aléatoire parmi une liste d'appartements disponibles
        /// </summary>
        /// <param name="appartementsDispo">Liste d'appartements disponibles</param>
        /// <returns>Appartemnt choisi au hasard</returns>
        private Appartement GetRandomFlat(List<Appartement> appartementsDispo)
        {
            if (appartementsDispo != null && appartementsDispo.Count > 0)
            {
                Random rand = new Random();
                return appartementsDispo.ElementAt(rand.Next(0, appartementsDispo.Count));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sélectionne les voitures disponibles correspondant aux critères du client
        /// </summary>
        /// <param name="nomFichier">Nom du fichier JSON</param>
        /// <param name="noArrondissement">Numéro de l'arrondissement désiré</param>
        /// <param name="nbChambres">Nombre de chambres désiré</param>
        /// <param name="note">Note minimale désirée</param>
        /// <returns></returns>
        private List<Appartement> SelectAvailableFlats(string nomFichier, int noArrondissement, int nbChambres, double note)
        {
            try
            {
                List<Appartement> allAppartements = (List<Appartement>)JsonConvert.DeserializeObject(File.ReadAllText(nomFichier), typeof(List<Appartement>));
                List<Appartement> appartementsDispo = allAppartements.Where(a => a.Disponible)
                                                                     .Where(a => a.Arrondissement == noArrondissement)
                                                                     .Where(a => a.NbChambres == nbChambres)
                                                                     .Where(a => a.Evaluation >= note)
                                                                     .ToList();
                return appartementsDispo;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
