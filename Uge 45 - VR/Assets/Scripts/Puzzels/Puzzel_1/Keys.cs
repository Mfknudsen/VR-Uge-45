#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
#endregion

public class Keys : MonoBehaviour
{
    #region public Data
    [HideInInspector]
    public string keyword;

    [HideInInspector]
    public Puzzel_1 P1;

    [HideInInspector]
    public Vector3 originalPosition;

    [HideInInspector]
    public Quaternion originalRotation;

    [HideInInspector]
    public List<GameObject> keyholesInRange = new List<GameObject>(); //For dynamic editing use list instead of arrays!! --- Lenght == Count, Add("Thing to add"), Remove(index)

    [HideInInspector]
    public GameObject currentKeyhole;

    [Header("TEMP BUTTOMS")]
    public bool PLACE_KEY = false;
    #endregion

    #region private Data
    Rigidbody RB;
    #endregion
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.useGravity = false;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (PLACE_KEY == true)
        {
            placeKey();
            PLACE_KEY = false;
        }
    }

    public void reset()
    {
        if (currentKeyhole != null)
        {
            currentKeyhole.GetComponent<Keyholes>().currentKey = null;
            currentKeyhole = null;
        }

        RB.isKinematic = false;
        RB.useGravity = false;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    public void addGrav()
    {
        RB.useGravity = true;
        RB.isKinematic = false;
    }

    public void placeKey()
    {
        if (keyholesInRange.Count > 0)
        {
            GameObject closestKeyhole = keyholesInRange[0];
            float shortestDistance = Vector3.Distance(transform.position, closestKeyhole.transform.position);

            for (int i = 1; i < keyholesInRange.Count; i++)
            {
                if (shortestDistance > Vector3.Distance(transform.position, keyholesInRange[i].transform.position))
                {
                    closestKeyhole = keyholesInRange[i];
                }
            }

            if (closestKeyhole.GetComponent<Keyholes>() != null)
            {
                Keyholes k = closestKeyhole.GetComponent<Keyholes>();
                if (k.currentKey == null)
                {
                    currentKeyhole = closestKeyhole;
                    k.currentKey = this.gameObject;
                    RB.isKinematic = true;

                    for (int i = 0; i < keyholesInRange.Count; i++)
                    {
                        k = keyholesInRange[i].GetComponent<Keyholes>();
                        k.keysInRange -= 1;

                        if (k.keysInRange <= 0)
                        {
                            k.GetComponent<Keyholes>().Visual.enabled = false;
                        }
                    }
                }
            }
        }
    }

    public void detachKey()
    {
        if (currentKeyhole != null)
        {
            currentKeyhole.GetComponent<Keyholes>().currentKey = null;
            currentKeyhole = null;
        }

        RB.isKinematic = false;
        RB.useGravity = false;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
    }

    private void OnDetachedFromHand()
    {
        placeKey();
    }

    private void OnAttachedToHand()
    {
        detachKey();
    }
}