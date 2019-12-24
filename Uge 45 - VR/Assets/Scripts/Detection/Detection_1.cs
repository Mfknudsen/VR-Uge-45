using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_1 : MonoBehaviour
{
    public Puzzel_1 P;
    public GameObject Teleportpoint;
    public bool t;

    void Update()
    {
        if (t)
        {
            P.D_Complete.active = false;
            P.D_Complete.SwitchOpenClosed();
            Teleportpoint.SetActive(false);
            t = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            P.D_Complete.active = false;
            P.D_Complete.SwitchOpenClosed();
            Teleportpoint.SetActive(false);
        }
    }
}
