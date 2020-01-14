using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_move : MonoBehaviour
{

    public GameObject Floor;
    bool accept = false;
    Vector3 moveBack;

    void Start()
    {
        moveBack = Floor.transform.position;
    }

    void Update()
    {
        if (accept)
        {
            Floor.transform.position = Vector3.Lerp(Floor.transform.position, moveBack + new Vector3(3, 0, 0), 0.5f);
        }
        else
        {
            Floor.transform.position = Vector3.Lerp(Floor.transform.position, moveBack, 0.5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            accept = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            accept = false;
        }
    }
}
