using System.Collections.Generic;
class Sprite
{
    public const short SPRITE_WIDTH = 40;
    public const short SPRITE_HEIGHT = 40;

    public static Image spritesheet =
        new Image("imgs/spritesheetbom.png",500,200);
            
    public short X { get; set; }
    public short Y { get; set; }
    public short SpriteXWhite { get; set; }
    public short SpriteYWhite { get; set; }
    public short SpriteXRed { get; set; }
    public short SpriteYRed { get; set; }
    public short SpriteX { get; set; }
    public short SpriteY { get; set; }

    public void MoveTo(short x, short y)
    {
        X = x;
        Y = y;
    }
        
    public bool CollidesWith(Sprite sp)
    {
        return (X + Sprite.SPRITE_WIDTH > sp.X && 
                X < sp.X + Sprite.SPRITE_WIDTH && 
                Y + Sprite.SPRITE_HEIGHT > sp.Y && 
                Y < sp.Y + Sprite.SPRITE_HEIGHT);
    }

    public bool CollidesWith(List<Sprite> sprites)
    {
        foreach (Sprite sp in sprites)
            if (this.CollidesWith(sp))
                return true;
        return false;
    }
}