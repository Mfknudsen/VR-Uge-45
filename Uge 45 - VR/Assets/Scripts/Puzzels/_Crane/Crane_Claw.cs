#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Crane_Claw : MonoBehaviour
{
    #region public DATA
    public string Keyword = "";
    public Crane Crane;
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == Keyword)
        {
            Crane.ObjectsInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == Keyword)
        {
            Crane.ObjectsInRange.Remove(other.gameObject);
        }
    }
}
