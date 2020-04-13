/****************************************************
    文件：TestFSM.cs
	功能：测试FSM状态机
*****************************************************/
using FSM;
using System.Collections;
using UnityEngine;

public class TestFSM : MonoBehaviour
{

    protected StateMachine m_StateMachime;

    void InitState()
    {
        //状态
        State s_Idle = new State("Idle");
        State s_Move = new State("Move");
        State s_Attack = new State("Attack");

        //状态机
        m_StateMachime = new StateMachine("StateMachime", s_Idle);
        m_StateMachime.AddState(s_Idle);
        m_StateMachime.AddState(s_Move);
        m_StateMachime.AddState(s_Attack);


        // Idle
        // 切换条件
        Transition t_IdleToMove = new Transition("IdleToMove", s_Idle, s_Move);
        Transition t_IdleToAttack = new Transition("IdleToAttack", s_Idle, s_Attack);
        t_IdleToMove.OnCheck += () =>
        {
            return CheckIdleToMove();
        };
        t_IdleToAttack.OnCheck += () =>
        {
            return CheckIdleToAttack();
        };
        s_Idle.AddTransition(t_IdleToMove);
        s_Idle.AddTransition(t_IdleToAttack);
        // 状态事件添加
        //s_Idle.OnEnter += IdleEnter;
        //s_Idle.OnUpdate += IdleUpdate;
        //s_Idle.OnExit += IdleExit;


        //etc...........




        StartCoroutine(UpdateState(0.1f));
    }

    /// <summary>
    /// 更新状态机
    /// </summary>
    /// <param name="delatTime">延迟</param>
    /// <returns></returns>
    IEnumerator UpdateState(float delatTime)
    {
        while (true){
            m_StateMachime.OnUpdateCallBack(delatTime);
            yield return new WaitForSeconds(delatTime);
        }
    }
    protected virtual bool CheckIdleToMove()
    {
        return false;
    }

    protected virtual bool CheckIdleToAttack()
    {
        return false;
    }




 }
