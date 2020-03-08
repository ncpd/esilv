using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Zombillenium
{
    class Administration
    {
        private List<Attraction> attractions;
        private List<Personnel> toutLePersonnel;

        // Fichier d'origine
        private const string FICHIER_CSV = "Listing.csv";
        // Fichier d'export
        private const string csv_export = "csv_export.csv";

        #region Constructeur

        public Administration()
        {
            attractions = new List<Attraction>();
            toutLePersonnel = new List<Personnel>();
        }

        #endregion

        #region Lecture & Écriture dans un fichier CSV

        /// <summary>
        /// Lit différents types d'attractions depuis un fichier CSV
        /// </summary>
        /// <param name="nomFichier">Nom du fichier CSV à charger</param>
        private void ReadAttractionsFromCSV(string nomFichier)
        {
            try
            {
                StreamReader streamReader = new StreamReader(nomFichier, true);
                while (streamReader.Peek() > 0)
                {
                    String ligne = streamReader.ReadLine();
                    String[] ligneSplitted = ligne.Split(';');
                    switch (ligneSplitted[0])
                    {
                        case "Boutique":
                            Boutique boutique = new Boutique(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], Int32.Parse(ligneSplitted[3]),
                                Boolean.Parse(ligneSplitted[4]), ligneSplitted[5], ligneSplitted[6]);
                            attractions.Add(boutique);
                            break;
                        case "DarkRide":
                            DarkRide darkRide = new DarkRide(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], Int32.Parse(ligneSplitted[3]),
                                Boolean.Parse(ligneSplitted[4]), ligneSplitted[5], ligneSplitted[6], Boolean.Parse(ligneSplitted[7]));
                            attractions.Add(darkRide);
                            break;
                        case "RollerCoaster":
                            RollerCoaster rollerCoaster = new RollerCoaster(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], Int32.Parse(ligneSplitted[3]),
                                Boolean.Parse(ligneSplitted[4]), ligneSplitted[5], ligneSplitted[6], Int32.Parse(ligneSplitted[7]), Double.Parse(ligneSplitted[8]));
                            attractions.Add(rollerCoaster);
                            break;
                        case "Spectacle":
                            Spectacle spectacle = new Spectacle(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], Int32.Parse(ligneSplitted[3]),
                                Boolean.Parse(ligneSplitted[4]), ligneSplitted[5], ligneSplitted[6], Int32.Parse(ligneSplitted[7]), ligneSplitted[8]);
                            attractions.Add(spectacle);
                            break;
                        default:
                            break;
                    }
                }
                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur est survenue lors de la lecture du fichier.\n Code : " + e.ToString());
            }
        }

        /// <summary>
        /// Lit différents types d'employés depuis un fichier CSV
        /// </summary>
        /// <param name="nomFichier">Nom du fichier CSV à charger</param>
        private void ReadEmployesFromCSV(string nomFichier)
        {
            try
            {
                StreamReader streamReader = new StreamReader(nomFichier, true);
                while (streamReader.Peek() > 0)
                {
                    List<String> nonMonstres = new List<String>(new[] { "Sorcier", "Boutique", "DarkRide", "RollerCoaster", "Spectacle" });
                    String ligne = streamReader.ReadLine();
                    String[] ligneSplitted = ligne.Split(';');
                    Attraction affectation = null;
                    String affectationStr = null;
                    Boolean affecteAutrePart = false;
                    if (!nonMonstres.Contains(ligneSplitted[0])) // Alors le type est forcément un monstre ou une déclinaison
                    {
                        if (ligneSplitted[7] == "parc" || ligneSplitted[7] == "neant") // affectation autre : parc / ne peut pas etre affecté
                        {
                            affecteAutrePart = true;
                            affectationStr = ligneSplitted[7];
                        }
                        else if (ligneSplitted[7] != "") // C'est un id d'attraction
                        {
                            affectation = DoesAttractionExists(ligneSplitted[7]);
                        }
                        else // C'est un "", rien à changer, les 2 sont null
                        {

                        }
                    }
                    switch (ligneSplitted[0])
                    {
                        case "Sorcier":
                            String[] pouvoirs = ligneSplitted[7].Split('-');
                            List<string> spells = new List<string>();
                            for (int i = 0; i < pouvoirs.Length; i++)
                            {
                                spells.Add(pouvoirs[i]);
                            }
                            Sorcier sorcier = new Sorcier(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                ligneSplitted[4], ligneSplitted[5], ligneSplitted[6], spells);
                            toutLePersonnel.Add(sorcier);
                            break;
                        case "Monstre":
                            // Affecté au parc / néant : constructeur avec une affectation de type string
                            if (affecteAutrePart)
                            {
                                Monstre monstreAutrePart = new Monstre(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectationStr);
                                toutLePersonnel.Add(monstreAutrePart);
                            }
                            // Non affecté au parc : constructeur avec une affectation de type Attraction (null si "" du coup)
                            else
                            {
                                Monstre monstre = new Monstre(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectation);
                                toutLePersonnel.Add(monstre);
                            }
                            break;
                        case "Demon":
                            if (affecteAutrePart)
                            {
                                Demon demonAutrePart = new Demon(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectationStr, Int32.Parse(ligneSplitted[8]));
                                toutLePersonnel.Add(demonAutrePart);
                            }
                            else
                            {
                                Demon demon = new Demon(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectation, Int32.Parse(ligneSplitted[8]));
                                toutLePersonnel.Add(demon);
                            }
                            break;
                        case "Fantome":
                            if (affecteAutrePart)
                            {
                                Fantome fantomeParc = new Fantome(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectationStr);
                                toutLePersonnel.Add(fantomeParc);
                            }
                            else
                            {
                                Fantome fantome = new Fantome(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectation);
                                toutLePersonnel.Add(fantome);
                            }
                            break;
                        case "LoupGarou":
                            if (affecteAutrePart)
                            {
                                LoupGarou loupGarouParc = new LoupGarou(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectationStr, Double.Parse(ligneSplitted[8]));
                                toutLePersonnel.Add(loupGarouParc);
                            }
                            else
                            {
                                LoupGarou loupGarou = new LoupGarou(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectation, Double.Parse(ligneSplitted[8]));
                                toutLePersonnel.Add(loupGarou);
                            }
                            break;
                        case "Vampire":
                            if (affecteAutrePart)
                            {
                                Vampire vampireParc = new Vampire(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectationStr, float.Parse(ligneSplitted[8]));
                                toutLePersonnel.Add(vampireParc);
                            }
                            else
                            {
                                Vampire vampire = new Vampire(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectation, float.Parse(ligneSplitted[8]));
                                toutLePersonnel.Add(vampire);
                            }
                            break;
                        case "Zombie":
                            if (affecteAutrePart)
                            {
                                Zombie zombieParc = new Zombie(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectationStr, ligneSplitted[8], Int32.Parse(ligneSplitted[9]));
                                toutLePersonnel.Add(zombieParc);
                            }
                            else
                            {
                                Zombie zombie = new Zombie(Int32.Parse(ligneSplitted[1]), ligneSplitted[2], ligneSplitted[3],
                                    ligneSplitted[4], ligneSplitted[5], Int32.Parse(ligneSplitted[6]), affectation, ligneSplitted[8], Int32.Parse(ligneSplitted[9]));
                                toutLePersonnel.Add(zombie);
                            }
                            break;
                        default:
                            break;
                    }
                }
                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur est survenue lors de la lecture du fichier.\n Code : " + e.ToString());
            }
        }

        /// <summary>
        /// Lit les informations d'un fichier CSV
        /// </summary>
        /// <param name="nomFichier">Nom du fichier CSV à charger</param>
        public void ReadCSV(string nomFichier)
        {
            ReadAttractionsFromCSV(nomFichier);
            ReadEmployesFromCSV(nomFichier);
        }

        /// <summary>
        /// Extrait les données de listes de Personnel / Attraction vers un fichier CSV
        /// </summary>
        /// <param name="nomFichier">Nom du fichier CSV qui sera exporté</param>
        public void ExtractListToCSV(string nomFichier, List<Personnel> personnel, List<Attraction> attractions)
        {
            if (File.Exists(nomFichier))
            {
                File.WriteAllText(nomFichier, String.Empty);
                // Clean si jamais le fichier existe déjà plutot que de réécrire par dessus
            }

            StreamWriter sw = File.AppendText(nomFichier);
            if (personnel != null)
            {
                foreach (Personnel p in personnel)
                {
                    if (p is Sorcier)
                    {
                        Sorcier wizard = (Sorcier)p;
                        sw.WriteLine(wizard.GetCSVline());
                    }
                    else if (p is Demon)
                    {
                        Demon demon = (Demon)p;
                        sw.WriteLine(demon.GetCSVline());
                    }
                    else if (p is Fantome)
                    {
                        Fantome fantome = (Fantome)p;
                        sw.WriteLine(fantome.GetCSVline());
                    }
                    else if (p is LoupGarou)
                    {
                        LoupGarou loupGarou = (LoupGarou)p;
                        sw.WriteLine(loupGarou.GetCSVline());
                    }
                    else if (p is Vampire)
                    {
                        Vampire vampire = (Vampire)p;
                        sw.WriteLine(vampire.GetCSVline());
                    }
                    else if (p is Zombie)
                    {
                        Zombie zombie = (Zombie)p;
                        sw.WriteLine(zombie.GetCSVline());
                    }
                    else if (p is Monstre)
                    {
                        Monstre monstre = (Monstre)p;
                        sw.WriteLine(monstre.GetCSVline());
                    }
                }
            }
            if (attractions != null)
            {
                foreach (Attraction a in attractions)
                {
                    if (a is Boutique)
                    {
                        Boutique boutique = (Boutique)a;
                        sw.WriteLine(boutique.GetCSVline());
                    }
                    else if (a is DarkRide)
                    {
                        DarkRide darkRide = (DarkRide)a;
                        sw.WriteLine(darkRide.GetCSVline());
                    }
                    else if (a is RollerCoaster)
                    {
                        RollerCoaster rollerCoaster = (RollerCoaster)a;
                        sw.WriteLine(rollerCoaster.GetCSVline());
                    }
                    else if (a is Spectacle)
                    {
                        Spectacle spectacle = (Spectacle)a;
                        sw.WriteLine(spectacle.GetCSVline());
                    }
                }
            }
            sw.Close();
        }

        #endregion

        #region Vérification de l'existence d'une attraction / d'un employé

        /// <summary>
        /// Vérifie si une attraction existe en la cherchant par son identifiant
        /// </summary>
        /// <param name="id">Identifiant de l'attraction</param>
        /// <returns>Attraction si elle est trouvée, null sinon</returns>
        public Attraction DoesAttractionExists(string id)
        {
            foreach (Attraction a in attractions)
            {
                if (id != "")
                {
                    if (Int32.Parse(id) == a.Identifiant)
                    {
                        return a;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Vérifie si un employé existe en le cherchant par son matricule
        /// </summary>
        /// <param name="matricule">Matricule de l'employé</param>
        /// <returns>Personnel si il est trouvé, null sinon</returns>
        public Personnel DoesEmployeExists(int matricule)
        {
            foreach (Personnel p in toutLePersonnel)
            {
                if (matricule == p.Matricule)
                {
                    return p;
                }
            }
            return null;
        }

        #endregion

        #region Modification du personnel / des attractions

        /// <summary>
        /// Vérifie la cagnotte actuelle pour savoir si l'employé doit être réaffecté au parc / à un stand barbe à papa
        /// </summary>
        /// <param name="matricule">Matricule de l'employé</param>
        public void CheckCagnotte(int matricule)
        {
            Monstre m = (Monstre)DoesEmployeExists(matricule);
            if (m != null)
            {
                if (m.Cagnotte < 50)
                {
                    if (SendToBAP(m))
                    {
                        Console.WriteLine("Le monstre a bien été envoyé au stand barbe à papa");
                    }
                    else
                    {
                        Console.WriteLine("Punition à la barbe à papa impossible");
                    }
                }
                else if (m.Cagnotte > 500 && (DoesEmployeExists(matricule).GetType().Equals(typeof(Zombie)) || (DoesEmployeExists(matricule).GetType().Equals(typeof(Demon)))))
                {
                    // Affectation du monstre au parc à faire peur au visiteurs
                    m.Affectation = null;
                    m.AffectationAutre = "parc";
                }
            }
        }

        /// <summary>
        /// Ajoute un employé au personnel
        /// </summary>
        /// <param name="nouvelleRecrue">Nouvel employé</param>
        public void Recruter(Personnel nouvelleRecrue)
        {
            toutLePersonnel.Add(nouvelleRecrue);
        }

        /// <summary>
        /// Supprime un employé du personnel
        /// </summary>
        /// <param name="matriculeEmployeLicencie">Matricule de l'employé à licencier</param>
        public void Licencier(int matriculeEmployeLicencie)
        {
            Personnel employeLicencie = DoesEmployeExists(matriculeEmployeLicencie);
            if (employeLicencie != null)
            {
                toutLePersonnel.Remove(employeLicencie);
            }
        }

        /// <summary>
        /// Supprime les employés d'une liste de personnel
        /// </summary>
        /// <param name="personnelALicencier">Liste de personnel à licencier</param>
        public void LicencierFromListe(List<Personnel> personnelALicencier)
        {
            if (personnelALicencier != null && personnelALicencier.Count > 0)
            {
                foreach (Personnel p in personnelALicencier)
                {
                    Licencier(p.Matricule);
                }
            }
        }

        /// <summary>
        ///  Ajoute une nouvelle attraction au parc
        /// </summary>
        /// <param name="nouvelleAttraction">Attraction à rajouter</param>
        public void AjouterAttraction(Attraction nouvelleAttraction)
        {
            attractions.Add(nouvelleAttraction);
        }

        /// <summary>
        /// Supprime une attraction du parc
        /// </summary>
        /// <param name="idAttraction">Identifiant de l'attraction à supprimer</param>
        public void SupprimerAttraction(string idAttraction)
        {
            Attraction attractionASupprimer = DoesAttractionExists(idAttraction);
            if (attractionASupprimer != null)
            {
                attractions.Remove(attractionASupprimer);
            }
        }

        /// <summary>
        /// Supprime les attractions d'une liste d'attraction
        /// </summary>
        /// <param name="attractions">Liste d'attraction à supprimer</param>
        public void SupprimerAttractionsFromListe(List<Attraction> attractions)
        {
            if (attractions != null && attractions.Count > 0)
            {
                foreach (Attraction a in attractions)
                {
                    SupprimerAttraction(a.Identifiant.ToString());
                }
            }
        }

        #endregion

        #region Modification des attributs du personnel

        /// <summary>
        /// Envoie un monstre à un stand de barbe à papa
        /// </summary>
        /// <param name="monstre">Monstre à affecter</param>
        public bool SendToBAP(Monstre monstre)
        {
            Random rand = new Random();
            // chercher les attractions barbe à papa
            List<Boutique> barbeAPapas = new List<Boutique>();
            for (int i = 0; i < attractions.Count(); i++)
            {
                if (attractions.ElementAt(i).GetType().Equals(typeof(Boutique)))
                {
                    Boutique b = attractions.ElementAt(i) as Boutique;
                    if (b.Type == TypeBoutique.barbeAPapa)
                    {
                        barbeAPapas.Add(b);
                    }
                }
            }
            if (barbeAPapas.Count > 0) // Si on a au moins une boutique de barbe à papa
            {
                foreach (Boutique bap in barbeAPapas)
                {
                    if (bap.Equipe.Count <= 0 || bap.Equipe == null)
                    {
                        // Cas où aucun monstre n'est affecté à une boutique de barbe à papa => affectation direct
                        bap.Equipe.Add(monstre);
                        monstre.Affectation = bap;
                        return true;
                    }
                }
                // Si on n'a pas trouvé de shop vide, on affecte à un shop random
                int whereToAffect = rand.Next(0, barbeAPapas.Count - 1);
                barbeAPapas.ElementAt(whereToAffect).Equipe.Add(monstre);
                monstre.Affectation = barbeAPapas.ElementAt(whereToAffect);
                return true;
            }
            else // Aucun shop de barbe à papa existant
            {
                return false;
            }

        }

        /// <summary>
        /// Modifie la fonction d'un employé du personnel identifié par son matricule
        /// </summary>
        /// <param name="matricule">Matricule de l'employé</param>
        /// <param name="newFonction">Nouvelle fonction</param>
        public void ModifierFonction(int matricule, string newFonction)
        {
            Personnel employe = DoesEmployeExists(matricule);
            if (employe != null)
            {
                employe.Fonction = newFonction;
            }
        }

        /// <summary>
        /// Modifie la cagnotte d'un monstre du personnel identifié par son matricule
        /// </summary>
        /// <param name="matricule">Matricule du monstre</param>
        /// <param name="newCagnotte">Nouvelle valeur de cagnotte</param>
        public void ModifierCagnotte(int matricule, int newCagnotte)
        {
            Monstre employe = (Monstre)DoesEmployeExists(matricule);
            if (employe != null && newCagnotte >= 0)
            {
                employe.Cagnotte = newCagnotte;
            }
            CheckCagnotte(matricule);
        }

        /// <summary>
        /// Modifie l'affectation d'un monstre du personnel identifié par son matricule
        /// </summary>
        /// <param name="matricule">Matricule du monstre</param>
        /// <param name="newAffectation">Nouvelle affectation</param>
        public void ModifierAffectation(int matricule, Attraction newAffectation)
        {
            Monstre employe = (Monstre)DoesEmployeExists(matricule);
            if (employe != null && newAffectation != null)
            {
                employe.Affectation = newAffectation;
            }
        }

        /// <summary>
        /// Modifie le grade d'un Sorcier identifié par son matricule
        /// </summary>
        /// <param name="matricule">Matricule du Sorcier</param>
        /// <param name="newGrade">Grade de promotion / déchéance</param>
        public void ModifierGrade(int matricule, string newGrade)
        {
            Grade Tatouage = Grade.novice;
            bool is_a_grade = true;
            switch (newGrade)
            {
                case "novice":
                    Tatouage = Grade.novice;
                    break;
                case "mega":
                    Tatouage = Grade.mega;
                    break;
                case "giga":
                    Tatouage = Grade.giga;
                    break;
                case "strata":
                    Tatouage = Grade.strata;
                    break;
                default:
                    is_a_grade = false;
                    break;
            }
            Sorcier employe = (Sorcier)DoesEmployeExists(matricule);
            if (employe != null && is_a_grade)
            {
                employe.Tatouage = Tatouage;
            }
        }

        /// <summary>
        /// Modifie les pouvoirs d'un Sorcier identifié par son matricule
        /// </summary>
        /// <param name="matricule">Matricule du Sorcier</param>
        /// <param name="newPouvoir">Nouvelle liste de pouvoirs</param>
        public void ModifierPouvoir(int matricule, List<string> newPouvoir)
        {
            Sorcier employe = (Sorcier)DoesEmployeExists(matricule);
            if (employe != null && newPouvoir != null)
            {
                employe.Pouvoirs = newPouvoir;
            }
        }

        /// <summary>
        /// Modifie la force d'un Démon identifié par son matricule
        /// </summary>
        /// <param name="matricule">Matricule du Démon</param>
        /// <param name="newForce">Nouvelle valeur de la force du Démon</param>
        public void ModifierForce(int matricule, int newForce)
        {
            Demon employe = (Demon)DoesEmployeExists(matricule);
            if (employe != null && newForce >= 0)
            {
                employe.Force = newForce;
            }
        }

        /// <summary>
        /// Modifie l'indice de cruauté d'un Loup Garou identifié par son matricule
        /// </summary>
        /// <param name="matricule">Matricule du Loup Garou</param>
        /// <param name="newCruaute">Nouvelle valeur de l'indice de cruauté du Loup Garou</param>
        public void ModifierCruaute(int matricule, double newCruaute)
        {
            LoupGarou employe = (LoupGarou)DoesEmployeExists(matricule);
            if (employe != null && newCruaute >= 0.0)
            {
                employe.IndiceCruaute = newCruaute;
            }
        }

        /// <summary>
        /// Modifie l'indice de luminosité d'un Vampire identifié par son matricule
        /// </summary>
        /// <param name="matricule">Matricule du Vampire</param>
        /// <param name="newLuminosite">Nouvelle valeur de l'indice de luminosité du Vampire</param>
        public void ModifierLuminosite(int matricule, double newLuminosite)
        {
            Vampire employe = (Vampire)DoesEmployeExists(matricule);
            if (employe != null && newLuminosite >= 0.0)
            {
                employe.IndiceLuminosite = newLuminosite;
            }
        }

        /// <summary>
        /// Modifie le niveau de décomposition d'un Zombie identifié par son matricule
        /// </summary>
        /// <param name="matricule">Matricule du Zombie</param>
        /// <param name="newDecomposition">Nouvelle valeur de décomposition du Zombie</param>
        public void ModifierDecomposition(int matricule, int newDecomposition)
        {
            Zombie employe = (Zombie)DoesEmployeExists(matricule);
            if (employe != null && newDecomposition >= 0 && newDecomposition <= 10)
            {
                employe.DegreDecomposition = newDecomposition;
            }
        }

        #endregion

        #region Modification des attributs des attractions

        /// <summary>
        /// Modifie le nombre minimal de monstres d'une attraction
        /// </summary>
        /// <param name="id">Identifiant de l'attraction</param>
        /// <param name="nbMinMonstres">Nouveau nombre minimum de monstres</param>
        public void ModifierNbMinMonstres(int id, int nbMinMonstres)
        {
            Attraction a = DoesAttractionExists(id.ToString());
            if (a != null && nbMinMonstres >= 0)
            {
                a.NbMinMonstres = nbMinMonstres;
            }
        }

        /// <summary>
        /// Modifie le besoin spécifique d'une attraction
        /// </summary>
        /// <param name="id">Identifiant de l'attraction</param>
        /// <param name="typeRequis">Nouveau type de monstre requis</param>
        public void ModifierBesoinSpecifique(int id, string typeRequis)
        {
            List<string> typesPossibles = new List<string>(new string[] { "demon", "fantome", "loup-garou", "vampire", "zombie", "" });
            Attraction a = DoesAttractionExists(id.ToString());
            if (a != null && typesPossibles.Contains(typeRequis))
            {
                if (typeRequis != "") // C'est un type de monstre
                {
                    a.BesoinSpecifique = true;
                    a.TypeDeBesoin = typeRequis;
                }
                else
                {
                    a.BesoinSpecifique = false;
                    a.TypeDeBesoin = typeRequis;
                }
            }
        }

        /// <summary>
        /// Modifie l'équipe d'une attraction
        /// </summary>
        /// <param name="id">Identifiant de l'attraction</param>
        /// <param name="equipe">Nouvelle équipe</param>
        public void ModifierEquipe(int id, List<Monstre> equipe)
        {
            Attraction a = DoesAttractionExists(id.ToString());
            if (a != null && equipe != null)
            {
                a.Equipe = equipe;
            }
        }

        /// <summary>
        /// Modifie le statut de maintenance d'une attraction
        /// </summary>
        /// <param name="id">Identifiant de l'attraction</param>
        /// <param name="natureMaintenance">Nature de la maintenance</param>
        /// <param name="dureeMaintenanceInDays">Duree de la maintenance</param>
        public void ModifierMaintenance(int id, string natureMaintenance, double dureeMaintenanceInDays)
        {
            Attraction a = DoesAttractionExists(id.ToString());
            if (a != null && natureMaintenance != null && dureeMaintenanceInDays >= 0.0)
            {
                if (natureMaintenance != "" && dureeMaintenanceInDays == 0.0) // plus de maintenance
                {
                    ModifierOuverture(id, true);
                    a.Maintenance = false;
                    a.NatureMaintenance = null;
                    a.DureeMaintenance = TimeSpan.Zero;
                }
                else
                {
                    ModifierOuverture(id, false);
                    a.Maintenance = true;
                    a.NatureMaintenance = natureMaintenance;
                    a.DureeMaintenance = TimeSpan.FromDays(dureeMaintenanceInDays);
                }
            }
        }

        /// <summary>
        /// Modifie le statut d'ouverture d'une attraction
        /// </summary>
        /// <param name="id">Identifiant de l'attraction</param>
        /// <param name="ouvert">Nouveau statut de l'ouverture</param>
        public void ModifierOuverture(int id, bool ouvert)
        {
            Attraction a = DoesAttractionExists(id.ToString());
            if (a != null)
            {
                a.Ouvert = ouvert;
            }
        }

        #endregion
        
        #region Filtrage

        #region Filtrage du personnel

        /// <summary>
        /// Récupère les Sorciers présents dans le personnel
        /// </summary>
        /// <returns>Liste des identifiants de sorciers existants dans le personnel</returns>
        private List<int> GetSorcierInParc()
        {
            if (toutLePersonnel != null)
            {
                List<int> Sorciers = new List<int>();
                foreach (Personnel m in toutLePersonnel)
                {
                    if (m is Sorcier)
                    {
                        Sorciers.Add(m.Matricule);
                    }
                }
                return (Sorciers.Count > 0) ? Sorciers : null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère les Démons présents dans le personnel
        /// </summary>
        /// <returns>Liste des identifiants de démons existants dans le personnel</returns>
        private List<int> GetDemonInParc()
        {
            if (toutLePersonnel != null)
            {
                List<int> Demons = new List<int>();
                foreach (Personnel m in toutLePersonnel)
                {
                    if (m is Demon)
                    {
                        Demons.Add(m.Matricule);
                    }
                }
                return (Demons.Count > 0) ? Demons : null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère les Fantomes présents dans le personnel
        /// </summary>
        /// <returns>Liste des identifiants de fantomes existants dans le personnel</returns>
        private List<int> GetFantomeInParc()
        {
            if (toutLePersonnel != null)
            {
                List<int> Fantomes = new List<int>();
                foreach (Personnel m in toutLePersonnel)
                {
                    if (m is Fantome)
                    {
                        Fantomes.Add(m.Matricule);
                    }
                }
                return (Fantomes.Count > 0) ? Fantomes : null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère les Loup-Garous présents dans le personnel
        /// </summary>
        /// <returns>Liste des identifiants de loups-garous existants dans le personnel</returns>
        private List<int> GetLoupGarouInParc()
        {
            if (toutLePersonnel != null)
            {
                List<int> LoupGarous = new List<int>();
                foreach (Personnel m in toutLePersonnel)
                {
                    if (m is LoupGarou)
                    {
                        LoupGarous.Add(m.Matricule);
                    }
                }
                return (LoupGarous.Count > 0) ? LoupGarous : null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère les Vampires présents dans le personnel
        /// </summary>
        /// <returns>Liste des identifiants de vampires existants dans le personnel</returns>
        private List<int> GetVampireInParc()
        {
            if (toutLePersonnel != null)
            {
                List<int> Vampires = new List<int>();
                foreach (Personnel m in toutLePersonnel)
                {
                    if (m is Vampire)
                    {
                        Vampires.Add(m.Matricule);
                    }
                }
                return (Vampires.Count > 0) ? Vampires : null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère les Zombies présents dans le personnel
        /// </summary>
        /// <returns>Liste des identifiants de zombies existants dans le personnel</returns>
        private List<int> GetZombieInParc()
        {
            if (toutLePersonnel != null)
            {
                List<int> Zombies = new List<int>();
                foreach (Personnel m in toutLePersonnel)
                {
                    if (m is Zombie)
                    {
                        Zombies.Add(m.Matricule);
                    }
                }
                return (Zombies.Count > 0) ? Zombies : null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère les identifiants de chacun des employés d'une liste de Personnel
        /// </summary>
        /// <param name="employés">Liste d'employés à récupérer</param>
        /// <returns>Liste des identifiants d'employés existants dans la liste de Personnel</returns>
        private List<int> GetIdFromAll(List<Personnel> employés)
        {
            List<int> ListId = new List<int>();
            foreach (Personnel p in employés)
            {
                ListId.Add(p.Matricule);
            }
            return ListId;
        }

        /// <summary>
        /// Récupère une liste de Personnel depuis une liste d'identifiants
        /// </summary>
        /// <param name="ids">Liste d'identifiants</param>
        /// <returns>Liste de personnel correspondante</returns>
        private List<Personnel> GetPersonnelFromIdList(List<int> ids)
        {
            if (ids != null && ids.Count > 0)
            {
                List<Personnel> personnel = new List<Personnel>();
                foreach (int id in ids)
                {
                    Personnel p = DoesEmployeExists(id);
                    if (p != null)
                    {
                        personnel.Add(p);
                    }
                }
                return personnel;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Filtre une liste de personnel selon le type en entrée
        /// </summary>
        /// <param name="typesDesires">Type à filtrer</param>
        /// <param name="LePersonnel">Liste de personnel à filtrer</param>
        /// <returns></returns>
        public List<int> FiltrerPersonnelSelonType(string typesDesires, List<Personnel> LePersonnel)
        {
            if (LePersonnel != null && LePersonnel.Count > 0)
            {
                if (typesDesires.Equals("sorcier"))
                {
                    return GetSorcierInParc();
                }
                else if (typesDesires.Equals("démon"))
                {
                    return GetDemonInParc();
                }
                else if (typesDesires.Equals("fantome"))
                {
                    return GetFantomeInParc();
                }
                else if (typesDesires.Equals("loupgarou"))
                {
                    return GetLoupGarouInParc();
                }
                else if (typesDesires.Equals("vampire"))
                {
                    return GetVampireInParc();
                }
                else if (typesDesires.Equals("zombie"))
                {
                    return GetZombieInParc();
                }
                else if(typesDesires.Equals("none"))
                {
                    return GetIdFromAll(toutLePersonnel);
                }
                else
                {
                    Console.WriteLine("\ttype en entrée non valide, la liste de personnel n'a pas été filtré.");
                    return GetIdFromAll(toutLePersonnel);
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Filtre une liste de personnel selon leur cagnotte
        /// </summary>
        /// <param name="comparateur">Opérateur de comparaison</param>
        /// <param name="cagnotte">Valeur de la cagnotte pour le filtrage</param>
        /// <param name="LePersonnel">Liste des identifiants du personnel à filtrer</param>
        /// <returns></returns>
        public List<int> FiltrerPersonnelSelonCagnotte(string comparateur, int cagnotte, List<int> LePersonnel)
        {
            bool isThereWizard = false;
            List<int> personnelFiltre = new List<int>();
            if (comparateur == ">")
            {
                foreach (int P in LePersonnel)
                {
                    Personnel p = DoesEmployeExists(P);
                    Monstre P1;
                    if (p is Monstre)
                    {
                        P1 = p as Monstre;
                        if (P1.Cagnotte > cagnotte)
                        {
                            personnelFiltre.Add(p.Matricule);
                        }
                    }
                    else
                    {
                        isThereWizard = true;
                    }
                }
                if(isThereWizard)
                    Console.WriteLine("\tErreur :  la fonction de filtrage par cagnotte a été appelée sur un ou plusieurs sorciers, il n'ont pas été selectionnés par le filtre");
                return personnelFiltre;
            }
            else if (comparateur == ">=")
            {
                foreach (int P in LePersonnel)
                {
                    Personnel p = DoesEmployeExists(P);
                    Monstre P1;
                    if (p is Monstre)
                    {
                        P1 = p as Monstre;
                        if (P1.Cagnotte >= cagnotte)
                        {
                            personnelFiltre.Add(p.Matricule);
                        }
                    }
                    else
                    {
                        isThereWizard = true;
                    }
                }
                if (isThereWizard)
                    Console.WriteLine("\tErreur :  la fonction de filtrage par cagnotte a été appelée sur un ou plusieurs sorciers, il n'ont pas été selectionnés par le filtre");
                return personnelFiltre;
            }
            else if (comparateur == "<")
            {
                foreach (int P in LePersonnel)
                {
                    Personnel p = DoesEmployeExists(P);
                    Monstre P1;
                    if (p is Monstre)
                    {
                        P1 = p as Monstre;
                        if (P1.Cagnotte < cagnotte)
                        {
                            personnelFiltre.Add(p.Matricule);
                        }
                    }
                    else
                    {
                        isThereWizard = true;
                    }
                }
                if (isThereWizard)
                    Console.WriteLine("\tErreur :  la fonction de filtrage par cagnotte a été appelée sur un ou plusieurs sorciers, il n'ont pas été selectionnés par le filtre");
                return personnelFiltre;
            }
            else if (comparateur == "<=")
            {
                foreach (int P in LePersonnel)
                {
                    Personnel p = DoesEmployeExists(P);
                    Monstre P1;
                    if (p is Monstre)
                    {
                        P1 = p as Monstre;
                        if (P1.Cagnotte <= cagnotte)
                        {
                            personnelFiltre.Add(p.Matricule);
                        }
                    }
                    else
                    {
                        isThereWizard = true;
                    }
                }
                if (isThereWizard)
                    Console.WriteLine("\tErreur :  la fonction de filtrage par cagnotte a été appelée sur un ou plusieurs sorciers, il n'ont pas été selectionnés par le filtre");
                return personnelFiltre;
            }
            else if (comparateur == "=")
            {
                foreach (int P in LePersonnel)
                {
                    Personnel p = DoesEmployeExists(P);
                    Monstre P1;
                    if (p is Monstre)
                    {
                        P1 = p as Monstre;
                        if (P1.Cagnotte == cagnotte)
                        {
                            personnelFiltre.Add(p.Matricule);
                        }
                    }
                    else
                    {
                        isThereWizard = true;
                    }
                }
                if (isThereWizard)
                    Console.WriteLine("\tErreur :  la fonction de filtrage par cagnotte a été appelée sur un ou plusieurs sorciers, il n'ont pas été selectionnés par le filtre");
                return personnelFiltre;
            }
            if (comparateur == "none")
            {
                return LePersonnel;
            }
            Console.WriteLine("\nErreur : le comparateur saisi lors du filtrage par cagnotte est invalide, la liste n'a pas été filtrée.");
            return LePersonnel;
        }

        /// <summary>
        /// Filtre une liste de personnel selon leur fonction
        /// </summary>
        /// <param name="fonction">Fonction ciblée pour le filtrage</param>
        /// <param name="LePersonnel">Liste des identifiants du personnel à filtrer</param>
        /// <returns></returns>
        public List<int> FiltrerPersonnelSelonFonction(string fonction, List<int> LePersonnel)
        {
            List<int> personnelFiltre = new List<int>();
            if (LePersonnel != null && LePersonnel.Count > 0)
            {
                if (fonction.Equals("toutes"))
                {
                    return LePersonnel;
                }
                foreach (int perso in LePersonnel)
                {
                    Personnel p = DoesEmployeExists(perso);
                    if (p.Fonction.Equals(fonction))
                    {
                        personnelFiltre.Add(perso);
                    }
                }
            }
            return personnelFiltre;
        }

        /// <summary>
        /// Filtre une liste de personnel selon type, cagnotte, fonction
        /// </summary>
        /// <param name="typesDesires">Types de personnel désirés</param>
        /// <param name="comparateur">Opérateur de comparaison</param>
        /// <param name="cagnotte">Valeur de cagnotte</param>
        /// <param name="fonction">Fonction en string</param>
        /// <returns></returns>
        public List<Personnel> FiltrerPersonnelSelonTypeCagnotteFonction(string typesDesires, string comparateur, int cagnotte, string fonction)
        {
            return (GetPersonnelFromIdList(FiltrerPersonnelSelonFonction(fonction, (FiltrerPersonnelSelonCagnotte(comparateur, cagnotte, (FiltrerPersonnelSelonType(typesDesires, toutLePersonnel)))))));
        }

        #endregion        

        #region Filtrage des attractions

        /// <summary>
        /// Filtre les Boutiques du parc selon un certain critère passé en paramètre
        /// </summary>
        /// <param name="critere">Critère de tri</param>
        /// <param name="operateur">Opérateur de comparaison</param>
        /// <param name="valueInt">Si paramètre à trier avec une valeur de type int, valeur de référence</param>
        /// <param name="valueStr">Si paramètre à trier avec une valeur de type string, valeur de référence</param>
        /// <returns>Liste de boutiques ordonnée</returns>
        private List<Attraction> FiltrerBoutiques(string critere, string operateur, int valueInt, string valueStr)
        {
            List<Boutique> boutiques = GetBoutiquesInParc().Cast<Boutique>().ToList();
            if (boutiques != null && boutiques.Count > 0)
            {
                if (operateur != null)
                {
                    // Filtrage avec un attribut entier
                    return Boutique.FiltrerBoutiquesParNbMinMonstres(boutiques, critere, operateur, valueInt).Cast<Attraction>().ToList();
                }
                else
                {
                    // Filtrage avec booléen : besoin spec
                    if (valueInt < 0 && valueStr == null)
                    {
                        return Boutique.FiltrerBoutiquesParBesoinSpec(boutiques, critere).Cast<Attraction>().ToList();
                    }
                    else // Filtrage avec string (ie. type de besoin, type de boutique)
                    {
                        return Boutique.FiltrerBoutiquesParTypeOuBesoin(boutiques, critere, valueStr).Cast<Attraction>().ToList();
                    }
                }
            }
            return (boutiques == null) ? null : boutiques.Cast<Attraction>().ToList();
        }

        /// <summary>
        /// Récupère les Boutiques présents dans les attractions du parc
        /// </summary>
        /// <returns>Liste de boutiques existants dans le parc</returns>
        private List<Attraction> GetBoutiquesInParc()
        {
            if (attractions != null)
            {
                List<Attraction> boutiques = new List<Attraction>();
                foreach (Attraction a in attractions)
                {
                    if (a is Boutique)
                    {
                        boutiques.Add(a);
                    }
                }
                return (boutiques.Count > 0) ? boutiques : null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Filtre les DarkRides du parc selon un certain critère passé en paramètre
        /// </summary>
        /// <param name="critere">Critère de tri</param>
        /// <param name="operateur">Opérateur de comparaison</param>
        /// <param name="valueInt">Si paramètre à trier avec une valeur de type int, valeur de référence</param>
        /// <param name="valueStr">Si paramètre à trier avec une valeur de type string, valeur de référence</param>
        /// <returns>Liste de darkrides ordonnée</returns>
        private List<Attraction> FiltrerDarkRides(string critere, string operateur, int valueInt, string valueStr)
        {
            List<DarkRide> darkRides = GetDarkRidesInParc().Cast<DarkRide>().ToList();
            if (darkRides != null && darkRides.Count > 0)
            {
                if (operateur != null)
                {
                    // Filtrage avec un attribut entier
                    return DarkRide.FiltrerDarkRidesParNbMinMonstresOuDuree(darkRides, critere, operateur, valueInt).Cast<Attraction>().ToList();
                }
                else
                {
                    // Filtrage avec booléen : besoin spec
                    if (valueInt < 0 && valueStr == null)
                    {
                        return DarkRide.FiltrerDarkRidesParBesoinSpecOuVehicule(darkRides, critere).Cast<Attraction>().ToList();
                    }
                    else // Filtrage avec string (ie. type de besoin, type de boutique)
                    {
                        return DarkRide.FiltrerDarkRidesParTypeDeBesoin(darkRides, critere, valueStr).Cast<Attraction>().ToList();
                    }
                }
            }
            return (darkRides == null) ? null : darkRides.Cast<Attraction>().ToList();
        }

        /// <summary>
        /// Récupère les DarkRides présents dans les attractions du parc
        /// </summary>
        /// <returns>Liste de darkrides existants dans le parc</returns>
        private List<Attraction> GetDarkRidesInParc()
        {
            if (attractions != null)
            {
                List<Attraction> darkRides = new List<Attraction>();
                foreach (Attraction a in attractions)
                {
                    if (a is DarkRide)
                    {
                        darkRides.Add(a);
                    }
                }
                return (darkRides.Count > 0) ? darkRides : null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Filtre les RollerCoasters du parc selon un certain critère passé en paramètre
        /// </summary>
        /// <param name="critere">Critère de tri</param>
        /// <param name="operateur">Opérateur de comparaison</param>
        /// <param name="valueInt">Si paramètre à trier avec une valeur de type int, valeur de référence</param>
        /// <param name="valueStr">Si paramètre à trier avec une valeur de type string, valeur de référence</param>
        /// <returns>Liste de rollercoasters ordonnée</returns>
        private List<Attraction> FiltrerRollerCoasters(string critere, string operateur, double valueInt, string valueStr)
        {
            List<RollerCoaster> rollerCoasters = GetRollerCoastersInParc().Cast<RollerCoaster>().ToList();
            if (rollerCoasters != null && rollerCoasters.Count > 0)
            {
                if (operateur != null)
                {
                    // Filtrage avec un attribut entier
                    return RollerCoaster.FiltrerRollerCoastersParNbMinMonstresTailleAgeMin(rollerCoasters, critere, operateur, valueInt).Cast<Attraction>().ToList();
                }
                else
                {
                    // Filtrage avec booléen : besoin spec
                    if (valueInt < 0 && valueStr == null)
                    {
                        return RollerCoaster.FiltrerRollerCoastersParBesoinSpec(rollerCoasters, critere).Cast<Attraction>().ToList();
                    }
                    else // Filtrage avec string (ie. type de besoin, type de boutique)
                    {
                        return RollerCoaster.FiltrerRollerCoastersParCategorieOuBesoin(rollerCoasters, critere, valueStr).Cast<Attraction>().ToList();
                    }
                }
            }
            return (rollerCoasters == null) ? null : rollerCoasters.Cast<Attraction>().ToList();
        }

        /// <summary>
        /// Récupère les RollerCoasters présents dans les attractions du parc
        /// </summary>
        /// <returns>Liste de rollercoasters existants dans le parc</returns>
        private List<Attraction> GetRollerCoastersInParc()
        {
            if (attractions != null)
            {
                List<Attraction> rollerCoasters = new List<Attraction>();
                foreach (Attraction a in attractions)
                {
                    if (a is RollerCoaster)
                    {
                        rollerCoasters.Add(a);
                    }
                }
                return (rollerCoasters.Count > 0) ? rollerCoasters : null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Filtre les spectacles du parc selon un certain critère passé en paramètre
        /// </summary>
        /// <param name="critere">Critère de tri</param>
        /// <param name="operateur">Opérateur de comparaison</param>
        /// <param name="valueInt">Si paramètre à trier avec une valeur de type int, valeur de référence</param>
        /// <param name="valueStr">Si paramètre à trier avec une valeur de type string, valeur de référence</param>
        /// <returns>Liste de spectacles ordonnée</returns>
        private List<Attraction> FiltrerSpectacles(string critere, string operateur, int valueInt, string valueStr)
        {
            List<Spectacle> spectacles = GetSpectaclesInParc().Cast<Spectacle>().ToList();
            if (spectacles != null && spectacles.Count > 0)
            {
                if (operateur != null)
                {
                    // Filtrage avec un attribut entier
                    return Spectacle.FiltrerSpectaclesParNbMinMonstresPlaces(spectacles, critere, operateur, valueInt).Cast<Attraction>().ToList();
                }
                else
                {
                    // Filtrage avec booléen : besoin spec
                    if (valueInt < 0 && valueStr == null)
                    {
                        return Spectacle.FiltrerSpectaclesParBesoinSpec(spectacles, critere).Cast<Attraction>().ToList();
                    }
                    else // Filtrage avec string (ie. type de besoin, type de boutique)
                    {
                        return Spectacle.FiltrerSpectaclesParSalleOuBesoin(spectacles, critere, valueStr).Cast<Attraction>().ToList();
                    }
                }
            }
            return (spectacles == null) ? null : spectacles.Cast<Attraction>().ToList();
        }

        /// <summary>
        /// Récupère les Spectacles présents dans les attractions du parc
        /// </summary>
        /// <returns>Liste de spectacles existants dans le parc</returns>
        private List<Attraction> GetSpectaclesInParc()
        {
            if (attractions != null)
            {
                List<Attraction> spectacles = new List<Attraction>();
                foreach (Attraction a in attractions)
                {
                    if (a is Spectacle)
                    {
                        spectacles.Add(a);
                    }
                }
                return (spectacles.Count > 0) ? spectacles : null;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #endregion

        #region Tri

        /// <summary>
        /// Vérifie si une liste d'attraction contient uniquement un certain type ou non
        /// </summary>
        /// <param name="type">Type de monstre</param>
        /// <param name="attractions">Liste d'attractions</param>
        /// <returns></returns>
        public bool ListContainsOnly(string type, List<Attraction> attractions)
        {
            if (attractions != null && attractions.Count > 0)
            {
                switch (type)
                {
                    case "Boutique":
                        foreach (Attraction a in attractions)
                        {
                            if (!(a is Boutique))
                            {
                                return false;
                            }
                        }
                        return true;
                    case "DarkRide":
                        foreach (Attraction a in attractions)
                        {
                            if (!(a is DarkRide))
                            {
                                return false;
                            }
                        }
                        return true;
                    case "RollerCoaster":
                        foreach (Attraction a in attractions)
                        {
                            if (!(a is RollerCoaster))
                            {
                                return false;
                            }
                        }
                        return true;
                    case "Spectacle":
                        foreach (Attraction a in attractions)
                        {
                            if (!(a is Spectacle))
                            {
                                return false;
                            }
                        }
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Trie une liste de personnel par leur méthode de Tri par défaut
        /// </summary>
        /// <param name="personnel">Liste de personnel à trier</param>
        /// <param name="typePersonnel">type de personnel contenu dans la liste</param>
        /// <returns></returns>
        public List<Personnel> TrierPersonnel(List<Personnel> personnel, string typePersonnel)
        {
            if (personnel != null)
            {
                if (typePersonnel.Equals("demon"))
                {
                    List<Demon> demons = personnel.Cast<Demon>().ToList();
                    Demon.TrierListeDemons(demons);
                    personnel = demons.Cast<Personnel>().ToList();
                }
                else if (typePersonnel.Equals("fantome"))
                {
                    return null;
                }
                else if (typePersonnel.Equals("loupgarou"))
                {
                    List<LoupGarou> loupGarous = personnel.Cast<LoupGarou>().ToList();
                    LoupGarou.TrierListeLoupsGarous(loupGarous);
                    personnel = loupGarous.Cast<Personnel>().ToList();
                }
                else if (typePersonnel.Equals("vampire"))
                {
                    List<Vampire> vampires = personnel.Cast<Vampire>().ToList();
                    Vampire.TrierListeVampires(vampires);
                    personnel = vampires.Cast<Personnel>().ToList();
                }
                else if (typePersonnel.Equals("zombie"))
                {
                    List<Zombie> zombies = personnel.Cast<Zombie>().ToList();
                    Zombie.TrierListeZombies(zombies);
                    personnel = zombies.Cast<Personnel>().ToList();
                }
                else if (typePersonnel.Equals("monstre"))
                {
                    List<Monstre> monstres = personnel.Cast<Monstre>().ToList();
                    Monstre.TrierListeMonstresParCagnotte(monstres);
                    personnel = monstres.Cast<Personnel>().ToList();
                }
                return personnel;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Réalise le tri d'une liste d'attraction à l'aide du critère passé en paramètre
        /// </summary>
        /// <param name="attractions">Liste à ordonner</param>
        /// <param name="critereDeTri">Critère de tri</param>
        /// <returns>Liste ordonnée</returns>
        public List<Attraction> TrierAttractions(List<Attraction> attractions, string critereDeTri)
        {
            if (attractions != null && attractions.Count > 0)
            {
                if (ListContainsOnly("Boutique", attractions))
                {
                    switch (critereDeTri)
                    {
                        case "TypeBoutique":
                            List<Boutique> boutiques = attractions.Cast<Boutique>().ToList();
                            Boutique.TrierListeBoutiquesParType(boutiques);
                            return boutiques.Cast<Attraction>().ToList();
                        case "Identifiant":
                            Attraction.TrierListeAttractionsParId(attractions);
                            return attractions;
                        case "Nom":
                            Attraction.TrierListeAttractionsParNom(attractions);
                            return attractions;
                        case "NbMinMonstres":
                            Attraction.TrierListeAttractionsParNbMinMonstres(attractions);
                            return attractions;
                        case "BesoinSpecifique":
                            Attraction.TrierListeAttractionsParBesoinSpecifique(attractions);
                            return attractions;
                        case "Maintenance":
                            Attraction.TrierListeAttractionsParMaintenance(attractions);
                            return attractions;
                        case "Ouverture":
                            Attraction.TrierListeAttractionsParOuverture(attractions);
                            return attractions;
                        default:
                            return attractions;
                    }
                }
                else if (ListContainsOnly("DarkRide", attractions))
                {
                    switch (critereDeTri)
                    {
                        case "Duree":
                            List<DarkRide> darkrides = attractions.Cast<DarkRide>().ToList();
                            DarkRide.TrierListeDarkRidesParDuree(darkrides);
                            return darkrides.Cast<Attraction>().ToList();
                        case "Vehicule":
                            List<DarkRide> darkridesV = attractions.Cast<DarkRide>().ToList();
                            DarkRide.TrierListeDarkRidesParVehicule(darkridesV);
                            return darkridesV.Cast<Attraction>().ToList();
                        case "Identifiant":
                            Attraction.TrierListeAttractionsParId(attractions);
                            return attractions;
                        case "Nom":
                            Attraction.TrierListeAttractionsParNom(attractions);
                            return attractions;
                        case "NbMinMonstres":
                            Attraction.TrierListeAttractionsParNbMinMonstres(attractions);
                            return attractions;
                        case "BesoinSpecifique":
                            Attraction.TrierListeAttractionsParBesoinSpecifique(attractions);
                            return attractions;
                        case "Maintenance":
                            Attraction.TrierListeAttractionsParMaintenance(attractions);
                            return attractions;
                        case "Ouverture":
                            Attraction.TrierListeAttractionsParOuverture(attractions);
                            return attractions;
                        default:
                            return attractions;
                    }
                }
                else if (ListContainsOnly("RollerCoaster", attractions))
                {
                    switch (critereDeTri)
                    {
                        case "AgeMin":
                            List<RollerCoaster> rollerCoasters = attractions.Cast<RollerCoaster>().ToList();
                            RollerCoaster.TrierListeRollerCoastersParAgeMin(rollerCoasters);
                            return rollerCoasters.Cast<Attraction>().ToList();
                        case "Categorie":
                            List<RollerCoaster> rollercoasters = attractions.Cast<RollerCoaster>().ToList();
                            RollerCoaster.TrierListeRollerCoastersParCategorie(rollercoasters);
                            return rollercoasters.Cast<Attraction>().ToList();
                        case "TailleMin":
                            List<RollerCoaster> rollercoasterst = attractions.Cast<RollerCoaster>().ToList();
                            RollerCoaster.TrierListeRollerCoastersParTailleMin(rollercoasterst);
                            return rollercoasterst.Cast<Attraction>().ToList();
                        case "Identifiant":
                            Attraction.TrierListeAttractionsParId(attractions);
                            return attractions;
                        case "Nom":
                            Attraction.TrierListeAttractionsParNom(attractions);
                            return attractions;
                        case "NbMinMonstres":
                            Attraction.TrierListeAttractionsParNbMinMonstres(attractions);
                            return attractions;
                        case "BesoinSpecifique":
                            Attraction.TrierListeAttractionsParBesoinSpecifique(attractions);
                            return attractions;
                        case "Maintenance":
                            Attraction.TrierListeAttractionsParMaintenance(attractions);
                            return attractions;
                        case "Ouverture":
                            Attraction.TrierListeAttractionsParOuverture(attractions);
                            return attractions;
                        default:
                            return attractions;
                    }
                }
                else if (ListContainsOnly("Spectacle", attractions))
                {
                    switch (critereDeTri)
                    {
                        case "NbPlaces":
                            List<Spectacle> spectacles = attractions.Cast<Spectacle>().ToList();
                            Spectacle.TrierListeSpectaclesParNbPlaces(spectacles);
                            return spectacles.Cast<Attraction>().ToList();
                        case "Categorie":
                            List<Spectacle> spectaclesn = attractions.Cast<Spectacle>().ToList();
                            Spectacle.TrierListeSpectaclesParNomSalle(spectaclesn);
                            return spectaclesn.Cast<Attraction>().ToList();
                        case "Identifiant":
                            Attraction.TrierListeAttractionsParId(attractions);
                            return attractions;
                        case "Nom":
                            Attraction.TrierListeAttractionsParNom(attractions);
                            return attractions;
                        case "NbMinMonstres":
                            Attraction.TrierListeAttractionsParNbMinMonstres(attractions);
                            return attractions;
                        case "BesoinSpecifique":
                            Attraction.TrierListeAttractionsParBesoinSpecifique(attractions);
                            return attractions;
                        case "Maintenance":
                            Attraction.TrierListeAttractionsParMaintenance(attractions);
                            return attractions;
                        case "Ouverture":
                            Attraction.TrierListeAttractionsParOuverture(attractions);
                            return attractions;
                        default:
                            return attractions;
                    }
                }
                else // Types divers contenus : seuls critères possibles sont ceux de Attraction
                {
                    switch (critereDeTri)
                    {
                        case "Identifiant":
                            Attraction.TrierListeAttractionsParId(attractions);
                            return attractions;
                        case "Nom":
                            Attraction.TrierListeAttractionsParNom(attractions);
                            return attractions;
                        case "NbMinMonstres":
                            Attraction.TrierListeAttractionsParNbMinMonstres(attractions);
                            return attractions;
                        case "BesoinSpecifique":
                            Attraction.TrierListeAttractionsParBesoinSpecifique(attractions);
                            return attractions;
                        case "Maintenance":
                            Attraction.TrierListeAttractionsParMaintenance(attractions);
                            return attractions;
                        case "Ouverture":
                            Attraction.TrierListeAttractionsParOuverture(attractions);
                            return attractions;
                        default:
                            return attractions;
                    }
                }
            }
            else
            {
                return attractions;
            }
        }

        #endregion

        #region ToString

        /// <summary>
        /// Récupère le contenu en toString() des contenus des listes de personnel / attractions
        /// </summary>
        /// <returns>Contenu des listes et leur ToString() respectifs</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (toutLePersonnel != null)
            {
                builder.AppendLine("\t=============== Personnel ===============\n");
                foreach (Personnel p in toutLePersonnel)
                {
                    builder.AppendLine("\t" + p.ToString());
                }
            }
            if (attractions != null)
            {
                builder.AppendLine("\n\t=============== Attractions ===============\n");
                foreach (Attraction a in attractions)
                {
                    builder.AppendLine("\t" + a.ToString());
                }
            }
            return builder.ToString();
        }

        #endregion

        #region Menu

        /// <summary>
        /// Dessine un Ascii Art Zombillénium
        /// </summary>
        public void drawAscii()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t\t\t\t___________________________________________________________________________");
            Console.WriteLine("                                                                           ");
            Console.WriteLine("\t\t\t\t\t       _____                   __    _   __           _                    ");
            Console.WriteLine("\t\t\t\t\t      /__  /  ____  ____ ___  / /_  (_) / /__  ____  (_)_  ______ ___      ");
            Console.WriteLine("\t\t\t\t\t        / /  / __ \\/ __ `__ \\/ __ \\/ / / / _ \\/ __ \\/ / / / / __ `__ \\     ");
            Console.WriteLine("\t\t\t\t\t       / /__/ /_/ / / / / / / /_/ / / / /  __/ / / / / /_/ / / / / / /     ");
            Console.WriteLine("\t\t\t\t\t      /____/\\____/_/ /_/ /_//.___/_/ /_/\\___/_/ /_/_/_____/_/ /_/ /_/      ");

            Console.WriteLine("\t\t\t\t\t   _____ _                 __      __                ___    ___________    ");
            Console.WriteLine("\t\t\t\t\t  / ___/(_)___ ___  __  __/ /___ _/ /_____  _____   |__ \\ / __ /  ( __ )   ");
            Console.WriteLine("\t\t\t\t\t  \\__ \\/ / __ `__ \\/ / / / / __ `/ __/ __ \\/ ___/   __/ // / / / / __  /   ");
            Console.WriteLine("\t\t\t\t\t ___/ / / / / / / / /_/ / / /_/ / /_/ /_/ / /      / __// /_/ / / /_/ /    ");
            Console.WriteLine("\t\t\t\t\t/____/_/_/ /_/ /_/\\__,_/_/\\__,_/\\__/\\____/_/      /____/\\____/_/\\____/     ");
            Console.WriteLine("\t\t\t\t\t                                                                           ");
            Console.WriteLine("\t\t\t\t\t___________________________________________________________________________");
            Console.WriteLine("                                                                           ");


            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

        }

        /// <summary>
        /// Réalise une démonstration des différentes fonctions de l'application
        /// </summary>
        public void LaunchDemo()
        {
            /**************************************************
             *                                                * 
             *        PARTIE INFORMATIONS SUR LE FICHIER      *
             *                                                *
             *************************************************/


            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*        PARTIE INFORMATIONS SUR LE FICHIER      *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\tLecture depuis le fichier csv : " + FICHIER_CSV);
            FileInfo f = new FileInfo(FICHIER_CSV);
            Console.WriteLine("\tChemin du fichier csv : " + Path.GetDirectoryName(f.FullName) + "\n");

            /**************************************************
             *                                                * 
             *           PARTIE INFORMATIONS SUR LE PARC      *
             *                                                *
             *************************************************/

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*           PARTIE INFORMATIONS SUR LE PARC      *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.WriteLine("\tA PARTIR DU FICHIER CSV D'ORIGINE, ON OBTIENT LES LISTES DE PERSONNEL ET D'ATTRACTIONS SUIVANTES :\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(ToString());

            /**************************************************
             *                                                * 
             *           PARTIE AJOUT DE PERSONNEL            *
             *                                                *
             *************************************************/

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*           PARTIE AJOUT DE PERSONNEL            *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.ForegroundColor = ConsoleColor.White;
            Attraction a = DoesAttractionExists("428");
            Recruter(new Demon(70455, "Taclismeenréserve", "Jessica", "femelle", "neant", 450, a, 8));
            Personnel p = DoesEmployeExists(70455);
            Console.WriteLine("\tLe personnel a bien été ajouté :\n\t" + p.ToString());

            /**************************************************
             *                                                * 
             *           PARTIE AJOUT D'ATTRACTION            *
             *                                                *
             *************************************************/

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*           PARTIE AJOUT D'ATTRACTION            *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.ForegroundColor = ConsoleColor.White;
            AjouterAttraction(new Spectacle(156, "Lion King", 5, false, "", "Thrall", 55, "10:30 12:30 16:30"));
            Attraction att = DoesAttractionExists("156");
            Console.WriteLine("\tL'attraction a bien été ajoutée :\n\t" + att.ToString());

            Console.ReadKey();
            Console.Clear();

            /**************************************************
             *                                                * 
             *         PARTIE MODIFICATION DES ATTRIBUTS      *
             *                   DU PERSONNEL                 *
             *                                                *
             *************************************************/

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*         PARTIE MODIFICATION DES ATTRIBUTS      *");
            Console.WriteLine("\t\t\t\t\t*                   DU PERSONNEL                 *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.WriteLine("\tMODIFICATION DE LA FONCTION D'UN EMPLOYE");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\t" + DoesEmployeExists(70455).ToString());
            ModifierFonction(70455, "directrice des Cataclysmes");
            Console.WriteLine("\t" + DoesEmployeExists(70455).ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DE L'AFFECTATION D'UN EMPLOYE");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\t" + DoesEmployeExists(70455).ToString());
            ModifierAffectation(70455, DoesAttractionExists("623"));
            Console.WriteLine("\t" + DoesEmployeExists(70455).ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DE LA CRUAUTE D'UN LOUPGAROU");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\t" + DoesEmployeExists(66604).ToString());
            ModifierCruaute(66604, 1.8);
            Console.WriteLine("\t" + DoesEmployeExists(66604).ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DE L'INDICE DE DECOMPOSITION D'UN ZOMBIE");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\t" + DoesEmployeExists(66855).ToString());
            ModifierDecomposition(66855, 8);
            Console.WriteLine("\t" + DoesEmployeExists(66855).ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DE LA FORCE D'UN DEMON");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\t" + DoesEmployeExists(70455).ToString());
            ModifierForce(70455, 10);
            Console.WriteLine("\t" + DoesEmployeExists(70455).ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DU GRADE D'UN SORCIER");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\t" + DoesEmployeExists(66966).ToString());
            ModifierGrade(66966, "mega");
            Console.WriteLine("\t" + DoesEmployeExists(66966).ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DE L'INDICE DE LUMINOSITE D'UN VAMPIRE");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\t" + DoesEmployeExists(66787).ToString());
            ModifierLuminosite(66787, 1.6);
            Console.WriteLine("\t" + DoesEmployeExists(66787).ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DES POUVOIRS D'UN SORCIER");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            List<string> newPouvoirs = new List<string>(new string[] { "telekinesie", "téléportation", "illusion" });
            Console.WriteLine("\t" + DoesEmployeExists(66966).ToString());
            ModifierPouvoir(66966, newPouvoirs);
            Console.WriteLine("\t" + DoesEmployeExists(66966).ToString() + "\n");

            /**************************************************
             *                                                * 
             *         PARTIE MODIFICATION DES ATTRIBUTS      *
             *                  DES ATTRACTIONS               *
             *                                                *
             *************************************************/

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*         PARTIE MODIFICATION DES ATTRIBUTS      *");
            Console.WriteLine("\t\t\t\t\t*                  DES ATTRACTIONS               *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.WriteLine("\tMODIFICATION DU NOMBRE DE MONSTRES NECESSAIRES A UNE ATTRACTION");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            Attraction nb = DoesAttractionExists("428");
            Console.WriteLine("\t" + nb.ToString());
            ModifierNbMinMonstres(428, 12);
            Console.WriteLine("\t" + nb.ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DU BESOIN SPECIFIQUE D'UNE ATTRACTION");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;
            Attraction bs = DoesAttractionExists("684");
            Console.WriteLine("\t" + bs.ToString());
            ModifierBesoinSpecifique(684, "zombie");
            Console.WriteLine("\t" + bs.ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DE L'EQUIPE D'UNE ATTRACTION");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;
            List<Monstre> nouvelleEquipe = new List<Monstre>(new Monstre[] { (Monstre)DoesEmployeExists(66855), (Monstre)DoesEmployeExists(66987) });
            Console.WriteLine("\t" + bs.ToString());
            ModifierEquipe(684, nouvelleEquipe);
            Console.WriteLine("\t" + bs.ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION DE LA MAINTENANCE D'UNE ATTRACTION");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t" + bs.ToString());
            ModifierMaintenance(684, "Attente de livraison de stock", 12.5698);
            Console.WriteLine("\t" + bs.ToString() + "\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tMODIFICATION D'OUVERTURE D'UNE ATTRACTION");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t" + DoesAttractionExists(623.ToString()).ToString());
            ModifierOuverture(623, false);
            Console.WriteLine("\t" + DoesAttractionExists(623.ToString()).ToString());

            /**************************************************
             *                                                * 
             *             PARTIE EXPORT VERS CSV             *
             *                                                *
             *************************************************/

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*             PARTIE EXPORT VERS CSV             *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\tEcriture dans le fichier csv : " + csv_export);
            FileInfo f_export = new FileInfo(csv_export);
            Console.WriteLine("\tChemin du fichier csv exporté : " + Path.GetDirectoryName(f_export.FullName) + "\n");
            Console.WriteLine("\tL'extraction vers le fichier CSV se fait à partir de listes de Personnel / Attraction , on peut donc filtrer ce qu'on désire puis l'exporter.");
            ExtractListToCSV(csv_export, toutLePersonnel, attractions);

            /**************************************************
             *                                                * 
             *             PARTIE FILTRE ET TRI               *
             *                 DES MONSTRES                   *
             *                                                *
             *************************************************/

            Console.ReadKey();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*             PARTIE FILTRE ET TRI               *");
            Console.WriteLine("\t\t\t\t\t*                 DES MONSTRES                   *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.WriteLine("\tFILTRAGE DU PERSONNEL  PAR TYPE");
            Console.WriteLine("___________________________________________________________________________\n");
            ///////////////
            Console.WriteLine("\tDémon, trié par force croissante: ");
            Console.ForegroundColor = ConsoleColor.White;
            List<Personnel> FiltreDémon = GetPersonnelFromIdList(FiltrerPersonnelSelonType("démon", toutLePersonnel));
            FiltreDémon = TrierPersonnel(FiltreDémon, "demon");
            foreach(Personnel perso in FiltreDémon)
            {
                Console.WriteLine("\t" + perso.ToString());
            }
            //////////////
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tFILTRAGE DU PERSONNEL  PAR CAGNOTTE");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.WriteLine("\tCagnotte supperieur à 500 : ");
            Console.ForegroundColor = ConsoleColor.White;
            List<Personnel> FiltreCagnotteSup = GetPersonnelFromIdList(FiltrerPersonnelSelonCagnotte(">", 500, GetIdFromAll(toutLePersonnel)));
            foreach (Personnel perso in FiltreCagnotteSup)
            {
                Console.WriteLine("\t" + perso.ToString());
            }
            //////////////////
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tCagnotte supperieur à 500 : même example en triant le personnel filtré en fonction de la cagnotte ");
            Console.ForegroundColor = ConsoleColor.White;
            List<Personnel> FiltreCagnotteSupTri = TrierPersonnel(FiltreCagnotteSup, "monstre");
            foreach (Personnel perso in FiltreCagnotteSupTri)
            {
                Console.WriteLine("\t" + perso.ToString());
            }
            //////////////
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tCagnotte inferieur ou égale à 134 , tout en triant les résultats du filtre par cagnotte: ");
            Console.ForegroundColor = ConsoleColor.White;
            List<Personnel> FiltreCagnotteInfEq = GetPersonnelFromIdList(FiltrerPersonnelSelonCagnotte("<=", 134, GetIdFromAll(toutLePersonnel)));
            List<Personnel> FiltreCagnotteInfEqTri = TrierPersonnel(FiltreCagnotteInfEq, "monstre");
            foreach (Personnel perso in FiltreCagnotteInfEqTri)
            {
                Console.WriteLine("\t" + perso.ToString());
            }
            ////////////
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tFILTRAGE DU PERSONNEL  PAR FONCTION");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.WriteLine("\tfonction de directeur d'exploitation :  ");
            Console.ForegroundColor = ConsoleColor.White;
            List<Personnel> FiltreFonctionDirExp = GetPersonnelFromIdList(FiltrerPersonnelSelonFonction("directeur d'exploitation", GetIdFromAll(toutLePersonnel)));
            foreach (Personnel perso in FiltreFonctionDirExp)
            {
                Console.WriteLine("\t" + perso.ToString());
            }
            //////////////
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tFILTRAGE DU PERSONNEL  PAR SUPERPOSITIONDE PLUSIEURS FILTRES");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.WriteLine("\tfiltre : loup-garou, cagnotte > 260, toutes les fonctions  ");
            Console.ForegroundColor = ConsoleColor.White;
            List<Personnel> FiltreTypeCagnotteFonction2 = FiltrerPersonnelSelonTypeCagnotteFonction("loupgarou", ">", 260, "toutes");
            FiltreTypeCagnotteFonction2 = TrierPersonnel(FiltreTypeCagnotteFonction2, "loupgarou");
            foreach (Personnel perso in FiltreTypeCagnotteFonction2)
            {
                Console.WriteLine("\t" + perso.ToString());
            }
            //////////////
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tfiltre : sorcier, pas de contraites sur la cagnotte, toutes les fonctions  ");
            Console.ForegroundColor = ConsoleColor.White;
            List<Personnel> FiltreTypeCagnotteFonction3 = FiltrerPersonnelSelonTypeCagnotteFonction("sorcier", "none", 0, "toutes");
            foreach (Personnel perso in FiltreTypeCagnotteFonction3)
            {
                Console.WriteLine("\t" + perso.ToString());
            }

            /**************************************************
             *                                                * 
             *             PARTIE FILTRE ET TRI               *
             *                DES ATTRACTIONS                 *
             *                                                *
             *************************************************/

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*             PARTIE FILTRE ET TRI               *");
            Console.WriteLine("\t\t\t\t\t*                DES ATTRACTIONS                 *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.WriteLine("\tAVANT LE TRI DES BOUTIQUES");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            AjouterAttraction(new Boutique(423, "Bouticomatic", 2, false, "", "souvenir"));
            foreach (Attraction attr in GetBoutiquesInParc())
            {
                Console.WriteLine("\t" + attr.ToString());
            }

            List<Attraction> boutiques = FiltrerBoutiques("typeBoutique", null, -1, "souvenir");

            boutiques = TrierAttractions(boutiques, "Identifiant");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tBOUTIQUES FILTRÉES (ON A FILTRÉ SUR LE TYPE DE BOUTIQUE \"SOUVENIR\" PUIS TRIÉ PAR IDENTIFIANT)");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Attraction attra in boutiques)
            {
                Console.WriteLine("\t" + attra.ToString());
            }

            /**************************************************
             *                                                * 
             *             PARTIE MODIFICATION DE             *
             *                   LA CAGNOTTE                  *
             *                                                *
             *************************************************/

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\t\t\t**************************************************");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t*             PARTIE MODIFICATION DE             *");
            Console.WriteLine("\t\t\t\t\t*                   LA CAGNOTTE                  *");
            Console.WriteLine("\t\t\t\t\t*                                                *");
            Console.WriteLine("\t\t\t\t\t**************************************************\n");

            Console.WriteLine("\n\tPASSAGE DE LA CAGNOTTE A 600 (ie +500pts : => affecté au parc)");
            Console.WriteLine("___________________________________________________________________________\n");
            Console.ForegroundColor = ConsoleColor.White;

            Monstre m = (Monstre)DoesEmployeExists(70455);
            Console.WriteLine("\t" + m.ToString());
            ModifierCagnotte(70455, 600);
            Console.WriteLine("\t" + m.ToString());

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tPASSAGE DE LA CAGNOTTE A 45 POINTS (ie -50pts : => affecté à un stand barbe à papa)");
            Console.WriteLine("___________________________________________________________________________\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t" + m.ToString());
            ModifierCagnotte(70455, 45);
            Monstre m2 = (Monstre)DoesEmployeExists(70455);
            Console.WriteLine("\t" + m.ToString());
            Attraction aff = DoesAttractionExists(m2.Affectation.Identifiant.ToString());
            Console.WriteLine("\t" + aff.ToString());
        }

        #endregion
    }
}
