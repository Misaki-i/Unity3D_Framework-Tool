using System.Collections.Generic;

namespace FSM {
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IState {
        string Name { get; }

        List<ITransition> Transition_List { get; }

        /// <summary>
        /// 进入状态
        /// </summary>
        /// <param Name="preState">前一状态</param>
        void OnEnterState(IState preState);


        /// <summary>
        /// 退出状态
        /// </summary>
        /// <param Name="nextState">下一状态</param>
        void OnExitState(IState nextState);

        /// <summary>
        /// 状态Update回调
        /// </summary>
        /// <param Name="deltaTime">延迟时间</param>
        void OnUpdateCallBack(float deltaTime);

        /// <summary>
        /// 状态LateUpdate回调
        /// </summary>
        /// <param Name="deltaTime">延迟时间</param>
        void OnLateUpdateCallBack(float deltaTime);

        /// <summary>
        /// 状态FixedUpdate回调
        /// </summary>
        void OnFixedUpdateCallBack();

        /// <summary>
        /// 状态销毁时调用
        /// </summary>
        void OnDestroy();

        /// <summary>
        /// 添加状态转换
        /// </summary>
        /// <param Name="t">转换</param>
        void AddTransition(ITransition t);


    }

}


