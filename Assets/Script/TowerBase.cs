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
        UIs = UIManager.GetUI(this.gameObject);
        if (UIs.Count > 0)
        {
            foreach (GameObject go in UIs)
            {
                Debug.Log(go.name);
            }
        }
    }
}
