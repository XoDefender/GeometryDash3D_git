using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private GameObject parent;
    private CollisionDetection collisionDetection;

    private void Awake()
    {
        parent = transform.parent.gameObject;
        collisionDetection = GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionDetection.hitData1.collider != null)
        {
            if (collisionDetection.hitData1.transform.gameObject.TryGetComponent(out ObstacleDetection component))
            {
                Destroy(parent);
                Debug.Log("1");
            }
        }
        if (collisionDetection.hitData2.collider != null)
        {
            if (collisionDetection.hitData2.transform.gameObject.TryGetComponent(out ObstacleDetection component))
            {
                Destroy(parent);
                Debug.Log("2");
            }
        }
        if (collisionDetection.hitData3.collider != null)
        {
            if (collisionDetection.hitData3.transform.gameObject.TryGetComponent(out ObstacleDetection component))
            {
                Destroy(parent);
                Debug.Log("3");
            }
        }
        if (collisionDetection.hitData4.collider != null)
        {
            Destroy(parent);
            Debug.Log("4");
        }
    }
}
