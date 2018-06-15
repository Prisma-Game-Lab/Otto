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
    public static bool chasingPlayer;
    private float timeSinceLastSighted;
    private float now;
    public float giveUpTime = 20;

    //if enemy is stationary he only turns on the direction of waypoint
    public bool stationary;
    private Vector3 direction;
    private float angle;

    public Material AlertEyeMaterial;
    public Material AlertVisionMaterial;
    private Material _stdEyeMaterial;
    private Material _stdVisionMaterial;

    //Color.(255, 244, 0, 160);


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
        agent.updatePosition = !stationary;

        // Pega todos os filhos do inimigo e salva aas cores originais dos olhos e da visão
        Renderer[] rend_child = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in rend_child)
        {
            if (child.CompareTag("Visao Inimigo"))
            {
                _stdVisionMaterial = child.GetComponent<Renderer>().material;
            } else if (child.CompareTag("Olhos Inimigo"))
            {
                _stdEyeMaterial = child.GetComponent<Renderer>().material;
            }
        }



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
            //change_view_color(_stdColor); // Alteração Gustavo
            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }
        else
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
        if (now > giveUpTime + timeSinceLastSighted)
        {
            agent.speed = normalSpeed;
            returnViewColor();
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
        changeViewColor();


        if (agent.remainingDistance < 1.3f)
        {
            agent.speed = normalSpeed;
        }

    }

    // Muda para as cores de perseguição 
    private void changeViewColor()
    {
        Renderer[] rend_child = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in rend_child)
        {
            if (child.CompareTag("Visao Inimigo"))
            {
                child.GetComponent<Renderer>().material = AlertVisionMaterial;
            }
            else if (child.CompareTag("Olhos Inimigo"))
            {
                child.GetComponent<Renderer>().material = AlertEyeMaterial;
            }
        }
    }

    // Muda para as cores originais
    private void returnViewColor()
    {
        Renderer[] rend_child = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in rend_child)
        {
            if (child.CompareTag("Visao Inimigo"))
            {
                child.GetComponent<Renderer>().material = _stdVisionMaterial;
            }
            else if (child.CompareTag("Olhos Inimigo"))
            {
                child.GetComponent<Renderer>().material = _stdEyeMaterial;
            }
        }
    }
}