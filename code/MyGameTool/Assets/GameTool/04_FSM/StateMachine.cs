using System.Collections.Generic;

namespace FSM {

    public class StateMachine : State, IStateMachine {
        ///// <summary>
        ///// 状态机名称
        ///// </summary>
        //public string Name {
        //    get { return m_Name; }
        //}
        /// <summary>
        /// 默认状态
        /// </summary>
        public IState DefaultState {
            get { return m_DefaultState; }
        }
        /// <summary>
        /// 当前状态
        /// </summary>
        public IState CurrentState {
            get { return m_CurrentState; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param Name="name"></param>
        public StateMachine(string name, IState defaultState) : base(name) {
            //m_Name = Name;
            m_DefaultState = defaultState;
            m_CurrentState = defaultState;

            m_State_List = new List<IState>();
            AddState(defaultState);
        }
        /// <summary>
        /// 添加一个状态
        /// </summary>
        /// <param Name="state">状态</param>
        public void AddState(IState state) {
            if (!m_State_List.Contains((state))) {
                m_State_List.Add(state);
            }
        }
        /// <summary>
        /// 移除一个状态
        /// </summary>
        /// <param Name="state">状态</param>
        public void RemoveState(IState state) {
            if (m_State_List.Contains((state))) {
                m_State_List.Remove(state);
                if (state.Equals(m_DefaultState) && m_State_List.Count > 0) {
                    m_DefaultState = m_State_List[0];
                }
                if (state.Equals(m_CurrentState)) {
                    m_CurrentState = m_DefaultState;
                }
            }
        }


        /// <summary>
        /// 状态Update回调
        /// </summary>
        /// <param Name="deltaTime">延迟时间</param>
        public override void OnUpdateCallBack(float deltaTime)
        {
            if (m_CurrentState == null) return;

            List<ITransition> trans_List = m_CurrentState.Transition_List;
            ITransition transTemp = null;
            foreach (ITransition trans in trans_List)
            {
                if (trans.OnCheckTrans())
                {
                    //转换状态
                    transTemp = trans;
                    break;
                }
            }
            //转换状态
            if (transTemp != null)
            {
                //转换中
                transTemp.OnTransition();
                //当前状态退出    
                m_CurrentState.OnExitState(transTemp.ToState);
                m_CurrentState = transTemp.ToState;
                //进入下一状态
                m_CurrentState.OnEnterState(transTemp.FromState);
            }

            //当天状态更新
            m_CurrentState.OnUpdateCallBack(deltaTime);
        }
        /// <summary>
        /// 状态LateUpdate回调
        /// </summary>
        /// <param Name="deltaTime">延迟时间</param>
        public override void OnLateUpdateCallBack(float deltaTime)
        {
            if (CurrentState == null) return;
            CurrentState.OnLateUpdateCallBack(deltaTime);
        }
        /// <summary>
        /// 状态FixedUpdate回调
        /// </summary>
        public override void OnFixedUpdateCallBack() {
            if (CurrentState == null) return;
            CurrentState.OnFixedUpdateCallBack();
        }

        /// <summary>
        /// 状态机销毁时调用
        /// </summary>
        public override  void OnDestroy() {
            base.OnDestroy();
            m_State_List.Clear();
            m_State_List = null;
            m_DefaultState = null;
            m_CurrentState = null;
        }


        private IState m_DefaultState;
        private IState m_CurrentState;
        //private string m_Name;

        private List<IState> m_State_List;
    }

}

