 void setup() {
   Serial.begin(9600);

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
    //Valores intervalo se envia el bus
    static uint32_t premillis=0;
    static uint32_t interval=100;
    //bus de datos: [Total de datos, b1, b2, chs]
    static uint8_t bufferTx[4];
    
    static const uint8_t movC_Tx[] = {0x03, 0x00, 0x01, 0x01};
    static const uint8_t movR_Tx[] = {0x05, 0x0F, 0x05, 0x06, 0x07, 0x21}; 
    static const uint8_t movL_Tx[] = {0x04, 0xEE 0x09, 0x02, 0xF9};
    static const uint8_t movRs_Tx[] = {0x06, 0x01, 0xDD, 0x0C, 0x05, 0x06, 0xF5}; 
    static const uint8_t movLs_Tx[] = {0x02, 0xCD, 0xCD};  

    static auto state = serialStates::waitLen;

    switch(state){
      case serialStates::waitLen:
        break;
      case serialStates::waitData:
        break;
    }
  }
  


unsigned int Checksum(uint8_t *arr){
  uint8_t total = *(arr+1);
  for (uint8_t i = 2; *arr > i; i++){
    total += *(arr+i);
  }
  return total;
}
