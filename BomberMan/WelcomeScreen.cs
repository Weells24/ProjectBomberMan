using System;
using Tao.Sdl;

class WelcomeScreen : Screen
{
    bool exit;
    Image imgWelcome, imgChosenOption;
    //add
    int chosenOption = 1;
    Audio audio;
    Font font;
    Sdl.SDL_Color yellow;
    IntPtr fontPlay, fontCreation, fontControl, fontCredits, fontExit;
    Screen select;

    public WelcomeScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        audio = new Audio(44100, 2, 4096);
        audio.AddWAV("music/reset.wav");
        font = new Font("font/Joystix.ttf", 36);
        imgWelcome =
                new Image("imgs/MenuPrincipal.png", 840, 755);
        imgChosenOption =
                new Image("imgs/BombMenu1.png", 50, 50);
        imgWelcome.MoveTo(0, 0);
        imgChosenOption.MoveTo(245, 420);
        yellow = new Sdl.SDL_Color(255, 255, 0);
    }

    public void Show()
    {
        //add
        bool enterPressed = false;
        bool escPressed = false;
        Lenguage len = new Lenguage();
        int option = SelectLenguage.option;

        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgWelcome);
            hardware.DrawImage(imgChosenOption);
            fontPlay = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 0), yellow);
            hardware.WriteText(fontPlay, 325, 430);
            fontCreation = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 1), yellow);
            hardware.WriteText(fontCreation, 325, 475);
            fontControl = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 2), yellow);
            hardware.WriteText(fontControl, 325, 520);
            fontCredits = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 3), yellow);
            hardware.WriteText(fontCredits, 325, 565);
            fontExit = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 4), yellow);
            hardware.WriteText(fontExit, 325, 610);

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
            hardware.UpdateScreen();
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