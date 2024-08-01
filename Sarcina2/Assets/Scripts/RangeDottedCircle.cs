using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDottedCircle : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Vector3 offset;
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
