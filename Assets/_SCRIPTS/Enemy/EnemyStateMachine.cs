public class EnemyStateMachine
{
    public EnemyState currentState;

    public void StartState(EnemyState newState)
    {
        currentState = newState;
        currentState.Enter();
    }
    public void ChangeState(EnemyState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
