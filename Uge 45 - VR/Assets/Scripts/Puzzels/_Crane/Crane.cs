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
    public GameObject Claw;
    #endregion

    #region private DATA
    float xDir, yDir, zDir;
    List<GameObject> ObjectsInRange = new List<GameObject>();
    #endregion

    void Start()
    {
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

    }

    public void ReleaseClaw()
    {

    }

    void OnTriggerEnter()
    {

    }

    void OnTriggerExit()
    {

    }
}
