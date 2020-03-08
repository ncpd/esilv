using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD2
{
    class Personne
    {
        private string nom;
        private string prenom;
        private bool sexe;
        private int age;
        private int anneeNaissance;
        private string situationFamiliale;

        public Personne(string nom, string prenom, bool sexe, int anneeNaiss, string statutFamilial)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.sexe = sexe;
            this.anneeNaissance = anneeNaiss;
            this.age = 2018 - anneeNaiss;
            this.situationFamiliale = statutFamilial;
        }

        public Personne(string nom, string prenom, bool sexe, int anneeNaiss)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.sexe = sexe;
            this.anneeNaissance = anneeNaiss;
            this.age = 2018 - anneeNaiss;
            this.situationFamiliale = "inconnu";
        }

        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                this.nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return prenom;
            }
            set
            {
                this.prenom = value;
            }
        }

        public int AnneeNaissance
        {
            get
            {
                return anneeNaissance;
            }
            set
            {
                this.anneeNaissance = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                this.age = value;
            }
        }

        public int RetournerAgeEn(int anneeRef)
        {
            if(anneeRef > 0)
            {
                return 2018 - anneeRef;
            } else
            {
                return -1;
            }
        }

        public String Message()
        {
            if(String.Equals(this.situationFamiliale, "inconnu"))
            {
                return prenom + " " + nom + " est né en " + anneeNaissance + ", sa situation familiale est " + situationFamiliale + "e";
            }
            else if(sexe)
            {
                return prenom + " " + nom + " est né en " + anneeNaissance + ", il est " + situationFamiliale;
            } else
            {
                return prenom + " " + nom + " est née en " + anneeNaissance + ", elle est " + situationFamiliale;
            }
            
        }

        public Boolean PlusVieuxQue(int ageRef)
        {
            return (this.age > ageRef);
        }

        public Boolean PlusVieuxQue(Personne pRef)
        {
            if(pRef != null)
            {
                return (this.age > pRef.Age);
            } else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Message();
        }
    }
}
