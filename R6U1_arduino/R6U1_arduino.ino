class PIN{
  private:
    uint8_t pin;
    uint8_t value;

  public:
    PIN(uint8_t _pin): pin(_pin){
      pinMode(_pin,INPUT);
      value = 0;
    }

    uint8_t pinValue(){
      uint8_t v = digitalRead(pin);
      return v;  
    }
};


unsigned long last_time = 0;
 void setup() {
   Serial.begin(115200);
   
 }

 void loop() {
   task_SerialASCII();
   //task_serialASCII_reload();
 }

void task_SerialASCII(){
      enum class serialStates{
      Action,
      Wait
    };

    static PIN pin_fire(7);
    static PIN pin_reload(8);
    static auto state = serialStates::Action;

    uint8_t value = (pin_fire.pinValue() + pin_reload.pinValue());

    switch(state){
      case serialStates::Action:
        if(value > 0){
          if (value == 2)Serial.println("Front&Back");
          else if (pin_fire.pinValue() == 1)Serial.println("Front");
          else Serial.println("Back");
          state = serialStates::Wait;
        }
        break;
      case serialStates::Wait:
        if(Serial.available() > 0){
          char dataRX = (char)Serial.read();        
          if (dataRX == 'f' || dataRX == 'b' || dataRX == '&')state = serialStates::Action;
        }
        break;
    }
}
