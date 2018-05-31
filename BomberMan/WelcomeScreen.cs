using System;
using Tao.Sdl;

class WelcomeScreen : Screen
{
    bool exit;
    Image imgWelcome, imgWelcome1, imgChosenOption;
    //add
    int chosenOption = 1;
    Audio audio;
    Font font;
    IntPtr play, controls, creation, ex, credits;
    SelectLenguage select;
    Sdl.SDL_Color white;

    public WelcomeScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        audio = new Audio(44100, 2, 4096);
        audio.AddWAV("music/reset.wav");
        font = new Font("font/Joystix.ttf", 28);
        imgWelcome =
                new Image("imgs/MenuPrincipal.png", 840, 755);
        imgWelcome1 =
                new Image("imgs/CreditsScreen.png", 840, 755);
        imgChosenOption =
                new Image("imgs/BombMenu1.png", 50, 50);
        imgWelcome.MoveTo(0, 0);
        imgWelcome1.MoveTo(0, 0);
        imgChosenOption.MoveTo(245, 420);
        select = new SelectLenguage(hardware);
        white = new Sdl.SDL_Color(255, 255, 255);

    }

    public void Show()
    {
        //add
        bool enterPressed = false;
        bool escPressed = false;
        int option = select.GetOption();
        Console.WriteLine(option);

        do
        {
            hardware.ClearScreen();
            if (option == 1)
                hardware.DrawImage(imgWelcome1);
            else
                hardware.DrawImage(imgWelcome);
            hardware.DrawImage(imgChosenOption);
            hardware.UpdateScreen();

            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_UP && chosenOption > 1)
            {
                audio.PlayWAV(0, 1, 0);
                chosenOption--;
                imgChosenOption.MoveTo(245, (short)(imgChosenOption.Y - 45));
            }
            else if (keyPressed == Hardware.KEY_DOWN && chosenOption < 5)
            {
                audio.PlayWAV(0, 1, 0);
                chosenOption++;
                imgChosenOption.MoveTo(245, (short)(imgChosenOption.Y + 45));
            }
            else if (keyPressed == Hardware.KEY_ENTER && chosenOption == 5)
            {
                enterPressed = true;
                exit = true;
            }
            else if (keyPressed == Hardware.KEY_ENTER)
            {
                enterPressed = true;
                exit = false;
            }
        }
        while (!enterPressed);
    }

    public bool GetExit()
    {
        return exit;
    }

    public int GetChosenOption()
    {
        return chosenOption;
    }
}