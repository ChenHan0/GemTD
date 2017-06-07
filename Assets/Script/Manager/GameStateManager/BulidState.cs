using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildState : GameState
{
    TowerManager towerManager;
    private static BuildState instance;

    private BuildState()
    {
        towerManager = GameObject.Find("Manager").GetComponent<TowerManager>();
    }

    public static BuildState Instance
    {
        get
        {
            if (null == instance)
                instance = new BuildState();
            return instance;
        }
    }

    public override void Enter()
    {
        // 改变UI 建造及删除
        
    }

    public override void Execute()
    {
        // 点击建造
        Bulid();
    }

    public override void Exit()
    {
        // 改变UI的显示 只能选择 不能删除底座
    }

    /// <summary>
    /// 当鼠标点击时，在所指网格建造塔
    /// </summary>
    void Bulid()
    {
        towerManager.BuildTower();
    }
}
