using UnityEngine;
using System.Collections;

public class DeTower : Tower
{
    void Start()
    {
        //Debug.Log(previousAttackTime);
        //TowerCore = "DeTower";

        //Debug.Log(TowerManager.GetRegularExpression(new string[] { "a", "b", "c" }));
        //Debug.Log(TowersInfo.TowerUpgradeFormulas.GetLength(0));
        //bool[] bools = TowerManager.CheckAllTower("A1C1B1");
        //foreach (bool b in bools)
        //    Debug.Log(b);
    }

    public override void AttackBehavior()
    {
        if (Traget)
        {
            Traget.GetComponent<Enemy>().Hurt(AttackValue);
        }
        else 
        {
            if (Enemies != null)
            {
                //Traget = Enemies[0];
                //Enemies.Remove(Traget);
                Traget.GetComponent<Enemy>().Hurt(AttackValue); 
            }
        }
    }

    void Update()
    {
        Attack();
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
                //Enemies.Add(Traget);
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
            //Enemies.Remove(other.gameObject);
        }
        //Debug.Log(Traget);
    }
}
