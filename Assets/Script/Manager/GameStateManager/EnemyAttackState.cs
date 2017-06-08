using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttackState : GameState {
    EnemyManager enemyManager;

    private static EnemyAttackState instance;

    private bool[] bools;

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
        //Debug.Log("Enemies: " + EnemyManager.GetEnemiesCount());

        CheckAllTower();
    }

    public override void Exit()
    {
        // 解锁各种操作
    }


    /// <summary>
    /// 检查全局塔列表里面有没有符合升级条件
    /// </summary>
    void CheckAllTower()
    {
        bools = TowerManager.CheckTower(TowerManager.GetTowerString(TowerManager.AllTowersList));

        List<int> list = new List<int>();

        for(int i = 0; i < bools.Length; i++)
        {
            if (bools[i])
            {
                list.Add(i);
            }
        }

        List<string> upgradableTowerCodes = new List<string>();

        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                string[] str = TowersInfo.GetTowerUpgradeFormulasWithRow(list[i]);
                for (int j = 0; j < str.Length; j++)
                {
                    upgradableTowerCodes.Add(str[j]);
                }
            }
        }

        TowerManager.AllUpgradableTowerCodes = upgradableTowerCodes;
    }
}
