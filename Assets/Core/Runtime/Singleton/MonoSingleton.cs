//------------------------------------------------------------
// 脚本名:		MonoSingleton.cs
// 作者:			海星
// 描述:			#继承Mono 但和场景同生同死的单例
//------------------------------------------------------------

using UnityEngine;

namespace StarFramework.Runtime
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError("目前场景中没有挂载该实例的脚本" + "或许应该检查一下Awake函数是否被占用" + "或者是挂载脚本的物体被失活了");
                }
                return instance;
            }
        }

        private static T instance;

        //一开始的时候赋值
        private void Awake()
        {
            instance = GetComponent<T>();

            OnAwake();
        }

        //子类的Awake会覆盖父类 我暂时没想到怎么改这里
        //目前来说可以重写这个OnAwake
        protected virtual void OnAwake()
        {

        }

        //跨场景的时候置空
        private void OnDestroy()
        {
            if (instance != null)
            {
                instance = null;
            }
        }
    }
}
