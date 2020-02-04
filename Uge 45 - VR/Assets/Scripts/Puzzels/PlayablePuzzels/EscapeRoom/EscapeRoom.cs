#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class EscapeRoom : Puzzel
{
    #region public DATA
    public List<Keyhole> Keyholes = new List<Keyhole>();
    public List<Key> Keys = new List<Key>();
    public Buttom Buttom;
    #endregion

    #region private DATA
    OSC OSC;
    #endregion

    void Start()
    {
        OSC = GetComponent<OSC>();

        for (int i = 0; i < Keyholes.Count; i++)
        {
            Keyholes[i].keyword = "E";
            GiveKeyKeyword(i);
        }

        for (int i = 0; i < Keys.Count; i++)
        {
            Keys[i].keyword = "E";
        }

        SendOSC();
    }

    void Update()
    {
        bool t = false;

        for (int i = 0; i < Keyholes.Count; i++)
        {
            if (Keyholes[i].currentKey == null)
            {
                t = false;
                break;
            }
            else if (i == Keyholes.Count - 1)
            {
                t = true;
                break;
            }
        }
        if (t && Buttom.active)
        {
            if (CheckAllKeysActive(Keyholes))
            {
                Debug.Log("You have escaped!");
            }
            else
            {
                resetOrder();
            }
        }
    }

    void GiveKeyKeyword(int i)
    {
        Keyhole kh = Keyholes[i];
        Key k = Keys[Random.Range(0, Keys.Count)];
        bool t = true;

        for (int e = 0; e < Keyholes.Count; e++)
        {
            if (Keyholes[e].correctKey == k.gameObject)
            {
                t = false;
            }
        }

        if (t)
        {
            kh.correctKey = k.gameObject;
        }

        if (kh.correctKey == null)
        {
            GiveKeyKeyword(i);
        }
    }

    public void resetOrder()
    {
        for (int i = 0; i < Keyholes.Count; i++)
        {
            Keyhole K = Keyholes[i];
            K.correctKey = null;
        }

        for (int i = 0; i < Keys.Count; i++)
        {
            Keys[i].reset();
        }

        for (int i = 0; i < Keyholes.Count; i++)
        {
            GiveKeyKeyword(i);
        }
    }

    void SendOSC()
    {
        for (int i = 0; i < Keyholes.Count; i++)
        {
            Key k = Keyholes[i].correctKey.GetComponent<Key>();

            OscMessage message = new OscMessage();
            message.address = "VR";
            message.values.Add(k.Color.x);
            message.values.Add(k.Color.y);
            message.values.Add(k.Color.z);
            OSC.Send(message);
        }
    }
}
