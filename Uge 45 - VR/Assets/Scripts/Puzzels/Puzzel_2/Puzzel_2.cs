#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Puzzel_2 : MonoBehaviour
{
    #region public DATA
    [HideInInspector]
    public bool puzzelIsFinished = false;
    [Header("Required Input:")]
    public List<GameObject> Plates;
    public GameObject Door;
    #endregion

    #region private DATA
    Vector3 doorTargetPosition, doorRestPosition, doorOpenPosition;
    #endregion

    void Start()
    {
        doorRestPosition = Door.transform.position;
        doorOpenPosition = Door.transform.position + new Vector3(0, 2, 0);
        doorTargetPosition = doorRestPosition;
    }

    void Update()
    {
        MoveDoor();
    }

    public void Check()
    {
        int succesCount = 0;
        for (int i = 0; i < Plates.Count; i++)
        {
            if (Plates[i].GetComponent<PressurePlate>().isDown == true)
            {
                succesCount += 1;
            }
            else
            {
                break;
            }
        }

        if (succesCount == Plates.Count)
        {
            puzzelIsFinished = true;
            StartCoroutine(SwitchTargetPosition(doorOpenPosition));
        }

        if (succesCount < Plates.Count)
        {
            puzzelIsFinished = false;
            StartCoroutine(SwitchTargetPosition(doorRestPosition));
        }
    }

    void MoveDoor()
    {
        if (Door.transform.position != doorTargetPosition)
        {
            Door.transform.position = Vector3.Lerp(Door.transform.position, doorTargetPosition, 0.005f);
        }
    }

    IEnumerator SwitchTargetPosition(Vector3 pos)
    {
        yield return new WaitForSeconds(0.5f);
        doorTargetPosition = pos;
    }
}
