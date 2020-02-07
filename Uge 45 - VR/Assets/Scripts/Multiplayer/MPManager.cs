#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class MPManager : MonoBehaviour
{
    #region public DATA
    public GameObject VR_UI;
    public GameObject Instructor_UI;
    public GameObject VR_Check;
    #endregion

    #region private DATA
    #endregion

    void Start(){
        StartCoroutine(CheckAfterDelay());
    }

    IEnumerator CheckAfterDelay(){
        yield return new WaitForSeconds(0.5f);

        if(VR_Check.active == true){
            VR_UI.SetActive(true);
            Instructor_UI.SetActive(false);
        } else {
            VR_UI.SetActive(false);
            Instructor_UI.SetActive(true);
        }
    }

}
