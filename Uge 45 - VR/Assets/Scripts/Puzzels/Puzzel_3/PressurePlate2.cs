#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion 

public class PressurePlate2 : MonoBehaviour
{
    #region public DATA
    public Puzzel_3 P3;
    [HideInInspector]
    public bool isDown = false;
    #endregion

    #region private DATA
    Transform visualPlate;
    Vector3 targetPosition, restPosition, downPosition;
    int objectOnPlate = 0;
    #endregion

    void Start()
    {
        visualPlate = transform.Find("VisualPlate");
        downPosition = visualPlate.transform.position;
        restPosition = transform.position;
        targetPosition = restPosition;
    }

    void Update()
    {
        MovePlate();
    }

    void OnTriggerEnter()
    {
        objectOnPlate += 1;

        if (objectOnPlate > 0)
        {
            targetPosition = downPosition;
            isDown = true;
            P3.Check();
        }
    }

    void OnTriggerExit()
    {
        objectOnPlate -= 1;

        if (objectOnPlate == 0)
        {
            targetPosition = restPosition;
            isDown = false;
            P3.Check();
        }
    }

    void MovePlate()
    {
        if (visualPlate.transform.position != targetPosition)
        {
            visualPlate.transform.position = Vector3.Lerp(visualPlate.transform.position, targetPosition, 0.5f);
        }
    }
}
