using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using StarFramework.Runtime;
using UnityEngine.UI;

public class HotUpdate : MonoBehaviour
{
    //状态显示UI
    public TMP_Text statusText;

    public Button startGame;

    //设定的超时
    public float Timeout = 5000f;

    //状态变量
    private bool isChecking = false;
    private float checkUpdateTime = 0f;

    void Start()
    {
        //进入游戏场景 按钮逻辑
        startGame.onClick.AddListener(()=>
        {
            SceneControl.Instance.LoadSceneAdditive("Main");
        });

        //一开始先检测网络状态 如果不联网则不推进
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            statusText.text = "没有网络连接，不进行推进";
            Debug.Log("没有网络连接，不进行推进");
        }
        else
        {
            statusText.text = "开始检测是否有资源更新";
            Debug.Log("开始检测是否有资源更新");
            StartCoroutine(CheckUpdate());
        }
    }

    void Update()
    {
        //超时检测
        if(isChecking)
        {
            checkUpdateTime += Time.deltaTime;
            if(checkUpdateTime > Timeout)
            {
                isChecking = false;
                StopAllCoroutines();
                statusText.text = "检测超时";
            }
        }
    }

    IEnumerator CheckUpdate()
    {
        isChecking = true;

        //计时用 用本地时间无所谓 因为只看加载区间
        var startTime = DateTime.Now;

        Addressables.CheckForCatalogUpdates(true).Completed += (CheckHandle)=>
        {
            isChecking = false;
            var logCheckTime = string.Format($"检测所消耗时间{(DateTime.Now - startTime).Milliseconds}ms");
            statusText.text = logCheckTime;
            Debug.Log(logCheckTime);

            //空代表失败了 这里其实也可以用状态枚举来判断
            if(CheckHandle.Result == null)
            {
                statusText.text = "无法连接到服务器 服务器未开启/维护中";
            }
            else
            {
                //判断返回的目录更新数目是否大于0 如果是就要进行更新
                if(CheckHandle.Result.Count > 0)
                {
                    startTime = DateTime.Now;
                    string logUpdateTime = string.Format($"更新花费了{(DateTime.Now - startTime).Milliseconds}ms");
                    statusText.text = logUpdateTime;
                    Debug.Log(logUpdateTime);

                    Addressables.UpdateCatalogs(CheckHandle.Result, true).Completed +=(UpdateHandle)=>
                    {
                        Debug.Log("更新成功，加载主场景");
                        startGame.gameObject.SetActive(true);
                    };
                }
                else
                {
                    Debug.Log("未检测到要更新的资源，直接加载主场景");
                    startGame.gameObject.SetActive(true);
                }
            }
        };

        yield return true;
    }

}
