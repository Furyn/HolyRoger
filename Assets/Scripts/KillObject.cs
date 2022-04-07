using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObject : MonoBehaviour
{
    public int Points = 1;
    public OnChangePosition HoleScript;
    public AudioSource source = null;
    public AudioSource sourceCOIN = null;
    public float pitch = 1f;
    public GameObject panelStart = null;

    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(other.gameObject);
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Obstacle obs = other.gameObject.GetComponent<Obstacle>();
            if (obs.notSound == false)
            {
                source.pitch = pitch;
                AudioManager.Instance.PlaySound(source, "HOLE_EAT");
                panelStart.SetActive(false);
            }
            
            if (obs.DontEat)
            {
                AudioManager.Instance.PlaySound(sourceCOIN, "PIRATE_DEAD");
                HoleScript.Defeat();
                return;
            }else if (obs.Objectif)
            {
                HoleScript.Win();
            }
            if (obs.Weight > 0)
            {
                CalculateProgress(obs);
            }
            else
            {
                AudioManager.Instance.PlaySound(sourceCOIN, "COIN");
            }
        }
            
    }

    public void CalculateProgress(Obstacle obstacle)
    {
        Points += obstacle.Weight;
        
        StartCoroutine(HoleScript.ScaleHole(Points));
    }
}
