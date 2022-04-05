using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObject : MonoBehaviour
{
    public int Points = 1;
    public OnChangePosition HoleScript;

    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(other.gameObject);
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Obstacle obs = other.gameObject.GetComponent<Obstacle>();
            if (obs.DontEat)
            {
                HoleScript.Defeat();
                return;
            }else if (obs.Objectif)
            {
                HoleScript.Win();
            }
            CalculateProgress(obs);
        }
            
    }

    public void CalculateProgress(Obstacle obstacle)
    {
        Points += obstacle.Weight;
        
        StartCoroutine(HoleScript.ScaleHole(Points));
    }
}
