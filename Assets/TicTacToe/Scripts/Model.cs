
public class Model
{
    public int[,] gridState { get; private set; } = new int[3,3];
    public int Turn { get; private set; } = 1;

    //memento - holds previous states of the game.

    //Stack<int[,]> previousStates = new Stack<int[,]>();
    //Stack<int[,]> nextStates = new Stack<int[,]>();
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

    //public int[,] GetPreviousState()
    //{
    //    //automatically moves the Popped previous state into the next state instead of having to rely on the controller handling it in the correct order.
    //    nextStates.Push(previousStates.Peek());
    //    return previousStates.Pop();
    //}

    //public void SetPreviousState(int[,] grid)
    //{
    //    previousStates.Push(grid);
    //}

    //public int[,] GetNextState()
    //{
    //    if(nextStates.Count == 0)
    //    {
    //        return null;
    //    }
    //    previousStates.Push(nextStates.Peek());
    //    return nextStates.Pop();
    //}
}
