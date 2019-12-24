using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    #region  public DATA
    public string keyword = "";

    [HideInInspector]
    public GameObject currentKey;
    public GameObject correctKey;

    [HideInInspector]
    public bool active = false;

    [HideInInspector]
    public int keysInRange = 0;

    [HideInInspector]
    public MeshRenderer Visual;
    #endregion

    #region private DATA
    #endregion

    void Start()
    {
        Visual = GetComponent<MeshRenderer>();
        Visual.enabled = false;

        if (correctKey.GetComponent<Key>() != null)
        {
            correctKey.GetComponent<Key>().keyword = keyword;
        }
    }

    void Update()
    {
        if (currentKey != null)
        {
            updateKey();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Key>() != null)
        {
            Key k = other.gameObject.GetComponent<Key>();

            if (k.currentKeyhole == null && k.keyword == keyword)
            {
                keysInRange += 1;
                Visual.enabled = true;
                int i = k.keyholesInRange.Count;
                k.keyholesInRange.Add(this.gameObject);
            }
        }

        if (keysInRange > 0)
        {
            Visual.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Key>() != null)
        {
            Key k = other.gameObject.GetComponent<Key>();

            if (k.keyword == keyword)
            {
                keysInRange -= 1;

                for (int i = 0; i < k.keyholesInRange.Count; i++)
                {
                    if (k.keyholesInRange[i] == this.gameObject)
                    {
                        k.keyholesInRange.RemoveAt(i);
                    }
                }
            }
        }

        if (keysInRange <= 0)
        {
            Visual.enabled = false;
        }
    }

    public bool checkCorrectKey()
    {
        if (currentKey == correctKey)
        {
            active = true;
            return true;
        }
        else
        {
            active = false;
            return false;
        }
    }

    void updateKey()
    {
        currentKey.transform.position = transform.position;
        currentKey.transform.rotation = transform.rotation;
    }
}
