using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ObstacleMatrix.RegisterSquare(transform.position, false);
	}

    public void DestroySelf()
    {
        ObstacleMatrix.RegisterSquare(transform.position, true);
        Destroy(transform.parent.gameObject);
    }
}
