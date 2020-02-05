using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    //Setting up the public values and input.
    #region  public DATA
    public string keyword = "";  //Only key with the same keyword as this script will be albe to be placed into this keyhole.
    public GameObject correctKey;  //The correct key for this keyhole.
    [HideInInspector]
    public GameObject currentKey;  //The current key that is placed in this keyhole.
    [HideInInspector]
    public bool active = false;  //If this keyhole has the correct key in it.
    [HideInInspector]
    public int keysInRange = 0;  //All the keys that is in range of the keyhole. Based on a trigger collider.
    [HideInInspector]
    public MeshRenderer Visual;  //The visual rendere for this object. Used to create a highlight.
    public GameObject Highlight;
    public string Color;
    #endregion

    void Start()  //When the script starts.
    {
        Visual = Highlight.GetComponent<MeshRenderer>();  //Getting the rendere from the game object.
        Visual.enabled = false;  //Making the object invisible.

        if (correctKey.GetComponent<Key>() != null)  //Making sure the object label as the correct key has is a key.
        {
            correctKey.GetComponent<Key>().keyword = keyword;  //Matching the keyword of the key with that of this script.
        }
    }

    void Update()
    {
        if (currentKey != null)  //If there is a key in this keyhole.
        {
            updateKey();  //Updating the key.
        }
    }

    void OnTriggerEnter(Collider other)  //When a collider of another object hits the trigger collider.
    {
        if (other.gameObject.GetComponent<Key>() != null)  //Checks if the object is a key.
        {
            Key k = other.gameObject.GetComponent<Key>();  //Getting the key script.

            if (currentKey == null && k.keyword == keyword)  //Checks if the key has the same keyword as this keyhole and if there already is a key in this keyhole.
            {
                keysInRange += 1;  //One more key is in range of this keyhole.
                Visual.enabled = true;  //This keyhole will now show a highlight.
                k.keyholesInRange.Add(this.gameObject);  //Telling the key that may be placed in this keyhole.
            }
        }
    }

    void OnTriggerExit(Collider other)  //When a collider of another object exits the trigger collider.
    {
        if (other.gameObject.GetComponent<Key>() != null)  //If the object is a key.
        {
            Key k = other.gameObject.GetComponent<Key>();  //Getting the key script.

            if (k.keyword == keyword)  //If the key has the right keyword.
            {
                keysInRange -= 1;  //There is now one less key in range of this keyhole.

                for (int i = 0; i < k.keyholesInRange.Count; i++)  //Running through the list of keys in range of this keyhole.
                {
                    if (k.keyholesInRange[i] == this.gameObject)  //When the key is found in the list then remove it from the list.
                    {
                        k.keyholesInRange.RemoveAt(i);  //Removing the this keyhole based on the index value.
                    }
                }
            }
        }

        if (keysInRange <= 0)  //If there is no more keys in range of this keyhole then it will stop showing the highlight.
        {
            Visual.enabled = false;
        }
    }

    public bool checkCorrectKey()  //Check if the placed key in this keyhole is the key that mach the correct key. 
    {
        if (currentKey == correctKey)  //If the key is the correct key then it will activate this keyhole.
        {
            active = true;
            return true;
        }
        else  //Else it will deactivate this keyhole.
        {
            active = false;
            return false;
        }
    }

    void updateKey()  //Updating the position and rotation of the current key placed in this keyhole to match the position and rotation of this keyhole.
    {
        currentKey.transform.position = transform.position - new Vector3(0,0,0.1f);
        currentKey.transform.rotation = Quaternion.Euler(270,270,270);
    }
}
