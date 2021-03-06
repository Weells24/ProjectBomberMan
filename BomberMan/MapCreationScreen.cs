﻿using System;
using System.Collections;
using Tao.Sdl;
using System.Threading;
using System.IO;
using System.Collections.Generic;

class MapCreationScreen : Screen
{
    const int WIDTH = 22;
    const int HEIGHT = 16;
    const short YMAP = 160;

    Font font20;
    IntPtr fontEdition, fontAlert, fontName, fontCursor, fontSave;
    Sdl.SDL_Color white;
    Image imgMap, imgFloor, imgCursor;
    Level level;
    char[][] map = new char[HEIGHT][];
    short XMap = 330;
    short cursorXGrafic = 40;
    short cursorYGrafic = 40;
    int cursorX = 1;
    int cursorY = 1;
    char[] characters;
    int row = 0;
    string fileDefault = "./lvldefault.lvl";
    string filename;
    public List<Sprite> Bricks { get; }
    public List<Sprite> BricksDestroyable { get; }
    Lenguage len = new Lenguage();
    int option = SelectLenguage.option;
    Font font32, font16;
    Sdl.SDL_Color yellow;


    public MapCreationScreen(Hardware hardware) : base(hardware)
    {
        font20 = new Font("font/Joystix.ttf", 20);
        imgMap = new Image("imgs/MapScreen.png", 840, 105);
        imgFloor = new Image("imgs/Floor.png", 840, 680);
        imgCursor = new Image("imgs/cursor.png", 40, 40);
        imgMap.MoveTo(0, 640);
        imgFloor.MoveTo(0, 0);
        imgCursor.MoveTo(40, 40);
        white = new Sdl.SDL_Color(255, 255, 255);
        Bricks = new List<Sprite>();
        BricksDestroyable = new List<Sprite>();
        font32 = new Font("font/Joystix.ttf", 32);
        font16 = new Font("font/Joystix.ttf", 16);
        yellow = new Sdl.SDL_Color(255, 255, 0);
    }

    public void LoadMap()
    {
        string alertLoad;
        try
        {
            if (!File.Exists(fileDefault))
            {
                alertLoad = "Map not found!";
                fontAlert =
                        SdlTtf.TTF_RenderText_Solid(font20.GetFontType(),
                        alertLoad, white);
                hardware.WriteText(fontAlert, XMap, YMAP);
            }
            StreamReader MapDefault =
                new StreamReader(fileDefault);
            string line;

            // Load the default map in a two-dimensional array
            do
            {
                line = MapDefault.ReadLine();
                if (line != null)
                {
                    characters = line.ToCharArray();
                    map[row] = new char[WIDTH];
                    for (int i = 0; i < characters.Length; i++)
                    {
                        map[row][i] = characters[i];
                    }
                    row++;
                }
            } while (line != null);
            MapDefault.Close();
        }
        catch (PathTooLongException){}
        catch (IOException e)
        {
            Console.WriteLine("I/O ERROR: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR: " + e.Message);
        }
        /* Operation without file, directly from array
        for (int height = 0; height < map.Length; height++)
        {
            map[height] = new char[WIDTH];
            for (int width = 0; width < map[height].Length; width++)
            {
                if (height == 0 || height == (map.Length-1) || 
                    width == (map[height].Length-1) || width == 0)
                    map[height][width] = 'B';
                else
                    map[height][width] = ' ';
            }
        }*/

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 'B' || map[i][j] == 'b')
                {
                    Bricks.Add(new Brick((short)(j * Sprite.SPRITE_WIDTH),
                    (short)(i * Sprite.SPRITE_HEIGHT)));
                }
                if (map[i][j] == 'D' || map[i][j] == 'd')
                {
                    BricksDestroyable.Add(new BrickDestroyable((short)(j * Sprite.SPRITE_WIDTH),
                    (short)(i * Sprite.SPRITE_HEIGHT)));
                }
            }
        }
    }

    public void CreateMap()
    {
        string alertCreate;

        try
        {
            if (File.Exists(filename+".lvl"))
            {
                alertCreate = "The file exists you want to overwrite it?";
                fontAlert =
                        SdlTtf.TTF_RenderText_Solid(font20.GetFontType(),
                        alertCreate, white);
                hardware.WriteText(fontAlert, XMap, YMAP);
            }
                StreamWriter NewMap =
                    File.CreateText(@".\levels\" + filename + ".lvl");
                for (int i = 0; i < map.Length; i++)
                {
                    for (int j = 0; j < map[i].Length; j++)
                    {
                        NewMap.Write(map[i][j]);
                    }
                    NewMap.WriteLine();
                }
                NewMap.Close();
            
                fontAlert =
                        SdlTtf.TTF_RenderText_Solid(font20.GetFontType(),
                        len.Change(option,8), white);
                hardware.WriteText(fontAlert, 650, 710);
                Thread.Sleep(100);
        }
        catch (PathTooLongException) { }
        catch (IOException e)
        {
            Console.WriteLine("I/O ERROR: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR: " + e.Message);
        }
    }

    public void MapName()
    {
        string letter = "";
        int size;

        do
        {
            letter += hardware.ReadCharacter(letter);
            size = letter.Length;
            Console.WriteLine(letter);
            fontEdition =
                SdlTtf.TTF_RenderText_Solid(font20.GetFontType(),
                letter, white);
            hardware.WriteText(fontEdition, XMap, YMAP);
            filename = letter;
            hardware.UpdateScreen();
        } while (size <= 7);
    }

    public override void Show()
    {
        hardware.ClearScreen();
        hardware.DrawImage(imgFloor);
        fontName = SdlTtf.TTF_RenderText_Solid(font32.GetFontType(),
                           len.Change(option, 21), yellow);
        hardware.WriteText(fontName, 150, 150);
        hardware.UpdateScreen();
        bool escPressed = false;
        level = new Level(fileDefault);
        MapName();
        LoadMap();

        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgFloor);
            hardware.DrawImage(imgMap);
            fontCursor = SdlTtf.TTF_RenderText_Solid(font16.GetFontType(),
                           len.Change(option, 9), yellow);
            hardware.WriteText(fontCursor, 425, 675);
            fontSave = SdlTtf.TTF_RenderText_Solid(font16.GetFontType(),
                           len.Change(option, 8), yellow);
            hardware.WriteText(fontSave, 63, 670);
            hardware.DrawImage(imgCursor);

            foreach (Brick brick in Bricks)
                hardware.DrawSprite(Sprite.spritesheet,
                    (short)(brick.X - level.XMap),
                    (short)(brick.Y - level.YMap),
                    brick.SpriteX, brick.SpriteY,
                    Sprite.SPRITE_WIDTH,
                    Sprite.SPRITE_HEIGHT);

            foreach (BrickDestroyable bdes in BricksDestroyable)
                hardware.DrawSprite(Sprite.spritesheet,
                    (short)(bdes.X - level.XMap),
                    (short)(bdes.Y - level.YMap),
                    bdes.SpriteX, bdes.SpriteY,
                    Sprite.SPRITE_WIDTH,
                    Sprite.SPRITE_HEIGHT);
            
            int keyPressed = hardware.KeyPressed();
            switch (keyPressed)
            {
                case Hardware.KEY_UP:
                    if (cursorY > 1)
                    {
                        cursorY--;
                        if (cursorYGrafic > 40)
                            cursorYGrafic -= 40;
                            imgCursor.MoveTo(cursorXGrafic, cursorYGrafic);
                    }
                    break;
                case Hardware.KEY_DOWN:
                    if (cursorY < 15)
                    {
                        cursorY++;
                        if (cursorYGrafic < 560)
                            cursorYGrafic += 40;
                        imgCursor.MoveTo(cursorXGrafic, cursorYGrafic);
                    }
                    break;
                case Hardware.KEY_LEFT:
                    if (cursorX > 1)
                    {
                        cursorX--;
                        if (cursorXGrafic > 40)
                            cursorXGrafic -= 40;
                        imgCursor.MoveTo(cursorXGrafic, cursorYGrafic);
                    }
                    break;
                case Hardware.KEY_RIGHT:
                    if (cursorX < 21)
                    {
                        cursorX++;
                        if (cursorXGrafic < 760)
                            cursorXGrafic += 40;
                        imgCursor.MoveTo(cursorXGrafic, cursorYGrafic);
                    }
                    break;
                case Hardware.KEY_B:
                    if (cursorX >= 1 && cursorX <= 20 &&
                            cursorY >= 1 && cursorY <= 16)
                    {
                        map[cursorY][cursorX] = 'B';
                        Bricks.Add(new Brick(cursorXGrafic, cursorYGrafic));
                    }
                    break;
                case Hardware.KEY_D:
                    if (cursorX >= 1 && cursorX <= 20 &&
                            cursorY >= 1 && cursorY <= 16)
                    {
                        map[cursorY][cursorX] = 'D';
                        BricksDestroyable.Add(new BrickDestroyable(
                               cursorXGrafic, cursorYGrafic));
                    }
                    break;
                case Hardware.KEY_SPACE:
                    if (cursorX >= 1 && cursorX <= 20 &&
                            cursorY >= 1 && cursorY <= 16)
                    {
                        map[cursorY][cursorX] = ' ';
                    }
                    break;
                case Hardware.KEY_ENTER:
                    CreateMap();
                    break;
            }

            //Pause Game
            Thread.Sleep(16);

            if (keyPressed == Hardware.KEY_ESC)
                escPressed = true;

            hardware.DrawImage(imgCursor);
            hardware.UpdateScreen();
        } while (!escPressed);
    }
}


