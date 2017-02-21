using UnityEngine;

public class Tilter : MonoBehaviour
{
    private bool _isCalibrating;
    private bool _isGameTime;

    private int _counts;

    private float _xCalibratedValue;
    private float _yCalibratedValue;

    private float _xRangeWidth;
    private float _yRangeWidth;

    private const float MAX_ANGLE = 30.0f;

    public bool IsGameTime
    {
        get
        {
            return _isGameTime;
        }

        set
        {
            _isGameTime = value;
        }
    }

    private void Update()
    {
        if (_isCalibrating)
        {
            _counts++;
            _xCalibratedValue += Input.acceleration.x;
            _yCalibratedValue += Input.acceleration.y;
        }
        else
        {
            if (IsGameTime)
            {
#if UNITY_EDITOR
                Vector3 temp = new Vector3(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
                transform.eulerAngles = temp * MAX_ANGLE;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
                Vector3 temp1 = new Vector3();
                temp1.x = ConsiderCalibration(Input.acceleration.y, _yCalibratedValue) * MAX_ANGLE;
                temp1.z = -ConsiderCalibration(Input.acceleration.x, _xCalibratedValue) * MAX_ANGLE;
                transform.eulerAngles = temp1;
#endif
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
        }

    }

    private float ConsiderCalibration(float value, float calibratedValue)
    {
        return value - calibratedValue;
    }

    public void StartCalibration()
    {
        _isCalibrating = true;
        _xCalibratedValue = 0.0f;
        _yCalibratedValue = 0.0f;
        Invoke("EndCalibrating", 0.5f);
    }

    private void EndCalibrating()
    {
        _xCalibratedValue /= _counts;
        _yCalibratedValue /= _counts;
        _isCalibrating = false;
        _counts = 0;
        IsGameTime = true;
    }
}
