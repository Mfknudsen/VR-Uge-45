#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
#endregion

public class Lever_Handel : MonoBehaviour
{
    #region public DATA
    public Transform ToReturnTo;  //Where the handel returns when not being held.
    #endregion

    #region private DATA
    public Transform ParentOfObject;  //The original parent of this object.
    #endregion

    void Start()
    {
        ParentOfObject = transform.parent;  //Get the parent transform.
    }

    void OnDetachedFromHand()  //When no longer being held.
    {
        transform.position = ToReturnTo.position;  //Return to the top of the lever.

        transform.SetParent(ParentOfObject);  //Return af a child to the original parent.
    }
}
