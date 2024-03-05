//------------------------------------------------------------
// 脚本名:		LoadManager.cs
// 作者:			海星
// 描述:			#基于Addressables的加载管理
//------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using XLua;

namespace StarFramework.Runtime
{
    [LuaCallCSharp]
    public class LoadManager : SingletonAutoMono<LoadManager>
    {
        public void LoadAsset<T>(string AAkey, UnityAction<T> action) where T : Object
        {
            StartCoroutine(IELoadAsset<T>(AAkey, action));
        }

        private IEnumerator IELoadAsset<T>(string AAkey, UnityAction<T> action) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(AAkey);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (handle.Result is GameObject)
                {
                    action(Instantiate(handle.Result));
                }
                else
                {
                    action(handle.Result);
                }
            }
        }
    }
}