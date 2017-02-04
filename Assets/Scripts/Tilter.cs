using System;
using UnityEngine;

public class Tilter : MonoBehaviour
{
    private bool _isCalibrating;
    private bool _isGameTime;

    private int _counts;

    private float _xCalibratedValue;
    private float _yCalibratedValue;
    private float _zCalibratedValue;

    private float _xRangeWidth;
    private float _yRangeWidth;
    private float _zRangeWidth;

    private const float MIN_Z = -1.0f;
    private const float MAX_Z = 1.0f;
    private const float MIN_X = -0.5f;
    private const float MAX_X = 0.5f;
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
            _zCalibratedValue += Input.acceleration.z;
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
        values.x = -RecalculateValue(ConsiderCalibration(Input.acceleration.x, _xCalibratedValue), 0.0f, MAX_ANGLE, MIN_X, MAX_X);
        values.z = RecalculateValue(ConsiderCalibration(Input.acceleration.z, _zCalibratedValue), 0.0f, MAX_ANGLE, MIN_Z, MAX_Z);

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
        IsGameTime = true;
    }
}
