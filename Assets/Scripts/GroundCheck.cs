using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public MovePlayer player;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Stump")
        {
            if (player.GetVelocityY() < 0.1f)
            {
                player.SetCanJump(true);
            }
            
        }

    }
}
