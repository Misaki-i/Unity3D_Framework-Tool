using System.Collections.Generic;

namespace FSM {

    public delegate void StateDelegate();
    public delegate void StateDelegateIState(IState state);
    public delegate void StateDelegateFloat(float deltaTime);

    /// <summary>
    /// 状态基础类
    /// </summary>
    public class State : IState {
        /// <summary>
        /// 状态名称
        /// </summary>
        public string Name {
            get {
                return m_Name;
            }
        }

        public List<ITransition> Transition_List {
            get { return m_Transition_List; }
        }

        /// <summary>
        /// 进入状态委托
        /// </summary>
        public event StateDelegateIState OnEnter;
        /// <summary>
        /// 推出状态委托
        /// </summary>
        public event StateDelegateIState OnExit;
        /// <summary>
        /// 状态Update处理委托
        /// </summary>
        public event StateDelegateFloat OnUpdate;
        /// <summary>
        /// 状态LateUpdate处理委托
        /// </summary>
        public event StateDelegateFloat OnLateUpdate;
        /// <summary>
        /// 状态FixedUpdate处理委托
        /// </summary>
        public event StateDelegate OnFixedUpdate;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param Name="name"></param>
        public State(string name) {
            m_Name = name;
            m_Transition_List = new List<ITransition>();
        }
        /// <summary>
        /// 进入状态
        /// </summary>
        /// <param Name="preState">前一状态</param>
        public virtual void OnEnterState(IState preState) {
            if (OnEnter != null) {
                OnEnter(preState);
            }
        }

        /// <summary>
        /// 退出状态
        /// </summary>
        /// <param Name="nextState">下一状态</param>
        public virtual void OnExitState(IState nextState) {
            if (OnExit != null) {
                OnExit(nextState);
            }
        }
        /// <summary>
        /// 状态Update回调
        /// </summary>
        /// <param Name="deltaTime">延迟时间</param>
        public virtual void OnUpdateCallBack(float deltaTime) {
            if (OnUpdate != null) {
                OnUpdate(deltaTime);
            }
        }
        /// <summary>
        /// 状态LateUpdate回调
        /// </summary>
        /// <param Name="deltaTime">延迟时间</param>
        public virtual void OnLateUpdateCallBack(float deltaTime) {
            if (OnLateUpdate != null) {
                OnLateUpdate(deltaTime);
            }
        }
        /// <summary>
        /// 状态FixedUpdate回调
        /// </summary>
        public virtual void OnFixedUpdateCallBack() {
            if (OnFixedUpdate != null) {
                OnFixedUpdate();
            }
        }
        /// <summary>
        /// 添加状态转换
        /// </summary>
        /// <param Name="t">转换</param>
        public virtual void AddTransition(ITransition t) {
            if (t != null)
                m_Transition_List.Add(t);
        }

        /// <summary>
        /// 状态机销毁时调用
        /// </summary>
        public virtual void OnDestroy() {
            m_Transition_List.Clear();
            m_Transition_List = null;
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        private string m_Name;
        private List<ITransition> m_Transition_List;

    }
}