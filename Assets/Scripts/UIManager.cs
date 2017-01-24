using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum Language
    {
        ENG,
        PL
    }
    [Header("Main Menu")]
    public GameObject RootMainMenu;
    public GameObject StartButton;
    public GameObject OptionsButton;
    public GameObject ExitButton;
    [Header("Options Menu")]
    public GameObject RootOptionsMenu;
    public GameObject BestScoreText;
    public GameObject RemoveScoreButton;
    public GameObject ChangeLanguageButton;
    public GameObject ReturnButton;
    [Header("In Game UI")]
    public GameObject RootInGameUIMenu;
    public GameObject ScoreText;
    public GameObject NewBestScoreButton;
    public GameObject NoBestScoreButton;

    private Language _currentLanguage;

    private void ChangeDisplayedText(Language lang)
    {
        if (lang == Language.ENG)
        {
            StartButton.GetComponentInChildren<Text>().text = "Start";
            OptionsButton.GetComponentInChildren<Text>().text = "Options";
            ExitButton.GetComponentInChildren<Text>().text = "Exit";
            UpdateBestScoreText();
            RemoveScoreButton.GetComponentInChildren<Text>().text = "Remove best score";
            ChangeLanguageButton.GetComponentInChildren<Text>().text = "Change language";
            ReturnButton.GetComponentInChildren<Text>().text = "Return";
            ScoreText.GetComponentInChildren<Text>().text = "Score: ";
            NewBestScoreButton.GetComponentInChildren<Text>().text = "New best score! Tap anywhere to return";
            NoBestScoreButton.GetComponentInChildren<Text>().text = "Tap anywhere to return";
        }
        else if (lang == Language.PL)
        {
            StartButton.GetComponentInChildren<Text>().text = "Rozpocznij";
            OptionsButton.GetComponentInChildren<Text>().text = "Opcje";
            ExitButton.GetComponentInChildren<Text>().text = "Zakończ";
            UpdateBestScoreText();
            RemoveScoreButton.GetComponentInChildren<Text>().text = "Usuń najlepszy wynik";
            ChangeLanguageButton.GetComponentInChildren<Text>().text = "Zmień język";
            ReturnButton.GetComponentInChildren<Text>().text = "Powrót";
            ScoreText.GetComponentInChildren<Text>().text = "Wynik: ";
            NewBestScoreButton.GetComponentInChildren<Text>().text = "Nowy najlepszy wynik! Dotknij gdziekolwiek, aby powrócić";
            NoBestScoreButton.GetComponentInChildren<Text>().text = "Dotknij gdziekolwiek, aby powrócić";
        }
    }

    public void ShowMainMenu()
    {
        RootMainMenu.SetActive(true);
        RootOptionsMenu.SetActive(false);
        RootInGameUIMenu.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        RootMainMenu.SetActive(false);
        RootOptionsMenu.SetActive(true);
        RootInGameUIMenu.SetActive(false);
        UpdateBestScoreText();
    }

    public void ShowInGameMenu()
    {
        RootMainMenu.SetActive(false);
        RootOptionsMenu.SetActive(false);
        RootInGameUIMenu.SetActive(true);
        NewBestScoreButton.SetActive(false);
        NoBestScoreButton.SetActive(false);
    }

    public void ShowGameOverMenu(bool isBestScore)
    {
        NewBestScoreButton.SetActive(isBestScore);
        NoBestScoreButton.SetActive(!isBestScore);
    }

    public void UpdateScoreText(int value)
    {
        if (_currentLanguage == Language.ENG)
        {
            ScoreText.GetComponentInChildren<Text>().text = string.Format("Score: {0}", value);
        }
        else if (_currentLanguage == Language.PL)
        {
            ScoreText.GetComponentInChildren<Text>().text = string.Format("Wynik: {0}", value);
        }
    }

    public void SwitchLanguage()
    {
        if (_currentLanguage == Language.ENG)
        {
            _currentLanguage = Language.PL;
        }
        else if(_currentLanguage == Language.PL)
        {
            _currentLanguage = Language.ENG;
        }
        ChangeDisplayedText(_currentLanguage);
    }

    public void UpdateBestScoreText()
    {
        int value = PlayerPrefs.GetInt("BestScore");
        if (_currentLanguage == Language.ENG)
        {
            BestScoreText.GetComponentInChildren<Text>().text = string.Format("Best score: {0}", value);
        }
        else if (_currentLanguage == Language.PL)
        {
            BestScoreText.GetComponentInChildren<Text>().text = string.Format("Najlepszy wynik: {0}", value);
        }
    }
}
