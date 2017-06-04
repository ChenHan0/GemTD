using UnityEngine;
using System.Collections;

public class DeTower : Tower
{
    void Start()
    {
        //Debug.Log(previousAttackTime);
    }

    public override void AttackBehavior()
    {
        if (Traget)
            Traget.GetComponent<Enemy>().Hurt(AttackValue);
    }

    void Update()
    {
        Attack();
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (Traget == null && other.tag == "Enemy")
        {
            Traget = other.gameObject;
        }
        //Debug.Log(Traget);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(Traget))
        {
            Traget = null;
        }
        //Debug.Log(Traget);
    }
}
