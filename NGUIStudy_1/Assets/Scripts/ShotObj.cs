using UnityEngine;
using System.Collections;

public class ShotObj : MonoBehaviour {

    protected float attackPower = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	if( transform.position.x >= 9)
    {
        Destroy(gameObject);
    }
	}

    public void InitShotObj(float setupAttackPower)
    {
        attackPower = setupAttackPower;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 적 캐릭터 인 경우, 공격하여 피해를 가한다.
        if (other.CompareTag("enemy") || other.CompareTag("boss"))
        {
            // 공격 후 제거
            AttackAndDestroy(other);
        }
    }

    protected void AttackAndDestroy(Collider2D other)
    {
        IDamageable damageTarget = (IDamageable)other.GetComponent(typeof(IDamageable));
        damageTarget.Damage(attackPower);
        // 공격 후 제거
        Destroy(gameObject);
    }
}
