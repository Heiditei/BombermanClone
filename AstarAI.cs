using UnityEngine;
// Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
// This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

public class AstarAI : MonoBehaviour
{
    public Transform targetPosition;
    public GameObject target;

    private Seeker seeker;

    public Path path;

    private float speed;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    public float repathRate = 0.5f;
    private float lastRepath = float.NegativeInfinity;

    public bool reachedEndOfPath;

    [SerializeField]
    private Animator animator;

    public void Start()
    {
        seeker = GetComponent<Seeker>();

        target = GetComponent<EnemyMovement>().FindClosestTarget(GetComponent<EnemyMovement>().FindTargets());
        targetPosition = target.transform;
        // Start a new path to the targetPosition, call the the OnPathComplete function
        // when the path has been calculated (which may take a few frames depending on the complexity)
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }


    public void OnPathComplete(Path p)
    {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);
        p.Claim(this);
        if (!p.error)
        {
            if (path != null) path.Release(this);
            path = p;
            targetPosition = transform;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
        else
        {
            p.Release(this);
        }
    }


    public void Update()
    {
        GetComponent<EnemyMovement>().MoveToSafety();

        speed = GetComponent<EnemyMovement>().speed;

        target = GetComponent<EnemyMovement>().FindClosestTarget(GetComponent<EnemyMovement>().FindTargets());
        targetPosition = target.transform;

        if (Time.time > lastRepath + repathRate && seeker.IsDone())
        {
            lastRepath = Time.time;

            // Start a new path to the targetPosition, call the the OnPathComplete function
            // when the path has been calculated (which may take a few frames depending on the complexity)
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }

        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;

        // The distance to the next waypoint in the path
        float distanceToWaypoint;

        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }
        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Animate(dir);
        // Multiply the direction by our desired speed to get a velocity
        Vector3 velocity = dir * speed * speedFactor;

        // Move the agent using the CharacterController component
        // Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
        //controller.SimpleMove(velocity);

        // If you are writing a 2D game you may want to remove the CharacterController and instead use e.g transform.Translate
        //transform.position += velocity * Time.deltaTime;
        transform.Translate(velocity*Time.deltaTime);
    }


    public void Animate(Vector3 direction)
    {
        Animator animator = GetComponent<Animator>();
        if (direction.x > transform.position.x)
        {
            animator.SetInteger("DirectionX", 1); //Tells the animator to use the walk right animation
        }
        if (direction.x < transform.position.x)
        {
            animator.SetInteger("DirectionX", -1); //Tells the animator to use the walk left animation
        }
        else
        {
            animator.SetInteger("DirectionX", 0); //If no movement, animation stops/idle animation
        }


        if (direction.y < transform.position.y)
        {
            animator.SetInteger("DirectionY", -1);  //Tells the animator to use the walk down animation
        }
        else if (direction.y > transform.position.y)
        {
            animator.SetInteger("DirectionY", 1);  //Tells the animator to use the walk up animation
        }
        else
        {
            animator.SetInteger("DirectionY", 0); //If no movement, animation stops/idle animation
        }
    }
}