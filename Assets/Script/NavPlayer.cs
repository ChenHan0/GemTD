using UnityEngine;
using System.Collections;

public class NavPlayer : MonoBehaviour {
    public Vector3[] points = { new Vector3(-3.5f, 0.5f, -0.5f),
                                new Vector3(3.5f, 0.5f, -0.5f),
                                new Vector3(3.5f, 0.5f, 3.5f),
                                new Vector3(0.0f, 0.5f, 3.5f),
                                new Vector3(0.0f, 0.5f, -3.5f),
                                new Vector3(4.5f, 0.5f, -3.5f)};

    private int currentPoint = 0;

    public GameObject Obstacle;

    private NavMeshAgent nav;
    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        nav.destination = points[currentPoint++];
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(nav.remainingDistance) < 0.01)
        {
            if (currentPoint < points.Length)
                nav.destination = points[currentPoint++];
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Instantiate(Obstacle);
        }
    }
}
