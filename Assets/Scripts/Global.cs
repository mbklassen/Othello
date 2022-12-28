
public class Global {

    // Initialize Global variables, which exist outside of the current scene
    
    public static char[,] gridArray = new char[8, 8];
    public static char[,] probeArray = new char[8, 8];
    public static char[,] pseudoProbeArray = new char[8, 8];
    
    public static bool blackTurn = true;
    public static bool whiteTurn = false;
    public static bool newTurnStarted = false;

    public static int blackScore = 2;
    public static int whiteScore = 2;

    public static bool gameOver = false;

}
