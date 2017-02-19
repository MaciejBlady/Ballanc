using UnityEngine;

public class TiltMeter : MonoBehaviour
{

    public GameObject Ball;
    public GameObject Target;
    public AudioClip WarningSound_PL;
    public AudioClip WarningSound_EN;

    private MeshRenderer _renderer;
    private float _difference = 0.0f;
    private float _distance = 0.0f;
    public float NormalizedDifference
    {
        get
        {
            return _difference;
        }
    }

    public float Distance
    {
        get
        {
            return _distance;
        }
    }


    void Start ()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
	
	
	void Update ()
    {
        float difference = transform.position.y - Target.transform.position.y;
        _difference = Mathf.Clamp01(difference);
        _distance = Vector3.Distance(transform.position, Ball.transform.position);
    }

    public void PlayWarningSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (GameObject.FindObjectOfType<UIManager>().CurrentLanguage == UIManager.Language.ENG)
        {
            audioSource.PlayOneShot(WarningSound_EN);
        }
        else
        {
            audioSource.PlayOneShot(WarningSound_PL);
        }
    }
}
