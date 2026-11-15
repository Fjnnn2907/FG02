using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    Skeleton enemy;

    Transform character;
    CharacterStat playerStat;
    public SkeletonMoveState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (PlayerManager.instance != null && PlayerManager.instance.character != null)
        {
            character = PlayerManager.instance.character.transform;
            playerStat = PlayerManager.instance.character.GetComponent<CharacterStat>();
        }

        startTimer = Random.Range(5, 10);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.facing * enemy.speed, enemy.rb.linearVelocity.y);

        if (character != null)
        {
            bool playerAlive = playerStat == null || !playerStat.isDeah;

            if (playerAlive && (enemy.IsPlayerCheck() || Vector2.Distance(enemy.transform.position, character.position) < 2))
            {
                enemy.stateMachine.ChangeState(enemy.battleState);
                Debug.Log($"{enemy.gameObject.name} Change State Battle target {character.gameObject.name}");
                return;
            }
        }

        if (enemy.IsWallCheck() || !enemy.IsGroundCheck())
        {
            enemy.SetZeroVelocity();
            enemy.stateMachine.ChangeState(enemy.idleState);
        }

        if (startTimer <= 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}
