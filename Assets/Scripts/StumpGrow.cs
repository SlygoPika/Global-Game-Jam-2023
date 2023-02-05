using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StumpGrow : MonoBehaviour
{

    Vector2 mousePosScreen;
    Vector2 mousePosWorld;
    Vector2 prevBranchPos;
    Vector2 prevMousePosWorld;
    float prevBranchAngle;

    public GameObject branchSprite;
    public Camera cam;
    public float distanceBetweenBranchSprites;
    public float maxBranchLength;
    public float maxAngleBetweenBranches;
    public bool isLeft;
    public float initialScale;
    public GameManager manager;

    float totalBranchLength;
    float tempMaxAngle;

    float camHeight;
    float camWidth;


    int spriteNum;
    int branchID;

    private void OnMouseDown()
    {
        tempMaxAngle = maxAngleBetweenBranches;

        spriteNum = 0;
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

        prevBranchPos = GetMousePosInWorld();
        prevMousePosWorld = GetMousePosInWorld();
        GameObject firstBranchSprite = SpawnBranch(prevBranchPos);

        firstBranchSprite.GetComponent<Collider2D>().enabled = false;

        FindObjectOfType<AudioManager>().Play("StumpGrowStart");
    }

    private void OnMouseDrag()
    {

        mousePosWorld = GetMousePosInWorld();

        float distance = Vector2.Distance(prevMousePosWorld, mousePosWorld);

        if (distance > distanceBetweenBranchSprites
            && totalBranchLength < maxBranchLength)
        {
            float temp = distance;

            Vector2 growDirection = GetGrowDirection(); ;

            while (temp > distanceBetweenBranchSprites)
            {
                SpawnBranch(prevBranchPos, growDirection);

                prevBranchPos += growDirection;

                temp -= distanceBetweenBranchSprites;
                totalBranchLength += distanceBetweenBranchSprites;
            }
            prevMousePosWorld = mousePosWorld;
        }
        
    }

    private Vector2 GetMousePosInWorld()
    {
        mousePosScreen = new Vector2((Input.mousePosition.x - Screen.width / 2) / Screen.width,
            (Input.mousePosition.y - Screen.height / 2) / Screen.height);

        return new Vector2(mousePosScreen.x * camWidth + cam.transform.position.x,
            mousePosScreen.y * camHeight + cam.transform.position.y);
    }

    private Vector2 GetGrowDirection()
    {
        Vector2 growDirection = (GetMousePosInWorld() - prevBranchPos).normalized * distanceBetweenBranchSprites;

        float angle = Mathf.Atan2(growDirection.y, growDirection.x) / (2 * Mathf.PI) * 360;
        //angle = AdjustAngle(angle);


        if (AdjustAngle(prevBranchAngle - angle) > tempMaxAngle)
        {
            angle = prevBranchAngle - tempMaxAngle;
            growDirection = new Vector2(Mathf.Cos(angle / 360 * 2 * (Mathf.PI)) * distanceBetweenBranchSprites,
                Mathf.Sin(angle / 360 * 2 * (Mathf.PI)) * distanceBetweenBranchSprites);
        }
        else if (AdjustAngle(angle - prevBranchAngle) > tempMaxAngle)
        {
            angle = prevBranchAngle + tempMaxAngle;
            growDirection = new Vector2(Mathf.Cos(angle / 360 * 2 * (Mathf.PI)) * distanceBetweenBranchSprites,
                Mathf.Sin(angle / 360 * 2 * (Mathf.PI)) * distanceBetweenBranchSprites);
        }
        Debug.Log(growDirection.normalized);


        return growDirection;
    }
    private GameObject SpawnBranch(Vector2 pos)
    {
        GameObject branchPart = Instantiate(branchSprite, (Vector3)pos, Quaternion.Euler(0,0,-180.0f));
        branchPart.transform.localScale = new Vector3(1, initialScale, 1);
        SpriteRenderer[] arr = branchPart.GetComponentsInChildren<SpriteRenderer>();

        if (isLeft)
        {
            prevBranchAngle = -branchPart.transform.rotation.eulerAngles.z;
        }
        Debug.Log(prevBranchAngle);

        foreach (SpriteRenderer s in arr)
        {
            s.sortingOrder = spriteNum;
        }

        spriteNum++;

        manager.AddBranchSprite(branchID, branchPart);

        return branchPart;
    }

    private void SpawnBranch(Vector2 prevBranchPos, Vector2 growDirection)
    {
        float angle = Mathf.Atan2(growDirection.y, growDirection.x) / (2 * Mathf.PI) * 360;

        prevBranchAngle= angle;

        Quaternion orientation = Quaternion.Euler(0, 0, angle);

        float scale = (1.0f - totalBranchLength / maxBranchLength) * initialScale;

        if (scale > 0.05f)
        {
            GameObject branchPart = Instantiate(branchSprite, (Vector3)(prevBranchPos + growDirection), orientation);

            branchPart.transform.localScale = new Vector3(1, scale, 1);

            SpriteRenderer[] arr = branchPart.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer s in arr)
            {
                s.sortingOrder = spriteNum;
            }

            spriteNum++;

            manager.AddBranchSprite(branchID, branchPart);
        }
        tempMaxAngle = ShortenAngle();
    }

    private float AdjustAngle(float angle)
    {
        if (angle <= -180)
        {
            return AdjustAngle(angle + 360);
        } else if (angle > 180)
        {
            return AdjustAngle(angle - 360);
        }
        return angle;
    }

    private float ShortenAngle()
    {
        return maxAngleBetweenBranches - totalBranchLength / (maxBranchLength * 3) * maxAngleBetweenBranches;
    }
}
