using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_Handel : MonoBehaviour
{
    public Book B;
    Transform Parent;
    public Transform T;
    bool isActive = false;
    Vector3 localStart;

    void Start()
    {
        localStart = transform.localPosition;
        Parent = transform.parent;
    }

    void Update()
    {

    }

    void OnDetachedFromHand()
    {
        B.HandelsActive -= 1;
        transform.parent = Parent;

        transform.localPosition = localStart;
    }

    void OnAttachedToHand()
    {
        B.HandelsActive += 1;
        isActive = true;
    }
}
