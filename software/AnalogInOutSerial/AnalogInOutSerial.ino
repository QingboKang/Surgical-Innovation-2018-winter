/*
  Analog input, analog output, serial output

  Reads an analog input pin, maps the result to a range from 0 to 255 and uses
  the result to set the pulse width modulation (PWM) of an output pin.
  Also prints the results to the Serial Monitor.

  The circuit:
  - potentiometer connected to analog pin 0.
    Center pin of the potentiometer goes to the analog pin.
    side pins of the potentiometer go to +5V and ground
  - LED connected from digital pin 9 to ground

  created 29 Dec. 2008
  modified 9 Apr 2012
  by Tom Igoe

  This example code is in the public domain.

  http://www.arduino.cc/en/Tutorial/AnalogInOutSerial
*/

// These constants won't change. They're used to give names to the pins used:
const int analogInPin = A3;  // Analog input pin that the potentiometer is attached to


int sensorValue = 0;        // value read from the pot


void setup() {
  // initialize serial communications at 9600 bps:
  Serial.begin(9600);
}

void loop() {

  char val = Serial.read();

  int iTimes = 0;
  switch(val){
    // read one data
    case 'a':
      iTimes = 1;
      break;

    // read 8 times data
    case 'b':
      iTimes = 8;
      break;
  }
  
  int i = 0;
  while( i < iTimes )
  {
    i++;
    // read the analog in value:
    sensorValue = analogRead(analogInPin);
    // print the results to the Serial Monitor:
    Serial.print(sensorValue);
    Serial.print('\n');
    delay(10);
  }

  if( val == 'a' || val == 'b' )
  {
    Serial.print('\0');
  }

  // wait 2 milliseconds before the next loop for the analog-to-digital
  // converter to settle after the last reading:
  delay(2);
}
