using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class fishAi : MonoBehaviour
{
    public GameObject player; 
    public GameObject Attractor;

    private NavMeshAgent agent;

    [SerializeField] private LayerMask groundLayer, appatLayer;

    private Vector3 destPoint;
    private bool walkPointSet;
    public float enemyDistanceRun = 5f;

    private bool isFleeing;

    
    [SerializeField] private float range;

    [SerializeField] private float sightRange;
    public bool attractorInSight;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player"); //pas opti du tout
    }

    void Update()
    {
        Flees(); //les poissons fuit le joueur toujours en prio au dessus d'etre attiré

        if (!isFleeing)
        {
            attractorInSight = Physics.CheckSphere(transform.position, sightRange, appatLayer);

            if (!attractorInSight)
            {
                Patrol(); //le pb vient de là, ils ne vont pas flees à cause de ca
            }
        
            if (attractorInSight)
            {
                Attracted();
            }
        }
    }

    void Patrol() //Patrouille avec des déplacements random
    {
        if (!walkPointSet)
        {
            SearchForDest();
        }

        if (walkPointSet)
        {
            agent.SetDestination(destPoint);
        }

        if (Vector3.Distance(transform.position, destPoint) < 10)
        {
            walkPointSet = false;
        }
    }

    void SearchForDest() //recherche des nouveaux points vers lesquels se déplacer
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }

    void Attracted() //Attirer par l'appat ==> TROUVER UNE SOLUTION AU PLUS VITE
    {
        agent.SetDestination(Appat.instance.gameObject.transform.position); //? nullref
    }

    void Flees() //Fuite des poissons face au joueur ==> ne fonctionne pas et je ne comprend pas du tout pq //faire un triggerEnter sur une sphere = plus opti
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        //Debug.Log(distance + " "+ enemyDistanceRun);

        if (distance < enemyDistanceRun)
        {
            isFleeing = true;
            
            Debug.Log("0");
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer; //newPos = dirToPlayer.normalized;

            agent.SetDestination(newPos); //permet de définir une nouvelle dest au navmesh agent
        }
        else
        {
            isFleeing = false;
        }
    }
    
}
