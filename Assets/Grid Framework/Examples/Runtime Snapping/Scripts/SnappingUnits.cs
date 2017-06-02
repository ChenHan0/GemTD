using UnityEngine;
using System.Collections;

/*
	ABOUT THIS SCRIPT
	
This script makes objects stick to the ground of a grid, similar to how
buildings are placed in stategy games. Objects will always be placed
with the bottom touching the grid. In the example scene I rotated the
grid to create a sort of isometric perspective effect, so you are not
limited to a certain view. Just make sure you handle mouse input correctly.

This script demonstrates the snapping feature during runtime and
conversions from world space to grid space and back.

NOTE: Due to popular request this example now also detects when the block
is intersecting with another block. In that case the active block will be
tinted red until you move out. if you let go while intersecting it will
snap back to the last non-intersecting position. This is achieved by using
a child object with a trigger that checks for intersections and reports back
to this script. This is just standard unity physics and has nothing to do
with Grid Framework itself, but it is still a question people ask often.
*/

public class SnappingUnits : MonoBehaviour {

	public GFGrid grid;
	
	public Material defaultMaterial;
	public Material redMaterial;
	private bool _intersecting;
	
	private bool beingDragged;
	private Vector3 oldPosition;
	
	//cache the transform for performance
	private Transform cachedTransform;
	
	void Awake () {
		cachedTransform = transform;
		
		//always make a sanity check
		if(grid){
			//perform an initial align and snap the objects to the bottom
			grid.AlignTransform(cachedTransform);
			cachedTransform.position = CalculateOffsetZ();
		}
		//store the first safe position
		oldPosition = cachedTransform.position;
		// setup the rigidbody for collision and contsruct a trigger
		SetupRigidbody();
		ConstructTrigger();
	}
	
	// these two methods toggle dragging
	void OnMouseDown(){
		beingDragged = true;
	}
	void OnMouseUp(){
		beingDragged = false;
		cachedTransform.position = oldPosition;
		_intersecting = false;
		TintRed(_intersecting);
	}
	
	// use FixedUpdate to allow colision detection to catch up with movement
	void FixedUpdate(){
		if(beingDragged){
			//store the position if it is safe
			if(!_intersecting)
				oldPosition = cachedTransform.position;
			DragObject();
		}
	}
	
	//this function gets called every frame while the object (its collider) is being dragged with the mouse
	void DragObject(){
		if(!grid)
			return;

        //handle mouse input to convert it to world coordinates
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, 100);



        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, hit.distance);
		Vector3 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);

        Debug.DrawLine(Camera.main.transform.position, cursorWorldPoint, Color.red);
		
		//we want to keep the Z coordinate, so apply it directly to the new position
		cursorWorldPoint.z = cachedTransform.position.z;
        Debug.Log(cursorWorldPoint);
    	
		//change the X and Y coordinates according to the cursor (the Z coordinate stays the same)
		cachedTransform.position = cursorWorldPoint;
		
		//now align the obbject and snap it to the bottom.
		grid.AlignTransform(cachedTransform);
		cachedTransform.position = CalculateOffsetZ();
	}
	
	// makes the object snap to the bottom of the grid, respecting the grid's rotation
	Vector3 CalculateOffsetZ(){
		//first store the objects position in grid coordinates
		Vector3 gridPosition = grid.WorldToGrid(cachedTransform.position);
		//then change only the Z coordinate
		gridPosition.z = -0.5f * cachedTransform.lossyScale.z;
		
		//convert the result back to world coordinates
		return grid.GridToWorld(gridPosition);
	}
	
	// this method will be called by the trigger
	public void SetIntersecting(bool intersecting){
		if(!beingDragged) // ignore sitting objects, only moving ones should respond
			return;
		_intersecting = intersecting;
		//update the colour
		TintRed(_intersecting);
	}
	
	void TintRed(bool red){
		if(red){
			GetComponent<Renderer>().material = redMaterial;
		} else{
			GetComponent<Renderer>().material = defaultMaterial;
		}
	}
	
	private void SetupRigidbody(){
		Rigidbody rb = GetComponent<Rigidbody>(); //the rigidbody component of this object
		if(!rb) // if there is no Rigidbody create a new one
			rb = gameObject.AddComponent<Rigidbody>();
		// non-kinematic to allow collision detection, no gravity and all rotations and movement frozen
		rb.isKinematic=false;
		rb.useGravity=false;
		rb.constraints = RigidbodyConstraints.FreezeAll; //prevents physics from moving the object
	}
	
	private void ConstructTrigger(){
		GameObject go = new GameObject();
		go.name = "IntersectionTrigger";
		// attach it to the block and make it exactly the same, except slightly smaller
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero; //exactly at the centre of the actual object
		go.transform.localScale = 0.9f * Vector3.one; //slightly smaller than the actual object
		go.transform.localRotation = Quaternion.identity;
		// add the same type of collider as the block has and make it a trigger
		Collider col = (Collider) go.AddComponent(GetComponent<Collider>().GetType());
		col.isTrigger = true;
		// attack the script to the collider and connect it to this script
		IntersectionTrigger script = go.AddComponent<IntersectionTrigger>();
		script.SetScript(this);
	}
}
