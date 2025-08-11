using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject Untouchable;
    public GameObject delayDisable;
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

    public void delayButton()
    {
        gameObject.SetActive(true);
        Invoke("ButtonAnim", 1f);
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
}
