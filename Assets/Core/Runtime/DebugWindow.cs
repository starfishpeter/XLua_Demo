//------------------------------------------------------------
// 脚本名:		DebugWindow.cs
// 作者:			海星
// 描述:			Debug工具箱
//------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWindow : MonoBehaviour
{
    //控制弹窗显示与隐藏的标志
    private bool showDebugWindow = false;
    //ScrollView的滚动位置
    private Vector2 scrollPosition;
    //存储接收到的日志
    private List<string> logList = new();
    private List<string> normalLog = new();
    private List<string> warningLog = new();
    private List<string> errorLog = new();

    private static readonly int buttonHeight = 40;

    //一级菜单选项
    private string[] primaryMenuOptions = { "<size=18>日志</size>", "<size=18>GM面板</size>", "<size=18>快捷工具</size>", "<size=18>系统信息</size>" };
    private int selectedPrimaryMenuOption = 0;

    //日志的二级菜单选项
    private string[] logMenuOptions = { "<size=16>全部日志</size>", "<size=16>只看普通</size>", "<size=16>只看警告</size>", "<size=16>只看错误</size>" };
    private int selectedLogMenuOption = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Application.logMessageReceived += LogCallBack;
    }

    private void OnGUI()
    {
        if (!showDebugWindow)
        {
            //显示Debug工具箱按钮
            if (GUILayout.Button("<size=18>Debug工具箱</size>", GUILayout.Width(150), GUILayout.Height(50)))
            {
                showDebugWindow = true;
            }
        }
        else
        {
            //创建弹窗
            GUILayout.Window(0, new Rect(0, 0, Screen.width / 2, Screen.height / 2), DebugWindowFunction, "<size=18>海星的Debug工具箱</size>");
        }
    }

    private void DebugWindowFunction(int windowID)
    {
        GUILayout.Space(5f);
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();

        selectedPrimaryMenuOption = GUILayout.SelectionGrid(selectedPrimaryMenuOption, primaryMenuOptions, primaryMenuOptions.Length, GUILayout.Height(buttonHeight));

        if (GUILayout.Button("<size=18>关闭</size>", GUILayout.Height(buttonHeight)))
        {
            showDebugWindow = false;
        }
        GUILayout.EndHorizontal();

        // 根据一级菜单选项显示不同的二级菜单
        switch (selectedPrimaryMenuOption)
        {
            case 0:
                GUILayout.BeginHorizontal();
                selectedLogMenuOption = GUILayout.SelectionGrid(selectedLogMenuOption, logMenuOptions, logMenuOptions.Length, GUILayout.Height(buttonHeight));

                if (GUILayout.Button("<size=16>清除日志</size>", GUILayout.Height(buttonHeight)))
                {
                    logList.Clear();
                    normalLog.Clear();
                    warningLog.Clear();
                    errorLog.Clear();
                    scrollPosition = Vector2.zero;
                }
                GUILayout.EndHorizontal();

                #region 绘制ScrollView
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Width(Screen.width / 2 - 30), GUILayout.Height(Screen.height / 2 - 100));

                switch (selectedLogMenuOption)
                {
                    case 0:
                        foreach (string log in logList)
                        {
                            GUILayout.Label(log);
                        }
                        break;
                    case 1:
                        foreach (string log in normalLog)
                        {
                            GUILayout.Label(log);
                        }
                        break;
                    case 2:
                        foreach (string log in warningLog)
                        {
                            GUILayout.Label(log);
                        }
                        break;
                    case 3:
                        foreach (string log in errorLog)
                        {
                            GUILayout.Label(log);
                        }
                        break;
                }
                GUILayout.EndScrollView();
                #endregion
                break;
            case 2:
                GUILayout.BeginVertical();
                if (GUILayout.Button("<size=16>日志捕获测试</size>", GUILayout.Height(buttonHeight)))
                {
                    Debug.Log("常规日志");
                    Debug.LogWarning("警告日志");
                    Debug.LogError("错误日志");
                }
                //这里的路径是持久化路径 windows的appdata下的存档 手机上估计用不了
                if (GUILayout.Button("<size=16>打开游戏存档文件夹</size>", GUILayout.Height(buttonHeight)))
                {
                    System.Diagnostics.Process.Start(Application.persistentDataPath);
                }
                GUILayout.EndVertical();
                break;
            case 3:
                GUILayout.BeginVertical();
                GUILayout.Label("<size=17>" + "当前操作系统：" + SystemInfo.operatingSystem + "</size>");
                GUILayout.Label("<size=17>" + "当前设备名：" + SystemInfo.deviceName + "</size>");
                GUILayout.Label("<size=17>" + "当前设备型号：" + SystemInfo.deviceModel + "</size>");
                GUILayout.Label("<size=17>" + "当前图形设备名：" + SystemInfo.graphicsDeviceName + "</size>");
                GUILayout.Label("<size=17>" + "当前处理器类型：" + SystemInfo.processorType + "</size>");
                GUILayout.Label("<size=17>" + "当前处理器核心数量：" + SystemInfo.processorCount + "</size>");
                GUILayout.Label("<size=17>" + "当前系统内存大小：" + SystemInfo.systemMemorySize + " MB" + "</size>");
                GUILayout.EndVertical();
                break;
        }
        GUILayout.EndVertical();
    }

    private void LogCallBack(string content, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Log:
                string log = "<size=15><color=white>" + "【日志】" + content + "</color></size>";
                normalLog.Add(log);
                logList.Add(log);
                break;
            case LogType.Warning:
                string warning = "<size=15><color=yellow>" + "【警告】" + content + "</color></size>";
                warningLog.Add(warning);
                logList.Add(warning);
                break;
            case LogType.Error:
                string error = "<size=15><color=red>" + "【错误】" + content + "</color></size>";
                errorLog.Add(error);
                logList.Add(error);
                break;
        }

        scrollPosition.y = Mathf.Infinity;
    }
}