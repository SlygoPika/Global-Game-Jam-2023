using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMovement : MonoBehaviour
{
    public float turnPeriod;
    public float scalePeriod;
    public float scaleVariance;

    float elapsedTime = 0.0f;
    float initialScale;

    private void Start()
    {
        initialScale = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if (turnPeriod == 0.0f || scalePeriod == 0.0f)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        float angle = Mathf.Sin(elapsedTime * 2 * Mathf.PI / turnPeriod) * 360;

        float scale = initialScale + Mathf.Sin(elapsedTime * 2 * Mathf.PI / scalePeriod) * scaleVariance;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.localScale = new Vector3(scale, scale, 1);
    }
}
