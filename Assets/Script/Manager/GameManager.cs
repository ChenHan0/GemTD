using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
	void Start () {
        GameStateManager.SetCurrentState(BuildState.Instance);
	}
	
	// Update is called once per frame
	void Update () {
	    if (TowerManager.CurrentTimeTowersList.Count > 4)
        {
            GameStateManager.ChangeState(ChooseState.Instance);
        }
	}
}
