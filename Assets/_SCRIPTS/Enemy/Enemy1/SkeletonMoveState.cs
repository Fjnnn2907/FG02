using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    Skeleton enemy;

    Transform character;
    public SkeletonMoveState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        character = PlayerManager.instance.character.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.facing * enemy.speed,enemy.rb.velocity.y);

        if (enemy.IsWallCheck() || !enemy.IsGroundCheck())
        {
            enemy.Flip();
            enemy.stateMachine.ChangeState(enemy.idleState);
        }

        if(enemy.IsPlayerCheck() || Vector2.Distance(enemy.transform.position, character.position) < 2)
            enemy.stateMachine.ChangeState(enemy.battleState);
    }
}
