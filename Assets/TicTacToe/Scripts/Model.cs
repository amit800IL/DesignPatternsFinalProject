
public class Model
{
    public int[,] gridState { get; private set; } = new int[3,3];
    public int Turn { get; private set; } = 1;
    public int Player1Score { get; set; } = 0;
    public int Player2Score { get; set; } = 0;

    public int[,] GetGridState()
    {
        return gridState;
    }

    public int[,] CloneGridState()
    {
        return (int[,])gridState.Clone();
    }

    public void SetGridState(int[,] grid)
    {
        gridState = grid;
    }

    public void SetTurn(int num)
    {
        Turn = num;
    }
}
