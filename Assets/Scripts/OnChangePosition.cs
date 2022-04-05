using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnChangePosition : MonoBehaviour
{
    public PolygonCollider2D hole2DColider;
    public PolygonCollider2D ground2DColider;
    public MeshCollider GeneratedMeshColider;
    public Collider GroundColider;

    public float[] LevelUpSize = new float[6];
    public float TimeScaleHole = 0.4f;
    public float initialScale = 0.5f;
    public float Speed = 1f;

    [Header("BOUNDARY")]
    public float UpBoundary = 5f;
    public float DownBoundary = -5f;

    Mesh GenerateMesh;
    Vector3 lastMovePoint = Vector3.zero;
    Vector3 direction = Vector3.zero;
    BoundaryCam boundaryCam = null;
    int MyLevel = 1;
    float distance = 0f;

    public void StartMove(BaseEventData myEvent)
    {
        if (((PointerEventData)myEvent).pointerCurrentRaycast.isValid)
        {
            lastMovePoint = ((PointerEventData)myEvent).pointerCurrentRaycast.worldPosition;
        }
    }


    public void Move(BaseEventData myEvent)
    {
        if (((PointerEventData)myEvent).pointerCurrentRaycast.isValid)
        {
            distance = Vector3.Distance(((PointerEventData)myEvent).pointerCurrentRaycast.worldPosition, lastMovePoint);
            direction = (((PointerEventData)myEvent).pointerCurrentRaycast.worldPosition - lastMovePoint).normalized;
            lastMovePoint = ((PointerEventData)myEvent).pointerCurrentRaycast.worldPosition;
        }
        else{
            StopMovement();
        }
    }

    public void EndMove(BaseEventData myEvent)
    {
        if (((PointerEventData)myEvent).pointerCurrentRaycast.isValid)
        {
            StopMovement();
        }
    }

    public IEnumerator ScaleHole(int points)
    {
        if (points < LevelUpSize.Length || MyLevel < LevelUpSize.Length)
        {

            while (MyLevel < points && MyLevel < LevelUpSize.Length)
            {
                MyLevel++;
                Debug.Log("LEVEL : " + MyLevel);
                Vector3 StartScale = transform.localScale;
                Vector3 EndScale = new Vector3(LevelUpSize[MyLevel - 1], LevelUpSize[MyLevel - 1], LevelUpSize[MyLevel - 1]);

                float t = 0;
                while (t < 1)
                {
                    t += Time.deltaTime;
                    transform.localScale = Vector3.Lerp(StartScale, EndScale, t);
                    yield return null;
                }

            }
            
        }
    }

    public void Defeat()
    {
        GameManager.Instance.Lose();
    }

    public void Win()
    {
        GameManager.Instance.Win();
    }

    private void Start()
    {
        transform.localScale = transform.localScale * LevelUpSize[0];
        boundaryCam = Camera.main.GetComponent<BoundaryCam>();

        GameObject[] AllGOs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (var go in AllGOs)
        {
            if(go.layer == LayerMask.NameToLayer("Obstacles"))
            {
                Physics.IgnoreCollision(go.GetComponent<Collider>(), GeneratedMeshColider, true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(other, GroundColider, true);
        Physics.IgnoreCollision(other, GeneratedMeshColider, false);
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, GroundColider, false);
        Physics.IgnoreCollision(other, GeneratedMeshColider, true);
    }

    private void FixedUpdate()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            hole2DColider.transform.position = new Vector2(transform.position.x ,transform.position.z);
            hole2DColider.transform.localScale = transform.localScale * initialScale;
            MakeHole2D();
            Make3DMeshCollider();
        }
    }

    private void Update()
    {
        PerformeMovement();
    }

    private void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DColider.GetPath(0);

        for (int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2DColider.transform.TransformPoint(PointPositions[i]);
        }

        ground2DColider.pathCount = 2;
        ground2DColider.SetPath(1, PointPositions);

    }

    private void PerformeMovement()
    {
        if (direction != Vector3.zero)
        {
            transform.position += direction * distance;
            /*float z = Mathf.Clamp(transform.position.z, DownBoundary, UpBoundary);*/
            /*float z = Mathf.Clamp(transform.position.z, boundaryCam.up_left.z, boundaryCam.down_left.z);
            float x = Mathf.Clamp(transform.position.x, boundaryCam.up_left.x, boundaryCam.up_right.x);*/

            float z = transform.position.z;
            float x = transform.position.x;


            /*Debug.Log(boundaryCam.up_left.x);*/


            transform.position = new Vector3(x, transform.position.y , z);
            direction = Vector3.zero;
        }
        
    }

    private void Make3DMeshCollider()
    {
        if (GenerateMesh != null)
            Destroy(GenerateMesh);
        GenerateMesh = ground2DColider.CreateMesh(true,true);
        GeneratedMeshColider.sharedMesh = GenerateMesh;
    }

    private void StopMovement()
    {
        direction = Vector3.zero;
        lastMovePoint = Vector3.zero;
        distance = 0f;
    }
}
