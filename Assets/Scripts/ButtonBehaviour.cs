using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class ButtonBehaviour : MonoBehaviour, ISelectHandler
{
    public UnityEvent OnSecondClick;
    public AudioClip HintSound_PL;
    public AudioClip HintSound_EN;

    public void PlayHint()
    {
        if (GameObject.FindObjectOfType<UIManager>().CurrentLanguage == UIManager.Language.ENG)
        {
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<AudioSource>().PlayOneShot(HintSound_EN);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<AudioSource>().PlayOneShot(HintSound_PL);
        }
    }

    public void Do()
    {
        OnSecondClick.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Selected" + eventData.selectedObject);
    }
}
