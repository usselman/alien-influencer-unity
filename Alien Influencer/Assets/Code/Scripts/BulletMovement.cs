using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Transform target; // The target (UFO) the bullet will aim at initially
    public float speed = 5f; // Speed of the bullet
    private Vector3 direction; // The initial direction towards the UFO
    public float lifetime = 5f;
    public ParticleSystem BulletDisappearEffect;

    private void Start()
    {
        if (target == null)
        {
            // Find the UFO object by tag or name if not set in the inspector
            GameObject targetObject = GameObject.FindGameObjectWithTag("UFO");
            if (targetObject != null)
            {
                target = targetObject.transform;
            }
        }

        // Calculate and store the initial direction towards the target
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
            // Make the bullet face the initial direction it's moving
            transform.Rotate(90, 0, 0);
            transform.LookAt(target.position);
        }

        // Optionally instantiate the disappear effect if needed
        if (BulletDisappearEffect != null)
        {
            Instantiate(BulletDisappearEffect, transform.position, Quaternion.identity);
        }

        // Destroy the bullet after its lifetime
        DestroyBullet();
    }

    void Update()
    {
        if (target != null)
        {
            // Move the bullet in the stored initial direction
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject, lifetime);
    }
}
