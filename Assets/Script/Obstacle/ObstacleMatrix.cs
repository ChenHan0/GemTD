using UnityEngine;
using System.Collections;

public static class ObstacleMatrix {
    public static bool[,] allowedTiles;
    public static float[] originSquare;

    public static void Initialize()
    {
        BuildMatrix();
        SetOriginSquare();
    }

    public static void BuildMatrix()
    {
        int[] size = SetMatrixSize();

        allowedTiles = new bool[size[0], size[1]];

        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                allowedTiles[i, j] = true;
            }
        }
    }

    private static int[] SetMatrixSize()
    {
        int[] size = { 20, 20 };

        return size;
    }

    public static void SetOriginSquare()
    {
        originSquare = new float[2] { -9.5f, -9.5f };
    }

    public static void RegisterSquare(Vector3 vec, bool status)
    {
        int[] square = GetSquare(vec);
        allowedTiles[square[0], square[1]] = status;
    }

    public static bool CheckSquare(Vector3 vec)
    {
        int[] square = GetSquare(vec);
        return allowedTiles[square[0], square[1]];
    }

    private static int[] GetSquare(Vector3 vec)
    {
        int[] square = new int[2];
        square[0] = (int)(vec.x - originSquare[0]);
        square[1] = (int)(vec.z - originSquare[1]);

        return square;
    }

    public static string MatrixToString()
    {
        string text = "Occupied fields are 1, free fields are 0:\n\n";
        for (int j = allowedTiles.GetLength(1) - 1; j >= 0; j--)
        {
            for (int i = 0; i < allowedTiles.GetLength(0); i++)
            {
                text = text + (allowedTiles[i, j] ? "0" : "1") + " ";
            }
            text += "\n";
        }
        return text;
    }
}
