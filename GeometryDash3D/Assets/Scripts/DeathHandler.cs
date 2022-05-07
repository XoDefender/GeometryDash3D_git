using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private bool[] groundCheck = new bool[4];
    RaycastHit hitData1, hitData2, hitData3, hitData4;

    private GameObject parent;

    private void Awake()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GroundChecking();
        DeathDetection();
    }

    private void GroundChecking()
    {
        Vector3 firstRayPosition = transform.position;
        firstRayPosition.x += 1.5f;

        Vector3 secondRayPosition = transform.position;
        secondRayPosition.x -= 1.5f;

        Vector3 thirdRayPosition = transform.position;

        Vector3 fourthRayPosition = transform.position;
        fourthRayPosition.y -= 1.4f;

        Debug.DrawRay(firstRayPosition, Vector3.down * 1.6f, Color.green);
        Debug.DrawRay(secondRayPosition, Vector3.down * 1.6f, Color.red);
        Debug.DrawRay(thirdRayPosition, Vector3.down * 2f, Color.blue);
        Debug.DrawRay(fourthRayPosition, Vector3.right * 2f, Color.black);

        groundCheck[0] = Physics.Raycast(firstRayPosition, Vector3.down, out hitData1, 1.6f, layerMask);
        groundCheck[1] = Physics.Raycast(secondRayPosition, Vector3.down, out hitData2, 1.6f, layerMask);
        groundCheck[2] = Physics.Raycast(thirdRayPosition, Vector3.down, out hitData3, 2f, layerMask);
        groundCheck[3] = Physics.Raycast(fourthRayPosition, Vector3.right, out hitData4, 1.6f, layerMask);
    }

    private void DeathDetection()
    {
        if (hitData1.collider != null)
        {
            if (hitData1.transform.gameObject.TryGetComponent(out ObstacleDetection component))
            {
                Destroy(parent);
                Debug.Log("1");
            }
        }
        if (hitData2.collider != null)
        {
            if (hitData2.transform.gameObject.TryGetComponent(out ObstacleDetection component))
            {
                Destroy(parent);
                Debug.Log("2");
            }
        }
        if (hitData3.collider != null)
        {
            if (hitData3.transform.gameObject.TryGetComponent(out ObstacleDetection component))
            {
                Destroy(parent);
                Debug.Log("3");
            }
        }
        if (hitData4.collider != null)
        {
            Destroy(parent);
            Debug.Log("4");
        }
    }
}
