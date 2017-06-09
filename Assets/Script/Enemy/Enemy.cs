using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public int Health;
    private int currentHealth;
    public int Damaga;
    public float Speed;
    private float currentSpeed;

    private List<Tower_F> towerF;

    protected static NavMeshAgent agent;

    void Start()
    {
        currentHealth = Health;
        currentSpeed = Speed;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = currentSpeed;

        towerF = new List<Tower_F>();
    }

    void Update()
    {
        CheckTowerF();
    }

    void CheckTowerF()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, 1 << LayerMask.NameToLayer("F"));
        if (colliders.Length > 0)
        {
            for(int i = 0; i < colliders.Length; i++)
            {
                towerF.Add(colliders[i].gameObject.GetComponent<Tower_F>());
            }
        }
    }

    public void Hurt(int val)
    {
        Health -= val;
        //Debug.Log(Health);

        if (Health <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        EnemyManager.RemoveEnemy(gameObject);
        if (towerF.Count > 0)
        {
            for (int i = 0; i < towerF.Count; i++)
            {
                towerF[i].SoulsNum++;
            }
        }
        Destroy(gameObject);
    }

    public void SlowDown(float percentage)
    { 
        currentSpeed *= (1 - percentage);
        agent.speed = currentSpeed;
        Debug.Log("currentSpeed:" + currentSpeed);
        Debug.Log(agent.speed);
    }

    public void ResetSpeed() {
        currentSpeed = Speed;
        agent.speed = currentSpeed;
    }
}
