using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    class Score
    {
        public int points = 0;
        public int bestPoints = 0;
    }

    public Spawner spawner;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI bestPointsText;

    public GameObject player;
    public GameObject lostUI;

    private Score score;
    private bool gameLost = false;
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }
    public bool GameLost { get { return gameLost; } }

    // ====================================================

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        score = new Score();
        gameLost = false;
    }

    void Start ()
    {
        LoadScore();

        bestPointsText.text = score.bestPoints.ToString();
	}

    void OnDestroy()
    {
        SaveScore();
    }

    // ====================================================

    public void AddPoints(int p)
    {
        score.points += p;

        if(score.points > score.bestPoints)
        {
            score.bestPoints = score.points;
            bestPointsText.text = score.bestPoints.ToString();
        }

        pointsText.text = score.points.ToString();
    }

    public void GameOver()
    {
        // TODO: Do more things
        //  - Load a lose scene or enable lost UI
        gameLost = true;
        Destroy(player);
        SaveScore();

        EnableGameOverUI();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        score.bestPoints = 0;
        score.points = 0;
        bestPointsText.text = score.bestPoints.ToString();
        pointsText.text = score.points.ToString();
        SaveScore();
    }

    // -------

    private void SaveScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "score.sc");
        bf.Serialize(file, score);
        file.Close();
    }

    private void LoadScore()
    {
        if(File.Exists(Application.persistentDataPath + "score.sc"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "score.sc", FileMode.Open);
            if (score == null) score = new Score();
            score = (Score)bf.Deserialize(file);
            file.Close();
        }
    }

    private void EnableGameOverUI()
    {
        lostUI.SetActive(true);
    }
}
