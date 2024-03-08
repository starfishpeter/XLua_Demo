BasePanel:subClass("Shop")

function Shop:Init()
    self.base.Init(self, "Shop")
end
Shop:Init()

Shop.RightBottom = nil
Shop.ItemPrefab = LoadPrefab("ShopItemGrid")

function Shop:OnStart()

    self.RightBottom = self:GetControl("RightBottom", "Image").gameObject

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

    --有三个按钮成组 组成一个Tab
    --这里其实可以优化一下 写一个批量给按钮赋值近似逻辑的接口
    self:GetControl("Tab1", "Button").onClick:AddListener
    (
        function()
            self:InitShopItems(1)
        end
    )

    self:GetControl("Tab2", "Button").onClick:AddListener
    (
        function()
            self:InitShopItems(2)
        end
    )

    self:GetControl("Tab3", "Button").onClick:AddListener
    (
        function()
            self:InitShopItems(3)
        end
    )

    self:InitShopItems(1)
end

function Shop:OnEnable()
    Debug.Log("商店面板被打开了");
end

function Shop:InitShopItems(sheet)
    self:RecycleShopItems()

    --要从不同的sheet里去拿数据
    if sheet == 1 then
        sheetOne = Table._ShopOneItems
        --数组索引依然是C#规则 从零开始而不是一
        for i=0, sheetOne.Length-1 do
            self:CreateShopItems(sheetOne[i])
        end
    elseif sheet == 2 then
        sheetTwo = Table._ShopTwoItems
        for i=0, sheetTwo.Length-1 do
            self:CreateShopItems(sheetTwo[i])
        end
    elseif sheet == 3 then
        sheetThree = Table._ShopThreeItems
        for i=0, sheetThree.Length-1 do
            self:CreateShopItems(sheetThree[i])
        end
    end
end

--创建预制体实例
function Shop:CreateShopItems(itemData)
    --问题：创建实例的时候如何绑定lua实体呢？
    local Grid = GameObjectPoolManager:GetPoolItem(self.ItemPrefab, self.RightBottom.transform)
    --创建lua实体的时候直接把自己传进来
    nowGridLuaEntity = ShopItemGrid:Init(Grid)
    --设置数据
    nowGridLuaEntity:SetGridData(itemData)
end

--一次性回收所有物体
function Shop:RecycleShopItems()
    GameObjectPoolManager:RecycleAll(self.ItemPrefab)
end
