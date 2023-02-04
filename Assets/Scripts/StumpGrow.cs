using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StumpGrow : MonoBehaviour
{

    Vector2 mousePosScreen;
    Vector2 mousePosWorld;
    Vector2 prevBranchPos;

    public GameObject branchSprite;
    public Camera cam;
    public float distanceBetweenBranchSprites;
    public float maxBranchLength;
    public GameManager manager;

    float totalBranchLength;

    float camHeight;
    float camWidth;

    int branchID;

    private void OnMouseDown()
    {
        branchID = manager.GetNewBranchID();

        int deleteBranch = manager.GetBranchIDToDelete();

        Debug.Log(branchID);
        Debug.Log(deleteBranch);

        if (deleteBranch != -1)
        {
            manager.DeleteBranch(deleteBranch);
        }

        

        totalBranchLength = 0.0f;

        camHeight = cam.orthographicSize * 2;
        camWidth = cam.aspect * camHeight;

        prevBranchPos = transform.position;
        GameObject firstBranchSprite = SpawnBranch(prevBranchPos);

        firstBranchSprite.GetComponent<Collider2D>().enabled = false;

        FindObjectOfType<AudioManager>().Play("StumpGrowStart");
    }

    private void OnMouseDrag()
    {

        mousePosWorld = GetMousePosInWorld();

        float distance = Vector2.Distance(prevBranchPos, mousePosWorld);

        if (distance > distanceBetweenBranchSprites
            && totalBranchLength < maxBranchLength)
        {
            float temp = distance;

            Vector2 growDirection = (mousePosWorld - prevBranchPos).normalized;

            while (temp > distanceBetweenBranchSprites)
            {
                SpawnBranch(prevBranchPos, prevBranchPos + growDirection * distanceBetweenBranchSprites);

                prevBranchPos += growDirection * distanceBetweenBranchSprites;

                temp -= distanceBetweenBranchSprites;
                totalBranchLength += distanceBetweenBranchSprites;
            }
        }
    }

    private Vector2 GetMousePosInWorld()
    {
        mousePosScreen = new Vector2((Input.mousePosition.x - Screen.width / 2) / Screen.width,
            (Input.mousePosition.y - Screen.height / 2) / Screen.height);

        return new Vector2(mousePosScreen.x * camWidth + cam.transform.position.x,
            mousePosScreen.y * camHeight + cam.transform.position.y);
    }

    private GameObject SpawnBranch(Vector2 pos)
    {
        GameObject branchPart = Instantiate(branchSprite, (Vector3)pos, Quaternion.identity);

        manager.AddBranchSprite(branchID, branchPart);

        return branchPart;
    }

    private void SpawnBranch(Vector2 prevBranchPos, Vector2 currentBranchPos)
    {
        Vector2 growDirection = currentBranchPos - prevBranchPos;
        float angle = Mathf.Atan2(growDirection.y, growDirection.x) / (2 * Mathf.PI) * 360;

        Quaternion orientation = Quaternion.Euler(0, 0, angle);

        float scale = 1.0f - totalBranchLength / maxBranchLength;

        if (scale > 0.05f)
        {
            GameObject branchPart = Instantiate(branchSprite, (Vector3)currentBranchPos, orientation);

            branchPart.transform.localScale = new Vector3(1, scale, 1);

            manager.AddBranchSprite(branchID, branchPart);
        }
        
    }
}
