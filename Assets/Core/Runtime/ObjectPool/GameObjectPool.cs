//------------------------------------------------------------
// 脚本名:		GameObjectPool.cs
// 作者:			海星
// 描述:			游戏对象池
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFramework.Runtime
{

    public class GameObjectPool : MonoBehaviour
    {
        public Transform poolRoot;

        public GameObject prefab;

        public List<GameObject> poolItems;

        public List<GameObject> activeItems;
        public List<GameObject> recycleBin;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="root"></param>
        public void Init(GameObject prefab, Transform root)
        {
            this.prefab = prefab;
            poolRoot = root;
            poolItems = new List<GameObject>();
            activeItems = new List<GameObject>();
            recycleBin = new List<GameObject>();
        }

        /// <summary>
        /// 回收用的协程
        /// </summary>
        /// <param name="time"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private IEnumerator Recycle(float time, GameObject obj)
        {
            yield return new WaitForSeconds(time);
            obj.SetActive(false);

            //回收站里添加被回收的元素
            recycleBin.Add(obj);
            //从激活列表里移除
            activeItems.Remove(obj);
        }

        /// <summary>
        /// 获取单个元素 回收时间不填就不回收
        /// </summary>
        /// <param name="recycleTime"></param>
        /// <returns></returns>
        public GameObject GetItem(float recycleTime = 0)
        {
            GameObject obj;
            if (recycleBin.Count > 0)
            {
                //直接从回收站里拿 然后剔除出回收站
                obj = recycleBin[0];
                recycleBin.RemoveAt(0);
                obj.SetActive(true);
                //进激活列表
                activeItems.Add(obj);
            }
            else
            {
                //一个都没有就实例化新的
                obj = Instantiate(prefab);
                obj.name = prefab.name;
                obj.SetActive(true);
                poolItems.Add(obj);
                activeItems.Add(obj);
            }

            //设一下父级
            obj.transform.SetParent(poolRoot);

            //Debug.Log(obj.name + obj.transform.GetSiblingIndex());

            //时长不为0 启用回收
            if (recycleTime != 0)
            {
                StartCoroutine(Recycle(recycleTime, obj));
            }

            return obj;
        }

        /// <summary>
        /// 获取一堆元素 回收时间不填不回收
        /// </summary>
        /// <param name="count"></param>
        /// <param name="recycleTime"></param>
        /// <returns></returns>
        public List<GameObject> GetItems(int count, float recycleTime = 0)
        {
            List<GameObject> list = new();
            if (recycleBin.Count >= count)
            {
                //把指定数量的Obj从回收站添加进列表
                for (int i = 0; i < count; i++)
                {
                    list.Add(recycleBin[i]);
                    activeItems.Add(recycleBin[i]);
                }

                //直接范围移除出去
                recycleBin.RemoveRange(0, count - 1);
            }
            else
            {
                int remain = count - recycleBin.Count;

                //不够 整个回收站进
                foreach (var obj in recycleBin)
                {
                    list.Add(obj);
                    activeItems.Add(obj);
                }
                recycleBin.Clear();

                //差的补上来
                for (int i = 0; i < remain; i++)
                {
                    var obj = Instantiate(prefab);
                    obj.name = prefab.name;
                    obj.SetActive(true);
                    poolItems.Add(obj);
                    activeItems.Add(obj);
                }
            }

            foreach (var obj in list)
            {
                obj.SetActive(true);
                obj.transform.SetParent(poolRoot);

                //同上 不为0要按提供的时长来回收
                if (recycleTime != 0)
                {
                    StartCoroutine(Recycle(recycleTime, obj));
                }
            }

            return list;
        }


        public void ManualRecycle(GameObject obj)
        {
            if (activeItems.Contains(obj))
            {
                activeItems.Remove(obj);
                recycleBin.Add(obj);
                obj.SetActive(false);
            }
            else
            {
                Debug.LogError("该物体不在池子内，请检查是池子不对还是传进来的物体有问题");
            }
        }

        /// <summary>
        /// 一般是UI会一次性回收所有激活的元素
        /// </summary>
        public void RecycleAll()
        {
            //但是这里会引起一个比较大的问题
            //我们知道UI元素在Layout的显示跟siblingIndex是有联系的
            //所以要保证数据的下标要和UI元素的显示下标一一对应

            //举例遇到的坑
            //第一次 只需要使用1个 active 0 
            //第二次 需要使用5个 回收站回收0 active进0 然后active创建1 2 3 4
            //第三次 只需要使用1个 回收站里有1 2 3 4 active进0
            //第四次 需要使用5个 active的0被塞进了回收站里的最后一个 也就是说回收站是 1 2 3 4 0 
            //那就出大问题了 1 2 3 4 0 对应的是数据下标 0 1 2 3 4
            foreach (var obj in activeItems)
            {
                //Debug.Log(obj.name + obj.transform.GetSiblingIndex());
                obj.SetActive(false);
                recycleBin.Add(obj);
            }

            GameObject nowObj = null;

            //所以回收站塞满后 这里要做一遍整理
            List<GameObject> newList = new List<GameObject>(recycleBin);

            try
            {
                foreach (var item in recycleBin)
                {
                    nowObj = item;

                    //实际排序的下标 例如说实际排在1 就放在1下面
                    newList[item.transform.GetSiblingIndex()] = item;
                }
                recycleBin = newList;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                Debug.LogError("用于复制的容器的长度为" + newList.Count);
                Debug.LogError("当前物体的层级下标" + nowObj.transform.GetSiblingIndex());
                if (newList.Count == nowObj.transform.GetSiblingIndex())
                {
                    Debug.LogError("注意 父级下出现了不在Pool管理的物体影响了SiblingIndex 请检查");
                }
            }


            activeItems.Clear();
        }
    }
}
