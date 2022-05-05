using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private float gravity = 15f;
    [SerializeField] private float maxJumpHeight = 10f;
    [SerializeField] private LayerMask layerMask;

    private GameObject parent;

    private bool inJump = false;
    private bool isFalling = false;
    private bool isOnGround;
    private bool[] groundCheck = new bool[4];

    private Vector3 preJumpedPosition;

    float timeElapsed;
    float lerpDuration = 1f;

    Quaternion targetRotation = Quaternion.identity;

    RaycastHit hitData1, hitData2, hitData3, hitData4;

    private void Awake()
    {
        parent = transform.parent.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GroundChecking();
        HandleJumping();

        parent.transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
    }

    private void HandleJumping()
    {
        if (groundCheck[0] || groundCheck[1])
        {
            isOnGround = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            isOnGround = false;
        }

        if (isOnGround)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                inJump = true;
                preJumpedPosition = parent.transform.position;

                targetRotation = Quaternion.Euler(transform.eulerAngles + Vector3.forward * 180.1f);
            }

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

        if (inJump)
        {
            if (timeElapsed < lerpDuration)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (timeElapsed / lerpDuration) * 0.05f);

                timeElapsed += Time.deltaTime;
            }

            if (parent.transform.position.y <= preJumpedPosition.y + maxJumpHeight && !isFalling)
            {
                parent.transform.Translate(new Vector3(0, jumpSpeed * Time.deltaTime, 0));
            }
            else
            {
                isFalling = true;

                parent.transform.Translate(new Vector3(0, -gravity * Time.deltaTime, 0));
                
                if (isOnGround)
                {
                    inJump = false;
                    isFalling = false;

                    timeElapsed = 0;
                }
            }
        }
        else if(!inJump && !isOnGround)
        {
            parent.transform.Translate(new Vector3(0, -gravity * Time.deltaTime, 0));
        }
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
        Debug.DrawRay(fourthRayPosition, Vector3.right * 1.6f, Color.black);

        groundCheck[0] = Physics.Raycast(firstRayPosition, Vector3.down, out hitData1, 1.6f, layerMask);
        groundCheck[1] = Physics.Raycast(secondRayPosition, Vector3.down, out hitData2, 1.6f, layerMask);
        groundCheck[2] = Physics.Raycast(thirdRayPosition, Vector3.down, out hitData3, 2f, layerMask);
        groundCheck[3] = Physics.Raycast(fourthRayPosition, Vector3.right, out hitData4, 1.6f, layerMask);
    }
}