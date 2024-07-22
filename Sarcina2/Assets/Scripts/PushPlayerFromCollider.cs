using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayerFromCollider : MonoBehaviour
{
    private GameObject player;
    bool hasExited = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
        Invoke("CancelPushFromCollider", 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExited)
        {
            Vector3 pushDirection = collision.transform.position - transform.position;
            pushDirection.y = 0;
            pushDirection.Normalize();

            player.transform.position += pushDirection;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        hasExited = true;
    }


    private void CancelPushFromCollider()
    {
        hasExited = true;
    }


}
