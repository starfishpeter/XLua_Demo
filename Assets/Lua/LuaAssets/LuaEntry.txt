print("Lua逻辑开始执行")

require("LuaBase")

--从这里开始引入UI管理
require("PanelManager")
require("BasePanel")

--从这里开始引入面板具体逻辑
require("MainPanel")
require("ShopPanel")

--最后唤醒主面板
PanelManager:ShowPanel("Main")
