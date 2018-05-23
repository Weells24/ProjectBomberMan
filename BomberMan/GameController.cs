using System;
using Tao.Sdl;


class GameController
{
    public const short SCREEN_WIDTH = 840;
    public const short SCREEN_HEIGHT = 755;
    public void Start()
    {
        Hardware hardware = new Hardware(840, 755, 24, false);
        WelcomeScreen welcome = new WelcomeScreen(hardware);
        SettingScreen setting = new SettingScreen(hardware);
        ControllerScreen controller = new ControllerScreen(hardware);
        CreditsScreen credits = new CreditsScreen(hardware);

        do
        {
            hardware.ClearScreen();
            if (!welcome.GetExit())
            {
                welcome.Show();
                switch (welcome.GetChosenOption())
                {
                    case 1:
                        setting.Show();
                        break;
                    case 2:
                        //TO DO
                        break;
                    case 3:
                        controller.Show();
                        break;
                    case 4:
                        credits.Show();
                        break;
                    case 5:
                        welcome.GetExit();
                        break;
                }
            }
        } while (!welcome.GetExit());
    }
}
