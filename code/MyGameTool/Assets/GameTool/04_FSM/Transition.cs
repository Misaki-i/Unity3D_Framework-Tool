namespace FSM {
    public delegate void TransitionDelegate();
    public delegate bool TransitionDelegateBool();

    public class Transition : ITransition {
        /// <summary>
        /// 状态转换名称
        /// </summary>
        public string Name {
            get { return m_Name; } }
        /// <summary>
        /// 源状态
        /// </summary>
        public IState FromState {
            get { return m_FromState; }
            set { m_FromState = value; }
        }
        /// <summary>
        /// 变化状态
        /// </summary>
        public IState ToState {
            get { return m_ToState; }
            set { m_ToState = value; }
        }

        /// <summary>
        /// 状态转换检查回调
        /// </summary>
        public event TransitionDelegateBool OnCheck;
        /// <summary>
        /// 状态转换进行回调
        /// </summary>
        public event TransitionDelegate onTransIng;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param Name="name">转换名称</param>
        /// <param Name="fromState">源状态</param>
        /// <param Name="toState">目标转换状态</param>
        public Transition(string name, IState fromState, IState toState) {
            m_Name = name;
            m_FromState = fromState;
            m_ToState = toState;
        }

        public virtual bool OnCheckTrans() {
            if (OnCheck != null) {
                return OnCheck();
            }

            return false;
        }

        public virtual void OnTransition() {
            if (onTransIng != null) {
                onTransIng();
            }
        }


        private string m_Name;
        private IState m_FromState;
        private IState m_ToState;

    }
}

