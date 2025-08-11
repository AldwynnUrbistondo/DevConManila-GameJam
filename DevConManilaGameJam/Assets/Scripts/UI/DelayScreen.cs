using UnityEngine;

public class DelayScreen : MonoBehaviour
{
    public GameObject UntouchableScreen;

    public void Start()
    {
        Invoke("UnclickableScreen", 1f);
    }


    void UnclickableScreen()
    {
        UntouchableScreen.SetActive(false);
    }
}
