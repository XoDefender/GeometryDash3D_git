using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    private GameObject player;
    private CollisionDetection collisionDetection;
    private GameObject coin;

    private const string playerName = "PlayerBody";

    private float groundOffset;
    private float coinCollectPosition;

    private bool isCollected = false;

    private void Awake()
    {
        player = GameObject.Find(playerName);
        collisionDetection = player.GetComponent<CollisionDetection>();
    }

    private void Start()
    {
        groundOffset = transform.position.y - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionDetection.hitData5.collider != null)
        {
            coin = collisionDetection.hitData5.transform.gameObject;
            coin.GetComponent<CoinAnimation>().isCollected = true;

            coinCollectPosition = coin.transform.position.y + 4f;
        }

        if(!isCollected)
        {
            float posX = transform.position.x;
            float posY = Mathf.Sin(Time.time) * 1.5f;
            float posZ = transform.position.z;

            if (posY < 0)
                posY *= -1;

            transform.position = new Vector3(posX, posY + groundOffset, posZ);
        }
        else
        {
            if (coin.transform.position.y <= coinCollectPosition)
            {
                coin.transform.Translate(new Vector3(0, 0.1f, 0), Space.World);
            }
        }

        transform.Rotate(0, 0.5f, 0, Space.World);
    }
}
