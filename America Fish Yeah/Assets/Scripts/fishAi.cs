using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class fishAi : MonoBehaviour
{
    private GameObject player;
    public GameObject Attractor;

    private NavMeshAgent agent;

    [SerializeField] private LayerMask groundLayer, playerLayer;

    private Vector3 destPoint;
    private bool walkPointSet;
    [SerializeField] private float range;

    [SerializeField] private float sightRange;
    public bool attractorInSight;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        Attractor = GameObject.Find("Attractor");
    }

    void Update()
    {
        attractorInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        
        if(!attractorInSight) Patrol();
        if(attractorInSight) Attracted();
    }

    void Patrol()
    {
        if (!walkPointSet) SearchForDest();
        if (walkPointSet) agent.SetDestination(destPoint);
        if (Vector3.Distance(transform.position, destPoint) < 10) walkPointSet = false;
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }

    void Attracted()
    {
        agent.SetDestination(Attractor.transform.position);
    }
}
