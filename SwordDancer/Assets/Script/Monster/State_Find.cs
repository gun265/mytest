using UnityEngine;
using System;
using System.Collections;

public class State_Find : FSM_State<Slime>
{
    static readonly State_Find instance = new State_Find();
    public static State_Find Instance
    {
        get 
        { 
            return instance; 
        }
    }

    static State_Find() { }
    private State_Find() { }

    public override void EnterState(Slime _Slime)
    {
#if DEBUG
        Debug.Log("FindState에 진입.");
#endif
        if( _Slime.myTarget != null)
        {
#if DEBUG
            Debug.Log("Slime 클래스가 타겟을 가지고 있습니다. FindState를 빠져나옵니다.");
#endif
            return;
        }


    }

    public override void UpdateState(Slime _Slime)
    {

    }

    public override void ExitState(Slime _Slime)
    {

    }
}