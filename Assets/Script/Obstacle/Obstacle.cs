using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ObstacleMatrix.RegisterSquare(transform.position, false);
	}
}
