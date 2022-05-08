using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private float gravity = 15f;
    [SerializeField] private float maxJumpHeight = 10f;

    private GameObject parent;
    private CollisionDetection collisionDetection;

    private bool inJump = false;
    private bool isFalling = false;

    private Vector3 preJumpedPosition;

    float timeElapsed;
    float lerpDuration = 1f;

    Quaternion targetRotation = Quaternion.identity;

    private void Awake()
    {
        parent = transform.parent.gameObject;
        collisionDetection = GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJumping();

        parent.transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
    }

    private void HandleJumping()
    {
        if (collisionDetection.groundCheck[0] || collisionDetection.groundCheck[1])
        {
            collisionDetection.isOnGround = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            collisionDetection.isOnGround = false;
        }

        if (collisionDetection.isOnGround)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                inJump = true;
                preJumpedPosition = parent.transform.position;

                targetRotation = Quaternion.Euler(transform.eulerAngles + Vector3.forward * -90.1f);
            }
        }

        if (inJump)
        {
            if (timeElapsed < lerpDuration)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (timeElapsed / lerpDuration) * 0.15f);

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
                
                if (collisionDetection.isOnGround)
                {
                    inJump = false;
                    isFalling = false;

                    timeElapsed = 0;
                }
            }
        }
        else if(!inJump && !collisionDetection.isOnGround)
        {
            parent.transform.Translate(new Vector3(0, -gravity * Time.deltaTime, 0));
        }
    }
}