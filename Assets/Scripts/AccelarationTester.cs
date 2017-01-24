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
        Invoke("EndCalibrating", 1.0f);
    }

    private void Update()
    {
        string rawText = string.Format("Raw Values\nX:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}", Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        string calibratedText = string.Format("Is Calibrating = {0}\n", _isCalibrating);
        string recalculatedText = string.Format("Recalculated Values\n");
        if (_isCalibrating)
        {
            _counts++;
            _xCalibratedValue += Input.acceleration.x;
            _yCalibratedValue += Input.acceleration.y;
            _zCalibratedValue += Input.acceleration.z;
        }
        else
        {
            calibratedText += string.Format("Calibrated Values\nX:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}", _xCalibratedValue, _yCalibratedValue, _zCalibratedValue);
            //recalculatedText += string.Format("X:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}",
            //    RecalculateValue(Input.acceleration.x, _xCalibratedValue, _xRangeWidth),
            //    RecalculateValue(Input.acceleration.y, _yCalibratedValue, _yRangeWidth),
            //    RecalculateValue(Input.acceleration.z, _zCalibratedValue, _zRangeWidth));
            recalculatedText += string.Format("X:{0:0.###}\nY:{1:0.###}\nZ:{2:0.###}",
             RecalculateValue2(Input.acceleration.x, _xCalibratedValue, _xRangeWidth),
             RecalculateValue2(Input.acceleration.y, _yCalibratedValue, _yRangeWidth),
             RecalculateValue2(Input.acceleration.z, _zCalibratedValue, _zRangeWidth));
        }
        RawDataText.GetComponent<Text>().text = rawText;
        CalibratedText.GetComponent<Text>().text = calibratedText;
        RecalculatedText.GetComponent<Text>().text = recalculatedText;

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
        _isCalibrating = false;
    }

    private float RecalculateValue(float value, float calibratedValue, float width)
    {
        float newMax = calibratedValue + width;
        float newMin = calibratedValue - width;
        return ((value - MIN) / (MAX - MIN)) * ((newMax - newMin)+ newMin);
    }

    private float RecalculateValue2(float value, float calibratedValue, float width)
    {
        float temp = 0.0f;
        if (value >= calibratedValue)
        {
            temp = value - calibratedValue;
        }
        else
        {
            temp = calibratedValue - value;
        }

        return temp;
    }
}
