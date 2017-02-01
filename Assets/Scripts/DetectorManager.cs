using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorManager : MonoBehaviour
{
    public GameObject[] Detectors;
    public AudioClip WarningSound;

    private const float SCAN_TIME = 1.0f;

	void Start ()
    {
        ScanAndWarn();
    }

    void ScanAndWarn()
    {
        float maxTilt = -1.0f;
        GameObject maxTiltGO = null;
        foreach (var detector in Detectors)
        {
            float diff = detector.GetComponent<TiltMeter>().NormalizedDifference;
            detector.GetComponent<MeshRenderer>().material.color = Color.white;
            if (diff > maxTilt)
            {
                maxTilt = diff;
                maxTiltGO = detector;
            }
        }

        maxTiltGO.GetComponent<MeshRenderer>().material.color = Color.red;
        AudioSource.PlayClipAtPoint(maxTiltGO.GetComponent<TiltMeter>().WarningSound, maxTiltGO.transform.position);
        Handheld.Vibrate();
        Invoke("ScanAndWarn", SCAN_TIME - maxTilt);
    }

}
