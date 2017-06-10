using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower_J : Tower
{
    private Tower_C towerC;
    private Tower_I towerI;

    private new List<GameObject> Enemies;

    void Start()
    {
        currentAttackValue = AttackValue;
        currentInterval = AttackInterval;
        Enemies = new List<GameObject>();
    }

    public override void AttackBehavior()
    {
        if (Enemies.Count > 0)
        {
            foreach (var enemy in Enemies)
            {
                if (enemy) {
                    enemy.GetComponent<Enemy>().Hurt(AttackValue);
                    shebao(enemy);
                }
            }
        }
    }

    void Update()
    {
        Attack();
        LookAtTraget();
        if (Traget)
            Debug.DrawLine(transform.position, Traget.transform.position, Color.red);

        CheckTowerC();
        Accelerate();

        CheckTowerI();
        IncreasedAttack();
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
            if (!Enemies.Contains(other.gameObject))
            {
                Enemies.Add(other.gameObject);
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
                Enemies.Remove(other.gameObject);
        }
        //Debug.Log(Traget);
    }
}
