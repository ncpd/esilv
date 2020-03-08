//SETUP PIR
const int irae = 4; //PINS EN TOR POUR LA PRESENCE OU NON
const int pir = 2;
const int ldr1 = A0; //PINS ANALOGIQUES POUR LES PHOTORESISTANCES
const int ldr2 = A1;
const int Led1 = 9; //LEDS EN PWM PINS 9 10 ET 11
const int Led2 = 10;
const int Led3 = 11;
const int Led4 = 5;
const int Led5 = 6;
int timer = 0; //TIMER DEFINI EN CAS DE PRESENCE

void setup() 
{
    Serial.begin(9600);
    pinMode(Led1,OUTPUT); //LEDS EN SORTIE
    pinMode(Led2,OUTPUT); 
    pinMode(Led3,OUTPUT);
    pinMode(Led4,OUTPUT);
    pinMode(Led5,OUTPUT);
    pinMode(irae,INPUT); //DETECTEUR IR 1
    pinMode(pir,INPUT); //DETECTEUR IR 2
    pinMode(ldr1,INPUT); //PHOTORESISTANCE 1
    pinMode(ldr2,INPUT); //PHOTORESISTANCE 2
    //pinMode(13,OUTPUT);
}

void loop() 
{
  int val1; //Valeur de la LDR 1
  int val2;
  int avg; //Moyenne des valeurs des LDR
  int presence = detection_mouvement();
  while(presence == 1 || timer != 0)
  {
    //0 = sombre ; 1023 = très lumineux ; 400 = environ 500 lux
        val1 = analogRead(ldr1); // LECTURE DES VALEURSDES LDR
        val2 = analogRead(ldr2);
        avg = (val1 + val2) / 2; // MOYENNE DES VALEURS DES LDR
        int ledfade = map(avg, 0, 400, 255, 0); //gestion continue de l'éclairement
        if(avg < 400)
        {
        //Serial.println(ledfade);
        analogWrite(Led1,ledfade);
        analogWrite(Led2,ledfade); //ECRITURE EN PWM SUR LES LEDS
        analogWrite(Led3,ledfade);
        analogWrite(Led4,ledfade);
        analogWrite(Led5,ledfade);
        }
        else
        {
          digitalWrite(Led1,LOW);
          digitalWrite(Led2,LOW); //EXTINCTION EN CAS DE NON PRESENCE OU DE TIMER TERMINE
          digitalWrite(Led3,LOW);
          digitalWrite(Led4,LOW);
          digitalWrite(Led5,LOW);
        }
        //Serial.print("Timer : ");
        //Serial.println(timer);
        timer--; //DECOMPTE DU TIMER
        presence = detection_mouvement(); //LECTURE DE LA PRESENCE
  }
  digitalWrite(Led1,LOW);
  digitalWrite(Led2,LOW); //EXTINCTION EN CAS DE NON PRESENCE OU DE TIMER TERMINE
  digitalWrite(Led3,LOW);
  digitalWrite(Led4,LOW);
  digitalWrite(Led5,LOW);
  delay(1000);
}

int detection_mouvement()
{
  int irlowrange = digitalRead(irae);
  int irwiderange = digitalRead(pir);
  //Serial.print("IR LOW RANGE: ");
  //Serial.println(irlowrange);
  //Serial.print("IR WIDE RANGE: ");
  //Serial.println(irwiderange);
  //Serial.println();
  //Serial.println(irwiderange);
  if(irwiderange == HIGH) //|| irlowrange == HIGH)
  {
    timer = 10000; //6 secondes par exemple
    return 1;
  }
  return 0;
}

