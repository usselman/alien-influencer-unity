using UnityEngine;

public class UfoSuction : MonoBehaviour
{
    public float suctionRadius = 10f;
    public float suctionPower = 50f;
    public int scorePoint = 10;
    public LayerMask personLayer;
    public GameObject suctionEffectPrefab; // Reference to the particle effect prefab
    public GameObject suctionEffectTrailPrefab; // Reference to the suction trail prefab
    public ParticleSystem suctionConeEffect;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            SuckUpPeople();
            if (!suctionConeEffect.isPlaying)
            {
                suctionConeEffect.Play();
            }

        }
        else
        {
            if (suctionConeEffect.isPlaying)
            {
                suctionConeEffect.Stop();
            }
        }
    }

    private void SuckUpPeople()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, suctionRadius, personLayer);
        foreach (var hitCollider in hitColliders)
        {
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 directionToUfo = (transform.position - hitCollider.transform.position).normalized;
                float verticalDistance = Mathf.Abs(transform.position.y - hitCollider.transform.position.y);
                float horizontalDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(hitCollider.transform.position.x, hitCollider.transform.position.z));
                float suctionEffect = CalculateSuctionEffect(horizontalDistance, verticalDistance);

                rb.AddForce(directionToUfo * suctionPower * suctionEffect, ForceMode.Acceleration);

                if (hitCollider.transform.Find(suctionEffectTrailPrefab.name + "(Clone)") == null) // Check if the trail is not already instantiated for this object
                {
                    Instantiate(suctionEffectTrailPrefab, hitCollider.transform.position, Quaternion.identity, hitCollider.transform);
                }

                if (horizontalDistance <= 2.5f && verticalDistance <= 1.5f)
                {
                    InstantiateSuctionParticleEffect(hitCollider.transform.position); // Instantiate the particle effect
                    Destroy(hitCollider.gameObject);
                    ScoreManager.instance.AddScore(scorePoint); // Increase the score
                }
            }
        }
    }

    private float CalculateSuctionEffect(float horizontalDistance, float verticalDistance)
    {
        float horizontalEffect = 1 - Mathf.Pow(horizontalDistance / suctionRadius, 2);
        float verticalEffect = 1 - Mathf.Pow(verticalDistance / suctionRadius, 2);
        return Mathf.Clamp(horizontalEffect * verticalEffect, 0, 1);
    }

    private void InstantiateSuctionParticleEffect(Vector3 position)
    {
        Instantiate(suctionEffectPrefab, position, Quaternion.identity);
    }

    private void InstantiateSuctionTrail(Vector3 position)
    {
        // Instantiate the suction trail effect
        GameObject trail = Instantiate(suctionEffectTrailPrefab, position, Quaternion.identity);
    }
}
