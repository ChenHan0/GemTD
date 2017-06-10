using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Tower_K : Tower
{
    private Tower_C towerC;
    private Tower_I towerI;
    public float AddAttackStep = 0.2f;

    private List<Tower_K> TowerK;

    void Start()
    {
        currentAttackValue = AttackValue;
        currentInterval = AttackInterval;
        Enemies = new Queue<GameObject>();
        TowerK = new List<Tower_K>();
    }

    public override void AttackBehavior()
    {
        if (Traget != null)
        {
            Traget.GetComponent<Enemy>().Hurt(AttackValue);
            shebao(Traget);
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
        CheckotherTowerK();
        AddAttack();

        Attack();
        LookAtTraget();
        if (Traget)
            Debug.DrawLine(transform.position, Traget.transform.position, Color.red);

        CheckTowerC();
        Accelerate();

        CheckTowerI();
        IncreasedAttack();

        
    }

    void AddAttack()
    {
        currentAttackValue = AttackValue;
        for (int i = 0; i < TowerK.Count; i++)
        {
            currentAttackValue *= (int)(1 + AddAttackStep);
        }
    }

    void CheckotherTowerK()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttackRange, 1 << LayerMask.NameToLayer("E"));
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject &&
                    !TowerK.Contains(colliders[i].gameObject.GetComponent<Tower_K>()))
                    TowerK.Add(colliders[i].gameObject.GetComponent<Tower_K>());
            }
        }
    }

    void IncreasedAttack()
    {
        if (towerI)
            currentAttackValue = (int)(AttackValue * (1 + towerI.AttackInterval));
        else
            currentAttackValue = AttackValue;
    }

    void CheckTowerI()
    {
        if (!towerI)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 3.0f, 1 << LayerMask.NameToLayer("I"));
            if (colliders.Length > 0)
            {
                Collider collider = colliders[0];
                float distance = collider.gameObject.GetComponent<Tower_I>().AttackRange;
                if (Vector3.Distance(transform.position, collider.transform.position) <= distance)
                    towerI = collider.gameObject.GetComponent<Tower_I>();
            }
        }
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
