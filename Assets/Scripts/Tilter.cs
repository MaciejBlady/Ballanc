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

    private void Start()
    {
        _isCalibrating = true;
        _xCalibratedValue = 0.0f;
        _yCalibratedValue = 0.0f;
        _zCalibratedValue = 0.0f;
        Invoke("EndCalibrating", 0.5f);
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
        values.x = GetSingleRotation(RecalculateValue(acceleration.x, _xCalibratedValue));
        values.z = GetSingleRotation(RecalculateValue(acceleration.z, _zCalibratedValue));

        return values;
    }

    private float GetSingleRotation(float x)
    {
        return x * 15.0f;
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
        _isCalibrating = false;
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
}
