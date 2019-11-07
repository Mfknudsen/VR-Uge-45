using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Test : MonoBehaviour
{
    public SteamVR_Input_Sources LeftHand;
    public SteamVR_Input_Sources RightHand;

    [Tooltip("Getting the Trigger input from the controllers")]
    public SteamVR_Action_Boolean trigger = SteamVR_Input.GetBooleanAction("GrabPinch");

    public SteamVR_Behaviour_Pose trackedHandLeft;
    public SteamVR_Behaviour_Pose trackedHandRight;

    Transform HL;
    Transform HR;
    void Start()
    {

    }

    void Update()
    {
        UpdateLeftHandPoint();
        UpdateRightHandPoint();
    }

    void UpdateLeftHandPoint()
    {
        HL.transform.position = trackedHandLeft.transform.position;
        HL.transform.rotation = trackedHandLeft.transform.rotation;

        if (trigger.GetState(LeftHand))
        {
            RaycastHit hit;
            if (Physics.Raycast(HL.transform.position, HL.forward, out hit))
            {

            }
            Debug.DrawRay(HL.transform.position, HL.forward);
        }
    }

    void UpdateRightHandPoint()
    {
        HR.transform.position = trackedHandRight.transform.position;
        HR.transform.rotation = trackedHandRight.transform.rotation;
    }
}
