using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnActivate : MonoBehaviour
{
    [SerializeField]
    private AudioClip _sound_PL;

    [SerializeField]
    private AudioClip _sound_EN;

    [SerializeField]
    private AudioSource _source;

    private void OnEnable()
    {
        if (GameObject.FindObjectOfType<UIManager>().CurrentLanguage == UIManager.Language.PL)
        {
            _source.PlayOneShot(_sound_PL, 1.0f);
        }
        else
        {
            _source.PlayOneShot(_sound_EN, 1.0f);
        }

    }
}
