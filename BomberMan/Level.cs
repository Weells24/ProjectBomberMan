using System;
using System.Collections.Generic;
using System.IO;

class Level
{
    public short Width { get; set; }
    public short Height { get; set; }
    public short XMap { get; set; }
    public short YMap { get; set; }
    public Image Floor { get; set; }
    public List<Sprite> Bricks { get; }
    public List<Sprite> BricksDestroyable { get; }
    public string Time { get; set; }
    public int TimeDown { get; set; }

    public Level(string fileName)
    {
        XMap = YMap = 0;
        Bricks = new List<Sprite>();
        BricksDestroyable = new List<Sprite>();
        Floor = new Image("imgs/Floor.png", 840, 680);
        string[] lines = File.ReadAllLines(fileName);
        if (lines.Length > 0)
        {
            Width = (short)(lines[0].Length * Sprite.SPRITE_WIDTH);
            Height = (short)(lines.Length * Sprite.SPRITE_HEIGHT);
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'B')
                    {
                        AddBrick(new Brick((short)(j * Sprite.SPRITE_WIDTH),
                        (short)(i * Sprite.SPRITE_HEIGHT)));
                    }
                    if (lines[i][j] == 'D')
                    {
                        BricksDestroyable.Add(new BrickDestroyable((short)(j * Sprite.SPRITE_WIDTH),
                        (short)(i * Sprite.SPRITE_HEIGHT)));
                    }
                    if (lines[i][j] == 'W')
                    {
                        BricksDestroyable.Add(new BrickDestroyable((short)(j * Sprite.SPRITE_WIDTH),
                        (short)(i * Sprite.SPRITE_HEIGHT)));
                    }
                }
            }
        }
    }
    public void AddBrick(Brick b)
    {
        Bricks.Add(b);
    }
}