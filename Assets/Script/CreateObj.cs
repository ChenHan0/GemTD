using UnityEngine;
using System.Collections;

public class CreateObj : MonoBehaviour {
    public GameObject Obj;

    private GameObject go;

    Vector3 hitPos;
    Vector3 targetPos;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, 100))
            {     
                hitPos = hit.point;
                //Debug.DrawLine(Camera.main.transform.position, hitPos, Color.red);
                //Debug.Log(hitPos);
                targetPos = new Vector3(((int)hitPos.x), 0.5f, ((int)hitPos.z));
                if (hitPos.x >= 0)
                    targetPos.x += 0.5f;
                else
                    targetPos.x -= 0.5f;

                if (hitPos.z >= 0)
                    targetPos.z += 0.5f;
                else
                    targetPos.z -= 0.5f;
                //Debug.Log((int)hitPos.z);
                go = Instantiate(Obj);
                go.transform.position = targetPos;
            }
        }        
    }    
}
