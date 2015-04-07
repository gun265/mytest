using UnityEngine;
using System.Collections;

// Enemy 스크립트를 상속받음
public class EnemyRanged : Enemy{

    // 발사할 게임 오브젝트
    public GameObject shotObj;

    // 발사 위치
    public Transform firePosition;

    // 발사 속도
    public float fireSpeed = 3;

    // Shot GameObject pool
    GameObjectPool objPool;
    Vector3 spawnPos = new Vector3(0, 50, 0);

    // 발사할 게임 오브젝트 생성에 사용.
    GameObject tempObj;
    Vector2 tempVector2 = new Vector2();

    public override void Attack()
    {
        if( objPool == null)
        {
            objPool = new GameObjectPool(GameData.Instance.gamePlayManager.
                gameObjectPoolPosition.transform.position.x, shotObj);
        }

        if( !objPool.NextGameObject(out tempObj))
        {
            tempObj = Instantiate(shotObj, GameData.Instance.gamePlayManager.gameObjectPoolPosition
                .transform.position, Quaternion.identity) as GameObject;
            tempObj.name = shotObj.name + objPool.lastIndex;
            objPool.AddGameObject(tempObj);
        }

        // 위치 지정
        tempObj.transform.position = firePosition.position;
        
        // 속도 지정
        tempVector2 = Vector2.right * -1 * fireSpeed;
        tempObj.rigidbody2D.velocity = tempVector2;

        // 공격력 설정
        EnemyShotObj tempEnemyShot = tempObj.GetComponent<EnemyShotObj>();
        tempEnemyShot.InitShotObj(attackPower);
    }

    public void CallAttack()
    {
        Attack();
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
