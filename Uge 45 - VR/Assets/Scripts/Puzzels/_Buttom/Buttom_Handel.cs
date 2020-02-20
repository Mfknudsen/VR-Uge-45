#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
#endregion

public class Buttom_Handel : MonoBehaviour
{
    #region public DATA
    public Buttom B;  //The buttom this handel belongs to.
    public Transform Visual;  //The visual part of the buttom.
    #endregion

    #region private DATA
    Transform Parent;
    #endregion

    void Start()
    {
        Parent = transform.parent;
    }

    void Update()
    {
        transform.position = Visual.position;  //This handel returns to visual part.
    }

    void OnDetachedFromHand()
    {
        B.SwitchActive(false);  //When the object is no longer being hold then the buttom is no longer active.
        transform.parent = Parent;
    }

    void OnAttachedToHand()
    {
        B.SwitchActive(true);  //When the object is being held then the buttom is active.
    }
}