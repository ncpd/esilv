#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

typedef enum legume {CAROTTE, FENOUIL, HARICOT} _legume;

typedef struct _legume_cours legume_cours;

struct _legume_cours { _legume nom; int cours; };

void afficherTableauLegumes(legume_cours tab[], _legume choix_legumes, int taille)
{
	for(int i = 0; i < taille; i++) {
		if(tab[i].nom == CAROTTE)
		printf("%s : %d", tab[i].nom, tab[i].cours);
	}
}

void exoInterro()
{
	_legume choix_legumes = {CAROTTE, FENOUIL};
	legume_cours carotte = {CAROTTE, 1};
	legume_cours fenouil = {FENOUIL, 2};
	legume_cours tab[2] = {carotte, fenouil};
	afficherTableauLegumes(tab, choix_legumes, 2);
}

void exo1()
{
	char c1 = 'A';
	printf("Caractère %c ; en tant qu'entier, valeur = %d\n", c1, c1); // A et 65
	// la valeur entière <=> code ASCII du caractère
	char c2 = 'a';
	printf("Caractère %c ; en tant qu'entier, valeur = %d\n", c2, c2); // a et 97

	int e3 = 65 + 1;
	char c3 = (char)e3;
	printf("c3 : %c \n", c3); // B

	int e4 = (int)'A' + 2;
	char c4 = e4;
	printf("c4 : %c \n", c4); // C

	char c5 = 'a' + 3; 
	printf("c5 : %c \n", c5); // d
}

unsigned int longueur(const char * str)
{
	unsigned int i = 0;
	if(str != NULL) {
		for(i = 0; str[i] != '\0'; i++) {}
	}
	return i;
}

bool estPalindrome(const char * str)
{
	bool b = true;
	int i = longueur(str) - 1;
	int j = 0;
	while(i >= 0) {
		if(str[j] != str[i]) {
			b = false;
		}
		i--;
		j++;
	}
	return b;
}

bool estEntier(const char * str)
{
	bool b = true;
	int lg = longueur(str);
	for(int i = 0; i < lg; i++) {
		if((int)*(str + i) < 48 || (int)*(str + i) > 57) {
			b = false;
		}
	}
	return b;
}

void enMinuscules(char * str)
{
	if(str != NULL) {
		for(int i = 0; i < longueur(str); i++) {
			if(str[i] >= 'A' && str[i] <= 'Z') {
				str[i] = 'a' + (str[i] - 'A');
			}
		}
	}
}

void exo2()
{
	const char chaine[] = "Bonjour";
	const char str[] = "1234";
	const char pal[] = "kayak";
	printf("Longueur de \"%s\" : %d octets\n", chaine, longueur(chaine));
	printf("Longueur de \"%s\" : %d octets\n", str, longueur(str));
	if(estEntier(str)) {
		printf("\"%s\" est un entier !\n", str);
	} else {
		printf("\"%s\" n'est pas un entier !\n", str);
	}
	if(estPalindrome(pal)) {
		printf("\"%s\" est un palindrome !\n", pal);
	} else {
		printf("\"%s\" n'est pas un palindrome !\n", pal);
	}
	printf("%s est devenu ", chaine);
	enMinuscules(chaine);
	printf("%s\n", chaine);
}

void moitie_moitie(const char * chaine1, const char * chaine2, char * resultat)
{
	int sizRes1 = (strlen(chaine1) / 2);
	int sizRes2 = (strlen(chaine2) / 2);
	char r1[sizRes1];
	char r2[sizRes2];
	strncpy(r1, chaine1, sizRes1);
	strncpy(r2, chaine2 + sizRes2, (strlen(chaine2) - 1) - (sizRes2 - 1));
	//strncpy(resultat, r1, sizeof(r1));
	printf("%s\n", r1);
	printf("%s\n", r2);
	strncpy(resultat, r1, sizRes1 + 1);
	//printf("%s\n", resultat);
	strncat(resultat, r2, sizRes2 + 1);
	//printf("%s\n", resultat);
}

void exo3()
{
	char str1[] = "bonjour";
	char str2[] = "coucou";
	char * resultat = (char*) malloc((6 + 1) * sizeof(char));
	moitie_moitie(str1, str2, resultat);
	printf("%s\n", resultat);
	free(resultat);
}

void coder(const char * src, char * dest)
{
	int lengthSrc = strlen(src);
	for(int i = 0; i < lengthSrc; i++) {
		dest[i] = (src[i] + 5 - 'a') % 26 + 'a';
	}
}

void decoder(const char * src, char * dest)
{
	int lengthSrc = strlen(src);
	for(int i = 0; i < lengthSrc; i++) {
		dest[i] = (src[i] - 5 - 'a') % 26 + 'a';
	}
}

void exo4()
{
	const char * src = "salut";
	char * dest = malloc(6*sizeof(char));
	printf("Message avant codage : %s\n", src);
	coder(src, dest);
	printf("Message apres codage : %s\n", dest);
	const char * source = "xfqzy";
	char * destination = malloc(6*sizeof(char));
	printf("Message avant decodage : %s\n", source);
	decoder(source, destination);
	destination[strlen(source)] = '\0';
	printf("Message apres decodage : %s\n", destination);
}

void exo5()
{
	char nom_fichier[] = "test.txt";
	FILE* fichier = fopen(nom_fichier,"r"); // ouverture en lecture (read)
	if( fichier != NULL )
	{
		char c = fgetc(fichier); // lecture du 1er caractère dans fichier, 
             // puis fgetc se positionne sur le caractère suivant (pour la proichaine lecture)
		
		while( c != EOF )   // tant que le caractère lu n'est pas la fin de fichier (End Of File)
		{
			printf("%c", c);
			c = fgetc(fichier); // lecture du caractère suivant
		}
		
		fclose(fichier); // fermeture du fichier
	}
	else
	{
		printf("Problème lors de l'ouverture du fichier %s\n", nom_fichier);
	}
}

typedef struct {
	double x;
	double y;
	double z;
} point3D;

void exo6()
{
	char nom_fichier[] = "polygones.txt";
	FILE* fichier = fopen(nom_fichier,"r"); // ouverture en lecture (read)
	if( fichier != NULL )
	{
		int nbPoints;
		fscanf(fichier, "%d", &nbPoints);
		printf("%d\n", nbPoints);
		point3D tab[nbPoints];
		int i = 0;
		for(int i = 0; i < nbPoints; i++) {
			fscanf(fichier, "%lf %lf %lf\n", &tab[i].x, &tab[i].y, &tab[i].z); // AFFECTATION DES POINTS
		}
		for(int j = 0; j < nbPoints; j++) {
			printf("Pt no.%d : (%lg, %lg, %lg)\n", j + 1, tab[j].x, tab[j].y, tab[j].z); // AFFICHAGE TABLEAU
		}
		fclose(fichier); // fermeture du fichier
	}
	else
	{
		printf("Problème lors de l'ouverture du fichier %s\n", nom_fichier);
	}
}

int nbCaracteres(const char* nom_fichier)
{
	FILE* fichier = fopen(nom_fichier,"r"); // ouverture en lecture (read)
	int count = -1;
	if( fichier != NULL )
	{
		count = 0;
		char c = fgetc(fichier); // lecture du 1er caractère dans fichier, 
             // puis fgetc se positionne sur le caractère suivant (pour la proichaine lecture)
		count++;
		while( c != EOF )   // tant que le caractère lu n'est pas la fin de fichier (End Of File)
		{
			count++;
			printf("%c", c);
			c = fgetc(fichier); // lecture du caractère suivant
		}
		fclose(fichier); // fermeture du fichier
	}
	else
	{
		printf("Problème lors de l'ouverture du fichier %s\n", nom_fichier);
	}
	return count;
}

int nbMots(const char* nom_fichier)
{
	FILE* fichier = fopen(nom_fichier,"r"); // ouverture en lecture (read)
	int count = -1;
	if( fichier != NULL )
	{
		count = 0;
		char c = fgetc(fichier); // lecture du 1er caractère dans fichier, 
             // puis fgetc se positionne sur le caractère suivant (pour la proichaine lecture)
		while(c != EOF) {
			while(c != ' ')   // tant que le caractère lu n'est pas la fin de fichier (End Of File)
			{
				//printf("%c", c);
				c = fgetc(fichier); // lecture du caractère suivant
			}
			count++;
		}
		fclose(fichier); // fermeture du fichier
	}
	else
	{
		printf("Problème lors de l'ouverture du fichier %s\n", nom_fichier);
	}
	return count;
}

void exo7()
{
	printf("\n\nNombre de caracteres du fichier : %d\n", nbCaracteres("test.txt"));
	printf("Nombre de mots du fichier : %d\n", nbMots("test.txt"));
}

int main(int argc, char **argv)
{
	//exo1();
	//exo2();
	//exo3();
	//exo4();
	//exoInterro();
	//exo5();
	//exo6();
	exo7();
	return 0;
}
