using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower_E : Tower
{
    private Tower_C towerC;
    private Tower_I towerI;

    private List<Tower_E> towerE;

    void Start()
    {
        currentInterval = AttackInterval;
        currentInterval = AttackInterval;
        Enemies = new Queue<GameObject>();
        towerE = new List<Tower_E>();
    }

    public override void AttackBehavior()
    {
        if (towerE.Count > 0)
        {
            for (int i = 0; i < towerE.Count; i++)
            {
                shebao(towerE[i].gameObject);
            }
        }
    }

    void CheckotherTowerE()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttackRange, 1 << LayerMask.NameToLayer("E"));
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject &&
                    !towerE.Contains(colliders[i].gameObject.GetComponent<Tower_E>()))
                    towerE.Add(colliders[i].gameObject.GetComponent<Tower_E>());
            }
        }
    }

    void Update()
    {
        CheckotherTowerE();
        Attack();
        LookAt();

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

    void LookAt()
    {
        if (towerE.Count > 0)
            transform.LookAt(towerE[0].transform);
    }

    public override void Attack()
    {
        for (int i = 0; i < towerE.Count; i++)
        {
            Vector3 dir = Vector3.Normalize(towerE[i].transform.position - transform.position);
            float distance = Vector3.Distance(transform.position, towerE[i].transform.position);
            Ray ray = new Ray(transform.position, dir);
            Debug.DrawLine(transform.position, towerE[i].transform.position, Color.red);
            RaycastHit[] hits = Physics.RaycastAll(ray, distance, 1 << LayerMask.NameToLayer("Enemy"));

            for (int j = 0; j < hits.Length; j++)
            {
                hits[j].transform.GetComponent<Enemy>().Hurt(AttackValue);
            }
        }
    }
}
