using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf_1 : MonoBehaviour
{
    public bool active = false;
    public Transform Open;
    Vector3 Closed;
    bool Done = false;

    void Start()
    {
        Closed = transform.position;

        transform.position = Open.position;
    }

    void Update()
    {
        if (active == true && transform.position != Closed)
        {
            if (transform.position.x < Closed.x)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(Closed.x, transform.position.y, transform.position.z), 0.5f * Time.deltaTime);

                if (transform.position.x >= Closed.x - 0.1f)
                {
                    transform.position = new Vector3(Closed.x, transform.position.y, transform.position.z);
                    StartCoroutine(Delay());
                }
            }

            if (transform.position.x == Closed.x && transform.position != Closed && Done == true)
            {
                transform.position = Vector3.Lerp(transform.position, Closed, 0.5f * Time.deltaTime);
                if (transform.position.z > Closed.z - 0.05f)
                {
                    transform.position = Closed;
                    active = false;
                    Destroy(this, 5);
                }
            }
        }
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Done is now true");
        Done = true;
    }
}
