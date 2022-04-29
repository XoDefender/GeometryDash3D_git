using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private float gravity = 15f;
    [SerializeField] private float maxJumpHeight = 10f;

    private GameObject parent;

    private bool inJump = false;
    private bool isFalling = false;
    private bool isOnGround;

    private Vector3 preJumpedPosition;

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

    private void OnTriggerEnter(Collider other)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        isOnGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOnGround = false;
    }
}