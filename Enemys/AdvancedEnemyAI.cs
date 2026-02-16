using UnityEngine;
public class EnemyAI : MonoBehaviour

{
    public float moveSpeed = 3f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").Transform;

    }


    void OnCollisionEnter(Collision collision)
        {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage(5);
        }
        }
}
