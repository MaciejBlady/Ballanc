using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltMeter : MonoBehaviour {

    public GameObject Target;

    private MeshRenderer _renderer;
    private float _difference = 0.0f;

    public float NormalizedDifference
    {
        get
        {
            return _difference;
        }
    }

    void Start ()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
	
	
	void Update ()
    {
        float difference = transform.position.y - Target.transform.position.y;
        Color color = Color.green;

        _difference = Mathf.Clamp01(difference);

        if (difference > 0.0f)
        {
            color = new Color(_difference, 0.0f, 0.0f);
        }

        //_renderer.material.color = color;       
    }
}
