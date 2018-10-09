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

    private Score score;
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    // ====================================================

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        score = new Score();
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

    public void GameLost()
    {
        // TODO: Do more things
        //  - Load a lose scene or enable lost UI
        SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
