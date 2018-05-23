class Bombs : Weapons
{
    public Bombs()
    {
        SpriteXCoordinates[(int)MovableSprite.SpriteMovementBomb.SPACE] =
                new int[] { 320, 360, 400 };
        SpriteYCoordinates[(int)MovableSprite.SpriteMovementBomb.SPACE] =
                new int[] { 120, 120, 120 };

        UpdateSpriteBomb();
    }
}

