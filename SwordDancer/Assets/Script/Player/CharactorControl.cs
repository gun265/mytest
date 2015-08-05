using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharactorControl : MonoBehaviour {

    public  int             playerHP                = 100;
    public  int             CurrentHP               = 0;
    public  int             playerMP                = 100;
    public  int             CurrentMP               = 0;
    public  int             playerDamage            = 3;
    public  int             DamageRandValue         = 2;
    public  int             CriDamage               = 2;
    public  float           CriPercent              = 20f;
    public  float           moveSpeed               = 1.0f;
    public  float           rotationSpeed           = 1.0f;
    public  float           AttackRange             = 2.0f;
    public  GameMGR         MGR                     = null;
    public  UISlider        HPbar                   = null;
    public  UISlider        MPbar                   = null;
    public  bool            IsDead                  = false;
            Transform       Windparticle            = null;
            ParticleSystem  WindEffect              = null;
            float           CoolTime                = 1f;
            float           CurrentTime             = 1f;

    
    //public  GameObject  Target                  = null;

    void Awake()
    {
        // 게임 매니저 등록
        MGR = GameObject.Find("GameMGR").GetComponent<GameMGR>();

        // HP설정
        CurrentHP = playerHP;
        CurrentMP = playerMP;

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

            if( CurrentMP < playerMP)
            {
                CurrentMP++;
            }
        }
        else
        {
            if (!IsDead)
            {
                GetComponent<Animation>().CrossFade("Dead");
                IsDead = true;
            }            
        }

        // HPMP바 조정부분
        float fHp = (float)CurrentHP / (float)playerHP;
        float fMP = (float)CurrentMP / (float)playerMP;
        HPbar.value = fHp;
        MPbar.value = fMP;        
	}

    public void Whirlwind()
    {
        //Instantiae(Resources.Load("Prefab/CFXM_SpikyAura_Character"), transform.position, Quaternion.identity);
        CurrentTime += Time.deltaTime;
        Instantiate(Resources.Load("Prefab/CFXM_ElectricityBolt"), transform.position, Quaternion.AngleAxis(-90, Vector3.right) * transform.rotation);
        if (CoolTime <= CurrentTime)
        {
            Instantiate(Resources.Load("Prefab/CFXM_Tornado"), transform.position, Quaternion.AngleAxis(-90, Vector3.right));
            CurrentTime = 0;
        }
        CurrentMP -= 2;
        transform.rotation *= Quaternion.AngleAxis(40f * rotationSpeed, transform.up);
        Attack();
    }

    public void Align()
    {
        if (WindEffect.isPlaying)
        {
            WindEffect.Stop();
            Debug.Log("이펙트 종료");
            CurrentTime = CoolTime;
        }
        //StopCoroutine(Wirldwind());
        transform.LookAt(transform.parent.FindChild("Forward").transform);
    }

    void Attack()
    {
        CriDamage = (Random.Range(0, 100) <= CriPercent ? 2 : 1);
        MGR.PlayerAttack(Random.Range(playerDamage * CriDamage, (playerDamage + Random.Range(0, DamageRandValue)) * CriDamage));
        GetComponent<Animation>().CrossFade("Attack", 0.2f);
    }

    public void onAniEnd()
    {
        GetComponent<Animation>().CrossFade("Wait", 0.2f);
    }
}
