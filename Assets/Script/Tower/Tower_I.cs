using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower_I : Tower
{
    private Tower_C towerC;
    public float AttackIncreased = 0.2f;

    void Start()
    {
        currentInterval = AttackInterval;
        currentInterval = AttackInterval;
        Enemies = new Queue<GameObject>();
    }

    public override void AttackBehavior()
    {
        if (Traget != null)
        {
            Traget.GetComponent<Enemy>().Hurt(AttackValue);
        }
        else
        {
            if (Enemies.Count > 0)
            {
                Traget = Enemies.Dequeue();
            }
        }
    }

    void Update()
    {
        Attack();
        LookAtTraget();
        if (Traget)
            Debug.DrawLine(transform.position, Traget.transform.position, Color.red);

        Accelerate();
        CheckTowerC();
    }

    void Accelerate()
    {
        if (towerC)
        {
            currentInterval = AttackInterval * (1 - towerC.AcceleratePercentage);
        }
        else
        {
            currentInterval = AttackInterval;
        }
    }

    void CheckTowerC()
    {
        if (!towerC)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 3.0f, 1 << LayerMask.NameToLayer("C"));
            if (colliders.Length > 0)
            {
                Collider collider = colliders[0];
                Debug.Log(collider.gameObject.name);
                float distance = collider.gameObject.GetComponent<Tower_C>().AttackRange;
                if (Vector3.Distance(transform.position, collider.transform.position) <= distance)
                    towerC = collider.gameObject.GetComponent<Tower_C>();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            if (Traget == null && Enemies.Count <= 0)
            {
                Debug.Log("Set Traget");
                Traget = other.gameObject;
            }
            else if (Traget != null)
            {
                Debug.Log("Add Enemies");
                Enemies.Enqueue(other.gameObject);
            }
        }
        //Debug.Log(Traget);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(Traget))
        {
            Traget = null;
        }
        else
        {
            if (Enemies.Count > 0)
                Enemies.Dequeue();
        }
        //Debug.Log(Traget);
    }

    
}
