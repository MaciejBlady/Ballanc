using UnityEngine;

public class PointTowardsBall : MonoBehaviour
{
    public GameObject Sphere;

    private void Update()
    {
        transform.LookAt(Sphere.transform);
    }
}