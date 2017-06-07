using UnityEngine;
using System.Collections;

public enum Quadrant
{
    FristQuadrant,
    SecondQuadrant,
    ThirdQuadrant,
    fourthQuadrant,
    none
}

public class MouseManager : MonoBehaviour {
    public float ResponseDistance = 100;

    public static Quadrant CurrentQuadrant = Quadrant.none;
    public static Quadrant FinalQuadrant = Quadrant.none;

    Vector2 mouseDownPos;
    Vector2 mouseCurrentPos;

	void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Event.current);

        if (Event.current != null)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                //记录鼠标按下的位置 　　  
                mouseDownPos = Event.current.mousePosition;
            }
            else if (Event.current.type == EventType.MouseDrag)
            {
                //记录鼠标拖动的位置 　　  
                mouseCurrentPos = Event.current.mousePosition;

                if (mouseCurrentPos.x < mouseDownPos.x - ResponseDistance)
                {
                    if (mouseCurrentPos.y < mouseDownPos.y - ResponseDistance)
                    {
                        CurrentQuadrant = Quadrant.ThirdQuadrant;
                    }
                    else if (mouseCurrentPos.y > mouseDownPos.y + ResponseDistance)
                    {
                        CurrentQuadrant = Quadrant.SecondQuadrant;
                    }
                }
                else if (mouseCurrentPos.x > mouseDownPos.x + ResponseDistance)
                {
                    if (mouseCurrentPos.y < mouseDownPos.y - ResponseDistance)
                    {
                        CurrentQuadrant = Quadrant.fourthQuadrant;
                    }
                    else if (mouseCurrentPos.y > mouseDownPos.y + ResponseDistance)
                    {
                        CurrentQuadrant = Quadrant.FristQuadrant;
                    }
                }

            }
            else if (Event.current.type == EventType.MouseUp)
            {
                FinalQuadrant = CurrentQuadrant;
                CurrentQuadrant = Quadrant.none;
                mouseDownPos = mouseCurrentPos;
            }

            Debug.Log("CurrentQuadrant" + CurrentQuadrant);
            Debug.Log("FinalQuadrant:" + FinalQuadrant);
        }        
    }
}
