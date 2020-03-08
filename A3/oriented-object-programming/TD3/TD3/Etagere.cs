using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD3
{
    class Etagere
    {
        private List<Document> listeDocuments;
        
        public Etagere(List<Document> documents)
        {
            this.listeDocuments = documents;
        }

        public Etagere()
        {
            this.listeDocuments = null;
        }

        public void ajouterOuvrage(Document d)
        {
            if (d != null)
            {
                if (listeDocuments != null)
                {
                    listeDocuments.Add(d);
                }
                else
                {
                    this.listeDocuments = new List<Document>();
                    listeDocuments.Add(d);
                }
            }
        }

        public void listerOuvrages()
        {
            if(listeDocuments != null || !listeDocuments.Any())
            {
                foreach(Document d in listeDocuments)
                {
                    Console.WriteLine(d.ToString());
                }
            }
        }

        public Document chercherOuvrageParTitre(string titre)
        {
            if(listeDocuments != null || !listeDocuments.Any())
            {
                for(int i = 0; i < listeDocuments.Count(); i++)
                {
                    if (listeDocuments[i].Titre.Equals(titre)){
                        return listeDocuments[i];
                    }
                }
                return null;
            } else
            {
                return null;
            }
        }

        public Document chercherOuvrageParNo(int no)
        {
            if (listeDocuments != null || !listeDocuments.Any())
            {
                for (int i = 0; i < listeDocuments.Count(); i++)
                {
                    if (listeDocuments[i].NoEnregistrement == no)
                    {
                        return listeDocuments[i];
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
