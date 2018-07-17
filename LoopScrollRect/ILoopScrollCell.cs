namespace UnityEngine.UI
{
    /// <summary>
    /// Loop用接口
    /// 也可以用LoopScrollRect中的m_provideDataCallBack来做
    /// </summary>
    public interface ILoopScrollCell
    {
        void ScrollCellIndex(int _idx);
    }
}