 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TileBoard board;

    public CanvasGroup gameOver;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private int score;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        bestScoreText.text = LoadBestScore().ToString();

        gameOver.alpha = 0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        gameOver.interactable = true;

        board.enabled = false;
        StartCoroutine(Fade(gameOver, 1.0f, 1.0f));
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int value)
    {
        SetScore(score + value);
    }

    private void SetScore(int value)
    {
        this.score = value;
        scoreText.text = value.ToString();

        SaveBestScore();
    }

    private void SaveBestScore()
    {
        int bestScore = LoadBestScore();

        if(score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
        }
    }

    private int LoadBestScore()
    {
        return PlayerPrefs.GetInt("bestScore", 0);
    }
}
