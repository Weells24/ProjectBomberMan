using System;
using System.Collections.Generic;
using Tao.Sdl;


struct Credits
{
    public string remaker;
}

class CreditsScreen : Screen
{
    const int SIZE = 3;
    bool exit;
    //Audio audio;
    Font font24, font32;
    Image imgCredits;
    IntPtr fontTexts, fontRemakers;
    Sdl.SDL_Color green, black, red;

    protected short yText = 40;
    protected short startY = 720;
        

    public CreditsScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        font24 = new Font("font/Joystix.ttf", 24);
        font32 = new Font("font/Joystix.ttf", 32);
        green = new Sdl.SDL_Color(0, 255, 0);
        black = new Sdl.SDL_Color(0, 0, 0);
        red = new Sdl.SDL_Color(255, 0, 0);
        imgCredits = new Image("imgs/CreditsScreen.png",840,755);
    }

    protected string[] textfixed = {
        "Credits",
        "-- Original Game --", "Brandon Blasco",
        "-- Version Game --"," V.04",
        "-- Year Game -- ","2018",
        " ",
        "-- Records --","100 points - Brandon Blasco",
        " ",
        "-- Remakers-- ", "",
        " ",
        "-- Proyect Boss --","Nacho Cabanes"
    };

    public override void Show()
    {
        Credits[] remakers = new Credits[SIZE];
        remakers[0].remaker = "Raul Gogna";
        remakers[1].remaker = "Guillermo Pastor";
        remakers[2].remaker = "Pepito Grillo";

        bool escPressed = false;
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgCredits);
            fontTexts = SdlTtf.TTF_RenderText_Solid(font32.GetFontType(),
                            textfixed[0], green);
            hardware.WriteText(fontTexts, 330, 10);
            yText = 40;
            for (int i = 1; i < textfixed.Length; i++)
            {
                fontTexts = SdlTtf.TTF_RenderText_Solid(font24.GetFontType(),
                    textfixed[i], black);
                hardware.WriteText(fontTexts, 250, (short)(startY + yText));
                fontTexts = SdlTtf.TTF_RenderText_Solid(font24.GetFontType(),
                    textfixed[i], red);
                hardware.WriteText(fontTexts, 248, (short)(startY + yText));
                if (i == 12)
                {
                    for (int j = 0; j < remakers.Length; j++)
                    {
                        fontRemakers = SdlTtf.TTF_RenderText_Solid(font24.GetFontType(),
                            remakers[j].remaker, black);
                        hardware.WriteText(fontRemakers, 250, (short)(startY + yText));
                        fontRemakers = SdlTtf.TTF_RenderText_Solid(font24.GetFontType(),
                            remakers[j].remaker, red);
                        hardware.WriteText(fontRemakers, 248, (short)(startY + yText));
                        yText += 20;
                    }
                }
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
                startY -= 5;
        } while (!escPressed || !exit);
    }
}