using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -5);

    void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            offset = Quaternion.Euler(0, 90, 0) * offset;
        }
    }
    

}