using UnityEngine;

/// <summary>
/// The purpose of this component is to allow a GameObject to be alive only
/// for a given period of time. Once the life time is up, the object is
/// destroyed.
/// </summary>
public class LimitedLifetime : MonoBehaviour {
    [Tooltip("The life time of the Game Object (in seconds).")]
    public float lifetime;

	void Update () {
        // Decrease the time to live.
        lifetime -= Time.deltaTime;

        // Time to die?
        if (lifetime <= 0)
            // It is, so destroy the object.
            Destroy(gameObject);
	}
}
