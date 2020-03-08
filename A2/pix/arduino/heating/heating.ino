//CAPTEUR D'HUMIDITE DHT22

#include <dht.h>
dht DHT;
#define DHT22_PIN 5

struct
{
    uint32_t total;
    uint32_t ok;
    uint32_t crc_error;
    uint32_t time_out;
    uint32_t connect;
    uint32_t ack_l;
    uint32_t ack_h;
    uint32_t unknown;
} 
stat = {0,0,0,0,0,0,0,0};

//SETUP PIR
const int pir = 2;
const int irae = 4;

//SETUP PINS ACTIONNEURS

const int fan = 13;
const int radiateur = 8;

//SEUILS

const float seuiltempLow = 19.0; //19° mini
const float seuiltempHigh = 21.0; //21° maxi

const int stopbutton = 12;
int timer = 0;

void setup()
{
    Serial.begin(115200);
    pinMode(fan,OUTPUT); //ACTIONNEURS EN SORTIE (Ventilateur, Radiateur)
    pinMode(radiateur,OUTPUT);
    pinMode(pir,INPUT);
    pinMode(irae,INPUT);
    pinMode(stopbutton,INPUT);
}

void loop()
{
  int arret = digitalRead(stopbutton);
    // -------------- DHT22 --------------
    uint32_t start = micros();
    int chk = DHT.read22(DHT22_PIN);
    uint32_t stop = micros();
    stat.total++;
    switch (chk)
    {
    case DHTLIB_OK:
        stat.ok++;
        break;
    case DHTLIB_ERROR_CHECKSUM:
        stat.crc_error++;
        Serial.print("Checksum error,\t");
        break;
    case DHTLIB_ERROR_TIMEOUT:
        stat.time_out++;
        Serial.print("Time out error,\t");
        break;
    default:
        stat.unknown++;
        Serial.print("Unknown error,\t");
        break;
    }
    int presence = detection_mouvement();
    while(presence == 1 || timer != 0)
    {
      Serial.print("Temperature :");
      Serial.print(DHT.temperature);
      Temperature(seuiltempLow,seuiltempHigh);
      timer--;
      Serial.print("\tTimer :");
      Serial.print(timer);
      Serial.print("\tPresence :");
      Serial.print(presence);
      Serial.println();
      arret = digitalRead(stopbutton);
      //Serial.println(arret);
      if(arret == LOW) //BOUTON APPUYE
      {
        digitalWrite(fan,LOW);
        digitalWrite(radiateur,LOW);
        delay(10000);
      }
      else
      {
        presence = detection_mouvement();
      }
    }
      digitalWrite(fan,LOW);
      digitalWrite(radiateur,LOW);
      delay(1000);
}

void Temperature(float seuiltempLow, float seuiltempHigh)
{
  if(DHT.temperature < seuiltempLow || DHT.temperature > seuiltempHigh)
  {
    if(DHT.temperature < seuiltempLow)
    {
      digitalWrite(radiateur,HIGH);
    }
    if(DHT.temperature > seuiltempHigh)
    {
      digitalWrite(fan,HIGH);
    }
  }
  else
  {
    digitalWrite(radiateur,LOW);
    digitalWrite(fan,LOW);
  }
}

int detection_mouvement()
{
  int irlowrange = digitalRead(irae);
  int irwiderange = digitalRead(pir);
  //Serial.println(irwiderange);
  if(irwiderange == HIGH)// || irlowrange == HIGH)
  {
    timer = 5000; //6 secondes par exemple
    return 1;
  }
  return 0;
}
