using UnityEngine;

public class UfoSuction : MonoBehaviour
{
    public float suctionRadius = 10f;
    public float suctionPower = 50f;
    public int scorePoint = 10;
    public LayerMask personLayer;
    public GameObject suctionEffectPrefab;
    public GameObject suctionEffectTrailPrefab;
    public ParticleSystem suctionConeEffect;
    public GameObject pikminSpherePrefab;
    public GameObject beamLight;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            SuckUpPeople();
            if (!suctionConeEffect.isPlaying)
            {
                suctionConeEffect.Play();
                beamLight.SetActive(true);
            }
        }
        else
        {
            if (suctionConeEffect.isPlaying)
            {
                suctionConeEffect.Stop();
                beamLight.SetActive(false);
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

                float speed = suctionPower * (1 - Mathf.Clamp01(distanceToUfo / suctionRadius));


                rb.velocity = directionToUfo * speed;

                if (hitCollider.transform.Find(suctionEffectTrailPrefab.name + "(Clone)") == null)
                {
                    Instantiate(suctionEffectTrailPrefab, hitCollider.transform.position, Quaternion.identity, hitCollider.transform);
                }

                if (distanceToUfo <= 5f)
                {
                    InstantiateSuctionParticleEffect(hitCollider.transform.position);
                    SpawnPikminSphere();
                    Destroy(hitCollider.gameObject);
                    /*
                    ! Probably need better way to add score + manage different point values
                    */
                }
            }
        }
    }

    private void SpawnPikminSphere()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        Instantiate(pikminSpherePrefab, spawnPosition, Quaternion.identity);
    }

    private void InstantiateSuctionParticleEffect(Vector3 position)
    {
        Instantiate(suctionEffectPrefab, position, Quaternion.identity);
    }

    private void InstantiateSuctionTrail(Vector3 position)
    {
        GameObject trail = Instantiate(suctionEffectTrailPrefab, position, Quaternion.identity);
    }
}
