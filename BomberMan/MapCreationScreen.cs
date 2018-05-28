using System;
using System.Collections;
using Tao.Sdl;
using System.Threading;
using System.IO;

class MapCreationScreen : Screen
{
    const int WIDTH = 22;
    const int HEIGHT = 16;
    const short YMAP = 250;
    //const short XFIXED = 14;
    
    Font font20;
    IntPtr fontEdition, fontAlert;
    Sdl.SDL_Color white;
    Image imgMap, imgFloor, imgCursor;
    Level level;
    char[][] map = new char[HEIGHT][];
    short XMap = 250;
    short cursorXGrafic = 40;
    short cursorYGrafic = 40;
    int cursorX = 1;
    int cursorY = 1;
    char[] characters;
    int row = 0;
    string filename;


    public MapCreationScreen(Hardware hardware) : base(hardware)
    {
        font20 = new Font("font/Joystix.ttf", 20);
        imgMap = new Image("imgs/MapScreen.png", 840, 755);
        imgFloor = new Image("imgs/Floor.png", 840, 680);
        imgCursor = new Image("imgs/cursor.png", 40, 40);
        imgFloor.MoveTo(0, 0);
        imgCursor.MoveTo(40, 40);
        white = new Sdl.SDL_Color(255, 255, 255);
    }

    public void LoadMap()
    {
        string alertLoad;
        try
        {
            if (!File.Exists("levels/lvldefault.lvl"))
            {
                alertLoad = "Map not found!";
                fontAlert =
                        SdlTtf.TTF_RenderText_Solid(font20.GetFontType(),
                        alertLoad, white);
                hardware.WriteText(fontAlert, XMap, YMAP);
                Thread.Sleep(500);
            }
            StreamReader MapDefault =
                new StreamReader("levels/lvldefault.lvl");
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
        catch (PathTooLongException e)
        {
            Console.WriteLine("Map not accesible: " + e.Message);
        }
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
    }

    public void CreateMap()
    {
        string alertCreate;
    
        try
        {
            if (!File.Exists(filename))
            {
                alertCreate = "The file exists you want to overwrite it";
                fontAlert =
                        SdlTtf.TTF_RenderText_Solid(font20.GetFontType(),
                        alertCreate, white);
                hardware.WriteText(fontAlert, XMap, YMAP);
                Thread.Sleep(500);
            }
            StreamWriter NewMap =
                File.CreateText(@"C:\Users\Brandon\Documents\ProjectBomberMan"+
                @"\BomberMan\levels\"+filename+".lvl");
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    NewMap.Write(map[i][j]);
                }
                NewMap.WriteLine();
            }
            NewMap.Close();
        }
        catch (PathTooLongException e)
        {
            Console.WriteLine("Map not accesible: " + e.Message);
        }
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
        int tamano = 0;
        bool finish = false;

        do
        {
            letter += hardware.ReadCharacter();
            tamano = letter.Length;
            fontEdition =
                SdlTtf.TTF_RenderText_Solid(font20.GetFontType(),
                letter, white);
            hardware.WriteText(fontEdition, XMap, YMAP);
            filename = letter;

            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_ENTER)
            {
                finish = true;
            }
            Console.WriteLine(letter);
            hardware.UpdateScreen();
        } while (tamano <= 11);
    }

    public override void Show()
    {
        bool escPressed = false;
        level = new Level("levels/lvldefault.lvl");
        LoadMap();
        MapName();

        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgFloor);
            hardware.DrawImage(imgCursor);

            foreach (Brick brick in level.Bricks)
                hardware.DrawSprite(Sprite.spritesheet,
                    (short)(brick.X - level.XMap),
                    (short)(brick.Y - level.YMap),
                    brick.SpriteX, brick.SpriteY,
                    Sprite.SPRITE_WIDTH,
                    Sprite.SPRITE_HEIGHT);

            foreach (BrickDestroyable bdes in level.BricksDestroyable)
                hardware.DrawSprite(Sprite.spritesheet,
                    (short)(bdes.X - level.XMap),
                    (short)(bdes.Y - level.YMap),
                    bdes.SpriteX, bdes.SpriteY,
                    Sprite.SPRITE_WIDTH,
                    Sprite.SPRITE_HEIGHT);

            int keyPressed = hardware.KeyPressed();
            keyPressed = hardware.KeyPressed();
            switch (keyPressed)
            {
                case Hardware.KEY_UP:
                    cursorY--;
                    cursorYGrafic -= 40;
                    imgCursor.MoveTo(cursorXGrafic, cursorYGrafic);
                    //Console.WriteLine("U: " + cursorYGrafic);
                    break;
                case Hardware.KEY_DOWN:
                    cursorY++;
                    cursorYGrafic += 40;
                    imgCursor.MoveTo(cursorXGrafic, cursorYGrafic);
                    //Console.WriteLine("D: " + cursorYGrafic);
                    break;
                case Hardware.KEY_LEFT:
                    cursorX--;
                    cursorXGrafic -= 40;
                    imgCursor.MoveTo(cursorXGrafic, cursorYGrafic);
                    //Console.WriteLine("L: " + cursorXGrafic);
                    break;
                case Hardware.KEY_RIGHT:
                    cursorX++;
                    cursorXGrafic += 40;
                    imgCursor.MoveTo(cursorXGrafic, cursorYGrafic);
                    //Console.WriteLine("R: " + cursorXGrafic);
                    break;
                case Hardware.KEY_B:
                    map[cursorX][cursorY] = 'B';
                    Console.WriteLine(map[cursorX][cursorY]);
                    break;
                case Hardware.KEY_D:
                    map[cursorX][cursorY] = 'D';
                    //Console.WriteLine(map[cursorX][cursorY]);
                    break;
                case Hardware.KEY_ENTER:
                    CreateMap();
                    //Console.WriteLine("Create!");
                    break;
            }

            //Pause Game
            Thread.Sleep(80);

            if (keyPressed == Hardware.KEY_ESC)
                escPressed = true;

            hardware.UpdateScreen();
        } while (!escPressed);
    }
}


