using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private const string playerName = "PlayerTrigger";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(playerName);
    }

    // Update is called once per frame
    void Update()
    {
        float offsetX = -42f;
        float offsetY = 10f;
        float offsetZ = -50f;
        transform.position = new Vector3(player.transform.position.x + offsetX, offsetY, player.transform.position.z + offsetZ);
    }
}
