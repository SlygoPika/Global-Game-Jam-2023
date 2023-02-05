using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameManager manager;

    void Update()
    {
        int timeLeft = manager.GetTimeLeft();
        int seconds = timeLeft % 60;
        int minutes = timeLeft / 60;

        if (seconds > 9)
        {
            text.SetText("" + minutes + ":" + seconds);
        }
        else
        {
            text.SetText("" + minutes + ":0" + seconds);
        }
    }
}
