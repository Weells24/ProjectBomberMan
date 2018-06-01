class Lenguage
{
    protected string[][] Select = new string[2][];
    int option = SelectLenguage.option;

    public string Change(int option, int word)
    {
        Select[0] = new string[23];
        Select[1] = new string[23];
        Select[0][0] = "Play";
        Select[0][1] = "Map Creation";
        Select[0][2] = "Controls";
        Select[0][3] = "Credits";
        Select[0][4] = "Exit";
        Select[0][5] = "Map:";
        Select[0][6] = "Start";
        Select[0][7] = "Player";
        Select[0][8] = "Save File!";
        Select[0][9] = "Move Cursor";
        Select[0][10] = "Up Move";
        Select[0][11] = "Down Move";
        Select[0][12] = "Left Move";
        Select[0][13] = "Right Move";
        Select[0][14] = "Pause";
        Select[0][15] = "Back";
        Select[0][16] = "Player White";
        Select[0][17] = "Player Red";
        Select[0][18] = "Spanish";
        Select[0][19] = "English";
        Select[0][20] = "Press Supr To Delete Map";
        Select[0][21] = "Name:";
        Select[1][22] = "Bomb";

        //Spanish
        Select[1][0] = "Jugar";
        Select[1][1] = "Creacion de Mapas";
        Select[1][2] = "Controles";
        Select[1][3] = "Creditos";
        Select[1][4] = "Salir";
        Select[1][5] = "Mapa:";
        Select[1][6] = "Empezar";
        Select[1][7] = "Jugador";
        Select[1][8] = "¡Mapa Guardado!";
        Select[1][9] = "Mover Cursor";
        Select[1][10] = "Mover arriba";
        Select[1][11] = "Mover Abajo";
        Select[1][12] = "Mover Izquierda";
        Select[1][13] = "Mover Derecha";
        Select[1][14] = "Pausa";
        Select[1][15] = "Atras";
        Select[1][16] = "Jugador Blanco";
        Select[1][17] = "Jugador Rojo";
        Select[1][18] = "Español";
        Select[1][19] = "Ingles";
        Select[1][20] = "Pulsa Supr Para borrar el Mapa";
        Select[0][21] = "Nombre:";
        Select[1][22] = "Poner Bomba";

        return Select[option][word];
    }
}
