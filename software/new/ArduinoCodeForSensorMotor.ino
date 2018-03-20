#include <Servo.h>

Servo myservo;
const int analogInPin = A3;  
int sensorValue = 0;        
int iTimes = 0;

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
  Serial.println(avg);
}
void doFullTurn()
{
  for(int i=0; i<8;i++)
  {
  myservo.write(60);
  delay(200);
  myservo.write(90); 
  MeasureDistance();
  }
}
void doTurnBack()
{
  myservo.write(120);
  delay(1500);
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
      break;
  }
}
  
}
