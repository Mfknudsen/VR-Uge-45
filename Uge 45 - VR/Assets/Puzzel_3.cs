#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Puzzel_3 : MonoBehaviour
{
    #region public DATA
    [HideInInspector]
    public bool puzzelIsFinished = false;
    [Header("Required Input:")]
    public GameObject Door;
    public GameObject PressurePlate, Keyhole, Key;
    public string Keyword = "Puzzel_3";
    #endregion

    #region private DATA
    Vector3 doorTargetPosition, doorRestPosition, doorOpenPosition;
    bool succes = false;
    #endregion

    void Start()
    {
        doorRestPosition = Door.transform.position;
        doorOpenPosition = Door.transform.position + new Vector3(0, 2, 0);
        doorTargetPosition = doorRestPosition;

        Key.GetComponent<Keys>().keyword = Keyword;
        Keyhole.GetComponent<Keyholes>().keyword = Keyword;
    }

    void Update()
    {
        MoveDoor();
    }

    public void Check()
    {
        if (PressurePlate.GetComponent<PressurePlate2>().isDown == true && Keyhole.GetComponent<Keyholes>().currentKey == Key)
        {
            succes = true;
        }
        else if(PressurePlate.GetComponent<PressurePlate2>().isDown == false || Keyhole.GetComponent<Keyholes>().currentKey != Key)
        {
            succes = false;
        }

        if (succes == true && puzzelIsFinished == false)
        {
            puzzelIsFinished = true;
            StartCoroutine(SwitchTargetPosition(doorOpenPosition));
        }

        if (succes == false && puzzelIsFinished == true)
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
