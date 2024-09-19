using UnityEngine;
using UnityEngine.AI;

public class PikminSphere : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform ufoTarget;
    private bool isReturning = false;
    public float followSpeed = 5f;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    /*
    !! Need to implement NavMeshAgents for the Pikmin to follow the UFO
    !! And for the followers to go where they are told to 
    */

    public void Initialize()
    {
        GameObject ufoObject = GameObject.FindWithTag("UFO");
        if (ufoObject != null)
        {
            ufoTarget = ufoObject.transform;
        }

        //agent = GetComponent<NavMeshAgent>();
        FollowUFO();
    }

    private void FollowUFO()
    {
        if (ufoTarget == null) return;

        //Vector3 groundPosition = new Vector3(ufoTarget.position.x, transform.position.y, ufoTarget.position.z);
        //agent.SetDestination(groundPosition);

        Vector3 targetPosition = new Vector3(ufoTarget.position.x, transform.position.y, ufoTarget.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, followSpeed);
    }

    public void SendToLocation(Vector3 targetPosition)
    {
        isReturning = false;
        /*
        ! Need to implement setting a destination to a target location
        */
        //agent.SetDestination(targetPosition);
    }

    void Update()
    {
        if (!isReturning)
        {
            FollowUFO();
        }

        if (isReturning && Vector3.Distance(transform.position, new Vector3(ufoTarget.position.x, transform.position.y, ufoTarget.position.z)) < 1f)
        {
            FollowUFO();
            isReturning = false;
        }
    }

    /*
    ! Need to implement setting a destination to a target location
    */

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("TargetLocation"))
    //     {
    //         isReturning = true;
    //         agent.SetDestination(new Vector3(ufoTarget.position.x, transform.position.y, ufoTarget.position.z));
    //     }
    // }
}



/*
* Methods for having a pikmin follower chase after other people and convert them into pikmin
* Probably deprecated in favor of having the pikmin follow you as a band and then only act when sent to do something
*/

// private void MoveTowardsTarget()
// {
//     Vector3 direction = (target.position - transform.position).normalized;
//     transform.position += direction * speed * Time.deltaTime;

//     if (Vector3.Distance(transform.position, target.position) < 0.5f)
//     {
//         ConvertToPikmin(target.gameObject);
//         FindNewTarget();
//     }
// }

// private void ConvertToPikmin(GameObject person)
// {
//     Vector3 position = person.transform.position;
//     Destroy(person);
//     Instantiate(gameObject, position, Quaternion.identity);
// }

// private void FindNewTarget()
// {
//     Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100f, personLayer);
//     float closestDistance = Mathf.Infinity;

//     foreach (var hitCollider in hitColliders)
//     {
//         float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
//         if (distance < closestDistance)
//         {
//             closestDistance = distance;
//             target = hitCollider.transform;
//         }
//     }
// }
