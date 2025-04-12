using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.PlayerCheck(true);
        }
        else
        {
            enemy.PlayerCheck(false);
        }
    }
}
