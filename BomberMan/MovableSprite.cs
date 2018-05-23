/*
    * Subtype of Sprite class to represent every movable sprite (for instance, 
    * the main character, the enemies or the weapons)
    */
class MovableSprite : Sprite
{
    const byte TOTAL_MOVEMENTS = 4;
    const byte SPRITE_CHANGE = 4;

    public enum SpriteMovementWhite { LEFT, UP, RIGHT, DOWN};
    public enum SpriteMovementRed { A, W, D, S};
    public SpriteMovementWhite CurrentDirectionWhite { get; set; }
    public SpriteMovementRed CurrentDirectionRed { get; set; }
    public byte CurrentSprite { get; set; }

    byte currentSpriteChange;

    public int[][] SpriteXCoordinates = new int[TOTAL_MOVEMENTS][];
    public int[][] SpriteYCoordinates = new int[TOTAL_MOVEMENTS][];

    public MovableSprite()
    {
        CurrentSprite = 0;
        CurrentDirectionWhite = SpriteMovementWhite.DOWN;
        currentSpriteChange = 0;
    }

    public void AnimateWhite(SpriteMovementWhite movement)
    {
        if (movement != CurrentDirectionWhite)
        {
            CurrentDirectionWhite = movement;
            CurrentSprite = 0;
            currentSpriteChange = 0;
        }
        else
        {
            currentSpriteChange++;
            if (currentSpriteChange >= SPRITE_CHANGE)
            {
                currentSpriteChange = 0;
                CurrentSprite = (byte)((CurrentSprite + 1) % 
                    SpriteXCoordinates[(int)CurrentDirectionWhite].Length);
            }
        }
        UpdateSpriteCoordinatesWhite();
    }

    public void AnimateRed(SpriteMovementRed movement)
    {
        if (movement != CurrentDirectionRed)
        {
            CurrentDirectionRed = movement;
            CurrentSprite = 0;
            currentSpriteChange = 0;
        }
        else
        {
            currentSpriteChange++;
            if (currentSpriteChange >= SPRITE_CHANGE)
            {
                currentSpriteChange = 0;
                CurrentSprite = (byte)((CurrentSprite + 1) % 
                    SpriteXCoordinates[(int)CurrentDirectionRed].Length);
            }
        }
        UpdateSpriteCoordinatesRed();
    }

    public void UpdateSpriteCoordinatesWhite()
    {
        SpriteXWhite = 
            (short)(SpriteXCoordinates[(int)CurrentDirectionWhite]
            [CurrentSprite]);
        SpriteYWhite = 
            (short)(SpriteYCoordinates[(int)CurrentDirectionWhite]
            [CurrentSprite]);
    }

    public void UpdateSpriteCoordinatesRed()
    {
        SpriteXRed = 
            (short)(SpriteXCoordinates[(int)CurrentDirectionRed]
            [CurrentSprite]);
        SpriteYRed = 
            (short)(SpriteYCoordinates[(int)CurrentDirectionRed]
            [CurrentSprite]);
    }
}
