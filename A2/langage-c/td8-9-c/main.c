#include <stdio.h>
#include <stdlib.h>
#include "labyrinthe.h"

int main(int argc, char **argv)
{
	char * nom_fichier = "laby1.txt";
	labyrinthe * newLab = chargerLaby(nom_fichier);
	afficherLaby(newLab);
	return 0;
}
