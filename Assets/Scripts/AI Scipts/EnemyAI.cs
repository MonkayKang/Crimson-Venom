using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public enum AIState { Patrol, Chase, Search } // Added Search State
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
    public float searchDuration = 3f;  // How long enemy waits at last known position
    private float searchTimer = 0f;    // Internal timer for search

    private NavMeshAgent agent;

    public bool isChaser = false;

    public float hitTIMER = 0f;

    void Start()
    {
        hitTIMER = 0f;
        agent = GetComponent<NavMeshAgent>(); // Find component
        if (waypoints.Length > 0)
        {
            currentIndex = Random.Range(0, waypoints.Length); // Pick a random waypoint
            agent.SetDestination(waypoints[currentIndex].position); // Go toward it
        } 
    }

    void Update()
    {
        Debug.Log(hitTIMER.ToString());
        if (isChaser) // The final chase
        {
            Chase(); // Will always follow the player
        }

        if (hitTIMER > 0f)
        {
            hitTIMER = Mathf.Min(hitTIMER - 0.1f, 100f); // Regress at a speed. MAX HOLD
        }
        if (hitTIMER < 0f)
        {
            hitTIMER = 0f; // Never below 0
        }


        if (!isChaser) // The roaming one
        {
            bool canSeePlayer = CanSeePlayer(); // Set the bool to reflect a Condition "CanSeePlayer()"

            if (currentState == AIState.Patrol && canSeePlayer || hitTIMER >= 25f) // If the state is in Patrol but can see the player
            {
                currentState = AIState.Chase; // Chase the player
            }
            else if (currentState == AIState.Chase && !canSeePlayer) // If the state is in chase, but cant see the player
            {
                currentState = AIState.Search; // Go to search state
                lastKnownPosition = player.position; // Save the last known position
                agent.SetDestination(lastKnownPosition); // Head to that position
                searchTimer = 0f; // Reset the search timer
            }
            else if (currentState == AIState.Search && canSeePlayer) // If searching and player comes back in view
            {
                currentState = AIState.Chase; // Resume chase
            }

            if (currentState == AIState.Patrol) // If state patrol
            {
                Patrol(); // Do this
            }
            else if (currentState == AIState.Chase) // if state chase
            {
                Chase(); // Do this
            }
            else if (currentState == AIState.Search) // If state search
            {
                Search(); // Do this
            }
        }
    }

    void Patrol()
    {
        UICounter.inChase = false;
        SecAI.playerSpotted = false;
        if (!agent.pathPending && agent.remainingDistance < 0.5f) // If path is not pending and the remaining distance to the waypoint is below a threshold
        {
            GoToNextWaypoint(); // go to next waypoint
        }
    }

    void Chase()
    {
        UICounter.inChase = true;
        SecAI.playerSpotted = true;
        agent.SetDestination(player.position); // Go towards player
        lastKnownPosition = player.position; // Update last known position constantly while chasing
    }

    void Search()
    {
        SecAI.playerSpotted = false;
        // Wait at last known position for a set time
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            searchTimer += Time.deltaTime; // Count how long enemy has been waiting

            transform.Rotate(Vector3.up * 120f * Time.deltaTime); // Rotates and looks for player

            if (searchTimer >= searchDuration) // If it searched long enough
            {
                currentState = AIState.Patrol; // Return to patrol
                GoToNextWaypoint(); // Resume patrolling
            }
        }
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

}