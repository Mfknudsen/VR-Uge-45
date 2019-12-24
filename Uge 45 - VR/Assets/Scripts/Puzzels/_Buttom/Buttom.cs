#region System
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Buttom : MonoBehaviour
{
    #region public DATA
    [Header("Required Input:")]
    public GameObject Visual;
    [HideInInspector]
    public bool active = false;
    public Transform rest;
    [Header("TEMP BUTTOMS:")]
    public bool ACTIVATE = false;
    #endregion

    #region private DATA
    Vector3 restTransform;
    Vector3 downTransform;
    Vector3 targetTransform;
    #endregion

    void Start()
    {
        downTransform = transform.position;
        restTransform = rest.transform.position;
        SwitchActive(false);
    }

    void Update()
    {
        MoveButtom();

        if (ACTIVATE == true)
        {
            TEMP();
        }
    }

    void MoveButtom()
    {
        if (transform.position != downTransform)
        {
            targetTransform = targetTransform - (downTransform - transform.position);
            downTransform = transform.position;
            restTransform = rest.transform.position;
        }

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
            active = true;
        }
        else
        {
            targetTransform = restTransform;
            active = false;
        }
    }

    void TEMP()
    {
        if (active)
        {
            SwitchActive(false);
        }
        else
        {
            SwitchActive(true);
        }

        ACTIVATE = false;
    }
}
