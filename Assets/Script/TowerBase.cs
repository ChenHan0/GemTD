using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBase : MonoBehaviour {
    private List<GameObject> UIs;


	// Use this for initialization
	void Start () {
        UIs = new List<GameObject>();
	}

    void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        UIs = UIManager.ShowUI(this.gameObject);
    }

    void OnMouseDrag()
    {
        if (GameStateManager.GetCurrentState() == BuildState.Instance)
        {
            if (Input.GetAxis("Mouse X") >= 0.8f)
            {
                OnMouseUp();
                Destroy(transform.parent.gameObject);
            }
        }
    }

    void OnMouseUp()
    {
        if (UIs.Count > 0)
        {
            foreach (var go in UIs)
            {
                Destroy(go);
            }
        }
    }
}
