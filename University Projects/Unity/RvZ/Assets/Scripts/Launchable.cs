using UnityEngine;

/// <summary>
/// This component is added to any GameObject that has a RigidBody2D component that the
/// user can launch by applying a force to it. The object is launched by touching it, then
/// dragging in the opposite direction to be launched.
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class Launchable : MonoBehaviour
{
    // This is the rigid body belonging to the GameObject to which this component is
    // attached. It will be used for applying forces to the object.
    protected Rigidbody2D rb;

    // A reference to the main camera. Used to convert user input actions in SCREEN space
    // into WORLD space that can be used with GameObjects.
    protected Camera mainCamera;

    [Tooltip("The maximum force that the object will be launched with.")]
    public float launchForce;

    [Tooltip("The radius (from the game object) that the user needs to drag to launch it with the maximum force.")]
    public float maxLaunchRadius;

    [Tooltip("The radius (from the game object) that the user needs to exceed to launch the object. If the radius is less than this, the launch is cancelled instead.")]
    public float minLaunchRadius;

    [Tooltip("The prefab of the range finder object that will be launched while range finding.")]
    public Rigidbody2D rangeFinderPrefab;

    [Tooltip("The period between range finder object launches.")]
    public float rangeFinderLaunchPeriod;

    [Tooltip("The mass of the entire launchable object.")]
    public float launchableMass = 0;

    protected float timeToLaunch;

    protected GameState gameState;

    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to the RigidBody2D component attached to the GameObject this
        // component is attached too.
        rb = GetComponent<Rigidbody2D>();

        // Get a reference to the GameObject named "Main Camera" and get its Camera
        // component.
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // Find game state object.
        gameState = GameObject.Find("LevelManager").GetComponent<GameState>();

        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool isDragging = false;
    protected Vector2 screenPos = Vector2.zero;
    protected Vector2 direction = Vector2.zero;

    private void OnMouseDown()
    {
        isDragging = true;
        screenPos = mainCamera.WorldToScreenPoint(rb.transform.position);
        direction = (Vector2)Input.mousePosition - screenPos;

        // Reset launch time.
        timeToLaunch = rangeFinderLaunchPeriod;

        ProjectileFollow temp = mainCamera.GetComponent<ProjectileFollow>();
        temp.projectile = transform;
    }

    private void OnMouseDrag()
    {
        screenPos = mainCamera.WorldToScreenPoint(rb.transform.position);
        direction = (Vector2)Input.mousePosition - screenPos;

        Debug.Log(gameObject.name + " was dragged. distance = " + direction.magnitude);

        // Update range finding object time to launch.
        timeToLaunch -= Time.deltaTime;

        // Time to launch range finder object?
        if(timeToLaunch <= 0)
        {
            // Reset timer.
            timeToLaunch = rangeFinderLaunchPeriod;

            // Should a range finder object be launched? i.e. user dragged past minimum distance?
            if(direction.magnitude > minLaunchRadius)
            {
                // They have...
                // Calculate the amount of force to apply. This is proportional to the distance from
                // the GameObject on the screen and the mouse cursor.
                float force = Mathf.Lerp(0, launchForce, direction.magnitude / maxLaunchRadius);

                // Create the range finder object to be launched.
                Rigidbody2D rangeFinder = GameObject.Instantiate(rangeFinderPrefab, transform.position, Quaternion.identity);

                // Ensure that the mass of this GameObject and the range finder are the same so that the movement looks
                // the same.
                if (launchableMass == 0)
                    rangeFinder.mass = rb.mass;
                else
                    rangeFinder.mass = launchableMass;

                // Launch the object. The user drags in the opposite direction they want to launch,
                // so change sign to mirror it.
                rangeFinder.AddForce(-direction.normalized * force);
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        screenPos = mainCamera.WorldToScreenPoint(rb.transform.position);
        direction = (Vector2)Input.mousePosition - screenPos;

        // Attempt to launch the object. 
        // Has the user dragged past the minimum radius to launch the object?
        // Are there launch attempts remaining?
        if ((gameState.shotsRemaining > 0) && (direction.magnitude > minLaunchRadius))
        {
            // They have...
            // Calculate the amount of force to apply. This is proportional to the distance from
            // the GameObject on the screen and the mouse cursor.
            float force = Mathf.Lerp(0, launchForce, direction.magnitude / maxLaunchRadius);

            // Launch the object. The user drags in the opposite direction they want to launch,
            // so change sign to mirror it.
            rb.AddForce(-direction.normalized * force);

            source.PlayOneShot(sound);

            // Update the shots remaining (and the UI).
            gameState.Shoot();
        }
    }
}
