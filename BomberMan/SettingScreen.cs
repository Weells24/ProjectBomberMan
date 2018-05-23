using System;
using Tao.Sdl;
using System.Collections.Generic;

class SettingScreen : Screen
{
    bool exit;
    Image imgSetting, imgChosenOption;
    int chosenOption = 1;
    Audio audio;
    Font font;
    IntPtr fontMap;
    string[] map = { "FireMap", "WaterMap", "TreeMap" };
    int count = 0;
    GameScreen game;
    Sdl.SDL_Color white;

    public SettingScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        audio = new Audio(44100, 2, 4096);
        audio.AddWAV("music/reset.wav");
        font = new Font("font/Joystix.ttf", 28);
        imgSetting =
                new Image("imgs/SettingsScreen.png", 840, 755);
        imgChosenOption =
                new Image("imgs/select.png", 40, 35);
        game = new GameScreen(hardware);
        imgSetting.MoveTo(0, 0);
        imgChosenOption.MoveTo(170, 240);
        white = new Sdl.SDL_Color(255, 255, 255);
    }


    public override void Show()
    {
        bool enterPressed = false;
        bool escPressed = false;

        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgSetting);
            hardware.DrawImage(imgChosenOption);
            hardware.WriteText(fontMap, 360, 250);
            hardware.UpdateScreen();

            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_UP && chosenOption > 1)
            {
                audio.PlayWAV(0, 1, 0);
                chosenOption--;
                imgChosenOption.MoveTo(160, (short)(imgChosenOption.Y - 180));
            }
            else if (keyPressed == Hardware.KEY_DOWN && chosenOption < 2)
            {
                audio.PlayWAV(0, 1, 0);
                chosenOption++;
                imgChosenOption.MoveTo(220, (short)(imgChosenOption.Y + 180));
            }
            // Option 1: change Map
            else if (chosenOption == 1 && keyPressed == Hardware.KEY_RIGHT)
            {
                if (count < 2)
                {
                    count++;
                    fontMap = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                            map[count], white);
                    hardware.WriteText(fontMap, 360, 250);
                    hardware.UpdateScreen();
                }
            }
            else if (chosenOption == 1 && keyPressed == Hardware.KEY_LEFT)
            {
                if (count > 0)
                {
                    count--;
                    fontMap = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                            map[count], white);
                    hardware.WriteText(fontMap, 360, 250);
                    hardware.UpdateScreen();
                }
            }
            // Option 2: Start Game
            else if (chosenOption == 2 && keyPressed == Hardware.KEY_ENTER)
                game.Show();

            else if (keyPressed == Hardware.KEY_ESC)
            {
                escPressed = true;
                exit = false;
            }
            else if (keyPressed == Hardware.KEY_ENTER)
            {
                enterPressed = true;
                exit = false;
            }
        }
        while (!escPressed && !enterPressed);
    }
}
