using UnityEngine;

public class UIScript : MonoBehaviour
{

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGameUI()
    {
        GameManager.PauseGame();
    }

    public void UnpauseGameUI()
    {
        GameManager.PauseGame();
    }
}
