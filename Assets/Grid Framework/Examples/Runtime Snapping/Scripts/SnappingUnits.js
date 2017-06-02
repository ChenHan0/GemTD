#pragma strict
/*
	ABOUT THIS SCRIPT
	
This script makes objects stick to the ground of a grid, similar to how
buildings are placed in stategy games. Objects will always be placed
with the bottom touching the grid. In the example scene I rotated the
grid to create a sort of isometric perspective effect, so you are not
limited to a certain view. Just make sure you handle mouse input correctly.

This script demonstrates the snapping feature during runtime and
conversions from world space to grid space and back.

NOTE: This script would work in theory, but due to Unity's compilation order
only the C# version works. o get the JavaScript verion to work you would have
to move IntersectionTrigger.cs to a folder with a higher priority (like Plugins)
*/

public var grid: GFGrid;

public var defaultMaterial: Material;
public var redMaterial: Material;
private var _intersecting: boolean;

private var beingDragged: boolean;
var oldPosition: Vector3;

//cache the transform for performance
private var cachedTransform: Transform;

function Awake () {
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

function OnMouseDown(){
	beingDragged = true;
}
function OnMouseUp(){
	beingDragged = false;
	cachedTransform.position = oldPosition;
	_intersecting = false;
	TeintRed(_intersecting);
}

function FixedUpdate(){
	if(beingDragged){
		//store the position if it is safe
		if(!_intersecting)
			oldPosition = cachedTransform.position;
		DragObject();
	}
}

//this function gets called every frame while the object (its collider) is being clicked
function DragObject(){
	if(!grid)
		return;
	
	//handle mouse input to convert it to world coordinates
	var cursorScreenPoint: Vector3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
	var cursorWorldPoint: Vector3 = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
    
    //we want to keep the Z coordinate, so apply it directly to the new position
    cursorWorldPoint.z = cachedTransform.position.z;
    
    //change the X and Y coordinates according to the cursor (the Z coordinate stays the same)
	cachedTransform.position = cursorWorldPoint;
	//cachedTransform.Translate((cursorWorldPoint - cachedTransform.position) * Time.deltaTime);
	//rigidbody.velocity = (cursorWorldPoint - cachedTransform.position);
		
	//now align the obbject and snap it to the bottom.
	grid.AlignTransform(cachedTransform);
	cachedTransform.position = CalculateOffsetZ();
}

// makes the object snap to the bottom of the grid, respecting the grid's rotation
function CalculateOffsetZ(){
	//first store the objects position in grid coordinates
	var gridPosition: Vector3 = grid.WorldToGrid(cachedTransform.position);
	//then change only the Z coordinate
	gridPosition.z = -0.5 * cachedTransform.lossyScale.z;
	
	//convert the result back to world coordinates
	return grid.GridToWorld(gridPosition);
}

// this method will be called by the trigger
public function SetIntersecting(intersecting: boolean){
		if(!beingDragged)
			return;
		_intersecting = intersecting;
		//update the colour
		TeintRed(_intersecting);
	}
	
function TeintRed(red: boolean){
	if(red){
		GetComponent.<Renderer>().material = redMaterial;
	} else{
		GetComponent.<Renderer>().material = defaultMaterial;
	}
}
	
function SetupRigidbody(){
	var rb: Rigidbody = gameObject.AddComponent.<Rigidbody>();
	// kinematic to allow collision detection, no gravity and all rotations and movement frozen
	rb.isKinematic=false;
	rb.useGravity=false;
	rb.constraints = RigidbodyConstraints.FreezeAll;
}
	
function ConstructTrigger(){
	var go: GameObject = new GameObject();
	// attach it to the block and make it exactly the same, except slightly smaller
	go.transform.parent = transform;
	go.transform.localPosition = Vector3.zero;
	go.transform.localScale = 0.9f * Vector3.one;
	go.transform.localRotation = Quaternion.identity;
	// add the same type of collider as the block has and make it a trigger
	var col: Collider = go.AddComponent(GetComponent.<Collider>().GetType()) as Collider;
	col.isTrigger = true;
	// attack the script to the collider and connect it to this script
	//var script: IntersectionTrigger = go.AddComponent.<IntersectionTrigger>();
	//script.SetScript(this);
}