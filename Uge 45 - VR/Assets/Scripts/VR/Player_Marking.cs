#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
#endregion

public class Player_Marking : MonoBehaviour
{
    #region public DATA
    public HUE_Controller HUE;
    public int TimeToMark = 20;
    public int TimeToRemove = 100;
    public Transform controller;
    #endregion

    #region private DATA
    GameObject ObjectToMark;
    GameObject MarkedObject;
    float MarkCounter = 0;
    float TimeToRemoveCounter = 0;
    #endregion

    void Update()
    {
        MarkObject();

        if (MarkedObject != null)
        {
            DetectObjectDistance();
        }
    }

    void DetectObjectDistance()
    {
        float distHeadset = Vector3.Distance(transform.position, MarkedObject.transform.position);
        float distController = Vector3.Distance(controller.position, MarkedObject.transform.position);

        if (distController < distHeadset)
        {
            distController = distController / (distHeadset / 100);

            HUE.ReceiveInfoScript(new Vector3(255 * (-255 * distController), 255 * distController, 0));
        }
        else if (distController == distHeadset)
        {
            HUE.ReceiveInfoScript(new Vector3(255, 0, 0));
        }
    }

    void MarkObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (ObjectToMark == null && hit.collider.GetComponent<Interactable>() != null)
            {
                ObjectToMark = hit.collider.gameObject;
            }

            if (ObjectToMark == hit.collider.gameObject)
            {
                TimeToRemoveCounter = 0;
                MarkCounter += Time.deltaTime;
            }
            else
            {
                TimeToRemoveCounter += Time.deltaTime;
            }
        }

        if (Mathf.Floor(MarkCounter) >= TimeToMark)
        {

        }

        if (Mathf.Floor(TimeToRemoveCounter) >= TimeToRemove)
        {
            ObjectToMark = null;
        }
    }


}
