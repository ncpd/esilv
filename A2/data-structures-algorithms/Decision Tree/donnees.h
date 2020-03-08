#ifndef DONNEES_H
#define DONNEES_H

//----------------------------------------------------------
// Données
//----------------------------------------------------------

typedef struct 
{
	unsigned int nb_colonnes; // la 1ere <=> classe à prédire (Y). Autres colonnes <=> variables d'observatio (Xi)
	unsigned int nb_lignes;   // <=> les individus
	double** matrice;         // tableau de tableaux de réels (i.e. tableau 2D de réels)
} matrice_donnees;


matrice_donnees* charger_donnees(const char* nom_fichier);

// Usage var =  liberer_donnees(var);
matrice_donnees* liberer_donnees(matrice_donnees * data);


#endif