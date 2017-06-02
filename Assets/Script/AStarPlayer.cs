using UnityEngine;
using System.Collections;
using Pathfinding;

public class AStarPlayer : MonoBehaviour {
    // 目标位置
    Vector3 targetPos;

    Seeker seeker;
    CharacterController characterController;

    // 计算出来的路线
    Path path;

    // 移动速度
    public float PlayerMoveSpeed = 10f;

    // 当前点
    int currentWayPoint = 0;

    bool stopMove = true;

    // Player中心点
    float playerCenterY = 1.0f;

    static Vector3[] points = { new Vector3(-3.5f, 0.5f, -0.5f),
                                new Vector3(3.5f, 0.5f, -0.5f),
                                new Vector3(3.5f, 0.5f, 3.5f),
                                new Vector3(0.0f, 0.5f, 3.5f),
                                new Vector3(0.0f, 0.5f, -3.5f),
                                new Vector3(4.5f, 0.5f, -3.5f),};
    int currentPoint = 0;

	// Use this for initialization
	void Start () {
        seeker = GetComponent<Seeker>();
        playerCenterY = transform.localPosition.y;
	}
	
    // 寻路结束
    public void OnPathComplete(Path p)
    {
        Debug.Log("OnPathComplete error = " + p.error);

        if (!p.error)
        {
            currentWayPoint = 0;
            path = p;
            stopMove = false;
        }

        for (int i = 0; i < path.vectorPath.Count; i++)
        {
            Debug.Log("path.vectorPath[" + i + "]=" + path.vectorPath[i]);
        }

        //if (currentPoint < points.Length)
        //{
        //    targetPos = points[currentPoint++];

        //    Debug.Log("targetPosition=" + targetPos);

        //    seeker.StartPath(transform.position, targetPos, OnPathComplete);
        //}
        
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, 100))
            {
                return;
            }
            if (!hit.transform)
            {
                return;
            }
            targetPos = hit.point;

            //targetPos = points[currentPoint++];

            Debug.Log("targetPosition=" + targetPos);

            seeker.StartPath(transform.position, targetPos, OnPathComplete);
        }
	}

    void FixedUpdate()
    {
        if (path == null || stopMove)
        {
            return;
        }

        // 根据Player当前位置和下一个寻路点的位置，计算方向；
        Vector3 currentWayPointV = new Vector3(path.vectorPath[currentWayPoint].x,
            path.vectorPath[currentWayPoint].y + playerCenterY,
            path.vectorPath[currentWayPoint].z);
        Vector3 dir = (currentWayPointV - transform.position).normalized;

        // 计算这一帧要朝向dir方向移动多少距离
        dir *= PlayerMoveSpeed * Time.fixedDeltaTime;

        // 计算加上这一帧的位移，是不是会超过下一个节点
        float offset = Vector3.Distance(transform.localPosition, currentWayPointV);

        if (offset < 0.1f)
        {
            transform.localPosition = currentWayPointV;

            currentWayPoint++;

            if (currentWayPoint == path.vectorPath.Count)
            {
                stopMove = true;

                currentWayPoint = 0;

                path = null;
            }
        }
        else
        {
            if (dir.magnitude > offset)
            {
                Vector3 tmpV3 = dir * (offset / dir.magnitude);
                dir = tmpV3;

                currentWayPoint++;

                if (currentWayPoint == path.vectorPath.Count)
                {
                    stopMove = true;

                    currentWayPoint = 0;

                    path = null;
                }
            }
            transform.localPosition += dir;
        }
    }
}
