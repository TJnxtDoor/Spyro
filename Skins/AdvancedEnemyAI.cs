// AdvancedEnemyAI.cs
using UnityEngine;
using System.Collections.Generic;

public enum EnemyState { Patrol, Chase, Attack, Retreat }

public class AdvancedEnemyAI : MonoBehaviour
{
    [Header("States")]
    public EnemyState currentState;
    private Transform player;

    [Header("Patrol Settings")]
    public List<Transform> waypoints = new List<Transform>();
    private int currentWaypoint = 0;
    public float patrolSpeed = 3f;
    public float waypointWaitTime = 2f;
    private float waitCounter;

    [Header("Chase/Attack Settings")]
    public float chaseSpeed = 6f;
    public float attackDistance = 2f;
    public float detectionRadius = 10f;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    [Header("Retreat Settings")]
    public float retreatHealthThreshold = 30f;
    public float retreatSpeed = 4f;
    public float retreatDistance = 15f;

    [Header("Misc")]
    public LayerMask obstacleMask;
    private HealthSystem healthSystem;

    private void Start()
    {
        // Initialize player and health system references
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthSystem = GetComponent<HealthSystem>();

        ApplyDifficultyModifiers();

        currentState = EnemyState.Patrol;
    }

    private void ApplyDifficultyModifiers()
    {
        DifficultySettings diff = GameManager.Instance.difficultySettings;

        patrolSpeed *= diff.enemySpeedMultiplier;
        chaseSpeed *= diff.enemySpeedMultiplier;
        retreatSpeed *= diff.enemySpeedMultiplier;
        detectionRadius *= diff.enemyDetectionMultiplier;
        attackCooldown /= diff.enemySpeedMultiplier;

        GetComponent<HealthSystem>().maxHealth =
            Mathf.RoundToInt(GetComponent<HealthSystem>().maxHealth * diff.enemyHealthMultiplier);
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                PatrolBehavior();
                CheckForPlayer();
                break;

            case EnemyState.Chase:
                ChaseBehavior();
                CheckAttackConditions();
                CheckRetreatConditions();
                break;

            case EnemyState.Attack:
                AttackBehavior();
                break;

            case EnemyState.Retreat:
                RetreatBehavior();
                break;
        }
    }

    // --- State Behaviors ---
    private void PatrolBehavior()
    {
        if (waypoints.Count == 0) return;

        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        transform.position += direction.normalized * patrolSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.5f)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waypointWaitTime)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
                waitCounter = 0f;
            }
        }
    }

    private void ChaseBehavior()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        if (!HasLineOfSight()) return;

        // Avoid obstacles using raycast
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRadius, obstacleMask))
        {
            Vector3 avoidDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
            directionToPlayer += avoidDirection * 2f;
        }

        transform.position += directionToPlayer.normalized * chaseSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    private void AttackBehavior()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            // Implement attack logic (e.g., melee, projectile)
            PerformAttack();
            lastAttackTime = Time.time;
        }

        if (Vector3.Distance(transform.position, player.position) > attackDistance * 1.5f)
        {
            currentState = EnemyState.Chase;
        }
    }

    private void RetreatBehavior()
    {
        Vector3 retreatDirection = (transform.position - player.position).normalized;
        transform.position += retreatDirection * retreatSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, player.position) > retreatDistance)
        {
            currentState = EnemyState.Patrol;
        }
    }

    // --- Helper Methods ---
    private bool HasLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, detectionRadius))
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }

    private void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRadius && HasLineOfSight())
        {
            currentState = EnemyState.Chase;
        }
    }

    private void CheckAttackConditions()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentState = EnemyState.Attack;
        }
    }

    private void CheckRetreatConditions()
    {
        if (healthSystem != null && healthSystem.CurrentHealth < retreatHealthThreshold)
        {
            currentState = EnemyState.Retreat;
        }
    }

    private void PerformAttack()
    {
        // Example: Melee attack
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            player.GetComponent<HealthSystem>()?.TakeDamage(10);
        }
    }

    // Visualize detection radius and attack range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}