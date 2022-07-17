/*  ___   ___  ___  _   _  ___   ___   ____ ___  ____  
 * / _ \ /___)/ _ \| | | |/ _ \ / _ \ / ___) _ \|    \ 
 *| |_| |___ | |_| | |_| | |_| | |_| ( (__| |_| | | | |
 * \___/(___/ \___/ \__  |\___/ \___(_)____)___/|_|_|_|
 *                  (____/ 
 * Use NodeMCU as Web Server to control LED and display remote temperature and humidity from DHT11 sensor
 * Tutorial URL http://osoyoo.com/2018/09/07/lesson-18-use-nodemcu-as-iot-web-server/
 * CopyRight www.osoyoo.com
 */
#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WebServer.h>
#include "DHT.h"           
//#include <SimpleTimer.h>
#define DHTTYPE DHT11

#define dht_dpin 5
DHT dht(dht_dpin, DHTTYPE); 
float t;
float h;
const int analogInPin = A0;
// Replace with your network credentials
const char* ssid = "TP-LINK"; //replace ssid value with your own wifi hotspot name
const char* password = "hoangthiha1995"; //replace password value with your wifi password
 
ESP8266WebServer server(80);   //instantiate server at port 80 (http port)
 
String page = "";
int LEDPin = D8;
void setup(void){
  //the HTML of the web page

  //make the LED pin output and initially turned off
  pinMode(LEDPin, OUTPUT);
  digitalWrite(LEDPin, LOW);
   dht.begin();
     float h = dht.readHumidity();
  float t = dht.readTemperature();
  delay(1000);
  Serial.begin(115200);
  WiFi.begin(ssid, password); //begin WiFi connection
  Serial.println("");
 
  // Wait for connection
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to ");
  Serial.println(ssid);
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());

 
   
  server.on("/", [](){
    server.send(200, "text/html", page);
  });
  server.on("/TurnOnLED", [](){
    page = "<h1>NodeMCU Web Server for IOT</h1><p><strong>LED is ON</strong></p>";
  page +="<p><a href=\"TurnOnLED\"><button>Turn on LED</button></a>&nbsp;<a href=\"TurnOffLED\"><button>Turn off LED</button></a></p>";
  page +="<p><a href=\"GetTempHumid\"><button>Display Temperature and Humidity</button>";
    server.send(200, "text/html", page);
    digitalWrite(LEDPin, HIGH);
    delay(1000);
  });
  server.on("/TurnOffLED", [](){
      page = "<h1>NodeMCU Web Server for IOT</h1><p><strong>LED is OFF</strong></p>";
  page +="<p><a href=\"TurnOnLED\"><button>Turn on LED</button></a>&nbsp;<a href=\"TurnOffLED\"><button>Turn off LED</button></a></p>";
  page +="<p><a href=\"GetTempHumid\"><button>Display Temperature and Humidity</button>";
    server.send(200, "text/html", page);
    digitalWrite(LEDPin, LOW);
    delay(1000); 
  });
   server.on("/GetTempHumid", [](){
    //  int chk = DHT.read11(DHT11_PIN);
     String msg="<p>Real time temperature: ";
     msg= msg+ dht.readTemperature();
     msg = msg+" C ;real time Humidity: " ;
     msg=msg+dht.readHumidity();
     msg=msg+"%</p>";
 
   
  page = "<h1>NodeMCU Web Server for IOT</h1>"+msg;
  
  page +="<p><a href=\"TurnOnLED\"><button>Turn on LED</button></a>&nbsp;<a href=\"TurnOffLED\"><button>Turn off LED</button></a></p>";
  
  page +="<p><a href=\"GetTempHumid\"><button>Display Temperature and Humidity</button>";
    server.send(200, "text/html", page);
    delay(1000); 
  });
  server.begin();
  Serial.println("Web server started!");
}
 
void loop(void){
  server.handleClient();
  digitalWrite(LEDPin, LOW);
  delay(3000);
  digitalWrite(LEDPin, HIGH);
}
