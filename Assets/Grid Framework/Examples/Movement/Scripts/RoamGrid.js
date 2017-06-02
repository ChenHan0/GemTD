#pragma strict
/*
	ABOUT THIS SCRIPT
	
This script, when attached to an object, makes it walk around the grid
randomly from one face to an adjacent one. The object will never walk
off the grid. You can even change the grid's size during runtime and the
object will adapt its behaviour. If you shrink the grid faster than the
object can react it will head straigt back inside, but it will take a
few turns to do so, depending on how far outside it is.

This script demonstrates how you can easily perform grid-based movement,
automate it and limit it to a certain range while still retaining the
option of dynamically altering the setup during runtime.
*/

var grid: GFGrid;
var roamingTime: float = 1.0;

//whether the object is to move or not, to where and how fast
private var doMove: boolean = false;
private var goal: Vector3;
private var roamingSpeed: float;

//cache the transform for performance
private var cachedTransform: Transform;

function Awake () {
	cachedTransform = transform;
	
	//make a check to prevent getting stuck in a null exception
	if(grid){
		//snap to the grid  no matter where we are
		grid.AlignTransform(cachedTransform);
	}
}

function Update(){
	if(!grid)
		return;
	
	if(doMove){
		//move towards the desination
		cachedTransform.position.x = Mathf.MoveTowards(cachedTransform.position.x, goal.x, roamingSpeed * Time.deltaTime);
		cachedTransform.position.y = Mathf.MoveTowards(cachedTransform.position.y, goal.y, roamingSpeed * Time.deltaTime);
		//check if we reached the destination (use a certain tolerance so we don't miss the point becase of rounding errors)
		if(Mathf.Abs(cachedTransform.position.x - goal.x) < 0.01 && Mathf.Abs(cachedTransform.position.y - goal.y) < 0.01)
			doMove = false;
		//if we did stop moving
	} else{
		//make sure the time is always positive
		if(roamingTime < 0.01)
			roamingTime = 0.01;
		//find the next destination
		goal = FindNextFace();
		//calculate speed by dividing distance (one of the two distances will be 0, we need the other one) through time
		roamingSpeed = Mathf.Max(Mathf.Abs(cachedTransform.position.x - goal.x), Mathf.Abs(cachedTransform.position.y - goal.y)) / roamingTime;
		//resume movement with the new goal
		doMove = true;
	}
}

function FindNextFace(){
	//we will be operating in grid space, so convert the position
	var newPosition: Vector3 = grid.WorldToGrid(cachedTransform.position);
	
	//first let's pick a random number for one of the four possible directions
	var i: int = Random.Range(0, 4);
	//now add one grid unit onto position in the picked direction
	if(i == 0){
		newPosition = newPosition + Vector3(1,0,0);
	} else if(i ==1){
		newPosition = newPosition + Vector3(-1,0,0);
	} else if(i == 2){
		newPosition = newPosition + Vector3(0,1,0);
	} else if(i == 3){
		newPosition = newPosition + Vector3(0,-1,0);
	}
	//if we would wander off beyond the size of the grid turn the other way around
	for(var j: int = 0; j < 2; j++){
		if(Mathf.Abs(newPosition[j]) > grid.size[j])
			newPosition[j] -= Mathf.Sign(newPosition[j]) * 2.0;
	}
	
	//return the position in world space
	return grid.GridToWorld(newPosition);
}