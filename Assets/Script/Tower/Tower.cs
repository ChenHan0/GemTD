using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

    public int AttackValue;
    public int currentAttackValue;
    public float AttackRange;
    public float AttackInterval;
    public float currentInterval;
    public bool IsAccelerated = false;
    protected float previousAttackTime;
    [HideInInspector]
    public GameObject Traget = null;
    [HideInInspector]
    public Queue<GameObject> Enemies;

    public string TowerCode;

    public GameObject TowerBase;

    public float SupplantDistance = 1.5f;

    [HideInInspector]
    public List<GameObject> UIs;

    void Start()
    {
        previousAttackTime = Time.time;
        UIs = new List<GameObject>();
        currentInterval = AttackInterval;
    }

    protected void LookAtTraget()
    {
        if (Traget)
        {
            transform.LookAt(Traget.transform);
            Vector3 rotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(0, rotation.y, 0));
        }
    }

    public virtual void Attack()
    {
        if (Time.time - previousAttackTime > currentInterval)
        {
            previousAttackTime = Time.time;
            AttackBehavior();            
        }
    }

    public virtual void AttackBehavior()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Tower");
        UIs = UIManager.GetUI(this.gameObject);
        if (UIs.Count > 0)
        {
            foreach (GameObject go in UIs)
            {
                Debug.Log(go.name);
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
