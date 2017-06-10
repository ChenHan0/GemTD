using UnityEngine;
using System.Collections;

public class ShowRadius : MonoBehaviour {
    static GameObject Attack_Range;

	// Use this for initialization
	void Start () {
        Attack_Range = Resources.Load("Attack_Range", typeof(GameObject)) as GameObject;
        Physics.queriesHitTriggers = false;
    }
	
	public static GameObject Show(Tower tower)
    {
        float radius = tower.AttackRange;
        Vector3 pos = new Vector3(tower.transform.position.x, 0.01f, tower.transform.position.z);
        GameObject go = Instantiate(Attack_Range, pos, Quaternion.Euler(180, 0, 0)) as GameObject;
        go.transform.localScale = new Vector3(radius, 1, radius);

        return go;
    }
}
