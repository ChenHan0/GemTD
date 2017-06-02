using UnityEngine;
using System.Collections;

public class GridDebugger : MonoBehaviour {
	public bool toggleDebugging = false;
	public bool printLogs = true;
	public GFGrid theGrid;
	public GFGrid.GridPlane debuggedPlane = GFGrid.GridPlane.XY;
	public enum GridFunction {FindNearestBox, FindNearestFace, FindNearestVertex, WorldToGrid, GridToWorld, VertexMatrix};
	public GridFunction debuggedFunction = GridFunction.FindNearestBox;
	public Color debugColor = Color.red;
	public int[] index = new int[3];
	
	[HideInInspector]
	Vector3[,,] vertexMatrix;
	
	[HideInInspector]
	Transform cachedTransform;
	// Use this for initialization
	void Awake () {
		cachedTransform = transform;
	}
	
	// Update is called once per frame
	void OnDrawGizmos() {
		if(!theGrid || ! toggleDebugging)
			return;
		if (!cachedTransform)
			cachedTransform = transform;
				
		Gizmos.color = debugColor;
		if((int)debuggedFunction == 0){
			DebugNearesBox();
		} else if((int)debuggedFunction == 1){
			DebugNearestFace();
		} else if((int)debuggedFunction == 2){
			DebugNearestVertex();
		} else if((int)debuggedFunction == 3){
			DebugWorldToGrid();
		} else if((int)debuggedFunction == 4){
			DebugGridToWorld();
		} else if((int)debuggedFunction == 5){
			DebugVertexMatrix();
		}
	}
	
	void DebugNearestVertex(){
		theGrid.FindNearestVertex(cachedTransform.position, true);
		if(printLogs)
			Debug.Log(theGrid.GetVertexCoordinates(cachedTransform.position));
	}
	
	void DebugNearestFace(){
		theGrid.FindNearestFace(cachedTransform.position, debuggedPlane, true);
		if(printLogs)
			Debug.Log(theGrid.GetFaceCoordinates(cachedTransform.position, debuggedPlane));
	}
	
	void DebugNearesBox(){
		theGrid.FindNearestBox(cachedTransform.position, true);
		if(printLogs)
			Debug.Log(theGrid.GetBoxCoordinates(cachedTransform.position));
	}
	
	void DebugWorldToGrid(){
		theGrid.WorldToGrid(cachedTransform.position);
		if(printLogs)
			Debug.Log(theGrid.WorldToGrid(cachedTransform.position));
	}
	
	void DebugGridToWorld(){
		if(printLogs)
			Debug.Log(theGrid.GridToWorld(theGrid.WorldToGrid(cachedTransform.position)));
	}
	
	void DebugVertexMatrix(){
		vertexMatrix = theGrid.BuildVertexMatrix(3.0f, 3.0f, 3.0f);
		theGrid.DrawVertices(vertexMatrix);
		if(printLogs){
			Gizmos.color = Color.red;
			Gizmos.DrawSphere((theGrid.ReadVertexMatrix(index[0], index[1], index[2], vertexMatrix)), 0.3f);
		}
	}
}
