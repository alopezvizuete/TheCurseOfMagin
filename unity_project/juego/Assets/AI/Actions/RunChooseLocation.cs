using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RunChooseLocation : RAINAction
{
	public Vector3 runDirectionVector;

	int runTurnAngle;
	int runDistance;
	Vector3 playerLocation;
	public RunChooseLocation()
	{
		actionName = "RunChooseLocation";
	}
		
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

		runTurnAngle = ai.WorkingMemory.GetItem<int>("runTurnAngle");
		runDistance=ai.WorkingMemory.GetItem<int>("runDistance");
		playerLocation = ai.WorkingMemory.GetItem<Vector3> ("detPLayer1");


		//Set Random Direction
		Vector2 newVector = Random.insideUnitCircle;
		runDirectionVector.x = newVector.x * runDistance;
		runDirectionVector.z = newVector.y * runDistance;
		runDirectionVector.y = 0;

		runDirectionVector += -playerLocation;

		float angle = Vector3.Angle(ai.Body.transform.forward, runDirectionVector);

		while(angle>runTurnAngle||Physics.Raycast(ai.Body.transform.position, runDirectionVector, runDirectionVector.magnitude)){
			//Debug.DrawRay(ai.Body.transform.position,ai.Body.transform.forward, Color.red);

			//Set Random Direction
			newVector = Random.insideUnitCircle;
			runDirectionVector.x = newVector.x * runDistance;
			runDirectionVector.z = newVector.y * runDistance;
			runDirectionVector.y = 0;

			runDirectionVector += -playerLocation;

			angle = Vector3.Angle(ai.Body.transform.forward, runDirectionVector);
		}

		//Set Position to Move to relative to Current Position
		runDirectionVector = ai.Body.transform.position + runDirectionVector;

		//Set location into working memory for Move action in BT
		ai.WorkingMemory.SetItem<Vector3>("runDirectionVector", runDirectionVector);

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