class BrickDestroyable : StaticSprite
{
    public BrickDestroyable()
    {
        SpriteX = 40;
        SpriteY = 0;
    }
    public BrickDestroyable(short x, short y) : this()
    {
        X = x;
        Y = y;
    }
}
