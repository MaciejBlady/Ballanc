using System;
using UnityEngine;

public class Tilter : MonoBehaviour
{
    private bool _isCalibrating;
    private bool _isGameTime;

    private int _counts;

    private float _xCalibratedValue;
    private float _yCalibratedValue;
    //private float _zCalibratedValue;

    private float _xRangeWidth;
    private float _yRangeWidth;
   // private float _zRangeWidth;

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

    private const float MIN = -1.0f;
    private const float MAX = 1.0f;
    private const float MAXANGLE = 15.0f;

    private void Start()
    {
        _isCalibrating = true;
        _xCalibratedValue = 0.0f;
        _yCalibratedValue = 0.0f;
        //_zCalibratedValue = 0.0f;
        Invoke("EndCalibrating", 0.5f);
    }

    private void Update()
    {
        if (_isCalibrating)
        {
            _counts++;
            _xCalibratedValue += Input.acceleration.x;
            _yCalibratedValue += Input.acceleration.y;
            //_zCalibratedValue += Input.acceleration.z;
        }
        else
        {
            if (IsGameTime)
            {
                transform.eulerAngles = GetRotation(Input.acceleration); 
            }
        }

    }

    private Vector3 GetRotation(Vector3 acceleration)
    {
        Vector3 values = new Vector3();
        values.x = RecalculateValue(RecalculateValue(acceleration.x, _xCalibratedValue, _xRangeWidth, MIN, MAX), 0.0f, MAXANGLE, _xCalibratedValue - _xRangeWidth, _xCalibratedValue + _xRangeWidth);
        values.z = -RecalculateValue(RecalculateValue(acceleration.y, _yCalibratedValue, _yRangeWidth, MIN, MAX), 0.0f, MAXANGLE, _yCalibratedValue - _yRangeWidth, _yCalibratedValue + _yRangeWidth);

        return values;
    }

    //private float GetSingleRotation(float x)
    //{
    //    return x * 15.0f;
    //}

    //public void Vibrate()
    //{
    //    Handheld.Vibrate();
    //}

    private void EndCalibrating()
    {
        _xCalibratedValue /= _counts;
        _yCalibratedValue /= _counts;
        //_zCalibratedValue /= _counts;
        _isCalibrating = false;

        _xRangeWidth = 1.0f - Mathf.Abs(_xCalibratedValue);
        _yRangeWidth = 1.0f - Mathf.Abs(_yCalibratedValue);
        //_zRangeWidth = 1.0f - Mathf.Abs(_zCalibratedValue);
    }

    private float RecalculateValue(float value, float calibratedValue)
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

    private float RecalculateValue(float value, float middle, float halfWidth, float oldMin, float oldMax)
    {
        float newMax = middle + halfWidth;
        float newMin = middle - halfWidth;
        return ((value - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
    }
}
