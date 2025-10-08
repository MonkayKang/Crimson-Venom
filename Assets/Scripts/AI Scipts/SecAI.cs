using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecAI : MonoBehaviour
{
    public enum AIState { Patrol, Chase } // Added Search State
    public AIState currentState = AIState.Patrol;

    // Patrol
    public Transform[] waypoints; // Places it Will go to
    private int currentIndex = 0; // Waypoint "1" is it's first stop

    // Chase
    public Transform player; // The player's position
    public float viewRange = 10f;       // How far enemy can see
    public float viewAngle = 120f;      // Degree of Vision Cone

    // Search
    private Vector3 lastKnownPosition; // Store last known player position

    // Floats
    public float patrolSP = 3.5f; // Original Speed
    public float MaxSP = 20f; // Chase Speed;

    private NavMeshAgent agent;

    // Bools
    public static bool playerSpotted;
    public float hitTIMER = 0f;

    void Start()
    {
        hitTIMER = 0f;
        playerSpotted = false;
        agent = GetComponent<NavMeshAgent>(); // Find component
        if (waypoints.Length > 0)
        {
            currentIndex = Random.Range(0, waypoints.Length); // Pick a random waypoint
            agent.SetDestination(waypoints[currentIndex].position); // Go toward it
        }
    }

    void Update()
    {
        
        bool canSeePlayer = CanSeePlayer(); // Set the bool to reflect a Condition "CanSeePlayer()"

        if (currentState == AIState.Patrol && (canSeePlayer || playerSpotted) || hitTIMER >= 25f)
        {
            currentState = AIState.Chase; // Chase the player
        }
        else if (currentState == AIState.Chase && (!canSeePlayer && !playerSpotted))
        {
            StartCoroutine(CoyoteTime()); // Give it a sec to break chase
        }

            if (currentState == AIState.Patrol) // If state patrol
            {
                Patrol(); // Do this
            }
            else if (currentState == AIState.Chase) // if state chase
            {
                Chase(); // Do this
            }

        if (hitTIMER > 0f)
        {
            hitTIMER = Mathf.Min(hitTIMER - 0.1f, 100f); // Regress at a speed. MAX HOLD
        }
        if (hitTIMER < 0f)
        {
            hitTIMER = 0f;
        }

        
    }

    void Patrol()
    {
        UICounter.inChase = false; // Hiding during chase
        if (!agent.pathPending && agent.remainingDistance < 0.5f) // If path is not pending and the remaining distance to the waypoint is below a threshold
        {
            agent.speed = patrolSP; // Patrol Speed
            GoToNextWaypoint(); // go to next waypoint
        }
    }

    void Chase()
    {
        UICounter.inChase = true; // Hiding during chase
        agent.speed = MaxSP; // Chase Speed
        agent.SetDestination(player.position); // Go towards player
        lastKnownPosition = player.position; // Update last known position constantly while chasing
    }


    void GoToNextWaypoint()
    {
        int nextIndex; // the number that would be next

        do
        {
            nextIndex = Random.Range(0, waypoints.Length); // Pick a random waypoint
        }
        while (nextIndex == currentIndex && waypoints.Length > 1); // Avoid picking the same one twice in a row

        currentIndex = nextIndex; // The new waypoint is the random one
        agent.SetDestination(waypoints[currentIndex].position); // "SET SAIL MATEY'S!"
    }

    bool CanSeePlayer() // Condition if it can see the player
    {
        Vector3 dirToPlayer = player.position - transform.position; // Calculate the direction from player to AI
        float distanceToPlayer = dirToPlayer.magnitude; // Calculate the distance from us (AI) to the player

        Debug.DrawRay(transform.position + Vector3.up * 1.5f, dirToPlayer.normalized * viewRange, Color.magenta);
        // Check distance
        if (distanceToPlayer > viewRange) return false; // If the distance from the player is greater than the view range set, it cannot see them

        // Check angle
        float angle = Vector3.Angle(transform.forward, dirToPlayer.normalized); // Set the angle
        if (angle > viewAngle / 2f) return false; // If the angle is greater than the set angle

        // Check raycast (line of sight)
        if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer.normalized, out RaycastHit hit, viewRange)) // Use the direction to player and the view angle
        {
            if (hit.collider.CompareTag("player"))
            {
                return true; // Player is visible
            }
        }


        return false; // Player is not visible
    }

    IEnumerator CoyoteTime()
    {
        bool canSeePlayer = CanSeePlayer(); // Set the bool to reflect a Condition "CanSeePlayer()"
        currentState = AIState.Chase;
        yield return new WaitForSeconds(2f); // Wait
        if (currentState == AIState.Chase && (!canSeePlayer && !playerSpotted)) // If they still cant see the player
        {
            currentState = AIState.Patrol; // Break off the chase
        }
    }
}
