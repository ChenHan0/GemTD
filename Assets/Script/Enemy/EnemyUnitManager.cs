using UnityEngine;
using System.Collections;

public class EnemyUnitManager : MonoBehaviour {

    public NavMeshAgent spawnPosition;
    public Vector3 targetPosition;

    [HideInInspector]
    public bool pathAvailable;
    public NavMeshPath navMeshPath;

	// Use this for initialization
	void Start () {
        navMeshPath = new NavMeshPath();
	}
	
	// Update is called once per frame
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
