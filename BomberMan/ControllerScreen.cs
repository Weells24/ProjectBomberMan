using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

class ControllerScreen : Screen
{
    Image imgController;
    Audio audio;
    bool exit = false;
    Font font;
    IntPtr fUp, fDown, fLeft, fRight, FPW, FPR, fPause, fEsc, fBomb, fControls;
    Sdl.SDL_Color yellow;

    public ControllerScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        audio = new Audio(44100, 2, 4096);
        audio.AddWAV("music/reset.wav");
        imgController = new Image("imgs/ControllerScreen.png", 840, 755);
        imgController.MoveTo(0,0);
        font = new Font("font/Joystix.ttf", 10);
        yellow = new Sdl.SDL_Color(255, 255, 0);
    }

    public override void Show()
    {
        bool enterPressed = false;
        bool escPressed = false;
        Lenguage len = new Lenguage();
        int option = SelectLenguage.option;

        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(imgController);
            fControls = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 2), yellow);
            hardware.WriteText(fControls, 400, 400);
            fUp = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 10), yellow);
            hardware.WriteText(fUp, 115, 650);
            hardware.WriteText(fUp, 590, 680);
            fDown = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 11), yellow);
            hardware.WriteText(fDown, 590, 710);
            hardware.WriteText(fDown, 115, 710);
            fLeft = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 12), yellow);
            hardware.WriteText(fLeft, 590, 650);
            hardware.WriteText(fLeft, 725, 680);
            fRight = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 13), yellow);
            hardware.WriteText(fRight, 115, 680);
            hardware.WriteText(fRight, 270, 710);
            fPause = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 14), yellow);
            hardware.WriteText(fPause, 380, 655);
            fEsc = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 15), yellow);
            hardware.WriteText(fEsc, 385, 680);
            fBomb = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 22), yellow);
            hardware.WriteText(fBomb, 270, 680);
            hardware.WriteText(fBomb, 725, 710);
            FPW = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 16), yellow);
            hardware.WriteText(FPW, 160, 730);
            FPR = SdlTtf.TTF_RenderText_Solid(font.GetFontType(),
                           len.Change(option, 17), yellow);
            hardware.WriteText(FPR, 615, 730);
            hardware.UpdateScreen();

            int keyPressed = hardware.KeyPressed();
            if (keyPressed == Hardware.KEY_ESC)
            {
                escPressed = true;
                exit = false;
            }
            else if (keyPressed == Hardware.KEY_ENTER)
            {
                enterPressed = true;
                exit = false;
            }

        } while (!escPressed && !enterPressed);
            
    }
}