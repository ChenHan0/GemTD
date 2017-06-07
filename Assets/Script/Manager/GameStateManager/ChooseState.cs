using UnityEngine;
using System.Collections;

public class ChooseState : GameState {

    private static ChooseState instance;

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
    }

    public override void Execute()
    {
        //
    }

    public override void Exit()
    {
        // 
    }
}
