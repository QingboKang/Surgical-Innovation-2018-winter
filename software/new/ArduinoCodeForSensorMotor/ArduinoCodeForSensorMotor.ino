#include <Servo.h>

Servo myservo;
const int analogInPin = A3;  
int sensorValue = 0;        
int iTimes = 10;

void setup() 
{
  Serial.begin(250000);
  myservo.attach(9);
   pinMode(12, OUTPUT);
  digitalWrite(12, HIGH);
}
void MeasureDistance()
{
  int i = 0;
  double avg=0;
  while( i < iTimes )
  {
    i++;
    // read the analog in value:
    avg += analogRead(analogInPin);
    delay(10);
  }
  avg=avg/10.0; //Calculate the average
  Serial.print(avg);
  Serial.print(",");
}
void doFullTurn()
{
  for(int i=0; i<36;i++)
  {
  myservo.write(80);
  delay(135);
  myservo.write(90); 
  delay(50);
  MeasureDistance();
  }
}
void doTurnBack()
{
  myservo.write(120);
  delay(1600);
  myservo.write(90); 
}
void loop() {
while(Serial.available())
{
  char cmd=Serial.read();
  switch(cmd)
  {
    case 'a':
      iTimes = 10;
      doFullTurn();
      doTurnBack();
      Serial.print("\n");
      break;
      case 'b':
      MeasureDistance();
      Serial.print("\n");
      break;
  }
}
  
}
