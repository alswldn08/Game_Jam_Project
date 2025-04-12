using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowPlayer : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player").transform;
        transform.position = new Vector3(player.position.x,player.position.y +3,-10);
    }
}
