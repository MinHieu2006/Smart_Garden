#include <ESP8266WiFi.h>
#include "DHT.h"           
//#include <SimpleTimer.h>
#define DHTTYPE DHT11

#define dht_dpin 5
DHT dht(dht_dpin, DHTTYPE); 
float t;
float h;
const int analogInPin = A0;
int sensorValue = 0; 
void setup()
{
    Serial.begin(9600);
    dht.begin();

  pinMode(D7, OUTPUT);
  pinMode(D9, OUTPUT);
    digitalWrite(D7,LOW);
  digitalWrite(D9,LOW); 
  pinMode(LED_BUILTIN, OUTPUT);
  float h = dht.readHumidity();
  float t = dht.readTemperature(); 
  
  Serial.println("Humidity and temperature\n\n");
  Serial.print("Current humidity = ");
  Serial.print(h);
  Serial.print("%  ");
  Serial.print("temperature = ");
  Serial.print(t); 
  sensorValue = int(analogRead(analogInPin)*100/1023);
 
  // print the readings in the Serial Monitor
  Serial.print("sensor = ");
  Serial.print(sensorValue);
   delay(3000);
       digitalWrite(D7,HIGH);
  digitalWrite(D9,HIGH); 
}
void loop()
{  
  digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
  delay(1000);                       // wait for a second
  digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
  delay(1000);                       // wait for a second
  delay(1000);
  float h = dht.readHumidity();
  float t = dht.readTemperature(); 
  Serial.println("Humidity and temperature\n\n");
  Serial.print("Current humidity = ");
  Serial.print(h);
  Serial.print("%  ");
  Serial.print("temperature = ");
  Serial.println(t); 
  sensorValue = int(analogRead(analogInPin)*1/3.2);
  Serial.print("Do am la:\n");
  Serial.println(sensorValue);
}
