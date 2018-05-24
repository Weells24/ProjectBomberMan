class Bombs : Weapons
{
    public const byte DAMAGE = 1;

    public Bombs()
    {
        SpriteXBomb = new int[] { 320, 360, 400 };
        SpriteYBomb = new int[] { 120, 120, 120 };

        UpdateSpriteBomb();
    }
}

