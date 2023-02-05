using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBobbing : MonoBehaviour
{
    public float bobbingPerSecond;
    public float bobbingDistance;

    private float bobbingTime = 0;
    private float originalY;

    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        bobbingTime += Time.deltaTime;

        if (bobbingTime * bobbingPerSecond > 1)
        {
            bobbingTime = 0;
        }

        this.transform.position = new Vector3(
            this.transform.position.x,
            Mathf.Sin(bobbingTime * bobbingPerSecond * 2 * Mathf.PI) * bobbingDistance + originalY,
            0.0f
        );

    }
}
