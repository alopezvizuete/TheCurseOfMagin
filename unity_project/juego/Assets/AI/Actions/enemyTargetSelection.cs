using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class enemyTargetSelection : RAINAction
{
	public Vector3 directionVector;

	//GameObject that is going to wander and contain AI
	float distanceToWander;
	int minDistanceToWander;
	int maxDistanceToWander;
	int maxTurnAngle;

	//Expression strength = new Expression();

	public enemyTargetSelection()
	{
		actionName = "EnemyTargetSelection";
	}
    public override void Start(RAIN.Core.AI ai)
    {

		base.Start(ai);

		//Load Parameters for Wander
		maxTurnAngle = ai.WorkingMemory.GetItem<int>("maxTurnAngle");
		minDistanceToWander = ai.WorkingMemory.GetItem<int>("minDistanceToWander");
		maxDistanceToWander = ai.WorkingMemory.GetItem<int>("maxDistanceToWander");

		//Calculate Distance to Wander
		distanceToWander = Random.Range(minDistanceToWander,maxDistanceToWander);

		//Set Random Direction
		Vector2 newVector = Random.insideUnitCircle;
		directionVector.x = newVector.x * distanceToWander;
		directionVector.z = newVector.y * distanceToWander;
		directionVector.y = 0;

		float angle = Vector3.Angle(ai.Body.transform.forward, directionVector);


		//Check if angle is larger than Max Turning Angle and if direction collides with any object

		while(angle>maxTurnAngle||Physics.Raycast(ai.Body.transform.position, directionVector, directionVector.magnitude)){
			//Debug.DrawRay(ai.Body.transform.position,ai.Body.transform.forward, Color.red);

			//Set Random Direction
			newVector = Random.insideUnitCircle;
			directionVector.x = newVector.x * distanceToWander;
			directionVector.z = newVector.y * distanceToWander;
			directionVector.y = 0;

			angle = Vector3.Angle(ai.Body.transform.forward, directionVector);

		}

		//Set Position to Move to relative to Current Position
		directionVector = ai.Body.transform.position + directionVector;

		//Set location into working memory for Move action in BT
		ai.WorkingMemory.SetItem<Vector3>("directionVector", directionVector);


    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}