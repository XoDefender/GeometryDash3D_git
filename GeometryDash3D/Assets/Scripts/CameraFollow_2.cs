using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_2 : MonoBehaviour
{
    private GameObject player;

    private const string playerName = "Player";

    private void Awake()
    {
        player = GameObject.Find(playerName);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
    }
}
