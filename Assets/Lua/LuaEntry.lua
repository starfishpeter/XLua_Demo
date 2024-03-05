print("Lua逻辑开始执行")

require("LuaBase")

--公共方法可以放在这里
--声明需要放在使用之前
function LoadUIPrefab()
    LoadManager.Instance:LuaTest();
    end

--从这里开始引入UI
require("UIControl")
require("MainPanel")
