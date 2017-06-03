using UnityEngine;
using System.Collections;


public class Tower : MonoBehaviour {

    public float AttackPower;
    public float AttackRange;
    public float AttackInterval;
    [HideInInspector]
    public GameObject Traget;

    public virtual void Attack()
    {
        //System.Text.RegularExpressions.Regex.IsMatch();
    }


}
