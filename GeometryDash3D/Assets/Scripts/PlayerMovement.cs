using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private float gravity = 15f;
    [SerializeField] private float maxJumpHeight = 10f;
    
    private float groundOffset = 0.05f;

    private bool inJump = false;
    private bool isFalling = false;

    private Vector3 preJumpedPosition;

    private GameObject parent;

    float timeElapsed;
    float lerpDuration = 1f;

    Quaternion targetRotation = Quaternion.identity;

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
        HandleJumping();

        parent.transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
    }

    private void HandleJumping()
    {
        float distanceToSurface = transform.localScale.y / 2 + groundOffset;

        bool isOnGround = Physics.Raycast(transform.position, Vector3.down, distanceToSurface);

        Debug.DrawRay(transform.position, Vector3.down * distanceToSurface, Color.green);
        if (isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inJump = true;
                preJumpedPosition = parent.transform.position;

                targetRotation = Quaternion.Euler(transform.eulerAngles + Vector3.forward * 180.1f);
            }
        }

        if (inJump)
        {
            if (timeElapsed < lerpDuration)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (timeElapsed / lerpDuration) * 0.05f);

                timeElapsed += Time.deltaTime;

                Debug.Log(transform.eulerAngles.z);
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
    }
}
