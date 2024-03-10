BasePanel:subClass("Inventory")

function Inventory:Init()
    self.base.Init(self, "Inventory")
end
Inventory:Init()

--因为没有实际的账号数据 所以做假数据 1000条
Inventory.Data = {}

Inventory.content = nil
Inventory.scrollView = nil
Inventory.gridList = {}

function Inventory:OnStart()

    self.content = self:GetControl("Content", "Image")
    self.scrollView = self:GetControl("Scroll View", "ScrollRect")

    self:GetControl("Back", "Button").onClick:AddListener
    (
        function()
            PanelManager:ShowPanel()
        end
    )

    for i = 1, 1000 do
        self.Data[i] = {45869, "弯弯猫枕头", 10}
    end
    
    --同屏展示的个数为16 但是实际滚动出现的个数为20
    --lua下标从1开始 谨记
    for i = 1, 20 do
        self.gridList[i] = LoadPrefab("ItemGrid")
        local obj = self.gridList[i]
        obj.transform:SetParent(self.content.transform, false)

        nowGrid = InventoryItemGrid:Init(obj)
        nowGrid:SetGridData(self.Data[i])
    end

    self.scrollView.onValueChanged:AddListener
    (
        function(value)
            self:UpdateList(value.y)
        end
    )
end

function Inventory:OnEnable()
    --Debug.Log("背包面板被打开了");
end

function Inventory:UpdateList(scrollPositon)
    print(scrollPositon)
end