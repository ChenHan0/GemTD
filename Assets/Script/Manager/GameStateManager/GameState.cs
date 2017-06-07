using UnityEngine;
using System.Collections;

public class GameState {
    public virtual void Enter() { }

    public virtual void Execute() { }

    public virtual void Exit() { }
}
