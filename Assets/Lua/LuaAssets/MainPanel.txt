BasePanel:subClass("Main")

function Main:OnStart()
    self.base.Init(self, "Main")
    self:GetControl("ShopButton", "Button").onClick:AddListener
    (
        function()
            PanelManager:ShowPanel("Shop")
        end
    )
    
    --更改文字 主要用于之后热更的展示
    self:GetControl("TextUpdateTest","TextMeshProUGUI").text = "xLua热更Demo"
end

Main:OnStart()