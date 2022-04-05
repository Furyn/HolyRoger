using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HERE");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("HERE");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HERE");
    }
}
