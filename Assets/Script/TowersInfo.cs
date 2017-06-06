using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TowersInfo
{
    public static string[,] TowerUpgradeFormulas =
        {
            { "A1", "B1", "C1" },   // J1
            { "D1", "E1", "F1" },   // K1
            { "G1", "H1", "I1" },   // L1
            { "A2", "B2", "C2" },   // J2
            { "D2", "E2", "F2" },   // K2
            { "G2", "H2", "I2" },   // L2
            { "A3", "B3", "C3" },   // J3
            { "D3", "E3", "F3" },   // K3
            { "G3", "H3", "I3" },   // L3
            { "J1", "K1", "L1" },   // M1
            { "J2", "K2", "L2" },   // M2
            { "J3", "K3", "L3" }    // M2
        };  // 12

    public static string[] GetTowerUpgradeFormulasWithRow(int row)
    {
        if (row >= TowerUpgradeFormulas.GetLength(0))
            throw new ArgumentOutOfRangeException("数组越界");
        string[] str = new string[3];

        for (int i = 0; i < 3; i++)
        {
            str[i] = TowerUpgradeFormulas[row, i];
        }

        return str;
    }
}
