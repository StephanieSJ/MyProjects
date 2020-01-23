using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Keeps track of the game's state, specifically the score and the
/// number of shots remaining. Later it will handle transitioning out
/// of this scene into a different one.
/// </summary>
public class GameState : MonoBehaviour
{
    [Tooltip("The text control that will contain the score text.")]
    public TextMeshProUGUI scoreText;

    [Tooltip("The current score.")]
    public int score = 0;

    [Tooltip("The initial number of shots that can be made.")]
    public int maxShots = 3;

    [Tooltip("How many shots are remaining.")]
    public int shotsRemaining;

    [Tooltip("After the last shot, how long should we wait until closing the scene and going back to the main scene?")]
    public float timeUntilClose = 10;

    protected float timeRemaining;

    private ImageUI rhinos;
    private ImageUI shots;

    public int rhinosSaved;
    public int rhinosInLevel;

    // Start is called before the first frame update
    void Start()
    {
        shotsRemaining = maxShots;
        rhinosInLevel = GameObject.FindGameObjectsWithTag("CagedRhinos").Length;
        rhinosSaved = 0;
        ImageUI[] stuff = GetComponents<ImageUI>();
        rhinos = stuff[0];
        shots = stuff[1];
        rhinos.max = rhinosInLevel;
        rhinos.current = 0;
        shots.max = maxShots;
        shots.current = shotsRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        shots.current = shotsRemaining;
        rhinos.current = rhinosSaved;

        // Reset the time remaining if the scene is not busy closing.
        if (shotsRemaining > 0)
            timeRemaining = timeUntilClose;
        else
        {
            // Scene busy closing, so decrease the remaining time,
            timeRemaining -= Time.deltaTime;

            // Time up?
            if (timeRemaining <= 0)
            {
                // Time's up, so update high score.
                // Get the current highscore.
                int highScore = 0;
                if (File.Exists(Application.persistentDataPath + "/highscore.data"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Application.persistentDataPath + "/highscore.data", FileMode.Open);
                    highScore = (int)bf.Deserialize(file);
                    file.Close();
                }
                Debug.Log("Highscore: " + highScore + ", Score: " + score);
                // Save if current score higher than the high score.
                if (score > highScore)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    // Application.persistentDataPath is a string, so if you wanted you can put that into debug.log 
                    // if you want to know where save games are located.
                    FileStream file = File.Create(Application.persistentDataPath + "/highscore.data");
                    bf.Serialize(file, score);
                    file.Close();
                }

                if (rhinosSaved >= rhinosInLevel)
                {
                    FileStream file;
                    BinaryFormatter bf = new BinaryFormatter();
                    int count = 3;
                    if (File.Exists(Application.persistentDataPath + "/levels.data"))
                    {
                        file = File.Open(Application.persistentDataPath + "/levels.data", FileMode.Open);
                        count = (int)bf.Deserialize(file);
                        file.Close();
                    }
                    file = File.Create(Application.persistentDataPath + "/levels.data");
                    bf.Serialize(file, count);
                    for (int i = 0; i < count; i++)
                    {
                        if (i <= SceneManager.GetActiveScene().buildIndex - 1)
                        {
                            bf.Serialize(file, true);
                        }
                        else
                        {
                            bf.Serialize(file, false);
                        }
                    }
                    file.Close();
                }

                // Change the scene to the main menu.
                SceneManager.LoadScene(0);
            }
        }
    }

    #region Methods added to be accessed via UnityEvents.
    public void Shoot()
    {
        shotsRemaining--;
    }

    public void Reload()
    {
        shotsRemaining++;
        if (shotsRemaining > maxShots)
        {
            shotsRemaining = maxShots;
        }
    }

    public void AwardPoints(int points)
    {
        score += points;
    }

    public void RhinoRescued()
    {
        rhinosSaved++;
        if (rhinosSaved >= rhinosInLevel)
        {
            shotsRemaining = 0;
        }
    }
    #endregion
}
