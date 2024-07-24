using UnityEngine;

public class MoveTowardsExample : MonoBehaviour
{
    public Transform target;
    public float speed;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
