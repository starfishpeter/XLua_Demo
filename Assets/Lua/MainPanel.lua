BasePanel:subClass("Main")

function Main:Init(name)
    self.base.Init(self, name)

    --为了只添加一次事件监听
    if self.isInitEvent == false then
        print(self:GetControl("ShopButton", "Button"))
        self.isInitEvent = true
    end
end

Main:ShowMe("Main")