using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class LoopScrollRectItem : MonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// 点击事件
        /// </summary>
        public Action<Transform, int> m_clickItemCallBack;

        private int m_index;

        public int Index
        {
            get { return m_index; }
            set { m_index = value; }
        }

        /// <summary>
        /// 继承点击事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_clickItemCallBack != null)
            {
                m_clickItemCallBack(this.transform, m_index);
            }
        }
    }
}