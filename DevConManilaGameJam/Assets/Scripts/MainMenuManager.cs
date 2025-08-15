using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string sceneName;

    public Button continueGameButton;
    public Button newGameButton;
    public Button newGameConfirmationButton;

    public GameObject mainMenuPanel;
    public GameObject newGamePanel;

    int checkSavedGame;

    private void Start()
    {
        Time.timeScale = 1;

        //PlayerPrefs.SetInt("Saved Game", 0);
        checkSavedGame = PlayerPrefs.GetInt("Saved Game", 0);

        if (checkSavedGame == 1)
        {
            continueGameButton.interactable = true;
        }

        continueGameButton.onClick.AddListener(ChangeScene);

        newGameButton.onClick.AddListener(NewGameButton);
        newGameConfirmationButton.onClick.AddListener(ConfirmNewGame);
    }

    void NewGameButton()
    {
        if (checkSavedGame == 1)
        {
            newGamePanel.SetActive(true);
            mainMenuPanel.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Saved Game", 1);
            ConfirmNewGame();
        }
    }

    void ConfirmNewGame()
    {
        PlayerPrefs.SetInt("Checkpoint Wave", 1);
        PlayerPrefs.SetInt("Credits", 0);

        PlayerPrefs.SetInt("Health Level", 1);
        PlayerPrefs.SetInt("Health Regen Level", 1);
        PlayerPrefs.SetInt("Damage Level", 1);
        PlayerPrefs.SetInt("Crit Damage Level", 1);
        PlayerPrefs.SetInt("Crit Rate Level", 1);
        PlayerPrefs.SetInt("Attack Speed Level", 1);
        PlayerPrefs.SetInt("Laser Pet Level", 0);
        PlayerPrefs.SetInt("Cryo Pet Level", 0);
        PlayerPrefs.SetInt("Energy Wave Pet Level", 0);

        ChangeScene();
    }

    void ChangeScene()
    {
        GameManager.isContinueGame = true;
        SceneManager.LoadScene(sceneName);
    }
}
