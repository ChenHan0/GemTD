using UnityEngine;
using System.Collections;


public class Tower : MonoBehaviour {

    public int AttackValue;
    public float AttackRange;
    public float AttackInterval;
    protected float previousAttackTime;
    [HideInInspector]
    public GameObject Traget;

    void Start()
    {
        previousAttackTime = Time.time;
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
        //System.Text.RegularExpressions.Regex.IsMatch();
    }
}
