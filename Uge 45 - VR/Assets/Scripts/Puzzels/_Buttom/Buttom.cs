#region System
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Buttom : MonoBehaviour
{
    #region public DATA
    public bool active = false;
    public GameObject Visual;
    #endregion

    #region private DATA
    Vector3 restTransform;
    Vector3 downTransform;
    Vector3 targetTransform;
    #endregion

    void Start()
    {
        downTransform = transform.position;
        restTransform = Visual.transform.position;
        SwitchActive(false);
    }

    void Update()
    {
        MoveButtom();
    }

    void MoveButtom()
    {
        if (Visual.transform.position != targetTransform)
        {
            Visual.transform.position = Vector3.Lerp(Visual.transform.position, targetTransform, 0.1f);
        }
    }

    public void SwitchActive(bool isInHand)
    {
        if (isInHand == true)
        {
            targetTransform = downTransform;
            active = false;
        }
        else
        {
            targetTransform = restTransform;
            active = true;
        }
    }
}
