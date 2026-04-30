using UnityEngine;
public class EnemyAI : MonoBehaviour 
{
    public float moveSpeed = 3f;
    public float maxHealth = 20f;
    public float currentHealth;
    public float weaponDamage = 5f;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    private Transform player;
    private Vector3 basePosition;
    private bool isRetreating = false;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        basePosition = transform.position;
        lastAttackTime = 0f;
    }

    void Update()
        // when Enemy healt is lower than 10, it will retreat to base to heal, otherwise it will chase the player
    {
        if (currentHealth < 10 && !isRetreating)
        {
            RetreatToBase();
        }
        else if (isRetreating)
        {
            ReturnToBase();
        }
        else if (player != null)
        {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
        // Retreat to base when health is low
    void RetreatToBase()
    {
        isRetreating = true;
    }

    void ReturnToBase()
    {
        transform.LookAt(basePosition);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                // Weapon damages player with 5 damage per hit, with a cooldown of 1 second
        if (Vector3.Distance(transform.position, basePosition) < 0.5f)
        {
            isRetreating = false;
            currentHealth = maxHealth;
        }
    }
        // Take damage from player attacks
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                GameManager.Instance.TakeDamage(Mathf.RoundToInt(weaponDamage));
                lastAttackTime = Time.time;
            }
        }
    }
}
