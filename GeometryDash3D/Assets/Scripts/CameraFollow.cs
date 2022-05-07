using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private const string playerName = "PlayerTrigger";

    private float offsetX = -42f;
    private float offsetY = 10f;
    private float offsetZ = -50f;

    private float followUpOffstet = 10f;

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
        if(player)
        {
            if (player.transform.position.y >= currentPositionY + followUpOffstet)
            {
                readyToMoveUp = true;
                currentPositionY += followUpOffstet;

                Debug.Log("Ascent = " + currentTargetPositionY);
            }
            else if (player.transform.position.y <= currentPositionY - followUpOffstet)
            {
                readyToMoveDown = true;
                currentPositionY -= followUpOffstet;

                Debug.Log("Descent = " + currentTargetPositionY);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x + offsetX, currentTargetPositionY, player.transform.position.z + offsetZ);

                Debug.Log("Out");
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
