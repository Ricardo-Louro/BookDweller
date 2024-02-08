using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI TMPro_score;
    public void EndGame(string sceneName, int score)
    {
        StartCoroutine(ChangeScene(TMPro_score, sceneName, score));
    }

    private IEnumerator ChangeScene(TextMeshProUGUI TMPro, string sceneName, int score)
    {
        gameOver.SetActive(true);
        TMPro.text = "SCORE: " + score.ToString();
        

        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}