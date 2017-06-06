using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

    public int AttackValue;
    public float AttackRange;
    public float AttackInterval;
    protected float previousAttackTime;
    [HideInInspector]
    public GameObject Traget;
    [HideInInspector]
    public List<GameObject> Enemies;

    public string TowerCore;

    void Start()
    {
        previousAttackTime = Time.time;
        Enemies = new List<GameObject>();
    }
    

    public void Attack()
    {
        if (Time.time - previousAttackTime > AttackInterval)
        {
            previousAttackTime = Time.time;
            AttackBehavior();            
        }
    }

    public virtual void AttackBehavior()
    {
        
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
