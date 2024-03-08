BasePanel:subClass("Inventory")

function Inventory:Init()
    self.base.Init(self, "Inventory")
end
Inventory:Init()

function Inventory:OnStart()

    self:GetControl("Back", "Button").onClick:AddListener
    (
        function()
            PanelManager:ShowPanel()
        end
    )

end

function Inventory:OnEnable()
    Debug.Log("背包面板被打开了");
end