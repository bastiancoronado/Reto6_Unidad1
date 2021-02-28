 void setup() {
   Serial.begin(9600);

 }

 void loop() {
   //vamos a transmitir el 16205

   static uint16_t x = 0xFF;

   if (Serial.available()) {
     if (Serial.read() == 's') {
       Serial.write((uint8_t)( x & 0x00FF));
       Serial.write( (uint8_t)( x >> 8 ));
     }
   }
 }
