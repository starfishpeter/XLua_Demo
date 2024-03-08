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

        //针对lua使用具体类型进行包裹，不使用泛型，我发现lua使用泛型问题很多，尽量避开吧
        public GameObject LoadGameObject(string AAkey)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(AAkey);
            handle.WaitForCompletion();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return Instantiate(handle.Result);
            }

            return null; 
        }

        public Sprite LoadSprite(string AAkey)
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(AAkey);
            handle.WaitForCompletion();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            
            return null; 
        }

    }
}