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
    public GameObject Joint;
    public GameObject Handel;
    public Transform Top;
    [HideInInspector]
    public bool active = false;
    [Header("TEMP BUTTOMS:")]
    public bool SWITCH_LOCK = false;
    bool lockHandel = false;
    #endregion

    #region private DATA
    Quaternion maxRot, minRot;
    Vector3 lookDirection;
    Quaternion targetRotation;
    #endregion

    void Start()
    {
        minRot = Quaternion.Euler(-45, 0, 0);
        maxRot = Quaternion.Euler(-45, 180, 0);

        Joint.transform.localRotation = maxRot;
        Handel.transform.position = Top.transform.position;
    }

    void Update()
    {
        RotateTowardsHandel();
        LockHandelTransform();
        CountProcent();

        TEMPLOCKHANDEL();
    }

    void RotateTowardsHandel()
    {
        Vector3 tempHandel = Handel.transform.localPosition;
        tempHandel.x = 0;
        Vector3 tempJoint = Joint.transform.localPosition;
        tempJoint.x = 0;
        lookDirection = tempHandel - tempJoint;
        targetRotation = Quaternion.LookRotation(lookDirection);

        if (targetRotation.y == 0 && targetRotation.z == 0)
        {
            Joint.transform.localRotation = targetRotation;

            if (targetRotation.eulerAngles.x > minRot.eulerAngles.x)
            {
                Joint.transform.localRotation = minRot;
            }
        }
        else if (targetRotation.x == 0 && targetRotation.w == 0)
        {
            Joint.transform.localRotation = targetRotation;

            if (targetRotation.eulerAngles.z < maxRot.eulerAngles.z)
            {
                Joint.transform.localRotation = maxRot;
            }
        }
    }

    void CountProcent()
    {
        float tempProcent = 180;
        Quaternion tempRot = Joint.transform.localRotation;

        if (tempRot.eulerAngles.y != 0)
        {
            tempProcent = (540 - (360 - tempRot.eulerAngles.x) * 4);
        }
        else
        {
            tempProcent = -(180 - (360 - tempRot.eulerAngles.x) * 4);
        }

        tempProcent = tempProcent / 3.6f;
    }
    void LockHandelTransform()
    {
        if (lockHandel == true)
        {
            Handel.transform.position = Top.transform.position;
        }
    }

    void OnDetachedFromHand()
    {
        lockHandel = true;
    }

    void OnAttachedToHand()
    {
        lockHandel = false;
    }

    void TEMPLOCKHANDEL()
    {
        if (SWITCH_LOCK)
        {
            if (lockHandel)
            {
                lockHandel = false;
                SWITCH_LOCK = false;
            }
            else
            {
                lockHandel = true;
                SWITCH_LOCK = false;
            }
        }
    }
}
