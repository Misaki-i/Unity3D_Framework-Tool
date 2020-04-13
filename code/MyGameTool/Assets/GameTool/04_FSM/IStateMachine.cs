namespace FSM {

    /// <summary>
    /// 状态机接口
    /// </summary>
    public interface IStateMachine {
        /// <summary>
        /// 状态机名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 默认状态
        /// </summary>
        IState DefaultState { get; }
        /// <summary>
        /// 当前状态
        /// </summary>
        IState CurrentState { get; }

        /// <summary>
        /// 添加一个状态
        /// </summary>
        /// <param Name="state">状态</param>
        void AddState(IState state);
        /// <summary>
        /// 移除一个状态
        /// </summary>
        /// <param Name="state">状态</param>
        void RemoveState(IState state);

        /// <summary>
        /// 状态机销毁时调用
        /// </summary>
        void OnDestroy();
    }
}
