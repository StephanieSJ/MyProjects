using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUI : MonoBehaviour {

    public int max;
    public int current;

    public Image[] pictures;
    public Sprite active;
    public Sprite notactive;

    void Update()
    {
        if (current > max)
        {
            current = max;
        }

        for (int i = 0; i < pictures.Length; i++)
        {
            if (i < current)
            {
                pictures[i].sprite = active;
            }
            else
            {
                pictures[i].sprite = notactive;
            }

            if (i < max)
            {
                pictures[i].enabled = true;
            }
            else
            {
                pictures[i].enabled = false;
            }
        }
    }
}
