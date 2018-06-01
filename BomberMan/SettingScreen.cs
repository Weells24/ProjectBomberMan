using System;
using Tao.Sdl;
using System.Collections.Generic;
using System.IO;

class SettingScreen : Screen
{
    bool exit;
    Image imgSetting, imgChosenOption, imgSupr;
    int chosenOption = 1;
    Audio audio;
    Font font, font42;
    IntPtr fontMap, fontChange, fontPlayer;
    string[] map;
    int count = 0;
    GameScreen game;
    Sdl.SDL_Color white, yellow;
    SelectLenguage select;

    public SettingScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        audio = new Audio(44100, 2, 4096);
        audio.AddWAV("music/reset.wav");
        font = new Font("font/Joystix.ttf", 28);
        font42 = new Font("font/Joystix.ttf", 42);
        imgSetting =
                new Image("imgs/SettingsScreen.png", 840, 755);
        imgChosenOption =
                new Image("imgs/select.png", 40, 35);
        imgSupr =
                new Image("imgs/delete.png", 563, 42);
        game = new GameScreen(hardware);
        imgSetting.MoveTo(0, 0);
        imgSupr.MoveTo(100, 100);
        imgChosenOption.MoveTo(140, 240);
        white = new Sdl.SDL_Color(255, 255, 255);
        yellow = new Sdl.SDL_Color(255, 255, 0);
        select = new SelectLenguage(hardware);
    }

    public void LoadMaps()
    {
        DirectoryInfo dir = 
            new DirectoryInfo(@".\levels");
        FileInfo[] files = dir.GetFiles();
        int size = files.Length;
        map = new string[size];
        
        for (int i = 0; i < map.Length; i++)
        {
            if (files[i].Extension == ".lvl")
            {
                map[i] = files[i].Name;
            }
        }
    }

    public void DrawOption()
    {
        hardware.DrawImage(imgSupr);
        string[] tmp = map[count].Split('.');
        fontMap = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                tmp[0], white);
        hardware.WriteText(fontMap, 360, 250);
    }

    public override void Show()
    {
        bool enterPressed = false;
        bool escPressed = false;
        Lenguage len = new Lenguage();
        int option = SelectLenguage.option;
        
        

        LoadMaps();


        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgSetting);
            hardware.DrawImage(imgChosenOption);
            fontChange = SdlTtf.TTF_RenderText_Solid(font42.GetFontType(),
                len.Change(option, 5), yellow);
            fontPlayer = SdlTtf.TTF_RenderText_Solid(font42.GetFontType(),
                len.Change(option, 7), yellow);
            hardware.WriteText(fontPlayer, 50, 500);
            hardware.WriteText(fontPlayer, 550, 500);
            hardware.WriteText(fontChange, 200, 240);
            hardware.WriteText(fontMap, 360, 250);
            

            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_UP && chosenOption > 1)
            {
                audio.PlayWAV(0, 1, 0);
                chosenOption--;
                imgChosenOption.MoveTo(140, (short)(imgChosenOption.Y - 180));
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
                if (count < map.Length - 1)
                {
                    count++;
                    DrawOption();
                }
            }
            else if (chosenOption == 1 && keyPressed == Hardware.KEY_LEFT)
            {
                if (count > 0)
                {
                    count--;
                    DrawOption();
                }
            }
            else if (chosenOption == 1 && keyPressed == Hardware.KEY_DELETE)
            {
                File.Delete(@".\levels\" + map[count]);
                LoadMaps();
                string[] tmp = map[count].Split('.');
                fontMap = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                        tmp[0], white);
                hardware.WriteText(fontMap, 360, 250);
            }
            else if (chosenOption == 1)
                DrawOption();

            // Option 2: Start Game
            else if (chosenOption == 2 && keyPressed == Hardware.KEY_ENTER)
                game.Show(map[count]);

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
            else
            {
                string[] tmp = map[count].Split('.');
                fontMap = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                        tmp[0], white);
                hardware.WriteText(fontMap, 360, 250);
            }

            hardware.UpdateScreen();
        }
        while (!escPressed && !enterPressed);
    }
}
