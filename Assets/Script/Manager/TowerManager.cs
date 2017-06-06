using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {

    /// <summary>
    /// 由当前游戏中所有塔组成的List
    /// </summary>
    static List<Tower> AllTowersList;

    /// <summary>
    /// 由当前回合建造的塔所组成的List
    /// </summary>
    static List<Tower> CurrentTimeTowersList;

	void Start () {
        AllTowersList = new List<Tower>();

        CurrentTimeTowersList = new List<Tower>();
	}

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
    public static void ClearCurrentTimeTowersList()
    {
        CurrentTimeTowersList.Clear();
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
    static string GetTowerString(List<Tower> list)
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
