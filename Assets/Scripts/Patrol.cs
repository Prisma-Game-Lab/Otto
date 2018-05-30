// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private Transform player;

    public float normalSpeed = 3;
    public float chaseSpeed = 3;
    private bool chasingPlayer;
    private float timeSinceLastSighted;
    private float now;
    public float giveUpTime = 20;

    //if enemy is stationary he only turns on the direction of waypoint
    public bool stationary;
    private Vector3 direction;
    private float angle;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
        agent.updatePosition = !stationary;

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        chasingPlayer = false;
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
        //print("Follow waypoint");
        // Set the agent to go to the currently selected destination.
        if (!chasingPlayer)
        {
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }else
        {
            agent.destination = player.position;
        }

    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        //if stationary
        if (stationary && !chasingPlayer)
        {
             agent.updatePosition = false;
             direction = agent.destination - agent.transform.position;
             angle = Vector3.Angle(direction, agent.transform.forward);//Draw the angle in front of the AI
             if (angle == 0)//This is the angle that the AI can see
             {
                    //agent.Warp(this.transform.position);
                    GotoNextPoint();
             }

        }

        // desiste de seguir player se passar GiveUpTimem sem ver player
        now = Time.time;
        if ( now> giveUpTime+ timeSinceLastSighted)
        {
            agent.speed = normalSpeed;
            chasingPlayer = false;
        }
    }


    public void GotoPlayer()
    {

        // Set the agent to go to the currently selected destination.
        agent.updatePosition = true;
        agent.destination = player.position;
        chasingPlayer = true;
        timeSinceLastSighted = Time.time;
        agent.speed = chaseSpeed;
        if (agent.remainingDistance < 1.3f)
        {
            agent.speed = normalSpeed;
            print("DANO");
        }

    }
}