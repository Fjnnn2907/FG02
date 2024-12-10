using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    Skeleton enemy;

    Transform character;
    public SkeletonIdleState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        startTimer = Random.Range(1, 3);
       

        character = PlayerManager.instance.character.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (startTimer <= 0)
            enemy.stateMachine.ChangeState(enemy.moveState);         

        if (enemy.IsPlayerCheck() || Vector2.Distance(enemy.transform.position, character.position) < 2)
            enemy.stateMachine.ChangeState(enemy.battleState);
    }
}
