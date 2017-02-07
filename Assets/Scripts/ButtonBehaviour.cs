using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{
    public UnityEvent OnSecondClick;
    public AudioClip HintSound_PL;
    public AudioClip HintSound_EN;

    public void PlayHint()
    {
        if (GameObject.FindObjectOfType<UIManager>().CurrentLanguage == UIManager.Language.ENG)
        {
            AudioSource.PlayClipAtPoint(HintSound_EN, Vector3.zero);
        }
        else
        {
            AudioSource.PlayClipAtPoint(HintSound_PL, Vector3.zero);
        }

    }

    public void Do()
    {
        OnSecondClick.Invoke();
    }
}
