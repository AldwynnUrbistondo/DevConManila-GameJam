using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    AudioManager am;
    public PlayerStats playerStats;
    public Animator transition;

    [Header("HP Bar Variables")]
    public GameObject healthBarPanel;
    public float lerpTimer;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Timer Variables")]
    public TextMeshProUGUI timerText;
    public float initialTime;
    public float remainingTime;

    public GameObject timeStop;
    public static bool isContinueGame;
    public static bool isPaused;
    public static bool canMove;
    public static bool canShoot;
    public static bool canPetShoot = true;

    bool isEnding = false;

    void Start()
    {
        isEnding = false;
        am = FindAnyObjectByType<AudioManager>();
        UnPauseGame();

        timeStop.SetActive(false);
        remainingTime = initialTime;
        if (isContinueGame)
        {
            transition.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(FadeIn(1f));
        }
    }

    void Update()
    {

        UpdateHealthUI();
        Timer();

        if (remainingTime == 0 || playerStats.currentHealth <= 0 && !isEnding)
        {
            isEnding = true;
            isContinueGame = false;
            PauseGame();
            StartCoroutine(FadeOut(1f));

        }

        /*
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 15;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
        */
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = playerStats.currentHealth / playerStats.maxHealth;

        // When taking damage
        if (fillB > hFraction)
        {
            lerpTimer = 0f;
            frontHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        // When healing
        else if (fillF < hFraction)
        {
            lerpTimer = 0f;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    public void Timer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 0)
        {
            remainingTime = 0;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator FadeIn(float time)
    {
        transition.speed = 1;
        transition.speed /= time;

        transition.gameObject.SetActive(true);
        transition.Play("FadeIn");
        yield return new WaitForSecondsRealtime(time);
        transition.gameObject.SetActive(false);
    }

    IEnumerator FadeOut(float time)
    {
        am.music.Stop();
        am.PlaySound(SoundType.TimeStop);
        timeStop.SetActive(true);
        yield return new WaitForSecondsRealtime(1);

        transition.speed = 1;
        transition.speed /= time;

        transition.gameObject.SetActive(true);
        transition.Play("FadeOut");
        yield return new WaitForSecondsRealtime(time + 1);
        //transition.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        canMove = false;
        canShoot = false;
    }

    public static void UnPauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        canMove = true;
        canShoot = true;
    }
}
