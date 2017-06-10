using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

    
    static int CurrentLevel;

    public static int[,] BuildProbability =
    {
        { 9, 1, 0 },
        { 4, 5, 1 },
        { 3, 5, 2 },
        { 1, 6, 3 },
    };

    void Start()
    {
        CurrentLevel = 0;
    }

    public static int GetCurrentLevel()
    {
        return CurrentLevel + 1;
    }

    public static void LevelUp()
    {
        CurrentLevel++;
    }

    public static int[] GetCurrentProbability()
    {
        return GetBuildProbabilityWithRow(CurrentLevel);
    }

    static int[] GetBuildProbabilityWithRow(int row)
    {
        if (row >= BuildProbability.GetLength(0))
            throw new ArgumentOutOfRangeException("数组越界");
        int[] probability = new int[3];

        for (int i = 0; i < 3; i++)
        {
            probability[i] = BuildProbability[row, i];
        }

        return probability;
    }
}
