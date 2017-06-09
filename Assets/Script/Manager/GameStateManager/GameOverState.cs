using UnityEngine;
using System.Collections;

public class GameOverState : GameState {
    private static GameOverState instance;

    private GameOverState()
    {
    }

    public static GameOverState Instance
    {
        get
        {
            if (null == instance)
                instance = new GameOverState();
            return instance;
        }
    }

    public override void Enter()
    {
        Debug.LogError("GAME OVER!!");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }
}
