using UnityEngine;

public class UfoSuction : MonoBehaviour
{
    public float suctionRadius = 20f; // Radius of the suction effect
    public float suctionPower = 10f; // Strength of the suction effect

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            SuckUpPeople();
        }
    }

    void SuckUpPeople()
    {
        // Find all colliders within the suction radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, suctionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Person")) // Ensure the object has a "Person" tag
            {
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 directionToUfo = (transform.position - hitCollider.transform.position).normalized;
                    // Apply force based on distance to UFO
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    float suctionEffect = 1 - (distance / suctionRadius); // Stronger when closer
                    rb.AddForce(directionToUfo * suctionPower * suctionEffect, ForceMode.Acceleration);

                    // Destroy the object if it's close enough to the UFO
                    if (hitCollider.transform.position.y >= 9f && distance < 2.5f)
                    {
                        Destroy(hitCollider.gameObject);
                    }
                }
            }
        }
    }
}
