
public class CharacterStateMachine
{
    public CharacterState currentState;

    public void StartState(CharacterState newState)
    {
        currentState = newState;
        currentState.Enter();
    }
    public void ChangeState(CharacterState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
