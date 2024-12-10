using UnityEngine;

public class EnemyState
{
    public Enemy enemyBase;
    public EnemyStateMachine stateMachine;

    public string isBoolName;
    public bool isCalled;
    public float startTimer;

    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string isBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.isBoolName = isBoolName;
    }
    public virtual void Enter()
    {
        enemyBase.anim.SetBool(isBoolName,true);
        isCalled = false;
    }
    public virtual void Update()
    {
        startTimer -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(isBoolName, false);
    }
    public void AminationTrigger()
    {
        isCalled = true;
    }
}
