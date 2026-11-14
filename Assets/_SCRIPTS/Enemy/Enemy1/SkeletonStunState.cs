
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    Skeleton enemy;
    public SkeletonStunState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        startTimer = enemy.stunTime > 0 ? enemy.stunTime : 1f;
        if (enemy.fx != null)
            enemy.fx.InvokeRepeating("RedColorBlink", 0, .2f);
        if (enemy.rb != null)
            enemy.rb.linearVelocity = new Vector2(-enemy.facing * enemy.stunDir.x, enemy.stunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        if (enemy.fx != null)
            enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if (startTimer < 0)
            stateMachine.ChangeState(enemy.battleState);      
    }
}
