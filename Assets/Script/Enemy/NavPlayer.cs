using UnityEngine;
using System.Collections;

public class NavPlayer : MonoBehaviour {
    public Vector3[] points = { new Vector3(-7.5f, 0.5f, -0.5f),
                                new Vector3(7.5f, 0.5f, -0.5f),
                                new Vector3(7.5f, 0.5f, 7.5f),
                                new Vector3(0.5f, 0.5f, 7.5f),
                                new Vector3(0.5f, 0.5f, -7.5f),
                                new Vector3(8.5f, 0.5f, -7.5f)};

    private int currentPoint = 0;

    private NavMeshAgent nav;
    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        
        nav.destination = points[currentPoint++];
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(nav.hasPath);
        if (Mathf.Abs(nav.remainingDistance) < 0.01)
        {
            if (currentPoint < points.Length)
            {
                nav.destination = points[currentPoint++];                
            }                
        }
    }
}
