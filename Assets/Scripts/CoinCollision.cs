using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    public GameManager manager;
    public AudioManager audioManager;
    public int extraTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            int rand = (int) Random.Range(0.0f, 100.0f) % 2 + 1;
            audioManager.Play("SunCoin" + rand);
            manager.AddTime(extraTime);
        }
    }
}
