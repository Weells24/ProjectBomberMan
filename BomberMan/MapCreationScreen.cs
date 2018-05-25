using System;
using System.Collections;
using Tao.Sdl;
using System.Threading;
using System.IO;


class MapCreationScreen : Screen
{
    const int WIDTH = 21;
    const int HEIGHT = 16;

    Font font20;
    IntPtr fontEdition;

    Sdl.SDL_Color white;
    Image imgMap,  imgFloor;
    char[][] map = new char[HEIGHT][];


    public MapCreationScreen(Hardware hardware) : base(hardware)
    {
        font20 = new Font("font/Joystix.ttf", 20);
        imgMap = new Image("imgs/MapScreen.png", 840, 755);
        imgFloor = new Image("imgs/Floor.png", 840, 680);
        imgFloor.MoveTo(0, 0);
        white = new Sdl.SDL_Color(255, 255, 255);
    }

    public override void Show()
    {
        bool escPressed = false;
        char[] letter = new char[12];
        int numLetter = 0;
        short XMap = 250;
        const short YMAP = 250;
        const short XFIXED = 14;
        string filename;

        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgMap);
            hardware.DrawImage(imgFloor);

            letter[numLetter] = hardware.ReadLetter();
            XMap += XFIXED;
            numLetter++;
            fontEdition = SdlTtf.TTF_RenderText_Solid(font20.GetFontType(),
                        letter[numLetter].ToString(), white);
            hardware.WriteText(fontEdition, XMap, YMAP);
            hardware.UpdateScreen();
            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_DELETE)
            {
                numLetter--;
                XMap -= XFIXED;
                letter[numLetter] = hardware.ReadLetter();
            }
            if (keyPressed == Hardware.KEY_ENTER)
            {
                Console.WriteLine("bien");//filename = new string(letter);
            }
            
            if (keyPressed == Hardware.KEY_SPACE || letter.Length == 11)
            {
                Console.WriteLine("Quieres guardar?");
                escPressed = true;
            }
            if (keyPressed == Hardware.KEY_ESC)
                escPressed = true;

            
        } while (!escPressed);
    }
}


