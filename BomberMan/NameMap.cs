using System;
using System.IO;

class NameMap : Screen
{
    const int WIDTH = 21;
    const int HEIGHT = 16;

    Image imgFloor, imgCursor;

    public NameMap(Hardware hardware) : base(hardware)
    {

    }
       
    public void CreateMap()
    {
        try
        {
            StreamReader MapDefault = new StreamReader("levels/lvldefault.lvl");
            StreamWriter NewMap = File.CreateText("hola" + ".lvl");
            string line;
            Level level;
            level = new Level("levels/lvldefault.lvl");
            char[] characters;
            char[][] map = new char[HEIGHT][];
            int row = 0;
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
            NewMap.Close();

            // Draw Map
            hardware.DrawImage(imgFloor);

            foreach (Brick brick in level.Bricks)
                hardware.DrawSprite(Sprite.spritesheet,
                    (short)(brick.X - level.XMap),
                    (short)(brick.Y - level.YMap),
                    brick.SpriteX, brick.SpriteY,
                    Sprite.SPRITE_WIDTH,
                    Sprite.SPRITE_HEIGHT);
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
}

