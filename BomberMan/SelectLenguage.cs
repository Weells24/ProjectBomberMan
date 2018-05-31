using System;
using Tao.Sdl;
using System.Collections.Generic;


class SelectLenguage : Screen
{
    Image imgSelect, imgChosenOption;
    int chosenOption = 1;
    Audio audio;
    bool exit;
    Font font;
    IntPtr fontSpanish, fontEnglish;
    Sdl.SDL_Color white;
    Dictionary<string, string> spanish =
            new Dictionary<string, string>();
    Dictionary<string, string> english =
            new Dictionary<string, string>();
    int option;

    public SelectLenguage(Hardware hardware) : base(hardware)
    {
        exit = false;
        audio = new Audio(44100, 2, 4096);
        audio.AddWAV("music/reset.wav");
        imgSelect =
            new Image("./imgs/castellano/SelectLenguage.png", 40, 40);
        imgChosenOption =
            new Image("imgs/BombMenu1.png", 50, 50);
        font = new Font("font/Joystix.ttf", 28);
        imgChosenOption.MoveTo(245, 420);
        white = new Sdl.SDL_Color(255, 255, 255);
    }

    public string English(string key)
    {
        option = 0;
        english.Add("ju", "Play");
        english.Add("creation", "Creation Map");
        english.Add("con", "Constrols");
        english.Add("cre", "Credits");
        english.Add("ex", "Exit");
        english.Add("ma", "Map:");
        english.Add("star", "Start");
        english.Add("player", "player");
        english.Add("save", "Save File");
        english.Add("move", "Move Cursor");
        english.Add("up", "up move");
        english.Add("down", "down move");
        english.Add("left", "left move");
        english.Add("right", "right move");
        english.Add("pause", "pause");
        english.Add("back", "back");
        english.Add("white", "PLAYER WHITE");
        english.Add("red", "PLAYER RED");

        return english["\"" + key + "\""];
    }

    public string Spanish(string key)
    {
        option = 1;
        spanish.Add("ju", "Jugar");
        spanish.Add("creation", "Creacion de Mapas");
        spanish.Add("con", "Controles");
        spanish.Add("cre", "Creditos");
        spanish.Add("ex", "Salir");
        spanish.Add("ma", "Mapa:");
        spanish.Add("star", "Empezar");
        spanish.Add("player", "Jugador");
        spanish.Add("save", "Guardar Fichero");
        spanish.Add("move", "Mover Cursor");
        spanish.Add("up", "Mover arriba");
        spanish.Add("down", "Mover Abajo");
        spanish.Add("left", "Mover Izquierda");
        spanish.Add("right", "Mover Derecha");
        spanish.Add("pause", "Pausa");
        spanish.Add("back", "Atras");
        spanish.Add("white", "Jugador Blanco");
        spanish.Add("red", "Jugador Rojo");

        return spanish["\"" + key + "\""];
    }

    public int GetOption()
    {
        return option;
    }

    public void SetOption(int NewOption)
    {
        option = NewOption;
    }

    public override void Show()
    {
        //add
        bool enterPressed = false;
        string spanish = "Español";
        string english = "Ingles";

        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgSelect);
            hardware.DrawImage(imgChosenOption);

            fontSpanish = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                            spanish, white);
            hardware.WriteText(fontSpanish, 300, 440);
            fontEnglish = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                            english, white);
            hardware.WriteText(fontEnglish, 300, 500);

            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_UP && chosenOption > 1)
            {
                audio.PlayWAV(0, 1, 0);
                chosenOption--;
                imgChosenOption.MoveTo(245, (short)(imgChosenOption.Y - 60));
            }
            else if (keyPressed == Hardware.KEY_DOWN && chosenOption < 2)
            {
                audio.PlayWAV(0, 1, 0);
                chosenOption++;
                imgChosenOption.MoveTo(245, (short)(imgChosenOption.Y + 60));
            }
            else if (keyPressed == Hardware.KEY_ENTER && chosenOption == 1)
            {
                SetOption(0);
                enterPressed = true;
                exit = true;
            }
            else if (keyPressed == Hardware.KEY_ENTER && chosenOption == 2)
            {
                SetOption(1);
                enterPressed = true;
                exit = true;
            }
            hardware.UpdateScreen();
        }
        while (!enterPressed);
    }
}

