using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public int Boundary = 50;
    public int HorizontalSpeed = 10;
    public int VerticalSpeed = 5;

    public Vector4 FinalPoint;

    public Vector2 HorizontalClampPoint;
    public Vector2 VerticalClampPoint;

    private int theScreenWidth;
    private int theScreenHeight;

    private float Height;

    //private float k_ScreenHorizontal;
    //private float k_ScreenVertical;

    private Vector3 pos;

	void Start () {
        Height = VerticalClampPoint[0] - VerticalClampPoint[1];

        //k_ScreenVertical = ((DownHorizontalClampPoint[0] - DownHorizontalClampPoint[1]) - 
        //    (UpHorizontalClampPoint[0] - UpHorizontalClampPoint[1])) / Height;
        //k_ScreenHorizontal = (DownHorizontalClampPoint[3] - UpHorizontalClampPoint[3]) / Height;

        //Debug.Log(k_ScreenVertical);
        //Debug.Log(k_ScreenHorizontal);
	}
	
	void Update () {
        pos = transform.position;

        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;

        //Debug.Log(GetClampPointPos());

        if (Input.mousePosition.x > theScreenWidth - Boundary && pos.x < HorizontalClampPoint.y)
        {
            pos.x += HorizontalSpeed;
            transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
        }

        if (Input.mousePosition.x < 0 + Boundary && pos.x > -HorizontalClampPoint.y)
        {
            pos.x -= HorizontalSpeed;
            transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
        }

        if (Input.mousePosition.y > theScreenHeight - Boundary && pos.z < HorizontalClampPoint.x)
        {
            pos.z += HorizontalSpeed;
            transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime);
        }

        if (Input.mousePosition.y < 0 + Boundary && pos.z > -HorizontalClampPoint.x)
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

    //Vector3 GetClampPointPos()
    //{
    //    Vector3 pos = Vector3.zero;

    //    float CurrentY = 9.5f - transform.position.y;

    //    //pos = new Vector3(UpHorizontalClampPoint[2] + CurrentY * k_ScreenHorizontal,
    //    //                  CurrentY,
    //    //                  UpHorizontalClampPoint[0] + CurrentY * k_ScreenVertical);

    //    pos = new Vector3(HorizontalClampPoint.y,
    //                      CurrentY,
    //                      HorizontalClampPoint.x);

    //    return pos;
    //}
}
