namespace FSM {
    /// <summary>
    /// 状态转换条件
    /// </summary>
    public interface ITransition {
        /// <summary>
        /// 状态转换名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 源状态
        /// </summary>
        IState FromState { get; set; }
        /// <summary>
        /// 变化状态
        /// </summary>
        IState ToState { get; set; }

        /// <summary>
        /// 检查是否达到转换条件
        /// </summary>
        /// <returns></returns>
        bool OnCheckTrans();
        /// <summary>
        /// 执行转换
        /// </summary>
        void OnTransition();
    }
}

