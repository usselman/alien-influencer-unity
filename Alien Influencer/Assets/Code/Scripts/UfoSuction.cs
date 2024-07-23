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
    public GameObject pikminSpherePrefab; // Reference to the PikminSphere prefab

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
                float distanceToUfo = Vector3.Distance(transform.position, hitCollider.transform.position);

                // Calculate the speed based on the distance to the UFO (closer = faster)
                float speed = suctionPower * (1 - Mathf.Clamp01(distanceToUfo / suctionRadius));

                // Set the velocity directly towards the bottom center of the UFO
                rb.velocity = directionToUfo * speed;

                if (hitCollider.transform.Find(suctionEffectTrailPrefab.name + "(Clone)") == null) // Check if the trail is not already instantiated for this object
                {
                    Instantiate(suctionEffectTrailPrefab, hitCollider.transform.position, Quaternion.identity, hitCollider.transform);
                }

                if (distanceToUfo <= 5f)
                {
                    InstantiateSuctionParticleEffect(hitCollider.transform.position); // Instantiate the particle effect
                    SpawnPikminSphere();
                    Destroy(hitCollider.gameObject);
                    ScoreManager.instance.AddScore(scorePoint); // Increase the score
                }
            }
        }
    }

    private void SpawnPikminSphere()
    {
        // Spawn a PikminSphere directly under the UFO
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        Instantiate(pikminSpherePrefab, spawnPosition, Quaternion.identity);
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
