using UnityEngine;

public class RestartOnPlayerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameController>().GameOver();
        }
    }
}
