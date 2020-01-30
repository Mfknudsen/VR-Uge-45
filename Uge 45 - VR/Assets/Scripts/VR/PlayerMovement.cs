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
    public float moveSpeed = 5;
    [Tooltip("Used to determin which hand is used")]
    public SteamVR_Input_Sources LeftHand;
    [Tooltip("Used to determin which hand is used")]
    public SteamVR_Input_Sources RightHand;
    public bool TrackPadMovementActive = true;
    public Transform Head;
    [Header("TEMP:")]
    public bool TEMP_UseKeysToMove;
    #endregion

    #region private DATA
    float MoveSpeed = 0;
    Vector3 Move;
    SteamVR_Action_Boolean TrackPadTouch;
    SteamVR_Action_Vector2 Trackpad;
    Transform CalcTransform;
    #endregion

    void Start()
    {
        Trackpad = SteamVR_Input.GetVector2Action("TrackPad");
        CalcTransform.position = Vector3.zero;
        CalcTransform.rotation = Quaternion.Euler(Vector3.zero);
    }

    void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        CalcTransform.rotation = Quaternion.Euler(new Vector3(0,Head.transform.rotation.eulerAngles.y,0));
        
        if (TrackPadMovementActive)
        {
            Vector2 moveDir = Vector3.zero;

            if (TEMP_UseKeysToMove == false)
            {
                if (Trackpad.GetAxis(LeftHand) != null)
                {
                    moveDir = Trackpad.GetAxis(LeftHand);
                }
            }
            else
            {
                moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }

            float y = transform.position.y;

            transform.position += ((CalcTransform.forward * moveDir.y) + (CalcTransform.right * moveDir.x)) * moveSpeed;

            if (transform.position.y != y)
            {
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }
        }
    }
}
