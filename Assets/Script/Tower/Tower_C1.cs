using UnityEngine;
using System.Collections;

public class Tower_C1 : Tower {

    void Start()
    {

    }

    public override void AttackBehavior()
    {
        if (Traget)
        {
            Traget.GetComponent<Enemy>().Hurt(AttackValue);
        }
        else
        {
            if (Enemies.Count > 0)
            {
                Traget = Enemies[0];
                Enemies.Remove(Traget);
            }
        }
    }

    void Update()
    {
        Attack();
        LookAtTraget();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            if (Traget == null)
            {
                Traget = other.gameObject;
            }
            else if (Traget != null)
            {
                Enemies.Add(Traget);
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
            Enemies.Remove(other.gameObject);
        }
        //Debug.Log(Traget);
    }
}
