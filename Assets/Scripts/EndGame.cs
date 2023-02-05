using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager manager;

    public void OnFadeComplete()
    {
        manager.OnFadeComplete();
    }
}
