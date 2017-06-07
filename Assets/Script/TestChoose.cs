using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Tower))]
public class TestChoose : MonoBehaviour {

	void OnMouseDown()
    {
        //Debug.Log(0);
        if (GameStateManager.GetCurrentState() == ChooseState.Instance)
        {
            //Debug.Log(1);
            TowerManager.ChooseTowerInCurrentTime(GetComponent<Tower>());
            GameStateManager.ChangeState(EnemyAttackState.Instance);
        }
    }
}
