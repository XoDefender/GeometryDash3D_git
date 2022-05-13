using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    [SerializeField] private float fallVelocityValue = 1.5f;
    [SerializeField] private float riseVelocityValue = 1.1f;

    private float fallVelocity;
    private float riseVelocity;

    private bool isGoingUp = false;

    // Start is called before the first frame update
    void Start()
    {
        fallVelocity = fallVelocityValue;
        riseVelocity = riseVelocityValue;
    }

    // Update is called once per frame
    void Update()
    {
        float finalVelocity;

        if (Input.GetKey(KeyCode.Space))
            isGoingUp = true;
        else
            isGoingUp = false;

        if(isGoingUp)
        {
            transform.Rotate(-0.4f, 0, 0);

            fallVelocity = fallVelocityValue;

            riseVelocity += 0.02f;
            finalVelocity = riseVelocity;
        }
        else
        {
            transform.Rotate(0.4f, 0, 0);

            riseVelocity = riseVelocityValue;

            fallVelocity += 0.02f;
            finalVelocity = fallVelocity;
        }

        transform.Translate(new Vector3(0, 0.02f * finalVelocity, 0), Space.Self);

        Debug.Log(isGoingUp);
    }
}
