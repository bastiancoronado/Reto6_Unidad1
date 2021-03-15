 void setup() {
   Serial.begin(115200);
 }

 void loop() {
   task_Componente();
 }

  void task_Componente(){
    enum class serialStates{
      waitLen,
      waitData
    };
    //Pin y valor de entrada del potenciometro
    static const uint8_t ledPin=34;
    static uint16_t value = 0;
    //bus de datos: [Total de datos, b1, b2, chs]
    static uint8_t bufferTx[8] = {0};
    static uint8_t dataCounter = 0;
    
    static const uint8_t movC_Tx[] = {0x03, 0x00, 0x01, 0x01};
    static const uint8_t movR_Tx[] = {0x05, 0x0F, 0x05, 0x06, 0x07, 0x21}; 
    static const uint8_t movL_Tx[] = {0x04, 0xEE, 0x09, 0x02, 0xF9};
    static const uint8_t movRs_Tx[] = {0x06, 0x01, 0xDD, 0x0C, 0x05, 0x06, 0xF5}; 
    static const uint8_t movLs_Tx[] = {0x02, 0xCD, 0xCD};  
    static const uint8_t fail[] = {0x02, 0x00, 0x00};
    
    static auto state = serialStates::waitLen;

    switch(state){
      case serialStates::waitLen:
        if(Serial.available()){
          bufferTx[dataCounter] = Serial.read();
          dataCounter ++;
          state = serialStates::waitData;
        }
        break;
      case serialStates::waitData:
        while(Serial.available()){
          //acomodando cada dato dentro del arreglo
          bufferTx[dataCounter] = Serial.read();
          dataCounter++;
          if(bufferTx[0] == dataCounter-1){
           //llegaron todos los datos 
           uint8_t cksm = Checksum(bufferTx);
           if(bufferTx[dataCounter-1] == cksm){
            
            value = analogRead(ledPin);
            uint8_t newVal = map(value, 0, 4095, 0, 4);
            
            switch(newVal){
              case 0:
                Serial.write(movR_Tx, sizeof(movR_Tx));
                break;
              case 1:
                Serial.write(movRs_Tx, sizeof(movRs_Tx));
                break;
              case 2:
                Serial.write(movC_Tx, sizeof(movC_Tx));
                break;
              case 3:
                Serial.write(movLs_Tx, sizeof(movLs_Tx));
                break;
              case 4:
                Serial.write(movL_Tx, sizeof(movL_Tx));
                break;            
            }
           }
           else{
            Serial.write(fail, sizeof(fail));
           }
           dataCounter = 0;
           state = serialStates::waitLen;           
          }
        }
        break;
    }
  }
  
uint8_t Checksum(uint8_t *arr){
  byte total = *(arr+1);
  for (uint8_t i = 2; *arr > i; i++){
    total += *(arr+i);
  }
  return total;
}
