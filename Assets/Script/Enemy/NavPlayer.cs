using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class NavPlayer : MonoBehaviour {
    public PathPoints PathPoints;

    private Transform[] points = { };

    private int currentPoint = 0;

    //public EnemyUnitManager unitManager;

    private NavMeshAgent unit;

    private bool isChangeTarget = false;

    //private NavMeshPath path;

    void Start () {
        points = PathPoints.Points;

        unit = GetComponent<NavMeshAgent>();

        unit.destination = points[currentPoint++].position;

        //path = new NavMeshPath();
    }
	
	// Update is called once per frame
	void Update () {
        //float dist = agent.remainingDistance;
        //if(unit.remainingDistance != Mathf.Infinity && unit.pathStatus == NavMeshPathStatus.PathComplete && unit.remainingDistance == 0)
        if (Mathf.Abs(unit.remainingDistance) < 0.01 && !isChangeTarget)
        {
            isChangeTarget = true;
            if (currentPoint < points.Length)
            {
                Debug.Log(points[currentPoint]);
                unit.destination = points[currentPoint++].position;
            }
        }

        if (Mathf.Abs(unit.remainingDistance) > 0.01 && isChangeTarget)
        {
            isChangeTarget = false;
        }
        //if (Input.GetButtonDown("Jump"))
        //{
        //    Debug.Log("!!!!!!!!!!!");
        //    if (unitManager.pathAvailable == false)
        //    {
        //        Debug.Log("Path not available");
        //    }
        //    else
        //    {
        //        unit.SetPath(unitManager.GetComponent<EnemyUnitManager>().navMeshPath);
        //    }
        //}
    }
}
