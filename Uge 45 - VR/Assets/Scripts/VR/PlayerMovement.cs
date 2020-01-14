#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
#endregion

public class PlayerMovement : MonoBehaviour
{
    #region public DATA
    [Header("Required Input:")]
    public float MoveSensitivity = 0.1f;
    public float MaxSpeed = 1;
    public SteamVR_Input_Sources LeftHand;
    public SteamVR_Input_Sources RightHand;
    public Transform PlayerTransform;
    public float MoveSpeed = 0;
    #endregion

    #region private DATA
    Vector3 Move;
    SteamVR_Action_Boolean TrackPadTouch;
    SteamVR_Action_Vector2 Trackpad;
    Transform CameraTransform, HeadTransform;
    #endregion

    void Start()
    {
        Trackpad = SteamVR_Input.GetVector2Action("TrackPad");

        CameraTransform = SteamVR_Render.Top().origin;
        HeadTransform = SteamVR_Render.Top().head;
    }

    void FixedUpdate()
    {
        Head();
        Movement();
    }
    void Head()
    {
        Vector3 oldPos = CameraTransform.position;
        Quaternion oldRot = CameraTransform.rotation;

        PlayerTransform.eulerAngles = new Vector3(0, HeadTransform.rotation.eulerAngles.y, 0);

        CameraTransform.position = oldPos;
        CameraTransform.rotation = oldRot;
    }
    void Movement()
    {
        Vector3 orientEuler = new Vector3(0, PlayerTransform.eulerAngles.y, 0);
        Quaternion orient = Quaternion.Euler(orientEuler);
        Vector3 movement = Vector3.zero;

        if (TrackPadTouch.GetStateUp(RightHand))
        {
            MoveSpeed = 0;
        }

        if (TrackPadTouch.GetState(RightHand))
        {
            MoveSpeed += Trackpad.GetAxis(RightHand).y * MoveSensitivity;
            MoveSpeed = Mathf.Clamp(MoveSpeed, -MaxSpeed, MaxSpeed);

            movement += orient * (MoveSpeed * PlayerTransform.forward) * Time.deltaTime;
        }

        PlayerTransform.position = Vector3.Lerp(PlayerTransform.position, PlayerTransform.position + movement, 0.9f);
    }
}
