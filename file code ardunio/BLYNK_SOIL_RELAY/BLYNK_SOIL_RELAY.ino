#define BLYNK_PRINT Serial
#include <SimpleTimer.h>
#include <ESP8266WiFi.h>
#include <BlynkSimpleEsp8266.h>

SimpleTimer timer;
#define D5 14
#define D0 16
bool BT0=0;
bool BT1=0;

// You should get Auth Token in the Blynk App.
// Go to the Project Settings (nut icon).
char auth[] = "vrpnAYLzJmm8lI7YOmTxp0e-8aIToBax";

// Your WiFi credentials.
// Set password to "" for open networks.
char ssid[] = "TP-LINK";
char pass[] = "hoangthiha1995";

BLYNK_CONNECTED() {
 
  Blynk.syncVirtual(V0); 
  Blynk.syncVirtual(V1);

}

BLYNK_WRITE(V0) {
  int pinData_0 = param.asInt();
  BT0 = pinData_0;
}
BLYNK_WRITE(V1) {//bom
  int pinData_1 = param.asInt();
  BT1 = pinData_1;
}

void setup() {
  Serial.begin(9600);
  //Blynk.begin(auth, ssid, pass); 
  Blynk.begin(auth, ssid, pass);
  pinMode(D5, OUTPUT);
  pinMode(D0, OUTPUT);
  digitalWrite(D5,LOW);
  digitalWrite(D0,LOW); 
}

void loop() {
  Blynk.run();
  timer.run();
  if (BT0 == 1){
    Serial.println("BAT BOM 0");
    digitalWrite(D5,HIGH);
  }
  else {
    Serial.println("TAT BOM 0");
  digitalWrite(D5,LOW);
  }

    if (BT1 == 1){
    Serial.println("BAT BOM 1");
    digitalWrite(D0,HIGH);
  }
  else {
    Serial.println("TAT BOM 1");
  digitalWrite(D0,LOW);
  }
}
