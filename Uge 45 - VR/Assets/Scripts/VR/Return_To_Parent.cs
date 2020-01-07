#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Return_To_Parent : MonoBehaviour
{
    #region private DATA
    Transform Parent;  //The original parent.
    #endregion

    void Start()
    {
        Parent = transform.parent;  //Getting the parent.
    }

    void OnDetachedFromHand(){
        transform.SetParent(Parent);  //Returning to the original parent when not being held.
    }
}
