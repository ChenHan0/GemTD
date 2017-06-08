using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    static GameObject Confirm_UI;
    static GameObject Delete_UI;
    static GameObject Upgrade_UI;

    void Start () {
        Confirm_UI = Resources.Load("Confirm_UI", typeof(GameObject)) as GameObject;
        Delete_UI = Resources.Load("Delete_UI", typeof(GameObject)) as GameObject;
        Upgrade_UI = Resources.Load("Upgrade_UI", typeof(GameObject)) as GameObject;
    }

    /// <summary>
    /// 根据传入的物体，获得他的UI
    /// </summary>
    /// <param name="go">传入的物体</param>
    /// <returns>UI</returns>
    public static List<GameObject> GetUI(GameObject go)
    {
        List<GameObject> result = new List<GameObject>();

        if (go.GetComponent<Tower>() != null)
        {
            if (GameStateManager.GetCurrentState().Equals(ChooseState.Instance))
            {
                result.Add(Confirm_UI);

                // 天胡
            }
            else if (GameStateManager.GetCurrentState().Equals(EnemyAttackState.Instance))
            {
                if (TowerManager.IsUpgradableInAll(go.GetComponent<Tower>()))
                {
                    result.Add(Upgrade_UI);
                }
            }
        }
        else if (go.GetComponent<TowerBase>() != null)
        {
            if (GameStateManager.GetCurrentState().Equals(BuildState.Instance))
            {
                result.Add(Delete_UI);
            }
        }

        return result;
    } 
}
