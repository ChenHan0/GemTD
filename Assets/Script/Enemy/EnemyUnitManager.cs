using UnityEngine;
using System.Collections;

public class EnemyUnitManager : MonoBehaviour {

    public NavMeshAgent spawnPosition;
    public Vector3 targetPosition;

    [HideInInspector]
    public bool pathAvailable;
    public NavMeshPath navMeshPath;

    public PathPoints Points;

	void Start () {
        navMeshPath = new NavMeshPath();

        
	}

	void Update () {
        if (CalculateNewPath() == true)
        {
            pathAvailable = true;
            print("Path available");
        }
        else
        {
            pathAvailable = false;
            print("Path not available");
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log(CalculatePath());
        }
    }

    public bool CalculatePath()
    {

        //for (int i = 0; i < Points.Points.Length; i++)
        //{
        //    spawnPosition.CalculatePath(Points.Points[i].position, navMeshPath);

        //    if (navMeshPath.status != NavMeshPathStatus.PathComplete)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        spawnPosition.transform.position = Points.Points[i].position;
        //    }
        //}

        //return true;

        spawnPosition.CalculatePath(Points.Points[Points.Points.Length - 1].position, navMeshPath);

        if (navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool CalculateNewPath()
    {
        spawnPosition.CalculatePath(targetPosition, navMeshPath);
        print("New path calculated");

        if (navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
