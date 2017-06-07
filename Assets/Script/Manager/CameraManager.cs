using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public int Boundary = 50;
    public int HorizontalSpeed = 10;
    public int VerticalSpeed = 5;

    public Vector2 UpHorizontalClampPoint;
    public Vector2 DownHorizontalClampPoint;
    public Vector2 VerticalClampPoint;

    private int theScreenWidth;
    private int theScreenHeight;

    private float Height;

    private float k_ScreenHorizontal;
    private float k_ScreenVertical;

    private Vector3 pos;

	void Start () {
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;

        Height = VerticalClampPoint[0] - VerticalClampPoint[1];

        k_ScreenHorizontal = (DownHorizontalClampPoint[1] - UpHorizontalClampPoint[1]) / Height;
        k_ScreenVertical = (DownHorizontalClampPoint[0] - UpHorizontalClampPoint[0]) / Height;

        Debug.Log(theScreenWidth);
        Debug.Log(theScreenHeight);
	}
	
	void Update () {
        pos = transform.position;

	    if (Input.mousePosition.x > theScreenWidth - Boundary && pos.x < GetClampPointPos().x)
        {
            pos.x += HorizontalSpeed;
            transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
        }

        if (Input.mousePosition.x < 0 + Boundary && pos.x > -GetClampPointPos().x)
        {
            pos.x -= HorizontalSpeed;
            transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
        }

        if (Input.mousePosition.y > theScreenHeight - Boundary && pos.z < GetClampPointPos().z)
        {
            pos.z += HorizontalSpeed;
            transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
        }

        if (Input.mousePosition.y < 0 + Boundary && pos.z > -GetClampPointPos().z)
        {
            pos.z -= HorizontalSpeed;
            transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && pos.y < VerticalClampPoint[0])
        {
            pos.y -= Input.GetAxis("Mouse ScrollWheel") * VerticalSpeed;
            transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && pos.y > VerticalClampPoint[1])
        {
            pos.y -= Input.GetAxis("Mouse ScrollWheel") * VerticalSpeed;
            transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
        }
	}

    Vector3 GetClampPointPos()
    {
        Vector3 pos = Vector3.zero;

        float CurrentY = 9.5f - transform.position.y;

        pos = new Vector3(UpHorizontalClampPoint[1] + CurrentY * k_ScreenHorizontal,
                          CurrentY,
                          UpHorizontalClampPoint[0] + CurrentY * k_ScreenVertical);
        Debug.Log(pos);

        return pos;
    }
}
