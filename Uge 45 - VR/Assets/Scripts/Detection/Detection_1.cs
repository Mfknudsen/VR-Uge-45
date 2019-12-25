using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_1 : MonoBehaviour
{
    public Puzzel_1 Puzzel;
    public GameObject Teleportpoint;
    public Bookshelf_1 Bookshelf;
    public bool trigger;

    void Update()
    {
        if (trigger)
        {
            Puzzel.D_Complete.active = false;
            Puzzel.D_Complete.SwitchOpenClosed();
            Teleportpoint.SetActive(false);
            Bookshelf.active = true;
            trigger = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Puzzel.D_Complete.active = false;
            Puzzel.D_Complete.SwitchOpenClosed();
            Teleportpoint.SetActive(false);
            Bookshelf.active = true;
        }
    }
}
