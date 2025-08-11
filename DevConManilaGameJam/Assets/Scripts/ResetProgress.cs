using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Checkpoint Wave", 1);
        PlayerPrefs.SetInt("Credits", 0);
    }

}
