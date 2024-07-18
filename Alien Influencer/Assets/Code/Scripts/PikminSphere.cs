using UnityEngine;

public class PikminSphere : MonoBehaviour
{
    public float speed = 5f; // Speed of the PikminSphere
    public LayerMask personLayer; // Layer mask for identifying PersonSpheres

    private Transform target; // Current target PersonSphere

    private void Start()
    {
        FindNewTarget();
    }

    private void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
        else
        {
            FindNewTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if we reached the target
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            ConvertToPikmin(target.gameObject);
            FindNewTarget();
        }
    }

    private void ConvertToPikmin(GameObject person)
    {
        Vector3 position = person.transform.position;
        Destroy(person);
        Instantiate(gameObject, position, Quaternion.identity); // Spawn a new PikminSphere at the person's position
    }

    private void FindNewTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100f, personLayer);
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = hitCollider.transform;
            }
        }
    }
}
