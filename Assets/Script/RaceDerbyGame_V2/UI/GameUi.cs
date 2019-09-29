using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameUi : MonoBehaviour
{
    public static GameUi self;

    public CanvasGroup fadePlane, gameoverFader;
    public GameObject gameOverUI;
    public float fadeSpeed = 2;

    void Start()
    {
        if (self)
        {
            Destroy(this);
        }
        else
        {
            self = this;
        }
        FindObjectOfType<PlayerCar>().OnDeath += OnGameOver;
    }

    public void OnGameOver()
    {
        StartCoroutine(Fade(1, fadePlane, 2));
        StartCoroutine(Fade(1, gameoverFader, 3));
    }

    IEnumerator Fade(float toAlpha, CanvasGroup target, float preDelay)
    {
        yield return new WaitForSeconds(preDelay);
        while (!Mathf.Approximately(target.alpha, toAlpha))
        {
            target.alpha = Mathf.MoveTowards(target.alpha, toAlpha, Time.deltaTime * 2);
            yield return null;
        }
    }

    public void LoadNewLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        Score.scoreValue = 0;
    }

    public void QuitGame()
    {
#if UNITY_ENGINE//when in unity engine and you clicked on quit game it quits
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();//when not in unity engine editor game quit is game quit
#endif
    }
}