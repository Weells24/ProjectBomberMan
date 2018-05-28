using System;
using Tao.Sdl;
using System.Threading;
using System.Collections;

/*
* This class will manage every hardware issue: screen resolution, 
* keyboard input and some other aspects
*/

class Hardware
{
    // Alternate key definitions

    public const int KEY_ESC = Sdl.SDLK_ESCAPE;
    public const int KEY_UP = Sdl.SDLK_UP;
    public const int KEY_DOWN = Sdl.SDLK_DOWN;
    public const int KEY_ENTER = Sdl.SDLK_RETURN;
    public const int KEY_LEFT = Sdl.SDLK_LEFT;
    public const int KEY_RIGHT = Sdl.SDLK_RIGHT;
    public const int KEY_SPACE = Sdl.SDLK_SPACE;
    public const int KEY_NENTER = Sdl.SDLK_KP_ENTER;
    public const int KEY_DELETE = Sdl.SDLK_DELETE;
    public const int KEY_A = Sdl.SDLK_a;
    public const int KEY_B = Sdl.SDLK_b;
    public const int KEY_C = Sdl.SDLK_c;
    public const int KEY_D = Sdl.SDLK_d;
    public const int KEY_E = Sdl.SDLK_e;
    public const int KEY_F = Sdl.SDLK_f;
    public const int KEY_G = Sdl.SDLK_g;
    public const int KEY_H = Sdl.SDLK_h;
    public const int KEY_I = Sdl.SDLK_i;
    public const int KEY_J = Sdl.SDLK_j;
    public const int KEY_K = Sdl.SDLK_k;
    public const int KEY_L = Sdl.SDLK_l;
    public const int KEY_M = Sdl.SDLK_m;
    public const int KEY_N = Sdl.SDLK_n;
    public const int KEY_O = Sdl.SDLK_o;
    public const int KEY_P = Sdl.SDLK_p;
    public const int KEY_Q = Sdl.SDLK_q;
    public const int KEY_R = Sdl.SDLK_r;
    public const int KEY_S = Sdl.SDLK_s;
    public const int KEY_T = Sdl.SDLK_t;
    public const int KEY_U = Sdl.SDLK_u;
    public const int KEY_V = Sdl.SDLK_v;
    public const int KEY_W = Sdl.SDLK_w;
    public const int KEY_X = Sdl.SDLK_x;
    public const int KEY_Y = Sdl.SDLK_y;
    public const int KEY_Z = Sdl.SDLK_z;
    public const int KEY_1 = Sdl.SDLK_1;
    public const int KEY_2 = Sdl.SDLK_2;
    public const int KEY_3 = Sdl.SDLK_3;
    public const int KEY_4 = Sdl.SDLK_4;
    public const int KEY_5 = Sdl.SDLK_5;
    public const int KEY_6 = Sdl.SDLK_6;
    public const int KEY_7 = Sdl.SDLK_7;
    public const int KEY_8 = Sdl.SDLK_8;
    public const int KEY_9 = Sdl.SDLK_9;
    public const int KEY_0 = Sdl.SDLK_0;

    short screenWidth;
    short screenHeight;
    short colorDepth;
    IntPtr screen;

    public Hardware(short width, short height, short depth, 
                    bool fullScreen)
    {
        screenWidth = width;
        screenHeight = height;
        colorDepth = depth;

        int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | 
            Sdl.SDL_ANYFORMAT;
        if (fullScreen)
            flags = flags | Sdl.SDL_FULLSCREEN;

        Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
        screen = Sdl.SDL_SetVideoMode(screenWidth, screenHeight, 
                colorDepth, flags);
        Sdl.SDL_Rect rect = new Sdl.SDL_Rect(0, 0, screenWidth, 
                screenHeight);
        Sdl.SDL_SetClipRect(screen, ref rect);

        SdlTtf.TTF_Init();
    }

    ~Hardware()
    {
        Sdl.SDL_Quit();
    }

    // Draws an image in its current coordinates
    public void DrawImage(Image img)
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, img.ImageWidth,
            img.ImageHeight);
        Sdl.SDL_Rect target = new Sdl.SDL_Rect(img.X, img.Y,
            img.ImageWidth, img.ImageHeight);
        Sdl.SDL_BlitSurface(img.ImagePtr, ref source, screen, 
                ref target);
    }

    /* Draws a sprite from a sprite sheet in the specified X and Y 
        * position of the screen
        * The sprite to be drawn is determined by the x and y coordinates 
        * within the image, and the width and height to be cropped
        */
    public void DrawSprite(Image image, short xScreen, short yScreen, 
            short x, short y, short width, short height)
    {
        Sdl.SDL_Rect src = new Sdl.SDL_Rect(x, y, width, height);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(xScreen, yScreen, width, 
                height);
        Sdl.SDL_BlitSurface(image.ImagePtr, ref src, screen, ref dest);
    }

    // Update screen
    public void UpdateScreen()
    {
        Sdl.SDL_Flip(screen);
    }

// Detects if the user presses a key and returns the code of the 
// key pressed
    public int KeyPressed()
    {
        int pressed = -1;

        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event keyEvent;
        if (Sdl.SDL_PollEvent(out keyEvent) == 1)
        {
            if (keyEvent.type == Sdl.SDL_KEYDOWN)
            {
                pressed = keyEvent.key.keysym.sym;
            }
        }
        return pressed;
    }

    // Checks if a given key is now being pressed
    public bool IsKeyPressed(int key)
    {
        bool pressed = false;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event evt;
        Sdl.SDL_PollEvent(out evt);
        int numKeys;
        byte[] keys = Sdl.SDL_GetKeyState(out numKeys);
        if (keys[key] == 1)
            pressed = true;
        return pressed;
    }

    // Clears the screen
    public void ClearScreen()
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, screenWidth, 
                screenHeight);
        Sdl.SDL_FillRect(screen, ref source, 0);
    }
    public void Pause(int milisegundos)
    {
        Thread.Sleep(milisegundos);
    }

    public void ClearBottom()
    {
        Sdl.SDL_Rect source = 
            new Sdl.SDL_Rect(0, GameController.SCREEN_HEIGHT,
                screenWidth, (short)(screenHeight - 
                GameController.SCREEN_HEIGHT));
        Sdl.SDL_FillRect(screen, ref source, 0);
    }


    // Writes a text in the specified coordinates
    public void WriteText(IntPtr textAsImage, short x, short y)
    {
        Sdl.SDL_Rect src = 
            new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
        Sdl.SDL_Rect dest = 
            new Sdl.SDL_Rect(x, y, screenWidth, screenHeight);
        Sdl.SDL_BlitSurface(textAsImage, ref src, screen, ref dest);
    }

    
    public string ReadCharacter()
    {
        string character = "";
        bool exit = false;
        int key_int;
        do
        {
            key_int = KeyPressed();
            //Console.WriteLine(key_int);
            /*if (key_int == 8)
            {
                int tamano = str.Length;
                character = str.Remove(tamano - 1);
            }*/
            if (key_int != -1)
                exit = true;
        } while (!exit);
        
        switch (key_int)
        {
            case KEY_1:
                character = "1";
                break;
            case KEY_2:
                character = "2";
                break;
            case KEY_3:
                character = "3";
                break;
            case KEY_4:
                character = "4";
                break;
            case KEY_5:
                character = "5";
                break;
            case KEY_6:
                character = "6";
                break;
            case KEY_7:
                character = "7";
                break;
            case KEY_8:
                character = "8";
                break;
            case KEY_9:
                character = "9";
                break;
            case KEY_0:
                character = "0";
                break;
            case KEY_A:
                character = "a";
                break;
            case KEY_B:
                character = "b";
                break;
            case KEY_C:
                character = "c";
                break;
            case KEY_D:
                character = "d";
                break;
            case KEY_E:
                character = "e";
                break;
            case KEY_F:
                character = "f";
                break;
            case KEY_G:
                character = "g";
                break;
            case KEY_H:
                character = "h";
                break;
            case KEY_I:
                character = "i";
                break;
            case KEY_J:
                character = "j";
                break;
            case KEY_K:
                character = "k";
                break;
            case KEY_L:
                character = "l";
                break;
            case KEY_M:
                character = "m";
                break;
            case KEY_N:
                character = "n";
                break;
            case KEY_O:
                character = "o";
                break;
            case KEY_P:
                character = "p";
                break;
            case KEY_Q:
                character = "q";
                break;
            case KEY_R:
                character = "r";
                break;
            case KEY_S:
                character = "s";
                break;
            case KEY_T:
                character = "t";
                break;
            case KEY_U:
                character = "u";
                break;
            case KEY_V:
                character = "v";
                break;
            case KEY_W:
                character = "w";
                break;
            case KEY_X:
                character = "x";
                break;
            case KEY_Y:
                character = "y";
                break;
            case KEY_Z:
                character = "z";
                break;
        }
        return character;
    }
}