using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beeper : MonoBehaviour {

    [SerializeField] private GameObject _target;
    [SerializeField] private AudioClip _beep;

    private float _distance = BEEP_FACTOR;

    private const float BEEP_FACTOR = 0.4f;

    void Start ()
    {
        Beep();
	}
	
	void Update ()
    {
        _distance = Vector3.Distance(transform.position, _target.transform.position);
	}

    private void Beep()
    {
        AudioSource.PlayClipAtPoint(_beep, transform.position);
        Invoke("Beep", BeepPeriod());
    }

    private float BeepPeriod()
    {
        return BEEP_FACTOR / _distance ;
    }
}
