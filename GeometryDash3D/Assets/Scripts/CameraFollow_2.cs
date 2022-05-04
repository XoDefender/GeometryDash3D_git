using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_2 : MonoBehaviour
{
    private GameObject player;
    private const string playerName = "Player";

    private float offsetX = -2.912773f;
    private float offsetY = 6.316955f;
    private float offsetZ = -22.65969f;

    private float followUpOffstet = 7f;

    private float ascentStep = 5f;
    private float descentStep = 5f;

    private bool readyToMoveUp = false;
    private bool readyToMoveDown = false;

    private float currentTargetPositionY;
    private float previousTargetPositionY;
    private float currentPositionY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(playerName);

        currentTargetPositionY = offsetY;
        previousTargetPositionY = offsetY;
        currentPositionY = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y >= currentPositionY + followUpOffstet)
        {
            readyToMoveUp = true;
            currentPositionY += followUpOffstet;
        }
        else if(player.transform.position.y <= currentPositionY - followUpOffstet)
        {
            readyToMoveDown = true;
            currentPositionY -= followUpOffstet;
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + offsetX, currentTargetPositionY, player.transform.position.z + offsetZ);
        }

        if(readyToMoveUp)
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
