#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Key : MonoBehaviour
{
    //Setting up the public value and input.
    #region public Data
    [HideInInspector]
    public string keyword = "";  //String used to determent what keyholes can be used.

    [HideInInspector]
    public Vector3 originalPosition;  //Where this key first is spawned. Used when key is reset.

    [HideInInspector]
    public Quaternion originalRotation;  //Where this key first is facing. Used when key is reset.

    [HideInInspector]
    public List<GameObject> keyholesInRange = new List<GameObject>(); //For dynamic editing use list instead of arrays!! --- Lenght == Count, Add("Thing to add"), Remove(index)

    [HideInInspector]
    public GameObject currentKeyhole;  //What keyhole this key is currently placed in.
    public string ColorName;
    #endregion

    #region private Data
    Rigidbody RB;  //The rigidbody of this object.
    Transform Parent;
    #endregion
    private void Start()
    {
        RB = GetComponent<Rigidbody>();  //Getting the rigidbody of this object.

        //Setting the start position and rotation.
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        Parent = transform.parent;
    }

    public void reset()  //Resets this key.
    {
        if (currentKeyhole != null)  //Run if this key was in a keyhole.
        {
            currentKeyhole.GetComponent<Keyhole>().currentKey = null;  //The keyhole no longer has this key as its current key.
            currentKeyhole = null;  //This key is no longer in a keyhole.
        }

        RB.isKinematic = false;  //This key is no longer still.
        RB.useGravity = true;  //This key no longer uses gravity.
        RB.velocity = Vector3.zero;  //Its velocity based on physics is reset.
        RB.angularVelocity = Vector3.zero;  //Its rotation velocity based on physics is reset.
        transform.position = originalPosition;  //Its position is reset.
        transform.rotation = originalRotation;  //Its rotation is reset.
    }

    public void placeKey()  //Places this key in the closest keyhole.
    {
        if (keyholesInRange.Count > 0)  //Only run if there is any keyholes in range.
        {
            //Set a standard to compare with other keyholes if any.
            GameObject closestKeyhole = keyholesInRange[0];
            float shortestDistance = Vector3.Distance(transform.position, closestKeyhole.transform.position);

            for (int i = 1; i < keyholesInRange.Count; i++)  //Running through the list.
            {
                if (shortestDistance > Vector3.Distance(transform.position, keyholesInRange[i].transform.position))  //If the distance is less then the previus distance then the new closest keyhole is the current keyhole.
                {
                    closestKeyhole = keyholesInRange[i];  //New closest keyhole from index.
                }
            }

            if (closestKeyhole.GetComponent<Keyhole>() != null)  //If the currenct closest keyhole has the script keyhole.
            {
                Keyhole k = closestKeyhole.GetComponent<Keyhole>();  //Getting the keyhole script.

                if (k.currentKey == null)  //If there already is a key in the current keyhole.
                {
                    currentKeyhole = closestKeyhole;  //The current keyhole is now where this key is placed.
                    k.currentKey = this.gameObject;  //This key is now the current key for the current keyhole.
                    RB.isKinematic = true;  //The key is now still.
                    k.checkCorrectKey();  //Tell the current keyhole to check if this key is the correct key.

                    //Removing this key from activating the highlight for all keyholes in range.
                    for (int i = 0; i < keyholesInRange.Count; i++)
                    {
                        if (keyholesInRange[i].GetComponent<Keyhole>() != null)
                        {
                            k = keyholesInRange[i].GetComponent<Keyhole>();
                            k.keysInRange -= 1;

                            if (k.keysInRange <= 0)
                            {
                                k.Visual.enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public void detachKey()  //Detach this key from the current keyhole.
    {
        if (currentKeyhole != null)
        {
            Keyhole k = currentKeyhole.GetComponent<Keyhole>();
            k.currentKey = null;
            k.checkCorrectKey();
            currentKeyhole = null;
        }

        RB.isKinematic = false;
        RB.useGravity = false;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
    }

    void OnDetachedFromHand()
    {
        RB.isKinematic = false;
        RB.useGravity = true;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        transform.parent = Parent;

        placeKey();
    }

    void OnAttachedToHand()
    {
        detachKey();
    }
}
