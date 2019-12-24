#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Puzzel : MonoBehaviour
{
    //"protected" means only this and script that have inherited from this script can used the funtions.

    //Gets a list of bools and check if theyre all active. Will return a bool.
    protected bool CheckAllActive(List<bool> actives)
    {
        for (int i = 0; i < actives.Count; i++)  //Running through the list.
        {
            if (actives[i] != true)  //Check is the current bool is active.
            {
                return false; //If even one of the list bools isn't active then it will return false;
            }
        }
        return true;  //If all the bools in the list is true then it will return true.
    }

    //Gets a list of keyholes and check if theyre all active. Will return a bool.
    protected bool CheckAllKeysActive(List<Keyhole> keyholes)
    {
        for (int i = 0; i < keyholes.Count; i++)  //Running through the list.
        {
            if (keyholes[i].checkCorrectKey() != true)  //Check if the current keyhole has the correct key in it. Check if it is active.
            {
                return false;  //If even one of the list keyholes isn't active then it will return false;
            }
        }
        return true;  //If all the keyholes in the list is true then it will return true.
    }

    //Gets a list of pressure plates and check if theyre all active. Will return a bool.
    protected bool CheckAllPressurePlatesActive(List<PressurePlate> pressurePlates)
    {
        for (int i = 0; i < pressurePlates.Count; i++)  //Running through the list.
        {
            if (pressurePlates[i].active != true)  //Check if the current pressure plate is active.
            {
                return false;  //If even one of the pressure plates isn't active then it will return false;
            }
        }
        return true;  //If all pressure plates is active then it will return true.
    }

    //Gets a list of levers and check if theyre all active. Will return a bool.
    protected bool CheckAllLeverActive(List<Lever> levers)
    {
        for (int i = 0; i < levers.Count; i++)  //Running through the list.
        {
            if (levers[i].active == true)  //Check if the current lever is active.
            {
                return false;  //If even one of the levers isn't active then it will return false.
            }
        }
        return true;  //If all levers is active then it will return true.
    }

    //Gets a list of levers and check the average progres of the levers. Will return a float between 0 and 100.
    protected float CheckAllLeverProgress(List<Lever> levers)
    {
        float tempProgress = 0;  //Starting the average progress.

        for (int i = 0; i < levers.Count; i++)  //Running through the list.
        {
            tempProgress += levers[i].CountProcent();  //Adding the progress of the current lever to the average lever.
        }

        tempProgress = Mathf.Ceil(tempProgress / levers.Count);  //Dividing the collected progress of all the levers by the number of levers and then getting the highest closest int from the result.

        if (tempProgress < 0)  //If the average progress is less then 0 procent then make it 0 procent.
        {
            tempProgress = 0;
        }
        else if (tempProgress > 100)  //If the average progress is greater then 100 procent then make it 100 procent.
        {
            tempProgress = 100;
        }

        return tempProgress;  //Returning the average progress.
    }

    //Gets a door and a bool. Will open or close a door based on the bool input.
    protected void OpenDoorByActive(bool active, Door door)
    {
        door.active = active;  //Setting the current doors active state to the bool.

        door.SwitchOpenClosed();  //Switching the current door to match its active state.
    }

    //Gets a float and a door. Will open a door based on the float value.
    protected void OpenDoorByProcent(float progress, Door door)
    {
        door.byProcent = true;  //Making so the door will open by procent instead of its acitve state.

        Vector3 t = door.ClosedTransform + ((door.OpenTransform - door.ClosedTransform) / 100 * progress);  //Making the new position the door should move to.

        door.targetTransform = t;  //Giving the door a new target position.
    }

    //Gets a float for x,y and z and a crane. Will move the crane based on the float values.
    protected void MoveCraneByProcent(float progressX, float progressY, float progressZ, Crane crane)
    {
        //Giving the crane new values.
        crane.progressX = progressX;
        crane.progressY = progressY;
        crane.progressZ = progressZ;

    }

    //Gets a bool and a crane. Will switch the cranes claw between lock or release based on the bool.
    protected void SwitchCraneClawByActive(bool active, Crane crane)
    {
        if (active == true)  //If the bool is true then lock the claw.
        {
            crane.LockClaw();
        }
        else  //If the bool is false then release the claw.
        {
            crane.ReleaseClaw();
        }
    }
}
