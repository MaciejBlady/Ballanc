using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnPlayerEnter : MonoBehaviour
{
    public event Action OnAfterLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            if (OnAfterLoad != null)
            {
                OnAfterLoad();
            }
        }
    }
}
