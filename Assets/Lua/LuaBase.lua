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
--Canvas = GameObject.Find("Canvas")

--公共方法
local LoadAssetMethod = xlua.get_generic_method(LoadManager, "LoadAsset")

--动态加载资源的泛型方法
function LoadAsset(AAkey, action, objectType)
    -- 创建泛型委托类型
    local actionType = typeof(UnityAction).MakeGenericType(objectType)
    -- 创建泛型委托实例
    local typedAction = actionType(action)
    -- 调用泛型方法
    LoadAssetMethod:Invoke(LoadManager.Instance, {AAkey, typedAction})
    end

--加载预制体 默认为AA下Prefab文件夹
function LoadPrefab(AAkey)
    local loadedObj = nil

    LoadAsset("Prefab/"..AAkey, function(obj)
        loadedObj = obj
    end, typeof(GameObject))
    return loadedObj
    end