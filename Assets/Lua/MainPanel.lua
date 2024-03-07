BasePanel:subClass("Main")

--这里每个类都要重复调用一次类初始化 感觉有优化空间
function Main:Init()
    self.base.Init(self, "Main")
end
Main:Init()

function Main:OnStart()
    self:GetControl("ShopButton", "Button").onClick:AddListener
    (
        function()
            PanelManager:ShowPanel("Shop")
        end
    )

    self:GetControl("InventoryButton", "Button").onClick:AddListener
    (
        function()
            PanelManager:ShowPanel("Inventory")
        end
    )
    
    --更改文字 主要用于之后热更的展示
    self:GetControl("TextUpdateTest","TextMeshProUGUI").text = "xLua热更Demo"
end

function Main:OnEnable()
    Debug.Log("主界面面板被打开了");
end