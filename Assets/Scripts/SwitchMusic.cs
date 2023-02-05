using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusic : MonoBehaviour
{
    public AudioManager audioManager;

    bool triggered;

    private void Awake()
    {
        triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !triggered)
        {
            SwitchTheme();
            triggered = true;
        }
    }

    private void SwitchTheme()
    {
        audioManager.PlayOnLoop("LightTheme");
        audioManager.Stop("DarkTheme");
    }
}
