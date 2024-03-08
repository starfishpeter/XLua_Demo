BasePanel:subClass("ShopItemGrid")

ShopItemGrid.countText = nil
ShopItemGrid.image = nil
ShopItemGrid.nameText = nil

--格子并非面板 不会由面板管理器来调用 所以初始化函数需要更改
function ShopItemGrid:Init(obj)
    --调用基类的另外一个Init函数
    self.base:InitGrid(obj)
    --把自己的lua实体返回出来
    return self
end

function ShopItemGrid:SetGridData(itemData)

    self.countText = self:GetControl("Count","TextMeshProUGUI")
    self.nameText = self:GetControl("Name","TextMeshProUGUI")
    self.image = self:GetControl("Icon","Image")

    self.countText.text = itemData.ItemCount
    self.nameText.text = itemData.ItemName
    self.image.sprite = LoadSprite(itemData.ItemID)
end

