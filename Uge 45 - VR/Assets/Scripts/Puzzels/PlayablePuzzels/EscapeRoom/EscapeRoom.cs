#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
#endregion

public class EscapeRoom : Puzzel, IPunObservable
{
    #region public DATA
    public MPManager manager;
    public List<GameObject> Colors = new List<GameObject>();
    public List<Keyhole> Keyholes = new List<Keyhole>();
    public List<Key> Keys = new List<Key>();
    public Buttom Buttom;
    public GameObject Done;
    #endregion

    #region private DATA
    OSC OSC;
    PhotonView PhotonView;
    bool isPressed = false;
    List<string> names = new List<string>();
    bool Complete = false;
    #endregion

    void Start()
    {
        Done.SetActive(false);

        if (manager.Player == "VR")
        {
            OSC = GetComponent<OSC>();
            PhotonView = GetComponent<PhotonView>();

            for (int i = 0; i < Keyholes.Count; i++)
            {
                Keyholes[i].keyword = "E";
                GiveKeyKeyword(i);
            }

            for (int i = 0; i < Keys.Count; i++)
            {
                Keys[i].keyword = "E";
            }

            if (OSC != null)
            {
                SendOSC(0f);
            }
        }
        else if (manager.Player == null)
        {
            manager.GivePlayerClass();
        }
    }

    void Update()
    {
        if (manager.Player == "VR")
        {
            if (OSC == null)
            {
                OSC = GetComponent<OSC>();
                PhotonView = GetComponent<PhotonView>();

                for (int i = 0; i < Keyholes.Count; i++)
                {
                    Keyholes[i].keyword = "E";
                    GiveKeyKeyword(i);
                }

                for (int i = 0; i < Keys.Count; i++)
                {
                    Keys[i].keyword = "E";
                }

                if (OSC != null)
                {
                    SendOSC(0f);
                }
            }

            if (Buttom.active)
            {
                if (CheckAllKeysActive(Keyholes))
                {
                    Complete = true;
                }
                else
                {
                    resetOrder();
                }
            }
        }
        else if (manager.Player == "Instructor")
        {
            for (int i = 0; i < names.Count; i++)
            {
                string temp = names[i];

                for (int e = 0; e < Colors.Count; e++)
                {
                    if (Colors[e].name == temp)
                    {
                        Colors[e].SetActive(true);
                        break;
                    }
                }
            }
        }
        else if (manager.Player == null)
        {
            manager.GivePlayerClass();
        }

        if (Complete)
        {
            Done.SetActive(true);
            Debug.Log("You have escaped!");
            SendOSC(1f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Keyholes[0].Color + Keyholes[0].gameObject.name);
            stream.SendNext(Keyholes[1].Color + Keyholes[1].gameObject.name);
            stream.SendNext(Keyholes[2].Color + Keyholes[2].gameObject.name);
            stream.SendNext(Complete);
        }
        else if (stream.IsReading)
        {
            names = new List<string>();
            for (int i = 0; i < Colors.Count; i++)
            {
                Colors[i].SetActive(false);
            }

            names.Add((string)stream.ReceiveNext());
            names.Add((string)stream.ReceiveNext());
            names.Add((string)stream.ReceiveNext());
            Complete = (bool)stream.ReceiveNext();
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
            kh.Color = k.ColorName;
        }

        if (kh.correctKey == null)
        {
            GiveKeyKeyword(i);
        }
    }

    public void resetOrder()
    {
        Debug.Log("Reseting Keys");
        for (int i = 0; i < Colors.Count; i++)
        {
            Colors[i].SetActive(false);
        }

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

    void SendOSC(float i)
    {
        OscMessage msg = new OscMessage();
        msg.address = "message";
        msg.values.Add(i);
        OSC.Send(msg);
        Debug.Log("Send to OSC: " + i);
    }
}
