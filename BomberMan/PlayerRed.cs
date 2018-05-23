using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PlayerRed : Player
{
    public PlayerRed() : base()
    {
        // DOWN
        SpriteXCoordinates[(int)MovableSprite.SpriteMovementRed.S] = 
            new int[] { 200, 240, 280 };
        SpriteYCoordinates[(int)MovableSprite.SpriteMovementRed.S] = 
            new int[] { 80, 80, 80 };

        // LEFT
        SpriteXCoordinates[(int)MovableSprite.SpriteMovementRed.A] = 
            new int[] { 80, 120, 160 };
        SpriteYCoordinates[(int)MovableSprite.SpriteMovementRed.A] =
            new int[] { 80, 80, 80 };

        // UP
        SpriteXCoordinates[(int)MovableSprite.SpriteMovementRed.W] = 
            new int[] { 440, 0, 40 };
        SpriteYCoordinates[(int)MovableSprite.SpriteMovementRed.W] = 
            new int[] { 40, 80, 80 };


        // RIGHT
        SpriteXCoordinates[(int)MovableSprite.SpriteMovementRed.D] =
            new int[] { 320, 360, 400 };
        SpriteYCoordinates[(int)MovableSprite.SpriteMovementRed.D] = 
            new int[] { 80, 80, 80 };

        UpdateSpriteCoordinatesRed();
    }

    public override void AddBomb()
    {
        Bombs newBomb = new Bombs();
        base.AddBomb(newBomb);
    }
}
