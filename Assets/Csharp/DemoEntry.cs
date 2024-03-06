using StarFramework.Runtime;
using UnityEngine;

public class DemoEntry : MonoBehaviour
{
    void Start()
    {
        LuaManager.Instance.Init();
        LuaManager.Instance.ExcuteLuaFile("LuaEntry");
    }

    void Update()
    {
        //Debug.Log("运行中");
    }
}
