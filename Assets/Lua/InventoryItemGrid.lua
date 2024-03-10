BasePanel:subClass("InventoryItemGrid")

InventoryItemGrid.countText = nil
InventoryItemGrid.image = nil
InventoryItemGrid.nameText = nil

--格子并非面板 不会由面板管理器来调用 所以初始化函数需要更改
function InventoryItemGrid:Init(obj)
    --调用基类的另外一个Init函数
    self.base:InitGrid(obj)
    --把自己的lua实体返回出来
    return self
end

function InventoryItemGrid:SetGridData(itemData)

    self.countText = self:GetControl("Count","TextMeshProUGUI")
    self.nameText = self:GetControl("Name","TextMeshProUGUI")
    self.image = self:GetControl("Icon","Image")

    self.countText.text = itemData[3]
    self.nameText.text = itemData[2]
    self.image.sprite = LoadSprite(itemData[1])
end

