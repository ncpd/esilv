#include <stdio.h>
#include <stdlib.h>
#include <liste.h>

void afficher_liste(element * liste)
{
	while(liste != NULL) {
		printf("%lg ", liste->elt);
		liste = liste->next;
	}
	printf("\n");
}

void exo1()
{
	element * liste = NULL;
	liste = inserer_en_tete(liste, 1.2);
	liste = inserer_en_tete(liste, 2.3);
	liste = inserer_en_tete(liste, 3.9); // Ajouts divers
	liste = inserer_en_tete(liste, 4.8);
	liste = inserer_en_queue(liste, 0.5);
	printf("Liste originale :\n");
	afficher_liste(liste);
	liste = supprimer_en_tete(liste); // Suppression élément en tête
	printf("\nListe apres supression du premier maillon :\n");
	afficher_liste(liste);
	liste = supprimer_en_queue(liste); // Suppression élément en queue
	printf("\nListe apres supression du dernier maillon :\n");
	afficher_liste(liste);
	liste = inserer_en_tete(liste, 5.3);
	liste = inserer_en_tete(liste, 6.4); // Nouvel ajout de valeurs
	liste = inserer_en_tete(liste, 7.5);
	printf("\nListe modifiee :\n");
	afficher_liste(liste);
	printf("\nAdresse de l'element 2 : %p Valeur : %lg\n", acceder_element(liste, 2), acceder_element(liste, 2)->elt); // Accès à l'élément k = 2
	liste = ajouter_element(liste, 4, 0.0);
	printf("\nListe apres ajout de 0.0 en 4eme position :\n");
	afficher_liste(liste);
	liste = supprimer_element(liste, 2);
	printf("\nListe apres supression de l'element en 2eme position :\n");
	afficher_liste(liste);
	printf("\nRecherche de 0.0 dans la liste :\n");
	printf("Adresse de l'element : %p\n", recherche(liste, 0.0));
	printf("Valeur de l'element : %lg\n", recherche(liste, 0.0)->elt);
	ajout_bis(recherche(liste, 0.0), 10.5);
	printf("\nListe apres ajout de 10.5 :\n");
	afficher_liste(liste);
	supress_bis(recherche(liste, 10.5));
	printf("\nListe apres supression de la valeur apres 10.5 :\n");
	afficher_liste(liste);
	printf("\nAdresse de la liste effacee : %p\n", effacer_liste(liste)); // Supression totale de la liste
}

int main(int argc, char **argv)
{
	exo1();
	return 0;
}
