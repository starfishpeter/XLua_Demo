//------------------------------------------------------------
// 脚本名:		SceneControl.cs
// 作者:			海星
// 描述:			场景加载和控制
//------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace StarFramework.Runtime
{
    public class SceneControl : SingletonAutoMono<SceneControl>
    {
        //加载完成的标志
        public bool IsLoadingComplete { get; private set; }

        /// <summary>
        /// 叠加式的加载场景 新场景加载完后会卸载当前场景
        /// </summary>
        /// <param name="AAkey">AA里的地址</param>
        /// <param name="action">成功后做什么 可空</param>
        public void LoadSceneAdditive(string AAkey, UnityAction<bool> action = null)
        {
            StartCoroutine(IELoadSceneAdditive(AAkey, action));
        }

        /// <summary>
        /// 实际执行叠加场景的API
        /// </summary>
        /// <param name="AAkey"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private IEnumerator IELoadSceneAdditive(string AAkey, UnityAction<bool> action)
        {
            //加载新场景 因为不允许卸载激活的最后一个场景 所以先加载
            var handle = Addressables.LoadSceneAsync(AAkey, LoadSceneMode.Additive);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                //标记加载完成
                IsLoadingComplete = true;
                Debug.Log("AAkey为" + AAkey + "的场景加载完成");

                //卸载当前场景
                var unloadHandle = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                yield return unloadHandle;

                if (unloadHandle.isDone)
                {
                    action?.Invoke(true);
                }
                else
                {
                    action?.Invoke(false);
                }
            }
            else
            {
                Debug.LogError("加载场景失败" + handle.OperationException);
                IsLoadingComplete = false;
                action?.Invoke(false);
            }
        }
    }
}
