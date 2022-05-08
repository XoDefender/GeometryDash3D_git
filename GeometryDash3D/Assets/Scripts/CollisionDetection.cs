using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private LayerMask basicLayerMask, coinLayerMask;

    public RaycastHit hitData1, hitData2, hitData3, hitData4, hitData5;

    public bool isOnGround;
    public bool[] groundCheck = new bool[5];

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
        fourthRayPosition.y -= 1f;

        Vector3 coinRayPosition = transform.position;

        Debug.DrawRay(firstRayPosition, Vector3.down * 1.6f, Color.green);
        Debug.DrawRay(secondRayPosition, Vector3.down * 1.6f, Color.red);
        Debug.DrawRay(thirdRayPosition, Vector3.down * 2f, Color.blue);
        Debug.DrawRay(fourthRayPosition, Vector3.right * 1.6f, Color.black);
        Debug.DrawRay(coinRayPosition, Vector3.right * 1.6f, Color.grey);

        groundCheck[0] = Physics.Raycast(firstRayPosition, Vector3.down, out hitData1, 1.6f, basicLayerMask);
        groundCheck[1] = Physics.Raycast(secondRayPosition, Vector3.down, out hitData2, 1.6f, basicLayerMask);
        groundCheck[2] = Physics.Raycast(thirdRayPosition, Vector3.down, out hitData3, 2f, basicLayerMask);
        groundCheck[3] = Physics.Raycast(fourthRayPosition, Vector3.right, out hitData4, 1.6f, basicLayerMask);
        groundCheck[4] = Physics.Raycast(coinRayPosition, Vector3.right, out hitData5, 1.8f, coinLayerMask);
    }
}
