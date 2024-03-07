BasePanel:subClass("Shop")

function Shop:Init()
    self.base.Init(self, "Shop")
end
Shop:Init()

function Shop:OnStart()

    self:GetControl("JumpInventory", "Button").onClick:AddListener
    (
        function()
            PanelManager:ShowPanel("Inventory")
        end
    )

    self:GetControl("Close", "Button").onClick:AddListener
    (
        function()
            PanelManager:ShowPanel()
        end
    )

end

function Shop:OnEnable()
    Debug.Log("商店面板被打开了");
end