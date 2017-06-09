using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {
    private static int Health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Health <= 0)
        {
            GameStateManager.ChangeState(GameOverState.Instance);
        }
	}

    public static void Hurt(int damaga)
    {
        Health -= damaga;
    }

    public static void AddHealth(int value)
    {
        Health += value;
        if (Health > 100)
        {
            Health = 100;
        }
    }
}
