using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Transform target; // The target (UFO) the bullet will aim at initially
    public float speed = 5f; // Speed of the bullet
    private Vector3 direction; // The initial direction towards the UFO
    public float lifetime = 5f;

    private void Start()
    {
        if (target == null)
        {
            // Find the UFO object by tag or name if not set in the inspector
            target = GameObject.FindGameObjectWithTag("UFO").transform;
        }

        // Calculate and store the initial direction towards the target
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
            // Make the bullet face the initial direction it's moving
            transform.Rotate(90, 0, 0);
            transform.LookAt(target.position);
        }

        // Destroy the bullet after 3 seconds
        Destroy(gameObject, lifetime);

        // Optionally, you can comment out the dynamic targeting logic below
        /*
        if(target != null)
        {
            MoveTowardsTarget();
        }
        */
    }

    void Update()
    {
        // Move the bullet in the stored initial direction
        transform.position += direction * speed * Time.deltaTime;
    }

    // This method was used for dynamic targeting, now commented out
    /*
    void MoveTowardsTarget()
    {
        // Calculate the direction vector from the bullet to the target
        Vector3 direction = (target.position - transform.position).normalized;
        // Move the bullet in the direction of the target
        transform.position += direction * speed * Time.deltaTime;

        // Make the bullet face the direction it's moving
        transform.LookAt(target);
    }
    */
}
