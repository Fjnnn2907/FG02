using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    Skeleton enemy;

    Transform character;
    CharacterStat playerStat;
    public SkeletonIdleState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        startTimer = Random.Range(1f, 3f);

        if (PlayerManager.instance != null && PlayerManager.instance.character != null)
        {
            character = PlayerManager.instance.character.transform;
            playerStat = PlayerManager.instance.character.GetComponent<CharacterStat>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(24, enemy.transform);
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (character != null)
        {
            bool playerAlive = playerStat == null || !playerStat.isDeah;

            if (playerAlive && (enemy.IsPlayerCheck() || Vector2.Distance(enemy.transform.position, character.position) < 2))
            {
                enemy.stateMachine.ChangeState(enemy.battleState);
                return;
            }
        }

        if (startTimer <= 0)
        {           
            if (enemy.IsWallCheck() || !enemy.IsGroundCheck())
                enemy.Flip();
            else
            {
                if (enemy.GetRandom(Random.Range(0, 100)))
                    enemy.Flip();
            }

            enemy.stateMachine.ChangeState(enemy.moveState);
        }
    }
}
