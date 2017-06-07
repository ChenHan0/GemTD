using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int Health;
    protected int currentHealth;
    public float Speed;

    protected static NavMeshAgent agent;

    void Start()
    {
        currentHealth = Health;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
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
        Destroy(gameObject);
    }
}
