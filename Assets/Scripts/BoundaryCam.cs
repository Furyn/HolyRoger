using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCam : MonoBehaviour
{
    public LayerMask mask;

    public Vector3 down_left;
    public Vector3 down_right;
    public Vector3 up_left;
    public Vector3 up_right;

    void Start()
    {
        Camera camera = Camera.main;
        float width = Screen.width;
        float height = Screen.height;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(new Vector3(0, 0, camera.nearClipPlane));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            down_left = hit.point;
            //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow, 50f);
        }

        ray = camera.ScreenPointToRay(new Vector3(0, height, camera.nearClipPlane));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            up_left = hit.point;
            //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow, 50f);
        }

        ray = camera.ScreenPointToRay(new Vector3(width, 0, camera.nearClipPlane));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            down_right = hit.point;
            //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow, 50f);
        }

        ray = camera.ScreenPointToRay(new Vector3(width, height, camera.nearClipPlane));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            up_right = hit.point;
            //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow, 50f);
        }
    }
}
