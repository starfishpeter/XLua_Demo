//------------------------------------------------------------
// 脚本名:    EditorTools.cs
// 作者:      海星
// 描述:      编辑器工具箱 封装了快捷用的方法
//------------------------------------------------------------

using UnityEditor;
using System.Reflection;
using UnityEngine;

namespace StarFramework.Editor
{
    public class EditorTools
    {
        /// <summary>
        /// 清除控制台日志
        /// </summary>
        public static void ClearLogs()
        {
            var logEntries = Assembly.GetAssembly(typeof(SceneView)).GetType("UnityEditor.LogEntries");
            var ClearLogMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
            ClearLogMethod.Invoke(null, null);
        }

        /// <summary>
        /// 快速在Hierarchy窗口定位物体
        /// </summary>
        /// <param name="name">需要定位物体的名字</param>
        public static void FocusOnHierarchy(string name)
        {
            //GameObject.Find性能差 目前暂时这么找
            var target = GameObject.Find(name);
            EditorGUIUtility.PingObject(target);
            Selection.activeGameObject = target;
        }
    }
}
