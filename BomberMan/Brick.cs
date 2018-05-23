class Brick : StaticSprite
{
    public Brick()
    {
        SpriteX = 0;
        SpriteY = 0;
    }

    public Brick(short x, short y) : this()
    {
        X = x;  
        Y = y;
    }

}
