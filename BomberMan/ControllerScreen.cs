using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ControllerScreen : Screen
{
    Image imgController;
    Audio audio;
    bool exit = false;

    public ControllerScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        audio = new Audio(44100, 2, 4096);
        audio.AddWAV("music/reset.wav");
        imgController = new Image("imgs/ControllerScreen.png", 840, 755);
        imgController.MoveTo(0,0);
    }

    public override void Show()
    {

        bool enterPressed = false;
        bool escPressed = false;
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgController);
            hardware.UpdateScreen();

            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_ESC)
            {
                escPressed = true;
                exit = false;
            }
            else if (keyPressed == Hardware.KEY_ENTER)
            {
                enterPressed = true;
                exit = false;
            }

        } while (!escPressed && !enterPressed);
            
    }
}