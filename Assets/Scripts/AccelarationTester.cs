using UnityEngine;
using UnityEngine.UI;

public class AccelarationTester : MonoBehaviour
{
    public GameObject RawDataText;
    public GameObject CalibratedText;
    public GameObject RecalculatedText;

    private const float MIN = -1.0f;
    private const float MAX = 1.0f;

    private bool _isCalibrating;

    private int _counts;

    private float _xCalibratedValue;
    private float _yCalibratedValue;
    private float _zCalibratedValue;
    private float _xRangeWidth;
    private float _yRangeWidth;
    private float _zRangeWidth;

    private void Start()
    {
        _isCalibrating = true;
        _xCalibratedValue = 0.0f;
        _yCalibratedValue = 0.0f;
        _zCalibratedValue = 0.0f;
        Invoke("EndCalibration", 0.5f);
    }

    private void Update()
    {
        string calibratedText = string.Format("Is Calibrating = {0}", _isCalibrating);
        if (_isCalibrating)
        {
            _counts++;
            _xCalibratedValue += Input.acceleration.x;
            _yCalibratedValue += Input.acceleration.z;
        }
        else
        {
            calibratedText += string.Format("Calibrated Values\nX:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}", _xCalibratedValue, _yCalibratedValue, _zCalibratedValue);
            RecalculatedText.GetComponent<Text>().text = string.Format("Recalculated Values\nX:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}",
                RecalculateValue(Input.acceleration.x, _xCalibratedValue, _xRangeWidth),
                RecalculateValue(Input.acceleration.y, _yCalibratedValue, _yRangeWidth),
                RecalculateValue(Input.acceleration.z, _zCalibratedValue, _zRangeWidth));
        }
        RawDataText.GetComponent<Text>().text = string.Format("Raw Values\nX:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}", Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
	}

    public void Vibrate()
    {
        Handheld.Vibrate();
    }

    private void EndCalibrating()
    {
        _xCalibratedValue /= _counts;
        _yCalibratedValue /= _counts;
        _zCalibratedValue /= _counts;
        _xRangeWidth = 1.0f - Mathf.Abs(_xCalibratedValue);
        _yRangeWidth = 1.0f - Mathf.Abs(_yCalibratedValue);
        _zRangeWidth = 1.0f - Mathf.Abs(_zCalibratedValue);
    }

    private float RecalculateValue(float value, float calibratedValue, float width)
    {
        float newMax = calibratedValue + width;
        float newMin = calibratedValue - width;
        return ((value - MIN) / (MAX - MIN)) * ((newMax - newMin)+ newMin);
    }
}
