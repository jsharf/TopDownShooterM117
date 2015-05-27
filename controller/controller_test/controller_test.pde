import processing.serial.*;

int posX, posY;
int buttons;

int BUTTON0 = 1;
int BUTTON1 = 2;
int BUTTON2 = 4;
int BUTTON3 = 8;
int BUTTON4 = 16;

Serial myPort;

void setup()
{
  // List all the available serial ports:
  println(Serial.list());
  // Open the port you are using at the rate you want:
  myPort = new Serial(this, Serial.list()[2], 9600);
  size(300, 150);
  posX = posY = 0;
  myPort.clear();
}

int counter = 0;
int header = 0;
int nPackets = 0;
void draw()
{
  int lf = '\n';
  
  if (myPort.available() > 0)
  {
    int num = myPort.read();
    if (num == lf && counter == 0)
    {
      header = 1;
      nPackets++;
      println("Packet #" + nPackets);
    }
    else
    {
       if (header == 1) 
       {
         counter = num;
         header = 0;
       }
       else
       {
         if (counter > 0)
         {
           if (counter == 4)
           {
              posX = num; 
           }
           if (counter == 3)
           {
              posY = num; 
           }
           if (counter == 2)
           {
              buttons = num; 
              println(num);
           }
           counter--; 
         }
       }
    }
  }


  rect(150 - 250/2, 75 - 100/2, 250, 100, 7);
  ellipse(210, 100, 30, 30);
  ellipse(240, 75, 30, 30);
  ellipse(180, 75, 30, 30);
  ellipse(210, 50, 30, 30);
  ellipse(75, 75, 80, 80);
  ellipse(75 + 80*posX/255, 75 + 80*posY/255, 10, 10);
  
  fill(204, 10, 5);
  
  if ((buttons & BUTTON0) != 0)
    ellipse(210, 100, 30, 30);
  if ((buttons & BUTTON1) != 0)
    ellipse(240, 75, 30, 30);
  if ((buttons & BUTTON2) != 0)
    ellipse(180, 75, 30, 30);
  if ((buttons & BUTTON3) != 0)
    ellipse(210, 50, 30, 30);
  if ((buttons & BUTTON4) != 0)
    ellipse(75 + 80*posX/255, 75 + 80*posY/255, 10, 10);
  
  fill(255, 255, 255);

}
