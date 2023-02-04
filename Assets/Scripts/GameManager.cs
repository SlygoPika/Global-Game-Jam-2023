using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int maxBranches;
    public float deleteDelay;

    Stack<GameObject>[] branches;
    Stack<GameObject> deleteBranch;

    private void Start()
    {
        branches = new Stack<GameObject>[maxBranches + 1];

        for (int i = 0; i < branches.Length; i++)
        {
            branches[i] = new Stack<GameObject>();
        }
    }


    public void DeleteBranch(int branchID)
    {
        deleteBranch = branches[branchID];

        DestroyBranch();
    }

    private void DestroyBranch()
    {
        GameObject branchSprite = deleteBranch.Pop();
        Destroy(branchSprite);

        if (deleteBranch.Count > 0)
        {
            Invoke("DestroyBranch", deleteDelay);
        }

        Destroy(branchSprite);
        Debug.Log("Sprite Deleted");
    }

    public void AddBranchSprite(int branchID, GameObject sprite)
    {
        branches[branchID].Push(sprite);
    }

    public int GetBranchIDToDelete()
    {
        GameObject result;

        for (int i = 0; i < branches.Length; i++)
        {
            if (branches[i].TryPeek(out result))
            {
                return i;
            }
        }

        return -1;
    }

    public int GetNewBranchID()
    {
        GameObject result;

        for (int i = 0; i < branches.Length; i++)
        {
            if (!branches[i].TryPeek(out result))
            {
                return i;
            }
        }

        return -1;
    }

    private void DebugText()
    {
        Debug.Log("uwu");
    }
}
