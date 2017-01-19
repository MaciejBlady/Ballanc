using UnityEngine;

public class ForceApplier : MonoBehaviour
{
    public Vector3 Up;
    public Vector3 Down;
    public Vector3 Left;
    public Vector3 Right;

    public float Modifier = 1.0f;

    private const float THRESHOLD = 0.05f;

    private bool _isCalibrating;

    private int _counts;

    private float _xCalibratedValue;
    private float _yCalibratedValue;
    private float _xRangeWidth;
    private float _yRangeWidth;

    private Rigidbody _rBody;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
       // _isCalibrating = true;
        _xCalibratedValue = 0.0f;
        _yCalibratedValue = 0.0f;
        //Invoke("EndCalibration", 0.5f);
	}
	
	private void Update()
    {
        if (_isCalibrating)
        {
            _counts++;
            _xCalibratedValue += Input.acceleration.x;
            _yCalibratedValue += Input.acceleration.z;
        }
        else
        {
            if (Input.acceleration.x > _xCalibratedValue + THRESHOLD)
            {
                //Apply force to down point
            }
            else if (Input.acceleration.x < _xCalibratedValue + THRESHOLD)
            {
                //Apply force to up point
            }

            if (Input.acceleration.y > _yCalibratedValue + THRESHOLD)
            {
                //Apply force to right point
            }
            else if (Input.acceleration.y < _yCalibratedValue + THRESHOLD)
            {
                //Apply force to left point
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Force to Up");
            Vector3 dir = transform.position + Vector3.up - Up;
            _rBody.AddForceAtPosition(dir.normalized * Modifier, Up);
        }
        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("Force to Left");
            _rBody.AddForceAtPosition(Vector3.down * Modifier, Left);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Force to Down");
            _rBody.AddForceAtPosition(Vector3.down * Modifier, Down);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Force to Right");
            _rBody.AddForceAtPosition(Vector3.down * Modifier, Right);
        }
    }

    private void EndCalibrating()
    {
        _xCalibratedValue /= _counts;
        _yCalibratedValue /= _counts;
        _xRangeWidth = 1.0f - Mathf.Abs(_xCalibratedValue);
        _yRangeWidth = 1.0f - Mathf.Abs(_yCalibratedValue);
    }
}
