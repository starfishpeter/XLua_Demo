# XLua_Demo

A Demo for testing xlua and Addressable

一个简单的用于测试xlua框架和Addreassable联动的Demo

## 尚未解决的问题

**1.ScrollView的OnValueChanged(Vector2) xlua无法正常调用**

尝试过在LuaCallCsharp里添加了泛型解析但没有生效，请教过其他人，但暂时找不到原因。

**2.从预加载场景进入主场景时lua逻辑会不执行 暂未找到具体故障原因**

## 代码架构

### Corel 核心代码

Corel文件夹下为通用的逻辑框架，编辑器下和运行时做了区分

### 编辑器下

1. CreateScripts 创建脚本用的模板工具
2. ExcelToScriptObject Excel文件直接转换为SO资产和解析用C#脚本
3. EditorTools 编辑器下常用API
4. LuaFileCopier 快速拷贝lua文件并修改后缀

### 运行时

1. Singleton 各种各样的单例基类
2. ObjectPool 对象池 GameObject单独封装
3. AudioManager 音效管理
4. DebugWindow 运行时查看日志信息 工具箱
5. JsonDataControl 基于Newtonsoft Json的解析脚本
6. LoadManager Addressable的加载管理
7. RuntimeTools 运行时常用API
8. SceneControl Addressable下的场景管理

### lua 代码

1. LuaEntry C#调用lua的入口
2. LuaBase 各种基类和缩写
3. Object lua面向对象的实现
4. BasePanel 基础的面板基类 用于绑定UI组件
5. PanelManager 面板管理