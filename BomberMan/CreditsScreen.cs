using System;
using System.Collections.Generic;
using Tao.Sdl;


class CreditsScreen : Screen
{
    bool exit;
    //Audio audio;
    Font font24, font32;
    Image imgCredits;
    IntPtr fontTexts;
    Sdl.SDL_Color green;
    Sdl.SDL_Color red;

    protected short yText = 40;
    protected short startY = 720;
        

    public CreditsScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        font24 = new Font("font/Joystix.ttf", 24);
        font32 = new Font("font/Joystix.ttf", 32);
        green = new Sdl.SDL_Color(0, 255, 0);
        red = new Sdl.SDL_Color(255, 20,0 );
        imgCredits = new Image("imgs/CreditsScreen.png",840,745);
    }

    protected string[] credits = {
        "Credits",
        "-- Original Game --", "Brandon Blasco",
        "-- Version Game --"," V.04",
        "-- Year Game -- ","2018",
        " ",
        "-- Records --","100 points - Brandon Blasco",
        " ",
        "-- Remakers-- ", "Raul Gogna",
        " ",
        "-- Proyect Boss --","Nacho Cabanes"
    };

    public override void Show()
    {
        bool escPressed = false;
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgCredits);
            fontTexts = SdlTtf.TTF_RenderText_Solid(font32.GetFontType(),
                            credits[0], green);
            hardware.WriteText(fontTexts, 330, 10);
            yText = 40;
            for (int i = 1; i < credits.Length; i++)
            {
                fontTexts = SdlTtf.TTF_RenderText_Solid(font24.GetFontType(),
                    credits[i], red);
                hardware.WriteText(fontTexts, 250, (short)(startY + yText));
                yText += 20;
            }
            hardware.UpdateScreen();

            int keyPressed = hardware.KeyPressed();
            hardware.Pause(20);
            if (keyPressed == Hardware.KEY_ESC)
            {
                startY = 720;
                escPressed = true;
                exit = true;
            }
            if (startY > 100)
                startY -= 2;
        } while (!escPressed || !exit);
    }
}