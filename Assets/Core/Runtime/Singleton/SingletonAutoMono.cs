//------------------------------------------------------------
// 脚本名:		SingletonAutoMono.cs
// 作者:			海星
// 描述:			继承mono的自动生成的单例
//------------------------------------------------------------

using UnityEngine;

namespace StarFramework
{
    public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        //跨场景
        //缺点 多线程不能保证唯一

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new()
                    {
                        name = typeof(T).ToString()
                    };
                    DontDestroyOnLoad(obj);
                    instance = obj.AddComponent<T>();
                }
                return instance;
            }
        }

        private static T instance;

    }
}

