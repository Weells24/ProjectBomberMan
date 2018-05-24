using System;
using System.Collections;
using Tao.Sdl;


class MapCreationScreen : Screen
{
    Font font36;
    IntPtr fontEdition;
    Sdl.SDL_Color white;
    Image img;
    string[] text;


    public MapCreationScreen(Hardware hardware) : base(hardware)
    {
        font36 = new Font("font/Joystix.ttf", 36);
        img = new Image("imgs/CreditsScreen.png", 840, 755);
        white = new Sdl.SDL_Color(255, 255, 255);
        text = new string[16];
    }

    public void WriteLetter()
    {
        
    }

    public override void Show()
    {
        bool escPressed = false;
        ArrayList text = new ArrayList();
        char letter;

        do
        {
            hardware.ClearScreen();
            int keyPressed = hardware.KeyPressed();
            letter = hardware.ReadLetter(keyPressed);

            fontEdition = SdlTtf.TTF_RenderText_Solid(font36.GetFontType(),
                            letter.ToString(), white);
            hardware.WriteText(fontEdition, 330, 10);
            hardware.UpdateScreen();

            
            if (keyPressed == Hardware.KEY_ESC)
                escPressed = true;

        } while (!escPressed);
    }
}