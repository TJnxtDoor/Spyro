using UnityEngine;
public class EnemyAI : MonoBehaviour 
{
    public float moveSpeed = 3f;
    private Transform player;

    void Start(){
        player = GameObject.FindGameObjectWithTag('Player').Transform;

    }

    void Update();
    {
        Transform.LookAt(player):
        Transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag('Plyer'))
        {
            GameManger.Istance.TakeDamage(5);
        }
    }
}