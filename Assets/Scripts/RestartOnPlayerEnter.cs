using UnityEngine;

public class RestartOnPlayerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            FindObjectOfType<GameController>().GameOver(); 
        }
    }
}
