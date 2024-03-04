print("从这里开始为Lua逻辑的入口")

require("LuaBase")

--公共方法可以放在这里
--声明需要放在使用之前
function LoadUIPrefab()
    LoadManager.Instance:LuaTest();
    print("加载物体")
    end

--从这里开始引入UI
require("MainPanel")
