using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMGR : MonoBehaviour 
{
                List<GameObject>            SlimeList           = null;
    public      int                         MaxSlime            = 2;
                GameObject                  playerObj           = null;
                CharactorControl            player              = null;
    public      GameObject                  SlimePrefab         = null;
    public      float                       RebornTime          = 3;
    public      List<Transform>             RegenPoint          = null;
    public      bool                        GameEnd             = false;
    public      int                         goldamount          = 0;
    void Awake()
    {
        // 몬스터 추가
        SlimeList = new List<GameObject>();

        //for (int i = 0; i < MaxSlime; ++i)
        //{
        //    //SlimeList.Add(new GameObject());
        //    SlimeList.Add(null);
        //}
        
        // 몬스터 생성 코루틴 시작
        StartCoroutine("SlimeGeneration");

        // 플레이어 정보 추출
        playerObj = GameObject.Find("PLAYER/Cha_Knight");
        player = playerObj.GetComponent<CharactorControl>();
    }

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        // test
        //Vector3 pos = playerObj.transform.position;
        //Debug.Log("플레이어 위치 x : " + pos.x + ", y : " + pos.y + ", z : " + pos.z);
        
        SlimeRegen();
        Debug.Log("gold : " + goldamount);
	}

    // 몬스터리스트에서 죽은 몬스터를 부활 대기시간 이후 부활시킴
    void SlimeRegen()
    {
        for (int i = 0; i < SlimeList.Count; ++i)
        {
            if (SlimeList[i] != null && SlimeList[i].GetComponent<Slime>() != null && SlimeList[i].active == false)
            {
                SlimeList[i].GetComponent<Slime>().DeadTimmer += Time.deltaTime;
                if (SlimeList[i].GetComponent<Slime>().DeadTimmer >= RebornTime)
                {
                    SlimeList[i].SetActive(true);
                    SlimeList[i].transform.position = RegenPoint[Random.Range(0, RegenPoint.Count)].position;
                    SlimeList[i].GetComponent<Slime>().ResetState();
                }
            }
        }
    }


    // 몬스터가 플레이어를 공격할 때
    public void SlimeAttack(int _Damage)
    {
        player.CurrentHP -= _Damage;
        player.animation.CrossFade("Damage",0.2f);
    }

    // 플레이어가 몬스터를 공격할 때
    public void PlayerAttack(int _Damage)
    {
        // 사거리 체크
        for (int i = 0; i < SlimeList.Count; ++i)
        {
            if (SlimeList[i].GetComponent<Slime>() != null && SlimeList[i].GetComponent<Slime>().CurrentHP >= 0)
            {
                if (Vector3.Distance(SlimeList[i].transform.position, player.transform.position) <= player.AttackRange)
                {
                    SlimeList[i].GetComponent<Slime>().CurrentHP -= _Damage;
                }
            }
        }
    }

    // 몬스터 활성화 코루틴
    IEnumerator SlimeGeneration()
    {
        for (int i = 0; i < MaxSlime; ++i)
        {
            SlimeList.Add((GameObject)Instantiate(SlimePrefab, RegenPoint[Random.Range(0, RegenPoint.Count)].position, Quaternion.identity));
            SlimeList[i].name += (i + 1);
            SlimeList[i].SetActive(true);
            float WaitTime = Random.Range(3f, 6f);
            yield return new WaitForSeconds(WaitTime);
        }
    }
    
}
