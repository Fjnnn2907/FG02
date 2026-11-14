using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    Skeleton enemy;

    private Transform character;
    private CharacterStat playerStat;
    public int moveDir;
    public SkeletonBattleState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        startTimer = enemy.battleTime;

        if (PlayerManager.instance != null && PlayerManager.instance.character != null)
        {
            character = PlayerManager.instance.character.transform;
            playerStat = PlayerManager.instance.character.GetComponent<CharacterStat>();
        }

        if (character == null)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (playerStat != null && playerStat.isDeah)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (character == null)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (playerStat != null && playerStat.isDeah)
        {
            stateMachine.ChangeState(enemy.moveState);
            return;
        }

        // Tính khoảng cách ngang giữa Skeleton và Character
        float deltaX = character.position.x - enemy.transform.position.x;
        float distanceToPlayer = Mathf.Abs(deltaX);

        // Quay mặt về phía Character nếu đang trong battle
        if (Mathf.Abs(deltaX) > 0.1f)
            enemy.FlipCtrl(deltaX);

        RaycastHit2D playerHit = enemy.IsPlayerCheck();
        bool inAttackRange = (playerHit && playerHit.distance <= enemy.radiusAttack) || distanceToPlayer <= enemy.radiusAttack;

        if (playerHit || distanceToPlayer <= enemy.attackDistace)
        {
            startTimer = enemy.battleTime;

            if (inAttackRange)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                    return;
                }

                // Trong thời gian hồi chiêu và character ở trong tầm đánh: đứng yên (xVelocity = 0 -> Idle)
                enemy.SetZeroVelocity();
                return;
            }
        }
        else
        {
            if (startTimer <= 0 || Vector2.Distance(enemy.transform.position, character.position) > 7)
            {
                stateMachine.ChangeState(enemy.idleState);
                return;
            }
        }

        // Khi character rời tầm đánh: di chuyển đuổi theo (xVelocity != 0 -> Move)
        if (distanceToPlayer > 0.2f)
            moveDir = deltaX > 0 ? 1 : -1;
        else
        {
            enemy.SetZeroVelocity();
            return;
        }

        if (!enemy.IsGroundCheck() && moveDir == enemy.facing)
        {
            enemy.SetZeroVelocity();
            return;
        }

        float chaseSpeed = enemy.speedBattle > 0 ? enemy.speedBattle : enemy.speed;
        enemy.SetVelocity(chaseSpeed * moveDir, enemy.rb.linearVelocity.y);
    }
    private bool CanAttack()
    {
        if (Time.time >= enemyBase.lastTimeAttacked + enemyBase.attackCoolDown)
        {
            enemyBase.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
