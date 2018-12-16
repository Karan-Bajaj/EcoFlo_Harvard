#include <SoftwareSerial.h>
#include <string.h>
//SoftwareSerial mySerial(10, 11); // RX, TX

void setup() {

  pinMode(8, OUTPUT);
  Serial.begin(9600);
  /*
  while(!Serial){
    Serial.println("Waiting for Serial connection...");
  }
  */
}

void loop() {

  using namespace std;

  int maxVal = analogRead(2); //Node at potentiometer's centre output pin
  int centreVal = analogRead(0);  //Node between resistor and motor

  float voltage = maxVal * (5.0/1023.0) * 1000; //Calculated in mV by scaling potentiometer's supply between 0-5v
  float current = ((maxVal - centreVal) * (5.0/1023.0))/4.7 * 1000; //Calculated by taking volt. drop across 4.7 ohm resistor and dividing by resistance 

  int power = abs((voltage * current)/1000); //in mW
  if(power==0) 
  {
    power=1;
  }
 
  char intStr[3];
  String str= itoa(power,intStr,10);    //Convert power as int to string to transmit serially to raspberry pi
  //String str = String(intStr);

  while(!Serial.read()=="DATA")   //Raspberry pi ready to read data
  {
  }
  
  Serial.println(str);
//  Serial.println("45");
  digitalWrite(8, HIGH);     //Control status LED
  delay(500);
  digitalWrite(8, LOW);
  delay(500);

}
