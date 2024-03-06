--执行基础配置的初始化过程

--引入面向对象相关
require("Object");

--系统类相关
Type = CS.System.Type

--Unity相关类简写
GameObject = CS.UnityEngine.GameObject
Transform = CS.UnityEngine.Transform
RectTransform = CS.UnityEngine.RectTransform

Vector3 = CS.UnityEngine.Vector3
vector2 = CS.UnityEngine.Vector2

UI = CS.UnityEngine.UI
Image = UI.Image
Button = UI.Button

UnityAction = CS.UnityEngine.Events.UnityAction

--框架相关类引入
JsonManager = CS.StarFramework.Runtime.JsonDataControl
LoadManager = CS.StarFramework.Runtime.LoadManager

--公共变量
Canvas = GameObject.Find("Canvas").transform

--公共方法

--加载预制体 默认为AA下Prefab文件夹
function LoadPrefab(AAkey)
    local loadedObj = nil
    loadedObj = LoadManager.Instance:LoadGameObject("Prefab/"..AAkey..".prefab")
    if loadedObj == nil then
        print("加载失败")
        end
    return loadedObj
    end