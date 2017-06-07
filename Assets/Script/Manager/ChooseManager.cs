using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseManager : MonoBehaviour {
    static List<Tower> CurrentTimeTowers;

    void Start()
    {
        CurrentTimeTowers = TowerManager.CurrentTimeTowersList;
    }


}
