using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Destroy", 0.1f);
	}
	
    void Destroy()
    {
        Destroy(gameObject);
    }
}
