using System;
using System.IO;

class NameMap : Screen
{
    const int WIDTH = 21;
    const int HEIGHT = 16;

    Image imgFloor, imgCursor;
    MapCreationScreen map;

    public NameMap(Hardware hardware) : base(hardware)
    {
        imgCursor = new Image("imgs/cursor.png", 40,40);
        imgFloor = new Image("imgs/Floor.png", 840, 680);
    }
}

