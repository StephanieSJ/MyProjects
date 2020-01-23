using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This component is responsible for managing the logic of the main screen,
/// specifically starting the play level scene when the play button is clicked.
/// It is also responsible for keeping track of the highest score obtained,
/// even between game play sessions.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Tooltip("What is highest score so far?")]
    public int highScore = 0;

    [Tooltip("The text control in which the high score is to be displayed.")]
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Load highscore and display in text.
        if (File.Exists(Application.persistentDataPath + "/highscore.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/highscore.data", FileMode.Open);
            highScore = (int)bf.Deserialize(file);
            file.Close();

            highScoreText.text = "Highscore: " + highScore;
        } else
        {
            highScoreText.text = "Highscore: none yet" ;
        }
    }

    /// <summary>
    /// The method to be called when the play button is clicked.
    /// </summary>
    public void PlayButtonClicked()
    {
        Debug.Log("I was clicked.");
        SceneManager.LoadScene(1);
    }
}
