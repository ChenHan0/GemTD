using UnityEngine;
using System.Collections;

public class SetupObstacleMatrix : MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
        ObstacleMatrix.Initialize();
    }

    void OnGUI()
    {
        //GUI.TextArea(new Rect(10, 10, 250, 350), ObstacleMatrix.MatrixToString());
    }
}
