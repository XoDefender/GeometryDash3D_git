using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    private GameObject player;
    private CollisionDetection collisionDetection;

    private const string playerName = "PlayerBody";

    private float coinCount = 0;

    private void Awake()
    {
        player = GameObject.Find(playerName);
        collisionDetection = player.GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collisionDetection.hitData5.collider != null)
        {
            coinCount += 1;
            Destroy(collisionDetection.hitData5.transform.gameObject, 0.5f);
        }
    }
}
