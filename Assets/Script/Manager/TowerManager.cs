using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {

    public GameObject[] LevelOneTowerPrefabs;

    public GameObject[] LevelTwoTowerPrefabs;

    public GameObject[] LevelThreeTowerPrefabs;

    /// <summary>
    /// 由当前游戏中所有塔组成的List
    /// </summary>
    static List<Tower> AllTowersList;

    /// <summary>
    /// 由当前回合建造的塔所组成的List
    /// </summary>
    public static List<Tower> CurrentTimeTowersList;

    Vector3 hitPos;
    Vector3 targetPos;

    EnemyUnitManager enemyUnitManager;
    GameObject PreviousObj;

    List<char> LEVEL = new List<char>(
    new char[] {
            '1','2','3'
    });

    void Start () {
        AllTowersList = new List<Tower>();

        CurrentTimeTowersList = new List<Tower>();

        enemyUnitManager = gameObject.GetComponent<EnemyUnitManager>();
	}

    #region BuildTower
    public void BuildTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, 100))
            {
                hitPos = hit.point;
                //Debug.DrawLine(Camera.main.transform.position, hitPos, Color.red);
                //Debug.Log(hitPos);
                targetPos = new Vector3(((int)hitPos.x), 0.5f, ((int)hitPos.z));
                if (hitPos.x >= 0)
                    targetPos.x += 0.5f;
                else
                    targetPos.x -= 0.5f;

                if (hitPos.z >= 0)
                    targetPos.z += 0.5f;
                else
                    targetPos.z -= 0.5f;
                //Debug.Log((int)hitPos.z);
                if (ObstacleMatrix.CheckSquare(targetPos))
                {
                    //go = Instantiate(Obj);
                    //go.transform.position = targetPos;
                    //PreviousObj = go;
                    // 随机创建一个塔
                    GameObject go = Instantiate(GetTower());
                    go.transform.position = targetPos;
                    PreviousObj = go;
                    CurrentTimeTowersList.Add(PreviousObj.transform.GetChild(1).GetComponent<Tower>());
                 }
            }
        }

        CheckPath();
    }
    
    /// <summary>
    /// 检测敌人是否能走到终点
    /// </summary>
    void CheckPath()
    {
        if (!enemyUnitManager.CalculatePath())
        {
            if (PreviousObj)
            {
                CurrentTimeTowersList.Remove(PreviousObj.GetComponent<Tower>());
                PreviousObj.GetComponent<Obstacle>().DestroySelf();
            }
                
            Debug.Log("There is no path!");
        }
    }

    /// <summary>
    /// 根据当前等级概率随机得到一个塔
    /// </summary>
    GameObject GetTower()
    {
        #region 加权随机
        int[] weights = LevelManager.GetCurrentProbability();

        Dictionary<char, int> dict = new Dictionary<char, int>(3);

        for (int i = LEVEL.Count - 1; i >= 0; i--)
        {
            dict.Add(LEVEL[i], Random.Range(0, 100) * weights[i]);
        }

        List<KeyValuePair<char, int>> listDict = SortByValue(dict);
        #endregion

        int result = int.Parse(listDict[0].Key.ToString());

        int randomIndex = Random.Range(0, 8);

        GameObject FinallyTower = null;

        switch (result)
        {
            case 1:
                FinallyTower =
                    LevelOneTowerPrefabs[randomIndex];
                break;
            case 2:
                FinallyTower =
                    LevelTwoTowerPrefabs[randomIndex];
                break;
            case 3:
                FinallyTower =
                    LevelThreeTowerPrefabs[randomIndex];
                break;
        }

        return FinallyTower;
    }
    
    #region Tools
    /// <summary>
    /// 排序集合
    /// </summary>
    /// <param name="dict"></param>
    /// <returns></returns>
    private List<KeyValuePair<char, int>> SortByValue(Dictionary<char, int> dict)
    {
        List<KeyValuePair<char, int>> list = new List<KeyValuePair<char, int>>();

        if (dict != null)
        {
            list.AddRange(dict);

            list.Sort(
              delegate (KeyValuePair<char, int> kvp1, KeyValuePair<char, int> kvp2)
              {
                  return kvp2.Value - kvp1.Value;
              });
        }
        return list;
    }
    #endregion
    #endregion


    /// <summary>
    /// 往全局塔列表中添加元素
    /// </summary>
    /// <param name="tower">要添加入列表的塔</param>
    public static void AddTowerToAll(Tower tower)
    {
        AllTowersList.Add(tower);
    }

    /// <summary>
    /// 往当前回合塔列表中添加元素
    /// </summary>
    /// <param name="tower">要添加入列表的塔</param>
    public static void AddTowerToCurrentTime(Tower tower)
    {
        CurrentTimeTowersList.Add(tower);
    }

    /// <summary>
    /// 清除当前回合塔列表中的元素
    /// </summary>
    static void ClearCurrentTimeTowersList()
    {
        CurrentTimeTowersList.Clear();
    }

    /// <summary>
    /// 在当前回合塔中选定一个留下
    /// </summary>
    /// <param name="tower">选择的塔</param>
    public static void ChooseTowerInCurrentTime(Tower tower)
    {
        if (CurrentTimeTowersList.Contains(tower))
        {
            AddTowerToAll(tower);

            for (int i = CurrentTimeTowersList.Count - 1; i >= 0; i--)
            {
                Tower t = CurrentTimeTowersList[i];
                if (!t.Equals(tower))
                {
                    t.DestroySelf();
                }
            }

            ClearCurrentTimeTowersList();
        }
    }

    /// 检查塔是否符合升级公式
    /// </summary>
    /// <param name="TowersCoresString">需要检查的塔形成的字符串(全局塔或当前回合塔)</param>
    /// <returns>表示符合哪些升级公式的布尔数组</returns>
    public static bool[] CheckTower(string TowersCoresString)
    {
        bool[] IsCanUpgradeFlag = new bool[12];

        for (int i = 0; i < IsCanUpgradeFlag.Length; i++)
        {
            IsCanUpgradeFlag[i] = false;
        }


        //string allTowerString = GetAllTowerString();

        for (int i = 0; i < TowersInfo.TowerUpgradeFormulas.GetLength(0); i++)
        {
            //System.Text.RegularExpressions.Regex.IsMatch();
            if (System.Text.RegularExpressions.Regex.IsMatch(TowersCoresString,
                GetRegularExpression(TowersInfo.GetTowerUpgradeFormulasWithRow(i))))
                IsCanUpgradeFlag[i] = true;
        }

        return IsCanUpgradeFlag;
    }

    /// <summary>
    /// 根据塔列表生成一个代号字符串
    /// </summary>
    /// <param name="list">需要生成字符串的塔列表</param>
    /// <returns>代号字符串</returns>
    public static string GetTowerString(List<Tower> list)
    {
        string str = "";

        for (int i = 0; i < list.Count; i++)
        {
            str += list[i].TowerCore;
        }

        return str;
    }

    // (?=.*a)(?=.*b)(?=.*c)^.*$
    /// <summary>
    /// 形成一个形如"(?=.*<codes[1]>)(?=.*<codes[2]>)(?=.*<codes[3]>)...^.*$"的正则表达式
    /// </summary>
    /// <param name="codes">正常为3长度的字符串数组</param>
    /// <returns>一个规定形式的正则表达式</returns>
    static string GetRegularExpression(string[] codes)
    {
        string RegularExpression = "";

        for(int i = 0; i < codes.Length;i++)
        {
            RegularExpression += ("(?=.*" + codes[i] + ")");
        }

        RegularExpression += "^.*$";

        return RegularExpression;
    }

    /// <summary>
    /// 在全局塔中进行升级，销毁升级素材
    /// </summary>
    /// <param name="codes">升级素材代号</param>
    /// <param name="newTower">新塔，用于加入全局塔列表</param>
    static void UpgradeTowerWithAllTower(string[] codes, Tower newTower)
    {
        Tower tower;
        for (int i = 0; i < codes.Length; i++)
        {
            for (int j = 0; j < AllTowersList.Count; j++)
            {
                if (AllTowersList[j].TowerCore.Equals(codes[i]))
                {
                    tower = AllTowersList[j];
                    AllTowersList.Remove(tower);
                    tower.DestroySelf();
                    break;
                }
            }
        }

        AddTowerToAll(newTower);
    }
}
