using UnityEngine;
using System.Collections;

public class HPPotion : MonoBehaviour 
{
    public          Transform           effect      = null;
    public          int                 heal        = 10;
    void Awake()
    {
        // 알파값 수정
        Color alpha = renderer.material.color;
        alpha.a = 0.6f;
        renderer.material.color = alpha;
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider _Other)
    {
        if( _Other.transform.tag == "Player")
        {
            // 치유 및 이펙트 효과 내고 파괴
            GameObject.Find("PLAYER").transform.FindChild("Cha_Knight").GetComponent<CharactorControl>().CurrentHP += heal;
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
