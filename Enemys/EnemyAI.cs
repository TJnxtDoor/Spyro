using UnityEngine;
public class EnemyAI : MonoBehaviour 
{
    public float moveSpeed = 3f;
    private Transform player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage(5);
        }
    }
}