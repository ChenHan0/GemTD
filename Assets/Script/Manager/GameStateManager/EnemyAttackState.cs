using UnityEngine;
using System.Collections;

public class EnemyAttackState : GameState {
    EnemyManager enemyManager;

    private static EnemyAttackState instance;

    private EnemyAttackState()
    {
        enemyManager = GameObject.Find("Manager").GetComponent<EnemyManager>();
    }

    public static EnemyAttackState Instance
    {
        get
        {
            if (null == instance)
                instance = new EnemyAttackState();
            return instance;
        }
    }

    public override void Enter()
    {
        // 锁定各种操作
        enemyManager.StartAttack();
    }

    public override void Execute()
    {
        // 
        Debug.Log("Enemies: " + EnemyManager.GetEnemiesCount());
    }

    public override void Exit()
    {
        // 解锁各种操作
    }
}
