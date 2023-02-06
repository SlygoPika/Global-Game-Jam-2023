using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int maxBranches;
    public float deleteDelay;
    public Animator gameEnder;
    public int StartTimer;
    public AudioManager audioManager;

    Stack<GameObject>[] branches;
    Stack<GameObject> deleteBranch;
    float timeLeft;
    bool hasEnded;
    string nextScene;

    private void Awake()
    {
        timeLeft = StartTimer;
        hasEnded = false;
        audioManager.PlayOnLoop("DarkTheme");
    }

    private void Start()
    {
        branches = new Stack<GameObject>[maxBranches + 1];

        for (int i = 0; i < branches.Length; i++)
        {
            branches[i] = new Stack<GameObject>();
        }
    }

    private void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= -59.99f)
        {
            timeLeft = 0;
        }
        if (timeLeft == 0 && !hasEnded)
        {
            hasEnded = true;
            EndGame("GameOver");
        }
    }

    public int GetTimeLeft()
    {
        return (int)(timeLeft + 60f);
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

    public void EndGame(string nextScene)
    {
        this.nextScene = nextScene;
        audioManager.Play("Death");
        gameEnder.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void AddTime(int time)
    {
        timeLeft += time;
    }
}
