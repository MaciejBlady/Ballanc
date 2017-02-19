using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorManager : MonoBehaviour
{
    public GameObject[] Detectors;
    private const float SCAN_TIME = 1.0f;

    private GameObject _nearestDetector;

    private void Update()
    {
        if (GameObject.FindObjectOfType<Tilter>().IsGameTime)
        {
            float minDistance = 100000f;

            GameObject minDistGO = null;
            foreach (var detector in Detectors)
            {
                float dist = detector.GetComponent<TiltMeter>().Distance;
                detector.GetComponent<MeshRenderer>().material.color = Color.white;
                if (dist < minDistance)
                {
                    minDistance = dist;
                    minDistGO = detector;
                }
            }

            if (minDistGO != _nearestDetector)
            {
                _nearestDetector = minDistGO;
                _nearestDetector.GetComponent<TiltMeter>().PlayWarningSound();
            }
        }
    }

    //void ScanAndWarn()
    //{
    //    if (!GameObject.FindObjectOfType<Tilter>().IsGameTime)
    //    {
    //        Invoke("ScanAndWarn", SCAN_TIME);
    //        return;
    //    }

    //    float maxTilt = -1.0f;
    //    GameObject maxTiltGO = null;
    //    foreach (var detector in Detectors)
    //    {
    //        float diff = detector.GetComponent<TiltMeter>().NormalizedDifference;
    //        detector.GetComponent<MeshRenderer>().material.color = Color.white;
    //        if (diff > maxTilt)
    //        {
    //            maxTilt = diff;
    //            maxTiltGO = detector;
    //        }
    //    }

    //    maxTiltGO.GetComponent<MeshRenderer>().material.color = Color.red;

    //    if (GameObject.FindObjectOfType<UIManager>().CurrentLanguage == UIManager.Language.ENG)
    //    {
    //        AudioSource.PlayClipAtPoint(maxTiltGO.GetComponent<TiltMeter>().WarningSound_EN, maxTiltGO.transform.position);
    //    }
    //    else
    //    {
    //        AudioSource.PlayClipAtPoint(maxTiltGO.GetComponent<TiltMeter>().WarningSound_PL, maxTiltGO.transform.position);
    //    }
    //    Debug.Log(maxTilt);
    //    //Handheld.Vibrate();
    //    Invoke("ScanAndWarn", SCAN_TIME - maxTilt);
    //}

}
