using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    void Awake()
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

        GameManager.isContinueGame = true;
    }

}
