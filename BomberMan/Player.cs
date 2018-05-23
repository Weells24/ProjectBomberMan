abstract class Player : MovableSprite
{
    public const byte STEP_LENGTH = 4;
    public ushort Lives { get; set; }

    public Player()
    {
        Lives = 3;
    }
}
