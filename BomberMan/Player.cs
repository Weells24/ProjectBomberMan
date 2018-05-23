using System.Collections.Generic;

abstract class Player : MovableSprite
{
    public const byte STEP_LENGTH = 4;
    public byte Energy { get; set; }
    public List<Weapons> Bombs { get; }

    public Player()
    {
        Bombs = new List<Weapons>();
    }

    public abstract void AddBomb();
    public abstract void RemoveBomb();

    public void AddBomb(Weapons b)
    {
        b.X = this.X;
        b.Y = this.Y;
        b.CurrentDirection = this.CurrentDirection;
        b.UpdateSpriteBomb();
        Bombs.Add(b);
    }

    public void RemoveBomb(Weapons b)
    {
        Bombs.Remove(b);
    }
}
