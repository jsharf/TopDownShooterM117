import processing.serial.*;

char posX, posY;
char buttons;

Serial myPort;

void setup()
{
  // List all the available serial ports:
  println(Serial.list());
  // Open the port you are using at the rate you want:
  myPort = new Serial(this, Serial.list()[2], 9600);
  size(300, 150);
}

void draw()
{
    int posX = 0, posY = 0;
    char lf = '\n';
    byte[] inBuffer = new byte[5];
    myPort.readBytesUntil(lf, inBuffer);
    if (inBuffer != null)
    {
       posX = inBuffer[0];
       posY = inBuffer[1];
       
    }

  rect(150 - 250/2, 75 - 100/2, 250, 100, 7);
  ellipse(210, 100, 30, 30);
  ellipse(240, 75, 30, 30);
  ellipse(180, 75, 30, 30);
  ellipse(210, 50, 30, 30);
  ellipse(75, 75, 80, 80);
  ellipse(75 + posX, 75 + posY, 10, 10);

}
