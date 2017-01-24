using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Ball;

    private Vector3 _ballPosition;
    private UIManager _uiManager;
    private int _points;

    private void Awake()
    {
        _uiManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        _uiManager.ShowMainMenu();
        _ballPosition = Ball.transform.position;
        Ball.SetActive(false);
    }
	
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        CancelInvoke();
        _uiManager.ShowInGameMenu();
        _points = 0;
        _uiManager.UpdateScoreText(_points);
        Invoke("AddPoint", 1.0f);
        Ball.transform.position = _ballPosition;
        Ball.SetActive(true);
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Ball.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0.0f, 0.25f), 0.0f, Random.Range(0.0f, 0.25f)));
        FindObjectOfType<Tilter>().IsGameTime = true;
    }

    public void RestartBestScore()
    {
        PlayerPrefs.SetInt("BestScore", 0);
    }

    private void AddPoint()
    {
        _points++;
        _uiManager.UpdateScoreText(_points);
        Invoke("AddPoint", 1.0f);
    }

    public void GameOver()
    {
        CancelInvoke();
        _uiManager.ShowGameOverMenu(_points > PlayerPrefs.GetInt("BestScore"));
        if (_points > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", _points);
        }      
        Ball.SetActive(false);
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        FindObjectOfType<Tilter>().IsGameTime = false;
    }
}
