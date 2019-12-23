#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Door : MonoBehaviour
{
    #region public DATA
    [Header("Required Input:")]
    public Transform Open;
    public float doorMoveSpeed = 0.25f;
    [Header("Optional Input")]
    public bool active = true;
    [HideInInspector]
    public Vector3 OpenTransform;
    [HideInInspector]
    public Vector3 ClosedTransform;
    #endregion

    #region private DATA
    Vector3 targetTransform;
    #endregion

    void Start()
    {
        OpenTransform = Open.transform.position;
        ClosedTransform = transform.position;

        if (active == true)
        {
            transform.position = OpenTransform;
            targetTransform = OpenTransform;
        }
        else
        {
            transform.position = ClosedTransform;
            targetTransform = ClosedTransform;
        }
    }

    void Update()
    {
        MoveDoor();
    }

    public void MoveDoor()
    {
        if (transform.position != targetTransform)
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform, doorMoveSpeed * Time.deltaTime);
        }
    }

    public void SwitchOpenClosed()
    {
        if (active == true)
        {
            targetTransform = ClosedTransform;
        }
        else
        {
            targetTransform = OpenTransform;
        }
    }
}
