using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject Untouchable;
    public GameObject delayDisable;
    public Animator anim;
    public GameObject MainMenuStuff;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ContinueGame()
    {
        GameManager.isContinueGame = true;
    }

    public void PauseGameUI()
    {
        GameManager.PauseGame();
    }

    public void UnpauseGameUI()
    {
        GameManager.PauseGame();
    }
    //Main Menu and untouchable and Animation
    public void delayButton()
    {
        Untouchable.SetActive(true);
        Invoke("ButtonAnim", 1.5f);
    }

    public void ButtonAnim()
    {
        Untouchable.SetActive(false);
    }

    public void delayFalse()
    {
        Invoke("gameObjectFalse", 0.4f);
        
    }

    public void gameObjectFalse()
    {
        delayDisable.SetActive(false);
    }

    public void NewGameButtonAnim()
    {
        anim.SetTrigger("NewGame");
    }

    public void NewGameButtonBack()
    {
        anim.SetTrigger("NewBack");
    }

    //Exit Button and confirmation

    public void ExitGameButtonAnim()
    {
        anim.SetTrigger("ExitGame");
    }

    public void ExitGameButtonReturnAnim()
    {
        anim.SetTrigger("ExitGameReturn");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
