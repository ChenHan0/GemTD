using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    static GameObject Confirm_UI;
    static GameObject Delete_UI;
    static GameObject Upgrade_UI;
    static GameObject Background_UI;

    void Start () {
        Confirm_UI = Resources.Load("Confirm_UI", typeof(GameObject)) as GameObject;
        Delete_UI = Resources.Load("Delete_UI", typeof(GameObject)) as GameObject;
        Upgrade_UI = Resources.Load("Upgrade_UI", typeof(GameObject)) as GameObject;
        Background_UI = Resources.Load("Background_UI", typeof(GameObject)) as GameObject;
    }

    /// <summary>
    /// 根据传入的物体，获得他的UI
    /// </summary>
    /// <param name="go">传入的物体</param>
    /// <returns>UI</returns>
    public static List<GameObject> ShowUI(GameObject go)
    {
        List<GameObject> result = new List<GameObject>();

        Transform parent = go.transform.parent;
        Vector3 pos = new Vector3(parent.position.x, 1.5f, parent.position.z);
        GameObject ui = Instantiate(Background_UI, pos, Quaternion.Euler(0, 180, 0)) as GameObject;
        result.Add(ui);

        if (go.GetComponent<Tower>() != null)
        {
            if (GameStateManager.GetCurrentState().Equals(ChooseState.Instance))
            {
                ui = Instantiate(Confirm_UI, pos + new Vector3(0.78f, 0.05f, 0f), Quaternion.Euler(0, 180, 0)) as GameObject;
                result.Add(ui);

                // 天胡
                if (TowerManager.IsUpgradableInCurrent(go.GetComponent<Tower>()))
                {
                    ui = Instantiate(Upgrade_UI, pos - new Vector3(0.78f, -0.05f, 0f), Quaternion.Euler(0, 180, 0)) as GameObject;
                    result.Add(ui);
                }
            }
            else if (GameStateManager.GetCurrentState().Equals(EnemyAttackState.Instance))
            {
                if (TowerManager.IsUpgradableInAll(go.GetComponent<Tower>()))
                {
                    ui = Instantiate(Upgrade_UI, pos + new Vector3(0.78f, 0.05f, 0f), Quaternion.Euler(0, 180, 0)) as GameObject;
                    result.Add(ui);
                }
            }
        }
        else if (go.GetComponent<TowerBase>() != null)
        {
            if (GameStateManager.GetCurrentState().Equals(BuildState.Instance))
            {
                ui = Instantiate(Delete_UI, pos + new Vector3(0.78f, 0.05f, 0f), Quaternion.Euler(0, 180, 0)) as GameObject;
                result.Add(ui);
            }
        }

        return result;
    } 
}
