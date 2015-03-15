using UnityEngine;
using System.Collections;

public class Fence : MonoBehaviour, IDamageable
{

    public void Damage(float damageTaken)
    {
        // 공격 받으면 농장의 HP를 감소하도록 한다.
        GameData.Instance.gamePlayManager.Damage(damageTaken);
    }

    void Update()
    {
        
    }
}