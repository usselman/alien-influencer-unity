using UnityEngine;

public class UfoSuction : MonoBehaviour
{
    public float suctionRadius = 10f; // Radius of the suction effect
    public float suctionPower = 50f; // Strength of the suction effect
    public LayerMask personLayer; // Layer of the person objects for optimized searching

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            SuckUpPeople();
        }
    }

    private void SuckUpPeople()
    {
        // Find all colliders within the suction radius that are on the "Person" layer
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, suctionRadius, personLayer);
        foreach (var hitCollider in hitColliders)
        {
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 directionToUfo = (transform.position - hitCollider.transform.position).normalized;
                // Separate distance calculation into vertical and horizontal components for differential falloff
                float verticalDistance = Mathf.Abs(transform.position.y - hitCollider.transform.position.y);
                float horizontalDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(hitCollider.transform.position.x, hitCollider.transform.position.z));

                // Calculate suction effect with more aggressive falloff
                float suctionEffect = CalculateSuctionEffect(horizontalDistance, verticalDistance);

                rb.AddForce(directionToUfo * suctionPower * suctionEffect, ForceMode.Acceleration);

                // Destroy the object if it's close enough to the UFO
                if (hitCollider.transform.position.y >= 10f)
                {
                    Destroy(hitCollider.gameObject);
                }
            }
        }
    }

    private float CalculateSuctionEffect(float horizontalDistance, float verticalDistance)
    {
        // Apply a more aggressive falloff for the suction effect based on distance
        float horizontalEffect = 1 - Mathf.Pow(horizontalDistance / suctionRadius, 2);
        float verticalEffect = 1 - Mathf.Pow(verticalDistance / suctionRadius, 2);

        // Combine effects, prioritizing vertical alignment (directly underneath) for stronger suction
        return Mathf.Clamp(horizontalEffect * verticalEffect, 0, 1);
    }
}
