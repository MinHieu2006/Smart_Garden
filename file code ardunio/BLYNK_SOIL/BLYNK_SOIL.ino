#define BLYNK_PRINT Serial
#include <ESP8266WiFi.h>
#include <BlynkSimpleEsp8266.h>
#include "DHT.h"           
//#include <SimpleTimer.h>
#define DHTTYPE DHT11

#define dht_dpin 5
DHT dht(dht_dpin, DHTTYPE); 
BlynkTimer timer;
char auth[] = "vrpnAYLzJmm8lI7YOmTxp0e-8aIToBax";
char ssid[] = "TP-LINK";
char pass[] = "hoangthiha000";
float t;
float h;
const int analogInPin = A0;
int sensorValue = 0; 
void setup()
{
    Serial.begin(9600);
    Blynk.begin(auth, ssid, pass);
    dht.begin();
    timer.setInterval(2000, sendUptime);
}

void sendUptime()
{
  float h = dht.readHumidity();
  float t = dht.readTemperature(); 
  Serial.println("Humidity and temperature\n\n");
  Serial.print("Current humidity = ");
  Serial.print(h);
  Serial.print("%  ");
  Serial.print("temperature = ");
  Serial.print(t); 
  Blynk.virtualWrite(V6, t);
  Blynk.virtualWrite(V5, h);
  sensorValue = int(analogRead(analogInPin)*100/1023);
 
  // print the readings in the Serial Monitor
  Serial.print("sensor = ");
  Serial.print(sensorValue);
  Blynk.virtualWrite(V7, sensorValue);
  
}

void loop()
{
  Blynk.run();
  timer.run();
}
