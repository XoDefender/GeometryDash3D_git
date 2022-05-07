using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    public RaycastHit hitData1, hitData2, hitData3, hitData4;

    public bool isOnGround;
    public bool[] groundCheck = new bool[4];

    private void Start()
    {
        Debug.Log(groundCheck.Length);
    }
    // Update is called once per frame
    void Update()
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
        Debug.DrawRay(fourthRayPosition, Vector3.right * 1.6f, Color.black);

        groundCheck[0] = Physics.Raycast(firstRayPosition, Vector3.down, out hitData1, 1.6f, layerMask);
        groundCheck[1] = Physics.Raycast(secondRayPosition, Vector3.down, out hitData2, 1.6f, layerMask);
        groundCheck[2] = Physics.Raycast(thirdRayPosition, Vector3.down, out hitData3, 2f, layerMask);
        groundCheck[3] = Physics.Raycast(fourthRayPosition, Vector3.right, out hitData4, 1.6f, layerMask);
    }
}
