using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObject : MonoBehaviour
{
    public int Points = 0;
    public OnChangePosition HoleScript;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        CalculateProgress();
    }

    public void CalculateProgress()
    {
        Points++;
        StartCoroutine(HoleScript.ScaleHole());
    }
}
