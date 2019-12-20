#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Puzzel_1 : MonoBehaviour
{
    #region public DATA
    [Header("Required Input:")]
    [Tooltip("Order of object determins the order they need to be placed")]
    public List<GameObject> keys = new List<GameObject>();

    [Tooltip("Order of object determins the order they need to be placed")]
    public List<GameObject> keyholes = new List<GameObject>();

    [HideInInspector]
    public bool PuzzelComplete = false;

    [Header("Optional Input:")]
    public string keyword = "Puzzel_1_Key";

    [Header("TEMP BOTTUMS:")]
    [Tooltip("IF PRESSED: WILL RESET POSITION OF KEYS TO START POSITION")]
    public bool RESET_KEY_POSITION = false;
    public bool ADD_GRAVITY = false;
    #endregion

    #region private DATA
    #endregion

    void Start()
    {
        if (keys.Count > 0)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                Keys k = keys[i].GetComponent<Keys>();
                k.keyword = keyword;
                k.P1 = this;
            }
        }

        if (keyholes.Count > 0)
        {
            for (int i = 0; i < keyholes.Count; i++)
            {
                Keyholes k = keyholes[i].GetComponent<Keyholes>();
                k.keyword = keyword;
                k.P1 = this;
            }
        }
    }

    void Update()
    {
        CheckOrder();

        TEMP_ACTIONS();
    }

    void CheckOrder()
    {
        if (keyholes.Count == 3 && keys.Count == 3 && !PuzzelComplete)
        {
            int t = 0;

            for (int i = 0; i < 3; i++)
            {
                if (keys[i] != keyholes[i].GetComponent<Keyholes>().currentKey)
                {
                    break;
                }

                t++;
            }
            if (t == 3)
            {
                PuzzelComplete = true;
                Debug.Log("Puzzel 1 has been solved!");
            }
        }
    }

    void ResetKeys()
    {
        for (var i = 0; i < keys.Count; i++)
        {
            Keys k = keys[i].GetComponent<Keys>();
            k.reset();
        }

        RESET_KEY_POSITION = false;
    }

    void addGrav()
    {
        for (var i = 0; i < keys.Count; i++)
        {
            Keys k = keys[i].GetComponent<Keys>();
            k.addGrav();
        }

        ADD_GRAVITY = false;
    }


    void TEMP_ACTIONS()
    {
        if (RESET_KEY_POSITION)
            ResetKeys();
        if (ADD_GRAVITY)
            addGrav();
    }
}
