using UnityEngine;
using System.Collections;

public class SlimeControl : MonoBehaviour {

    public GameObject player;
    public float moveSpeed = 1;
    public float rotationSpeed = 100;
    public float attackSpeed = 2;
    public float freezingTime = 1;
    public float attackRange = 2;
    public float damage = 5;
    public float attackAngle = 45;
    public float continuouslyMovingTime = 1;
    float attackTimer = 0;

    void Awake()
    {
        attackTimer = attackSpeed;
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        // 공격타이머 설정
        if( attackTimer > 0)
        {
            attackTimer -= Time.smoothDeltaTime;
        }
        
        FindPlayer();
	}

    void FindPlayer ()
    {
        Vector3 directionVector = player.transform.position - this.transform.position;
        float distance = directionVector.sqrMagnitude;
        Vector3 norDirectionVector =  directionVector.normalized;
        Vector3 norForward = this.transform.forward;

        // 플레이어가 공격 사거리와 공격각에 들어왔는지 확인
        if( distance <= attackRange &&
            Vector3.Angle(norDirectionVector, norForward) <= attackAngle)
        {
            Attack();           
        }
        else
        {
            // 플레이어방향으로 이동 및 방향 설정
            this.transform.LookAt(player.transform);
            this.transform.Translate(norForward * moveSpeed * Time.smoothDeltaTime, Space.World);
            // 이동 애니메이션으로 치환
            this.animation.CrossFade("Walk");
        }
    }

    void Attack ()
    {
        // 공격 타이머가 충전되었는지 확인
        if (attackTimer <= 0)
        {
            attackTimer = attackSpeed;
            this.animation.CrossFade("Attack");
        }
    }

    void Waiting ()
    {
        this.animation.CrossFade("Wait");
    }

    void Dead ()
    {

    }
}
