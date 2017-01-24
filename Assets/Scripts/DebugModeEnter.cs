using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugModeEnter : MonoBehaviour
{
    private int _clickCount;

    public void Click()
    {
        _clickCount++;
        if (_clickCount > 10.0f)
        {
            SceneManager.LoadScene("test");
        }
    }
}
