#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Keyholes : MonoBehaviour
{
    #region  public DATA
    public string keyword = "";

    public Puzzel_1 P1;

    public GameObject currentKey;
    #endregion

    #region private DATA
    MeshRenderer Visual;

    int keysInRange = 0;
    #endregion

    void Start()
    {
        Visual = GetComponent<MeshRenderer>();
        Visual.enabled = false;
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
        if (other.gameObject.GetComponent<Keys>() != null)
        {
            keysInRange += 1;
            Visual.enabled = true;
            Keys k = other.gameObject.GetComponent<Keys>();
            int i = k.keyholesInRange.Count;
            k.keyholesInRange.Add(this.gameObject);
        }

        if (keysInRange > 0)
        {
            Visual.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Keys>() != null)
        {
            keysInRange -= 1;
            Keys k = other.gameObject.GetComponent<Keys>();
            for (int i = 0; i < k.keyholesInRange.Count; i++)
            {
                if (k.keyholesInRange[i] == this.gameObject)
                {
                    k.keyholesInRange.RemoveAt(i);
                }
            }
        }

        if (keysInRange == 0)
        {
            Visual.enabled = false;
        }
    }

    void updateKey()
    {
        currentKey.transform.position = transform.position;
        currentKey.transform.rotation = transform.rotation;
    }

    public void PlaceKey(GameObject key)
    {
        currentKey = key;
        currentKey.transform.position = transform.position;
        currentKey.transform.rotation = transform.rotation;
        currentKey.transform.localScale = transform.localScale;
    }
}
