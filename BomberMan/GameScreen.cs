using System;
using Tao.Sdl;
using System.Threading;

class GameScreen : Screen
{
    Player playerWhite, playerRed;
    Image imgInfo, imgFloor;
    Font font36, font28;
    Level level;
    IntPtr fontLives, fontTime;
    Sdl.SDL_Color white;
    int lives = 3;

    public GameScreen(Hardware hardware) : base(hardware)
    {
        // preload text
        font28 = new Font("font/Joystix.ttf", 28);
        font36 = new Font("font/Joystix.ttf", 36);

        white = new Sdl.SDL_Color(255, 255, 255);
        fontLives = SdlTtf.TTF_RenderText_Solid(font28.GetFontType(),
                            lives.ToString(), white);

        // preload images
        imgFloor = new Image("imgs/Floor.png", 840, 680);
        imgFloor.MoveTo(0, 0);
        imgInfo = new Image("imgs/InfoPanel.png", 840, 75);
        imgInfo.MoveTo(0, 680);
        
        // preload level
        level = new Level("levels/level1.txt");
        
        playerWhite = new PlayerWhite();
        playerRed = new PlayerRed();
    }

    private void movePlayer()
    {
        // Player White movement
        bool left = hardware.IsKeyPressed(Hardware.KEY_LEFT);
        bool right = hardware.IsKeyPressed(Hardware.KEY_RIGHT);
        bool up = hardware.IsKeyPressed(Hardware.KEY_UP);
        bool down = hardware.IsKeyPressed(Hardware.KEY_DOWN);

        if (up)
        {
            if (playerWhite.Y > 0)
            {
                playerWhite.Y -= Player.STEP_LENGTH;
                if (level.YMap > 0)
                    level.YMap -= Player.STEP_LENGTH;
            }
        }
        if (down)
        {
            if (playerWhite.Y < level.Height - Sprite.SPRITE_HEIGHT)
            {
                playerWhite.Y += Player.STEP_LENGTH;
                if (level.YMap < level.Height - GameController.SCREEN_HEIGHT)
                    level.YMap += Player.STEP_LENGTH;
            }
        }
        if (left)
        {
            if (playerWhite.X > 0)
            {
                playerWhite.X -= Player.STEP_LENGTH;
                if (level.XMap > 0)
                    level.XMap -= Player.STEP_LENGTH;
            }
        }
        if (right)
        {
            if (playerWhite.X < level.Width - Sprite.SPRITE_WIDTH)
            {
                playerWhite.X += Player.STEP_LENGTH;
                if (level.XMap < level.Width - GameController.SCREEN_WIDTH)
                    level.XMap += Player.STEP_LENGTH;
            }
        }

        if (left)
            playerWhite.AnimateWhite(MovableSprite.SpriteMovementWhite.LEFT);
        else if (right)
            playerWhite.AnimateWhite(MovableSprite.SpriteMovementWhite.RIGHT);
        else if (up)
            playerWhite.AnimateWhite(MovableSprite.SpriteMovementWhite.UP);
        else if (down)
            playerWhite.AnimateWhite(MovableSprite.SpriteMovementWhite.DOWN);     

        // Player Red Movement
        bool a = hardware.IsKeyPressed(Hardware.KEY_A);
        bool d = hardware.IsKeyPressed(Hardware.KEY_D);
        bool w = hardware.IsKeyPressed(Hardware.KEY_W);
        bool s = hardware.IsKeyPressed(Hardware.KEY_S);

        if (w)
        {
            if (playerRed.Y > 0)
            {
                playerRed.Y -= Player.STEP_LENGTH;
                if (level.YMap > 0)
                    level.YMap -= Player.STEP_LENGTH;
            }
        }
        if (s)
        {
            if (playerRed.Y < level.Height - Sprite.SPRITE_HEIGHT)
            {
                playerRed.Y += Player.STEP_LENGTH;
                if (level.YMap < level.Height - GameController.SCREEN_HEIGHT)
                    level.YMap += Player.STEP_LENGTH;
            }
        }
        if (a)
        {
            if (playerRed.X > 0)
            {
                playerRed.X -= Player.STEP_LENGTH;
                if (level.XMap > 0)
                    level.XMap -= Player.STEP_LENGTH;
            }
        }
        if (d)
        {
            if (playerRed.X < level.Width - Sprite.SPRITE_WIDTH)
            {
                playerRed.X += Player.STEP_LENGTH;
                if (level.XMap < level.Width - GameController.SCREEN_WIDTH)
                    level.XMap += Player.STEP_LENGTH;
            }
        }

        if (a)
            playerRed.AnimateRed(MovableSprite.SpriteMovementRed.A);
        else if (d)
            playerRed.AnimateRed(MovableSprite.SpriteMovementRed.D);
        else if (w)
            playerRed.AnimateRed(MovableSprite.SpriteMovementRed.W);
        else if (s)
            playerRed.AnimateRed(MovableSprite.SpriteMovementRed.S);
    }

    public void DecreaseTime(Object o)
    {
        int min = 3;
        int sec = 0;

        sec--;
        if (sec < 0 && min != 0)
        {
            min = min--;
            sec = 59;
        }
        else if (min == 0 && sec < 0)
            sec = 0;

        fontTime = SdlTtf.TTF_RenderText_Solid(font36.GetFontType(), 
                min + ":" + sec, white);
    }

    public override void Show()
    {
        short oldXWhite, oldYWhite, oldXMapWhite, oldYMapWhite;
        short oldXRed, oldYRed, oldXMapRed, oldYMapRed;
        bool enterPressed = false;
        bool escPressed = false;
        level = new Level("levels/level1.txt");
        playerWhite.MoveTo(40, 40);
        playerRed.MoveTo(120, 120);
        var timer = new Timer(this.DecreaseTime, null, 1000, 1000);

        //audio.PlayMusic(0, -1);

        do
        {
            // 1. Draw Map
            hardware.ClearScreen();
            hardware.DrawImage(imgInfo);
            hardware.DrawImage(imgFloor);
            hardware.WriteText(fontTime, 360, 700);
            hardware.WriteText(fontLives, 158, 700);

            foreach (Brick brick in level.Bricks)
                hardware.DrawSprite(Sprite.spritesheet, 
                    (short)(brick.X - level.XMap), 
                    (short)(brick.Y - level.YMap), 
                    brick.SpriteX, brick.SpriteY, 
                    Sprite.SPRITE_WIDTH, 
                    Sprite.SPRITE_HEIGHT);

            foreach (BrickDestroyable bdes in level.BricksDestroyable)
                hardware.DrawSprite(Sprite.spritesheet, 
                    (short)(bdes.X - level.XMap), 
                    (short)(bdes.Y - level.YMap), 
                    bdes.SpriteX, bdes.SpriteY, 
                    Sprite.SPRITE_WIDTH,
                    Sprite.SPRITE_HEIGHT);

            hardware.DrawSprite(Sprite.spritesheet, 
                (short)(playerWhite.X - level.XMap), 
                (short)(playerWhite.Y - level.YMap), 
                playerWhite.SpriteXWhite, playerWhite.SpriteYWhite, 
                Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);

            hardware.DrawSprite(Sprite.spritesheet, 
                (short)(playerRed.X - level.XMap), 
                (short)(playerRed.Y - level.YMap), 
                playerRed.SpriteXRed, playerRed.SpriteYRed, 
                Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);

            hardware.UpdateScreen();

            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_ESC)
            {
                escPressed = true;
            }

            // 2.  Move character from keyboard input
            // Coordinates Player White
            oldXWhite = playerWhite.X;
            oldYWhite = playerWhite.Y;
            oldXMapWhite = level.XMap;
            oldYMapWhite = level.YMap;

            // Coordinates Player Red
            oldXRed = playerRed.X;
            oldYRed = playerRed.Y;
            oldXMapRed = level.XMap;
            oldYMapRed = level.YMap;

            movePlayer();

            // 3.  Check collisions and update game state
            if (playerWhite.CollidesWith(level.Bricks) ||
                playerWhite.CollidesWith(level.BricksDestroyable))
            {
                playerWhite.X = oldXWhite;
                playerWhite.Y = oldYWhite;
                level.XMap = oldXMapWhite;
                level.YMap = oldYMapWhite;
            }

            if (playerRed.CollidesWith(level.Bricks) ||
                playerRed.CollidesWith(level.BricksDestroyable))
            {
                playerRed.X = oldXRed;
                playerRed.Y = oldYRed;
                level.XMap = oldXMapRed;
                level.YMap = oldYMapRed;
            }

            //Pause Game
            Thread.Sleep(10);
        }
        while (!escPressed && !enterPressed);
        timer.Dispose();
        //audio.StopMusic();
    }
}
