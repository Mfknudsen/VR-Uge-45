#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Keyholes : MonoBehaviour
{
    #region  public DATA
    [HideInInspector]
    public string keyword;

    [HideInInspector]
    public Puzzel_1 P1;

    [HideInInspector]
    public GameObject currentKey;

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
            Keys k = other.gameObject.GetComponent<Keys>();

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
        if (other.gameObject.GetComponent<Keys>() != null)
        {
            Keys k = other.gameObject.GetComponent<Keys>();

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

    void updateKey()
    {
        currentKey.transform.position = transform.position;
        currentKey.transform.rotation = transform.rotation;
    }
}