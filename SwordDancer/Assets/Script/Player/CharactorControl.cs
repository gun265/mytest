using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharactorControl : MonoBehaviour {

    public  int            playerHP                = 100;
    public  int            CurrentHP               = 0;
    public  int             playerDamage            = 3;
    public  int             DamageRandValue         = 2;
    public  int             CriDamage               = 2;
    public  float           CriPercent              = 20f;
    public  float           moveSpeed               = 1.0f;
    public  float           rotationSpeed           = 1.0f;
    public  float           AttackRange             = 2.0f;
    public  GameMGR         MGR                     = null;
    public  UISlider        HPbar                   = null;
    public  bool            IsDead                  = false;
            Transform       Windparticle            = null;
            ParticleSystem  WindEffect              = null;

    
    //public  GameObject  Target                  = null;

    void Awake()
    {
        // 게임 매니저 등록
        MGR = GameObject.Find("GameMGR").GetComponent<GameMGR>();

        // HP설정
        CurrentHP = playerHP;

        // 파티클 장착
        Windparticle = (Transform)Instantiate(Resources.Load("Prefab/Wirlwind", typeof(Transform)), transform.position, Quaternion.AngleAxis(-90,Vector3.right));
        Windparticle.SetParent(transform);
        WindEffect = (ParticleSystem)Windparticle.GetComponent(typeof(ParticleSystem));
        WindEffect.Stop();
    }

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        // hp판단
        if (CurrentHP > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Attack();
            }

            if (Input.GetKey(KeyCode.R))
            {
                Whirlwind();
                //StartCoroutine(Wirldwind());
            }
            
            if (Input.GetKeyUp(KeyCode.R))
            {
                Align();
            }
        }
        else
        {
            if (!IsDead)
            {
                animation.CrossFade("Dead");
                IsDead = true;
            }            
        }

        // HP바 조정부분
        float fHp = (float)CurrentHP / (float)playerHP;
        HPbar.value = fHp;
	}

    //public void wirld()
    //{
    //    StartCoroutine(Wirldwind());
    //}

    //IEnumerator Wirldwind()
    //{
    //    while(true)
    //    {
    //        if (!WindEffect.isPlaying)
    //        {
    //        WindEffect.Play();
    //        }
    //    transform.rotation *= Quaternion.AngleAxis(10f * rotationSpeed, transform.up);
    //    Attack();
    //    }
    //    yield return null;
        
    //}

    public void Whirlwind()
    {
        if (!WindEffect.isPlaying)
        {
            WindEffect.Play();
        }
        transform.rotation *= Quaternion.AngleAxis(10f * rotationSpeed, transform.up);
        Attack();
    }

    public void Align()
    {
        //float angle = Quaternion.Angle(transform.rotation, transform.parent.FindChild("Forward").transform.rotation);
        //transform.forward = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        //transform.rotation = Quaternion.identity;
        if (WindEffect.isPlaying)
        {
            WindEffect.Stop();
        }
        //StopCoroutine(Wirldwind());
        transform.LookAt(transform.parent.FindChild("Forward").transform);
    }

    void Attack()
    {
        CriDamage = (Random.Range(0, 100) <= CriPercent ? 2 : 1);
        MGR.PlayerAttack(Random.Range(playerDamage * CriDamage, (playerDamage + Random.Range(0, DamageRandValue)) * CriDamage));
        animation.CrossFade("Attack", 0.2f);
    }

    public void onAniEnd()
    {
        animation.CrossFade("Wait", 0.2f);
    }
}
