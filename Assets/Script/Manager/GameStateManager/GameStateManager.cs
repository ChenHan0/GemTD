using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    private static GameState m_pCurrentState;

    void Awake()
    {
        m_pCurrentState = null;
    }

	// Update is called once per frame
	void Update () {
        if (m_pCurrentState != null)
            m_pCurrentState.Execute();
	}

    public static void SetCurrentState(GameState CurremtState)
    {
        m_pCurrentState = CurremtState;
        m_pCurrentState.Enter();
    }

    public static void ChangeState(GameState pNewState)
    {
        m_pCurrentState.Exit();

        m_pCurrentState = pNewState;

        m_pCurrentState.Enter();
    }

    public static GameState GetCurrentState()
    {
        return m_pCurrentState;
    }
}
