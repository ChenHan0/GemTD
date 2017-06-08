using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseState : GameState {

    private static ChooseState instance;

    private bool[] bools;

    private ChooseState()
    {
        
    }

    public static ChooseState Instance
    {
        get
        {
            if (null == instance)
                instance = new ChooseState();
            return instance;
        }
    }

    public override void Enter()
    {
        // 
        //Debug.Log(TowerManager.GetTowerString(TowerManager.CurrentTimeTowersList));
        CheckCurrentTimeTower();
    }

    public override void Execute()
    {
        //        
    }

    public override void Exit()
    {
        // 
    }

    void CheckCurrentTimeTower()
    {
        bools = TowerManager.CheckTower(TowerManager.GetTowerString(TowerManager.CurrentTimeTowersList));

        int no = 100;

        for (int i = 0; i < bools.Length; i++)
        {
            if (bools[i])
            {
                no = i;
            }
        }

        List<string> upgradableTowerCodes = new List<string>();

        //if (list.Count > 0)
        //{
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        string[] str = TowersInfo.GetTowerUpgradeFormulasWithRow(i);
        //        for (int j = 0; j < str.Length; j++)
        //        {
        //            upgradableTowerCodes.Add(str[j]);
        //        }
        //    }
        //}

        if (no < bools.Length)
        {
            string[] str = TowersInfo.GetTowerUpgradeFormulasWithRow(no);
            for (int i = 0; i < str.Length; i++)
            {
                upgradableTowerCodes.Add(str[i]);
            }
        }

        TowerManager.CurrentUpgradableTowerCodes = upgradableTowerCodes;
    }
}
