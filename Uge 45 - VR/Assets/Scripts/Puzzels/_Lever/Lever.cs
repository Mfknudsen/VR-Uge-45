#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
#endregion

public class Lever : MonoBehaviour
{
    #region public DATA
    [Header("Required Input")]
    public GameObject Joint;  //The point to rotate around.
    public GameObject Handel;  //The handel that the player can hold onto.
    public Transform Top;  //The top of the lever.
    [HideInInspector]
    public bool active = false;  //If the lever is active or not.
    [HideInInspector]
    public float progressInProcent = 0;  //The levers active state based in procent.
    public Transform TargetTransform;  //The target point to rotate towards.
    #endregion

    #region private DATA
    Quaternion maxRot, minRot;  //Boundaries of the lever.
    Vector3 lookDirection;  //Vector 3 used for rotation.
    Quaternion targetRotation;  //The new rotation based on the lookDirection.
    Vector3 lastHandelPosition;  //Where the handel were last frame.
    bool lockHandel = false;  //If the handel is locked to the position of the top.
    #endregion

    void Start()
    {
        //Setting the min and max rotations based on vector3's.
        minRot = Quaternion.Euler(-45, 0, 0);
        maxRot = Quaternion.Euler(-45, 180, 0);

        //Setting the levers rotation based on its start active state.
        if (active == true)
        {
            Joint.transform.localRotation = maxRot;
        }
        else
        {
            Joint.transform.localRotation = minRot;
        }

        Handel.transform.position = Top.transform.position;  //Placing the handel on the top.
    }

    void Update()
    {
        //Updating the different parts of the script.
        RotateTowardsHandel();
        CountProcent();
        IsActiveOrNot();
        LockHandelTransform();
    }

    void RotateTowardsHandel()  //Rotating the handel.
    {
        TargetTransform.position = Handel.transform.position;  //Due to VR changing the parent of the object being held. Therefor a second transform is used when using localPosition. Matching their global position.

        //Making two temp vector3's out of local position values and removing the x value so the lever wont rotate around that axis later in the code.
        Vector3 tempHandel = TargetTransform.localPosition;
        tempHandel.x = 0;
        Vector3 tempJoint = Joint.transform.localPosition;
        tempJoint.x = 0;

        lookDirection = tempHandel - tempJoint;  //The lookDirection is based on the vector between the joint and the top.

        if (lookDirection.y >= minRot.eulerAngles.y)  //If the new vector3 is not beyond the min rotation.
        {
            targetRotation = Quaternion.LookRotation(lookDirection);  //The new rotation is made based on the new vector3.

            Joint.transform.localRotation = targetRotation;  //Moving the rotation of the lever.

            lastHandelPosition = Handel.transform.position;  //Remembering where the handel were during this rotation.

            if (targetRotation.y == 0 && targetRotation.z == 0)
            {

                if (targetRotation.eulerAngles.x > minRot.eulerAngles.x)
                {
                    Joint.transform.localRotation = minRot;  //If the new rotation is beyond the min rotation then it is returned to the min rotation.
                }

            }
            else if (targetRotation.x == 0 && targetRotation.w == 0)
            {
                if (targetRotation.eulerAngles.x > maxRot.eulerAngles.x)
                {
                    Joint.transform.localRotation = maxRot;  //If the new rotation is beyond the max rotation then it is returned to the max rotation.
                }
            }
        }
    }

    public float CountProcent()  //Counting the placement of the lever between the min and max rotation.
    {
        float tempProcent = 180;
        Quaternion tempRot = Joint.transform.localRotation;

        //Getting the angel of the lever.
        if (tempRot.eulerAngles.y != 0)
        {
            tempProcent = (540 - (360 - tempRot.eulerAngles.x) * 4);
        }
        else
        {
            tempProcent = -(180 - (360 - tempRot.eulerAngles.x) * 4);
        }

        //Converting the angel to procent. 360 degress = 100 procent.
        tempProcent = tempProcent / 3.6f;

        //Making sure the current procent is not less then 0 or higher then 100.
        if (tempProcent < 0)
        {
            tempProcent = 0;
        }
        else if (tempProcent > 100)
        {
            tempProcent = 100;
        }

        progressInProcent = Mathf.Ceil(tempProcent);  //Getting the closest highest int number to the current procent. 100 -> 100, 23,4 -> 24.

        return progressInProcent;
    }

    public void IsActiveOrNot()  //Check when the lever is active.
    {
        if (progressInProcent <= 50)
        {
            active = false;
        }
        else if (progressInProcent > 50)
        {
            active = true;
        }
    }

    public void LockHandelTransform()  //Locking the handel position to the top position.
    {
        if (lockHandel == true)
        {
            Handel.transform.position = Top.transform.position;
        }
    }
}
