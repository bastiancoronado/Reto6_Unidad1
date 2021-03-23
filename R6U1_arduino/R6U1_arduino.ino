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
   task_serialASCII();
   //task_serialASCII_reload();
 }

void task_serialASCII(){
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
          if (value == 2)Serial.println("Fire&Reload");
          else if (pin_fire.pinValue() == 1)Serial.println("Fire");
          else Serial.println("Reload");
          state = serialStates::Wait;
        }
        break;
      case serialStates::Wait:
        if(Serial.available() > 0){
          char dataRX = (char)Serial.read();        
          if (dataRX == 'f' || dataRX == 'r' || dataRX == '&')state = serialStates::Action;
        }
        break;
    }
}
