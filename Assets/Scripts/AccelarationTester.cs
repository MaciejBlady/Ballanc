using UnityEngine;
using UnityEngine.UI;

public class AccelarationTester : MonoBehaviour
{
    public GameObject RawDataText;
    public GameObject CalibratedText;
    public GameObject RecalculatedText;
    public GameObject AnglesText;

    private const float MIN = -1.0f;
    private const float MAX = 1.0f;
    private const float MIN_X = -0.5f;
    private const float MAX_X = 0.5f;
    private const float MAX_ANGLE = 30.0f;
    private const float ANGLE_STEP = 1.0f;

    private bool _isCalibrating;

    private int _counts;

    private float _xCalibratedValue;
    private float _yCalibratedValue;
    private float _zCalibratedValue;
    private float _xRangeWidth;
    private float _yRangeWidth;
    private float _zRangeWidth;

    private Vector3 _values = new Vector3();

    private void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string rawText = string.Format("Raw Values\nX:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}", Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        string calibratedText = string.Format("Is Calibrating = {0}\n", _isCalibrating);
        string recalculatedText = string.Format("Recalculated Values\n");
        string anglesText = string.Format("Angles Values\n");
        if (_isCalibrating)
        {
            _counts++;
            _xCalibratedValue += Input.acceleration.x;
            _yCalibratedValue += Input.acceleration.y;
            _zCalibratedValue += Input.acceleration.z;
        }
        else
        {
            calibratedText += string.Format("Calibrated Values\nX:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}\n", _xCalibratedValue, _yCalibratedValue, _zCalibratedValue);
            recalculatedText += string.Format("X:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}",
                ConsiderCalibration(Input.acceleration.x, _xCalibratedValue),
                ConsiderCalibration(Input.acceleration.y, _yCalibratedValue),
                ConsiderCalibration(Input.acceleration.z, _zCalibratedValue));
            Vector3 values = new Vector3();
            values.x = RecalculateValue(ConsiderCalibration(Input.acceleration.x, _xCalibratedValue), 0.0f, MAX_ANGLE, MIN_X, MAX_X);
            values.y = RecalculateValue(ConsiderCalibration(Input.acceleration.y, _yCalibratedValue), 0.0f, MAX_ANGLE, MIN, MAX);
            values.z = RecalculateValue(ConsiderCalibration(Input.acceleration.z, _zCalibratedValue), 0.0f, MAX_ANGLE, MIN, MAX);
            _values.x = Mathf.Clamp(values.x, _values.x - ANGLE_STEP, _values.x + ANGLE_STEP);
            _values.z = Mathf.Clamp(values.z, _values.z - ANGLE_STEP, _values.z + ANGLE_STEP);
            _values.y = Mathf.Clamp(values.y, _values.y - ANGLE_STEP, _values.y + ANGLE_STEP);
            anglesText += string.Format("X:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}", _values.x, _values.y, _values.z);
        }
        RawDataText.GetComponent<Text>().text = rawText;
        CalibratedText.GetComponent<Text>().text = calibratedText;
        RecalculatedText.GetComponent<Text>().text = recalculatedText;
        AnglesText.GetComponent<Text>().text = anglesText;
#endif
    }

    public void Vibrate()
    {
        Handheld.Vibrate();
    }

    private Vector3 GetRotation(Vector3 acceleration)
    {
        Vector3 values = new Vector3();
        values.x = RecalculateValue(ConsiderCalibration(Input.acceleration.x, _xCalibratedValue), 0.0f, MAX_ANGLE, MIN_X, MAX_X);
        values.z = RecalculateValue(ConsiderCalibration(Input.acceleration.z, _zCalibratedValue), 0.0f, MAX_ANGLE, MIN, MAX);

        return values;
    }

    private float ConsiderCalibration(float value, float calibratedValue)
    {
        return value - calibratedValue;
    }

    private float RecalculateValue(float value, float middle, float halfWidth, float oldMin, float oldMax)
    {
        float newMax = middle + halfWidth;
        float newMin = middle - halfWidth;
        return ((value - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
    }

    public void StartCalibration()
    {
        _isCalibrating = true;
        _xCalibratedValue = 0.0f;
        _yCalibratedValue = 0.0f;
        _zCalibratedValue = 0.0f;
        Invoke("EndCalibrating", 0.5f);
    }

    private void EndCalibrating()
    {
        _xCalibratedValue /= _counts;
        _yCalibratedValue /= _counts;
        _zCalibratedValue /= _counts;
        _isCalibrating = false;
        _counts = 0;
    }
}
