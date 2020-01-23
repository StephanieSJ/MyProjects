using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour {

    public Button[] levels;

    void Start ()
    {
        if (File.Exists(Application.persistentDataPath + "/levels.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/levels.data", FileMode.Open);
            int count = (int)bf.Deserialize(file);
            for (int i = 0; i < count; i++)
            {
                try
                {
                    levels[i].enabled = (bool)bf.Deserialize(file);
                }
                catch
                {
                    levels[i].enabled = false;
                }
            }
            file.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/levels.data");
            levels[0].enabled = true;
            bf.Serialize(file, levels.Length);
            bf.Serialize(file, true);
            for (int i = 1; i < levels.Length; i++)
            {
                levels[i].enabled = false;
                bf.Serialize(file, false);
            }
            file.Close();
        }
    }

    public void ButtonClicked(int num)
    {
        if (num < levels.Length)
        {
            SceneManager.LoadScene(num + 2);
        }
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/levels.data");
        levels[0].enabled = true;
        bf.Serialize(file, levels.Length);
        bf.Serialize(file, true);
        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].enabled = false;
            bf.Serialize(file, false);
        }
        file.Close();
    }
}
