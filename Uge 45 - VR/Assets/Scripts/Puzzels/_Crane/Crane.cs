#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Crane : MonoBehaviour
{
    #region public DATA
    [HideInInspector]
    public float progressX = 0, progressY = 0, progressZ = 0;
    [Header("Required Input:")]
    public Transform xMin;
    public Transform xMax, yMin, yMax, zMin, zMax;
    public Transform xzPos;
    public Crane_Claw Claw;
    public string Keyword = "";
    public List<GameObject> ObjectsInRange = new List<GameObject>();
    #endregion

    #region private DATA
    float xDir, yDir, zDir;
    bool ClawActive = false;
    GameObject parentToReturnTo = null;
    GameObject ObjectInClaw = null;
    #endregion

    void Start()
    {
        Claw.Keyword = Keyword;
        Claw.Crane = this;

        xDir = xMax.position.x - xMin.position.x;
        yDir = yMax.position.y - yMin.position.y;
        zDir = zMax.position.z - zMin.position.z;

        xzPos.position = Vector3.Lerp(xzPos.position, new Vector3(xMin.position.x + xDir * (progressX / 100), xMin.position.y, zMin.position.z + zDir * (progressZ / 100)), 0.5f);
        Claw.transform.position = Vector3.Lerp(Claw.transform.position, new Vector3(xzPos.position.x, yMin.position.y + yDir * (progressY / 100), xzPos.position.z), 0.5f);
        yMax.parent.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        MoveCrane();
    }

    public void MoveCrane()
    {
        if (xzPos.position.x < xMin.position.x)
        {
            xzPos.position = new Vector3(xMin.position.x, xzPos.position.y, xzPos.position.z);
        }
        else if (xzPos.position.x > xMax.position.x)
        {
            xzPos.position = new Vector3(xMax.position.x, xzPos.position.y, xzPos.position.z);
        }

        if (xzPos.position.z < zMin.position.z)
        {
            xzPos.position = new Vector3(xMin.position.x, xzPos.position.y, xzPos.position.z);
        }
        else if (xzPos.position.z > xMax.position.z)
        {
            xzPos.position = new Vector3(xMax.position.x, xzPos.position.y, xzPos.position.z);
        }

        xzPos.position = Vector3.Lerp(xzPos.position, new Vector3(xMin.position.x + xDir * (progressX / 100), xMin.position.y, zMin.position.z + zDir * (progressZ / 100)), 0.5f);
        Claw.transform.position = Vector3.Lerp(Claw.transform.position, new Vector3(xzPos.position.x, yMin.position.y + yDir * (progressY / 100), xzPos.position.z), 0.5f);

        zMax.parent.localPosition = new Vector3(xzPos.localPosition.x, 0, 0);
        xMax.parent.localPosition = new Vector3(0, 0, xzPos.localPosition.z);
    }

    public void LockClaw()
    {
        if (ObjectsInRange.Count > 0 && ClawActive == false)
        {
            //Set a standard to compare with other objects if any.
            GameObject closestObject = ObjectsInRange[0];
            float shortestDistance = Vector3.Distance(transform.position, closestObject.transform.position);

            for (int i = 1; i < ObjectsInRange.Count; i++)  //Running through the list.
            {
                if (shortestDistance > Vector3.Distance(transform.position, ObjectsInRange[i].transform.position))  //If the distance is less then the previus distance then the new closest object is the current object.
                {
                    closestObject = ObjectsInRange[i];  //New closest object from index.
                }
            }
            parentToReturnTo = closestObject.transform.parent.gameObject;
            closestObject.transform.parent = Claw.transform;
            ObjectInClaw = closestObject;
            ClawActive = true;

            if (ObjectInClaw.GetComponent<Rigidbody>() != null)
            {
                Rigidbody r = ObjectInClaw.GetComponent<Rigidbody>();
                r.useGravity = false;
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
            }
        }
    }

    public void ReleaseClaw()
    {
        if (ClawActive == true)
        {
            ObjectInClaw.transform.parent = parentToReturnTo.transform;

            if (ObjectInClaw.GetComponent<Rigidbody>() != null)
            {
                Rigidbody r = ObjectInClaw.GetComponent<Rigidbody>();
                r.useGravity = true;
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
            }
        }
    }
}
