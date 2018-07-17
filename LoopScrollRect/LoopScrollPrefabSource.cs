using System.Collections.Generic;

namespace UnityEngine.UI
{
    [System.Serializable]
    public class LoopScrollPrefabSource
    {
        public const string __POOLSNAME = "[ItemPool]";

        /// <summary>
        /// 池节点
        /// </summary>
        public Transform m_poolGameObject;

        // 以下2种2选1
        public GameObject m_gameObject;

        public string assetBundle;
        public string assetName;

        public int poolSize = 5;

        private bool inited = false;

        /// <summary>
        /// 池
        /// </summary>
        private Queue<GameObject> m_queue = new Queue<GameObject>();

        /// <summary>
        /// 模板
        /// </summary>
        private GameObject m_templateGo;

        /// <summary>
        /// 池节点
        /// </summary>
        public Transform PoolGameObject
        {
            set { m_poolGameObject = value; }
        }

        public virtual GameObject GetObject()
        {
            if (!inited)
            {
                InitPool();

                inited = true;
            }

            return GetObjectFromPool();
        }

        public virtual void ReturnObject(Transform go)
        {
            go.SendMessage("ScrollCellReturn", SendMessageOptions.DontRequireReceiver);

            ReturnObjectToPool(go.gameObject);
        }

        /// <summary>
        /// 初始化池
        /// </summary>
        private void InitPool()
        {
            if (m_gameObject == null &&
                assetBundle == null)
            {
                Debug.LogError("no set template!!!");

                return;
            }

            // Ab加载
            if (assetBundle != null &&
                m_gameObject == null)
            {
                m_templateGo = GameObject.Instantiate(AssetBundleManager.Instance.LoadBundleSync<GameObject>(assetBundle, assetName));
            }
            else
            {
                //m_templateGo = GameObject.Instantiate(m_gameObject);
                m_templateGo = m_gameObject;

                m_gameObject.SetActive(false);
            }

            // 如果没有赋值就往Template下面丢
            if (m_poolGameObject == null)
            {
                m_poolGameObject = m_gameObject.transform;
            }

            int _s = poolSize - m_queue.Count;

            m_templateGo.transform.SetParent(m_poolGameObject);

            // 创建池
            for (int i = 0; i < _s; ++i)
            {
                m_queue.Enqueue(GameObject.Instantiate(m_templateGo));
            }

            m_templateGo.SetActive(false);
        }

        public void DeInitPool()
        {
            if (assetBundle != null &&
                m_gameObject == null)
            {
                AssetBundleManager.Instance.ReleaseBundle(assetBundle);
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
                // 翻倍
                for (int i = 0; i < poolSize; ++i)
                {
                    m_queue.Enqueue(GameObject.Instantiate(m_templateGo));
                }

                poolSize *= 2;

                return m_queue.Dequeue();
            }
        }

        private void ReturnObjectToPool(GameObject _go)
        {
            if (m_queue.Count == poolSize)
            {
                GameObject.Destroy(_go);
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