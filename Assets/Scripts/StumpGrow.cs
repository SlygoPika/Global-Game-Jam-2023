using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StumpGrow : MonoBehaviour
{

    Vector2 mousePosScreen;
    Vector2 mousePosWorld;

    public GameObject branchSprite;
    public Camera cam;

    private void OnEnable()
    {
        float height = cam.orthographicSize * 2;
        float width = cam.aspect * height;
        Debug.Log(height);
        Debug.Log(width);
    }

    private void OnMouseDrag()
    {
        

        mousePosScreen = new Vector2((Input.mousePosition.x - Screen.width / 2) / Screen.width,
            (Input.mousePosition.y - Screen.height / 2) / Screen.height);

        mousePosWorld = new Vector2(mousePosScreen.x + cam.transform.position.x,
            mousePosScreen.y + cam.transform.position.y);

        Debug.Log(mousePosWorld);
    }
}
