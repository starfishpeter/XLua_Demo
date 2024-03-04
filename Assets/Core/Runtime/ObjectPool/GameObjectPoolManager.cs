//------------------------------------------------------------
// 脚本名:		GameObjectPoolManager.cs
// 作者:			海星
// 描述:			#游戏对象池管理器
//------------------------------------------------------------

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace StarFramework.Runtime
{
    public class GameObjectPoolManager : SingletonAutoMono<GameObjectPoolManager>
    {
        public Dictionary<string, GameObjectPool> poolDic = new();

        public GameObject GetPoolItem(GameObject prefab, Transform parent, float time = 0)
        {
            if (!poolDic.ContainsKey(prefab.name))
            {
                var pool = parent.AddComponent<GameObjectPool>();
                pool.Init(prefab, parent);
                poolDic.Add(prefab.name, pool);
            }

            return poolDic[prefab.name].GetItem(time);
        }

        public List<GameObject> GetPoolItems(GameObject prefab, Transform parent, int count, float time = 0)
        {
            if (!poolDic.ContainsKey(prefab.name))
            {
                var pool = parent.AddComponent<GameObjectPool>();
                pool.Init(prefab, parent);
                poolDic.Add(prefab.name, pool);
            }

            return poolDic[prefab.name].GetItems(count, time);
        }

        /// <summary>
        /// 手动回收 针对池子自己控制的情况提供的API
        /// </summary>
        /// <param name="prefab"></param>
        public void ManualRecycle(GameObject prefab)
        {
            if (poolDic.ContainsKey(prefab.name))
            {
                poolDic[prefab.name].ManualRecycle(prefab);
            }
            else
            {
                Debug.LogWarning("要手动回收的池子不存在");
            }
        }

        /// <summary>
        /// 全体回收 针对UI元素需要全体回收的情况提供的API
        /// </summary>
        public void RecycleAll(GameObject prefab)
        {
            if (poolDic.ContainsKey(prefab.name))
            {
                poolDic[prefab.name].RecycleAll();
            }
            else
            {
                //Debug.LogWarning("要全体回收的池子不存在");
            }

        }
    }
}
