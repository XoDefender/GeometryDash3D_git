using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private const string playerName = "PlayerBody";

    private float offsetX = -1f;
    private float offsetY = 8f;
    private float offsetZ = -25f;

    private float followUpOffstet = 10f;

    private float ascentStep = 5f;
    private float descentStep = 5f;

    private bool readyToMoveUp = false;
    private bool readyToMoveDown = false;

    private float currentTargetPositionY;
    private float previousTargetPositionY;
    private float currentPositionY;

    private void Awake()
    {
        player = GameObject.Find(playerName);
    }

    // Start is called before the first frame update
    void Start()
    { 
        currentTargetPositionY = offsetY;
        previousTargetPositionY = offsetY;
        currentPositionY = player.transform.position.y;

        transform.rotation = Quaternion.Euler(0, 18f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            if (player.transform.position.y >= currentPositionY + followUpOffstet)
            {
                readyToMoveUp = true;
                currentPositionY += followUpOffstet;
            }
            else if (player.transform.position.y <= currentPositionY - followUpOffstet)
            {
                readyToMoveDown = true;
                currentPositionY -= followUpOffstet;
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x + offsetX, currentTargetPositionY, player.transform.position.z + offsetZ);
            }

            if (readyToMoveUp)
            {
                if (currentTargetPositionY < previousTargetPositionY + ascentStep)
                {
                    currentTargetPositionY += 0.1f;
                }
                else
                {
                    readyToMoveUp = false;
                    previousTargetPositionY = currentTargetPositionY;
                }
            }

            if (readyToMoveDown)
            {
                if (currentTargetPositionY > previousTargetPositionY - descentStep)
                {
                    currentTargetPositionY -= 0.1f;
                }
                else
                {
                    readyToMoveDown = false;
                    previousTargetPositionY = currentTargetPositionY;
                }
            }
        }
    }
}
