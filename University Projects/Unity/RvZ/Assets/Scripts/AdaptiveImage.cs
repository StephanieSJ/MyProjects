using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Breakable))]
public class AdaptiveImage : MonoBehaviour {

    public int current;

    public Sprite[] sprites;

    public SpriteRenderer sprite;

    private Breakable breakable;

    void Start()
    {
        breakable = GetComponent<Breakable>();
    }

    void Update()
    {
        current = (int)(breakable.durability / breakable.maxdurability * sprites.Length);
        if (current >= sprites.Length)
        {
            current = sprites.Length - 1;
        }
        sprite.sprite = sprites[current];
    }
}
