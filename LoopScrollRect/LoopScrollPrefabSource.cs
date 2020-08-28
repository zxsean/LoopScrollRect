using Client.Library;
using Client.Library.TMNSUtility;
using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
    [System.Serializable]
    public class LoopScrollPrefabSource
    {
        public const string __POOL_NAME = "[ItemPool]";

        /// <summary>
        /// 池节点
        /// </summary>
        public Transform m_poolGameObject;

        public GameObject m_gameObject;

        public string m_assetName;

        public int m_poolSize = 5;

        private bool m_inited = false;

        /// <summary>
        /// 池
        /// </summary>
        private Queue<GameObject> m_queue = new Queue<GameObject>();

        /// <summary>
        /// 模板
        /// </summary>
        private GameObject m_templateGo;

        /// <summary>
        /// 模板
        /// </summary>
        public GameObject TemplateGo => m_templateGo;

        [HideInInspector]
        public static Func<string, GameObject> m_loadGoFromAB;

        [HideInInspector]
        public static Action<string> m_unloadAB;

        /// <summary>
        /// 池节点
        /// </summary>
        public Transform PoolGameObject
        {
            set => m_poolGameObject = value;
        }

        public virtual GameObject GetObject()
        {
            if (!m_inited)
            {
                InitPool();

                m_inited = true;
            }

            return GetObjectFromPool();
        }

        public virtual void ReturnObject(Transform _go)
        {
            _go.SendMessage("ScrollCellReturn", SendMessageOptions.DontRequireReceiver);

            ReturnObjectToPool(_go.gameObject);
        }

        /// <summary>
        /// 初始化池
        /// </summary>
        private void InitPool()
        {
            if (m_gameObject == null &&
                m_assetName == null)
            {
                Debug.LogError("no set template!!!");

                return;
            }

            if (m_assetName != null &&
                m_gameObject == null)
            {
                if (m_loadGoFromAB != null)
                {
                    m_templateGo = Object.Instantiate(m_loadGoFromAB(m_assetName));
                }
                else
                {
                    LogUtils.LogError("m_loadGoFromAB is null!");

                    return;
                }
            }
            else
            {
                if (m_gameObject == null)
                {
                    LogUtils.LogError("m_gameObject is null!");

                    return;
                }

                //m_templateGo = GameObject.Instantiate(m_gameObject);
                m_templateGo = m_gameObject;

                m_gameObject.SetActive(false);
            }

            if (m_poolGameObject == null &&
                m_gameObject != null)
            {
                m_poolGameObject = m_gameObject.transform;
            }

            int _size = m_poolSize - m_queue.Count;

            m_templateGo.transform.SetParent(m_poolGameObject);

            // 创建池
            for (int i = 0; i < _size; ++i)
            {
                GameObject _poolItem = Object.Instantiate(m_templateGo);

                _poolItem.name = _poolItem.name.Replace("(Clone)", "");

                _poolItem.SetParent(m_poolGameObject);

                m_queue.Enqueue(_poolItem);
            }

            m_templateGo.SetActive(false);
        }

        public void DeInitPool()
        {
            if (m_assetName != null &&
                m_gameObject == null)
            {
                if (m_unloadAB != null)
                {
                    m_unloadAB(m_assetName);
                }
                else
                {
                    Debug.LogError("m_unloadAB is null!");
                }
            }
        }

        private GameObject GetObjectFromPool()
        {
            if (m_queue.Count > 0)
            {
                return m_queue.Dequeue();
            }
            else
            {
                for (int i = 0; i < m_poolSize; ++i)
                {
                    GameObject _poolItem = Object.Instantiate(m_templateGo);

                    _poolItem.name = _poolItem.name.Replace("(Clone)", "");

                    _poolItem.SetParent(m_poolGameObject);

                    m_queue.Enqueue(_poolItem);
                }

                m_poolSize *= 2;

                return m_queue.Dequeue();
            }
        }

        private void ReturnObjectToPool(GameObject _go)
        {
            if (m_queue.Count == m_poolSize)
            {
                Object.Destroy(_go);
            }
            else
            {
                _go.SetActive(false);
                m_queue.Enqueue(_go);

                _go.transform.SetParent(m_poolGameObject);
            }
        }
    }
}