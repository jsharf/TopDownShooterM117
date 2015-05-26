char lastADCX, lastADCY;

#define BUTTON0 0b00001
#define BUTTON1 0b00010
#define BUTTON2 0b00100
#define BUTTON3 0b01000
#define BUTTON4 0b10000

void setup()
{
  delay(100);
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(P2_0,INPUT);
  pinMode(P2_1,INPUT);
  pinMode(P2_2,INPUT);
  pinMode(P2_3,INPUT);
  pinMode(P1_5,INPUT_PULLUP);
  attachInterrupt(P2_0, Button0, RISING);
  attachInterrupt(P2_1, Button1, RISING);
  attachInterrupt(P2_2, Button2, RISING);
  attachInterrupt(P2_3, Button3, RISING);
  attachInterrupt(P1_5, Button4, FALLING);
  delay(100);
}

struct packet
{
  char x, y, buttons, parity;
};

//CRC-8 - based on the CRC8 formulas by Dallas/Maxim
//code released under the therms of the GNU GPL 3.0 license
char CRC8(const char *data, char len) {
  char crc = 0x00;
  while (len--) {
    char extract = *data++;
    for (char tempI = 8; tempI; tempI--) {
      char sum = (crc ^ extract) & 0x01;
      crc >>= 1;
      if (sum) {
        crc ^= 0x8C;
      }
      extract >>= 1;
    }
  }
  return crc;
}

void calcParity(struct packet *p)
{
    char *raw = (char *)p;
    p->parity = CRC8(raw, 3);
}

void sendPacket(struct packet *p)
{
  calcParity(p);
  Serial.write(p->x);
  Serial.write(p->y);
  Serial.write(p->buttons);
  Serial.write(p->parity);
    Serial.write("\n");
}

void initPacket(struct packet *p)
{
   p->x = lastADCX;
   p->y = lastADCY;
}

void Button0()
{
    struct packet p;
    initPacket(&p);
    p.buttons = BUTTON0;
    sendPacket(&p);
}

void Button1()
{
    struct packet p;
    initPacket(&p);
    p.buttons = BUTTON1;
    sendPacket(&p);
}

void Button2()
{
    struct packet p;
    initPacket(&p);
    p.buttons = BUTTON2;
    sendPacket(&p);
}

void Button3()
{
    struct packet p;
    initPacket(&p);
    p.buttons = BUTTON3;
    sendPacket(&p);
}

void Button4()
{
    struct packet p;
    initPacket(&p);
    p.buttons = BUTTON4;
    sendPacket(&p);
}

void ADC()
{
  int xValue, yValue;
  xValue = analogRead(P1_4) - 512;
  yValue = analogRead(P1_3) - 512;
  
  xValue >>= 2;
  yValue >>= 2;
  
  lastADCX = (char) xValue;
  lastADCY = (char) yValue;
  
  struct packet p;
  initPacket(&p);
  sendPacket(&p);
}

void loop()
{
  //ADC();
}


