using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public bool campType;

    public float detectRadius = 5f;
    public float returnToSpawnRadius = 25f;
    private Animator animator; 

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 target;
    private Vector3 spawnLocation;

    private CharacterCombat enemyCombat;
    private CharacterStats playerStats;
    private ZombieStats zombieStats;

    private enum EnemyStates { Passive, Attack, Hurt, Dead };
    private EnemyStates enemyState = EnemyStates.Passive;

    private float distanceToPlayer;
    private float distanceToSpawn;
    private float randomAngle;

	// Use this for initialization
	void Start ()
    {
        spawnLocation = transform.position;
        target = spawnLocation;
        agent = GetComponent<NavMeshAgent>();
        player = PlayerStats.instance.player.transform;
        animator = GetComponent<Animator>();
        enemyCombat = GetComponent<CharacterCombat>();
        zombieStats = GetComponent<ZombieStats>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!zombieStats.isDeath())
        {
            distanceToPlayer = Vector3.Distance(player.position, transform.position);
            distanceToSpawn = Vector3.Distance(spawnLocation, transform.position);

            switch (enemyState)
            {
                case EnemyStates.Passive:

                        if (!agent.pathPending)
                            if (agent.remainingDistance <= agent.stoppingDistance)
                                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                                    if (campType)
                                        animator.SetBool("idle", true);
                                    else
                                        RandomMovement();

                    if (distanceToPlayer <= detectRadius && distanceToSpawn < returnToSpawnRadius)
                    {
                        animator.SetBool("idle", false);
                        enemyState = EnemyStates.Attack;
                        MoveToTarget();

                    }
                    break;

                case EnemyStates.Attack:

                    MoveToTarget();

                    if (distanceToPlayer <= (agent.stoppingDistance + 0.6f))
                    {
                        animator.SetBool("attack", true);
                        playerStats = player.GetComponent<CharacterStats>();

                        if (playerStats != null)
                            enemyCombat.Attack(playerStats);
                    }
                    else
                        animator.SetBool("attack", false);

                    if (distanceToSpawn >= returnToSpawnRadius)
                    {
                        LookAtTarget(spawnLocation);
                        agent.SetDestination(spawnLocation);
                        enemyState = EnemyStates.Passive;
                    }
                    break;
            }
        }
	}

    void MoveToTarget()
    {
        agent.SetDestination(player.position);

        if (distanceToPlayer <= agent.stoppingDistance)
        {
            LookAtTarget(player.position);
        }
    }

    void LookAtTarget(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void RandomMovement()
    {
        if (!zombieStats.isDeath())
        {
            randomAngle = Random.Range(0f, Mathf.PI * 2f);
            target = new Vector3(spawnLocation.x + 5f * Mathf.Sin(randomAngle), spawnLocation.y, spawnLocation.z + 5f * Mathf.Cos(randomAngle));
            LookAtTarget(target);
            agent.SetDestination(target);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

    }
}
