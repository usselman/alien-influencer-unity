
public class Minion : MonoBehaviour
{

    #region Variables

    public enum MinionState
    {
        StartIdle,
        Idle,
        StartMovingToBuilding,
        MovingToBuilding,
        StartAttackingBuilding,
        AttackingBuilding,
        StartMovingToUFO,
        MovingToUFO
    }
    public MinionState state = MinionState.Idle;
    public enum AttackState
    {
        StartAttack,
        Attacking
    }
    public AttackState attackState = AttackState.StartAttack;
    public float DistanceToTarget;
    public Animator animator;
    public Transform houseTrans;
    public ParticleSystem attackParticles;
    public float walkingSpeed = 2.0f;
    Vector3 destination;
    Transform ufoTrans;

    #endregion
    #region Unity Methods

    void Start()
    {
        ufoTrans = GameObject.FindWithTag("UFO").transform;
    }
    void OnEnable()
    {
        MinionManager.OnNewBuildingSelected += MoveToBuilding;
    }
    void OnDisable()
    {
        MinionManager.OnNewBuildingSelected -= MoveToBuilding;
    }
    private void Update()
    {
        switch (state)
        {
            case MinionState.StartIdle:
                StartIdle();
                break;
            case MinionState.Idle:
                Idle();
                break;
            case MinionState.MovingToBuilding:
                MovingToBuilding();
                break;
            case MinionState.StartAttackingBuilding:
                StartAttackingBuilding();
                break;
            case MinionState.AttackingBuilding:
                AttackingBuilding();
                break;
            case MinionState.StartMovingToUFO:
                StartMovingToUFO();
                break;
            case MinionState.MovingToUFO:
                MovingToUFO();
                break;
            default:
                break;
        }
    }

    #endregion
    #region State Functions

    void StartIdle()
    {
        animator.SetTrigger("StartIdle");
        state = MinionState.Idle;
    }
    void Idle()
    {

    }
    public void MoveToBuilding()
    {
        destination = MinionManager.Instance.SelectedBuilding.transform.position;
        houseTrans = MinionManager.Instance.SelectedBuilding.transform;
        transform.LookAt(destination);
        animator.SetTrigger("StartWalk");
        state = MinionState.MovingToBuilding;
    }
    void MovingToBuilding()
    {
        DistanceToTarget = Vector3.Distance(transform.position, destination);
        transform.position = Vector3.MoveTowards(transform.position, destination, walkingSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < 8.0f)
        {
            state = MinionState.StartAttackingBuilding;
        }
    }
    void StartAttackingBuilding()
    {
        animator.SetTrigger("StartAttack");
        state = MinionState.AttackingBuilding;
    }
    void AttackingBuilding()
    {
        switch (attackState)
        {
            case AttackState.StartAttack:
                StartCoroutine(StallAttack());
                attackState = AttackState.Attacking;
                break;
            case AttackState.Attacking:
                break;
        }
    }
    void StartMovingToUFO()
    {
        animator.SetTrigger("StartWalk");
        destination = GameObject.Find("UFO").transform.position;
        state = MinionState.MovingToUFO;
    }
    void MovingToUFO()
    {
        Vector3 ufoGroundPosition = new Vector3(ufoTrans.position.x, transform.position.y, ufoTrans.position.z);
        transform.position = Vector3.MoveTowards(transform.position, ufoGroundPosition, walkingSpeed * Time.deltaTime);
        transform.LookAt(ufoGroundPosition);
        DistanceToTarget = Vector3.Distance(transform.position, ufoGroundPosition);
        if (Vector3.Distance(transform.position, ufoGroundPosition) < 3.0f)
        {
            state = MinionState.StartIdle;
        }
    }

    #endregion
    #region Utility Functions

    IEnumerator StallAttack()
    {
        if (!MinionManager.Instance.CanAttackBuilding())
        {
            state = MinionState.StartMovingToUFO;
            yield return 0;
        }
        attackParticles.Play();
        MinionManager.Instance.AttackBuilding();
        yield return new WaitForSeconds(1.3f);
        attackState = AttackState.StartAttack;
    }

    #endregion

}
