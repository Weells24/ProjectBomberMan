using System;
using Tao.Sdl;
using System.Collections.Generic;

class SelectLenguage : Screen
{
    Image imgChosenOption, imgWelcome;
    int chosenOption = 1;
    Audio audio;
    bool exit;
    Font font;
    IntPtr fontSpanish, fontPrueba, fontEnglish;
    Sdl.SDL_Color yellow;
    Screen select;
    public static int option { get; set; }

    public SelectLenguage(Hardware hardware) : base(hardware)
    {
        exit = false;
        audio = new Audio(44100, 2, 4096);
        audio.AddWAV("music/reset.wav");
        imgWelcome =
                new Image("imgs/MenuPrincipal.png", 840, 755);
        imgChosenOption =
            new Image("imgs/BombMenu1.png", 50, 50);
        font = new Font("font/Joystix.ttf", 28);
        imgWelcome.MoveTo(0, 0);
        imgChosenOption.MoveTo(245, 420);
        yellow = new Sdl.SDL_Color(255, 255, 0);
    }

    public override void Show()
    {
        //add
        bool enterPressed = false;
        Lenguage len = new Lenguage();
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgWelcome);
            hardware.DrawImage(imgChosenOption);

            fontSpanish = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                            len.Change(option,19), yellow);
            hardware.WriteText(fontSpanish, 325, 430);
            fontEnglish = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                             len.Change(option,18), yellow);
            hardware.WriteText(fontEnglish, 325, 475);
            
            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_UP && chosenOption > 1)
            {
                audio.PlayWAV(0, 1, 0);
                chosenOption--;
                option = 0;
                imgChosenOption.MoveTo(245, (short)(imgChosenOption.Y - 45));
            }
            else if (keyPressed == Hardware.KEY_DOWN && chosenOption < 2)
            {
                audio.PlayWAV(0, 1, 0);
                chosenOption++;
                option = 1;
                imgChosenOption.MoveTo(245, (short)(imgChosenOption.Y + 45));
            }
            else if (keyPressed == Hardware.KEY_ENTER && chosenOption == 1)
            {
                enterPressed = true;
                exit = true;
            }
            else if (keyPressed == Hardware.KEY_ENTER && chosenOption == 2)
            {
                enterPressed = true;
                exit = true;
            }
            hardware.UpdateScreen();
        }
        while (!enterPressed);
    }
}

