using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Delete", 8.0f);
    }


    private void Delete()
    {
        gameObject.SetActive(false);
    }

}
