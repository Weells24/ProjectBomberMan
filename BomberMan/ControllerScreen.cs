using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ControllerScreen : Screen
{
    Image imgController/*,imgChosenOption*/;
    //int chosenOption = 1;
    Audio audio;
    bool exit = false;

    public ControllerScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        audio = new Audio(44100, 2, 4096);
        audio.AddWAV("music/reset.wav");
        imgController = new Image("imgs/SettingsScreen.png", 800, 700);
        //imgChosenOption = new Image("imgs/BombMenu1.png", 50, 50);
        imgController.MoveTo(0,0);
        //imgChosenOption.MoveTo(240, 390);
    }

    public override void Show()
    {

        bool enterPressed = false;
        bool escPressed = false;
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgController);
            //hardware.DrawImage(imgChosenOption);
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