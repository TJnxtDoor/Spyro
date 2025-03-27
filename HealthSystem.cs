using UnityEngine;

public class HealthSystem : MonoBehaviour
{   
    public int maxHealth = 100;
    public int CurrentHealth { get; private set;}

    void Start() => CurrentHealth = maxHealth;


    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth -damage, 0, maxHealth);
        if(CurrentHealth <= 0) Die();
    }


    private void Die()
    {
        //play Death Animation
        Destroy(gameObject, 1f);
    }

public GameObject projectilePrefab;

private void ShootProjectile()
{
    GameObject projectile = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
    Vector3 direction = (player.position - transform.position).normalized;
    projectile.GetComponent<Rigidbody>().velocity = direction * 20f;
}

public GameObject projectilePrefab;

private void ShootProjectile()
{
    GameObject projectile = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
    Vector3 direction = (player.position - transform.position).normalized;
    projectile.GetComponent<Rigidbody>().velocity = direction * 20f;
}