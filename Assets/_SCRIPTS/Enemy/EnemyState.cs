using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyState
{
    public Enemy enemyBase;
    public EnemyStateMachine stateMachine;

    public string isBoolName;
    public int animBoolHash { get; private set; }
    public static readonly int xVelocityHash = Animator.StringToHash("xVelocity");
    public bool isCalled;
    public float startTimer;

    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string isBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.isBoolName = isBoolName;
        this.animBoolHash = !string.IsNullOrEmpty(isBoolName) ? Animator.StringToHash(isBoolName) : 0;
    }
    public virtual void Enter()
    {
        enemyBase.anim.SetBool(animBoolHash, true);
        isCalled = false;
    }
    public virtual void Update()
    {
        startTimer -= Time.deltaTime;
        enemyBase.anim.SetFloat(xVelocityHash, enemyBase.rb.linearVelocity.x);

    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolHash, false);
    }
    public void AminationTrigger()
    {
        isCalled = true;
    }
}
