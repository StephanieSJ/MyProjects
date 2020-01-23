using UnityEngine;

/// <summary>
/// This component allows a GameObject to be "broken". This is done by
/// reducing the object's durability each time it collides with another
/// object. When the durability reaches zero, the object is replaced
/// with a destroyed object.
/// </summary>
public class Breakable : MonoBehaviour {
    [Tooltip("The amount of damage that can be ignored on impact.")]
    public float toughness;

    public float maxdurability;
    [Tooltip("The amount of damage can take before breaking.")]
    public float durability;

    [Tooltip("The object that replaces this object when it is destroyed.")]
    public GameObject destroyedObjectPrefab;

    [Tooltip("Should the object instantiated from the prefab be scaled to match this object's local scale?")]
    public bool scalePrefab = true;

    [Tooltip("Points scored when the object is destroyed.")]
    public int pointsForDestroying = 0;

    public bool Reloads;
    private bool colliding;

    void Start()
    {
        maxdurability = durability;
        colliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Called when this object's collider(s) involved in a collision.

        // The damage done is simply taken as the velocity in this example
        // and not the force of the impact. For homework, see if you can
        // determine the force of the impact.
        if (colliding)
        {
            return;
        }
        else
        {
            colliding = true;
        }

        float damage = collision.relativeVelocity.magnitude;

        Debug.Log(gameObject.name + " was damaged = " + damage);

        // Is the damage above some threshold? This ignores minor movement
        // damaging the object.
        if (damage > toughness)
            // Reduce the durability of the object.
            durability -= damage - toughness;

        // Has the object been destroyed?
        if (durability < 0)
        {
            // It has, so create and position the destroyed object...
            if (destroyedObjectPrefab != null)
            {
                GameObject go = Instantiate(destroyedObjectPrefab, transform.position, Quaternion.identity);
                if (scalePrefab)
                    go.transform.localScale = transform.localScale;
            }

            // Find game state object and award points.
            GameState gameState = GameObject.Find("LevelManager").GetComponent<GameState>();
            gameState.AwardPoints(pointsForDestroying);

            if (Reloads)
            {
                gameState.Reload();
            }

            if (tag.Equals("CagedRhinos"))
            {
                gameState.RhinoRescued();
            }

            // and destroy this one.
            Destroy(gameObject);
        }
        else
        {
            colliding = false;
        }
    }
}