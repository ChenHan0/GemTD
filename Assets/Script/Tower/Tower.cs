using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

    public int AttackValue;
    public int currentAttackValue;
    public float AttackRange;
    public float AttackInterval;
    public float currentInterval;
    protected float previousAttackTime;
    [HideInInspector]
    public GameObject Traget = null;
    [HideInInspector]
    public Queue<GameObject> Enemies;

    private bool isChoose = false;

    public string TowerCode;

    public GameObject TowerBase;

    public float SupplantDistance = 0.78f;

    [HideInInspector]
    public List<GameObject> UIs;

    public GameObject AttackEffect;
    public Transform AttackPoint;
    public float RayContinuousTime;

    private GameObject AttackRangeObject;

    void Start()
    {
        previousAttackTime = Time.time;
        UIs = new List<GameObject>();
        currentInterval = AttackInterval;
    }

    protected void LookAtTraget()
    {
        if (Traget)
        {
            transform.LookAt(Traget.transform);
            Vector3 rotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(0, rotation.y, 0));
        }
    }

    public virtual void Attack()
    {
        if (Time.time - previousAttackTime > currentInterval)
        {
            previousAttackTime = Time.time;
            AttackBehavior();            
        }
    }

    public virtual void AttackBehavior()
    {
        
    }

    public void shebao(GameObject go)
    {
        GameObject sb = Instantiate(AttackEffect, AttackPoint.transform.position, Quaternion.Euler(Vector3.zero)) as GameObject;
        Debug.Log("go" + go.transform.position);
        Debug.Log("sb" + sb.transform.position);
        sb.transform.GetChild(2).position = go.transform.position;
        //sb.transform.LookAt(go.transform);        
        //sb.GetComponent<F3DBeam>().MaxBeamLength = Vector3.Distance(sb.transform.position, go.transform.position);
        sb.GetComponent<LineRenderer>().SetPosition(1, go.transform.position - AttackPoint.transform.position);
    }

    void OnMouseDown()
    {
        if (GameStateManager.GetCurrentState().Equals(ChooseState.Instance)
            || GameStateManager.GetCurrentState().Equals(EnemyAttackState.Instance))
        {
            AttackRangeObject = ShowRadius.Show(this);

            UIs = UIManager.ShowUI(gameObject);
        }

        if (GameStateManager.GetCurrentState().Equals(EnemyAttackState.Instance))
        {
            isChoose = true;
        }
    }

    void OnMouseDrag()
    {
        if (GameStateManager.GetCurrentState() == ChooseState.Instance)
        {
            if (Input.GetAxis("Mouse X") >= 0.8f)
            {
                OnMouseUp();
                TowerManager.ChooseTowerInCurrentTime(GetComponent<Tower>());
                GameStateManager.ChangeState(EnemyAttackState.Instance);
                return;
            }
            else if (Input.GetAxis("Mouse X") <= -0.8f)
            {
                if (TowerManager.IsUpgradableInCurrent(this))
                {
                    OnMouseUp();
                    TowerManager.UpgradeFormCurrentTower(this);
                    GameStateManager.ChangeState(EnemyAttackState.Instance);
                    return;
                }
            }
        } 
        else if (GameStateManager.GetCurrentState() == EnemyAttackState.Instance)
        {
            if (Input.GetAxis("Mouse X") >= 0.8f && isChoose)
            {
                Debug.Log("OnMouseDrag");
                if (TowerManager.IsUpgradableInAll(this))
                {
                    OnMouseUp();
                    TowerManager.UpgradeFormAllTower(this);
                    return;
                }
            }
        }
    }

    void OnMouseUp()
    {
        if (AttackRangeObject)
            Destroy(AttackRangeObject);

        if (UIs.Count > 0)
        {
            foreach (var go in UIs)
            {
                Destroy(go);
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
