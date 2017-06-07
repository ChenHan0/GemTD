using UnityEngine;
using System.Collections;

public class PathPoints : MonoBehaviour {
    public Transform[] Points;

    void Start()
    {
        for(int i = 0; i < Points.Length; i++)
        {
            ObstacleMatrix.RegisterSquare(Points[i].position, false);
        }        
    }
}