#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
#endregion

public class Test : MonoBehaviour
{
    #region public DATA
    [Header("VR Settings")]
    [Tooltip("Setting the controller reference")]
    public SteamVR_Input_Sources LeftHand;
    [Tooltip("Setting the controller reference")]
    public SteamVR_Input_Sources RightHand;

    [Tooltip("Getting the Trigger input from the controllers")]
    public SteamVR_Action_Boolean trigger;

    [Tooltip("Setting up the left hand controller")]
    public SteamVR_Behaviour_Pose trackedHandLeft;

    [Tooltip("Setting up the right hand controller")]
    public SteamVR_Behaviour_Pose trackedHandRight;

    #endregion

    #region private Data
    private Transform HL;
    private Transform HR;
    private SteamVR_Action_Vector2 trackPad;
    float moveSpeed = 5f;
    #endregion

    void Start()
    {
        trackPad = SteamVR_Input.GetVector2Action("TrackPad");
        trigger = SteamVR_Input.GetBooleanAction("GrabPinch");
    }

    void Update()
    {
        UpdateLeftHandPoint();
        UpdateRightHandPoint();
    }

    void UpdateLeftHandPoint()
    {
        //HL.transform.position = trackedHandLeft.transform.position;
        HL.transform.rotation = trackedHandLeft.transform.rotation;

        HL.transform.position = Vector3.Lerp(HL.transform.position, trackedHandLeft.transform.position, 0.9f);

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

    void Movement()
    {
        Vector3 moveDir = Vector3.zero;
        if (trackPad.GetAxis(LeftHand) != null)
        {
            moveDir = trackPad.GetAxis(LeftHand);
        }

        transform.position += ((transform.forward * trackPad.GetAxis(LeftHand).y)+(transform.right * trackPad.GetAxis(LeftHand).x)).normalized * moveSpeed;
    }
}
