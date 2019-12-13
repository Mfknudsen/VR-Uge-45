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

    [Tooltip("Setting up the left hand controller")]
    public SteamVR_Behaviour_Pose trackedHandLeft;

    [Tooltip("Setting up the right hand controller")]
    public SteamVR_Behaviour_Pose trackedHandRight;

    Transform HL;
    Transform HR;
    
    SteamVR_Action_Vector2 trackPad = SteamVR_Input.GetVector2Action("TrackPad");


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
