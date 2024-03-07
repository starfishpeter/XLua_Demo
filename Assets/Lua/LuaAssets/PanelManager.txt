Object:subClass("PanelManager")

PanelManager.loadedPanels = {}
PanelManager.panelStack = {}
PanelManager.nowPanel = nil

--如果有该面板 直接返回 没有就加载后再返回
function PanelManager:GetPanel(panelName)
    if self.loadedPanels[panelName] == nil then

        local obj = LoadPrefab(panelName)
        obj.transform:SetParent(Canvas, false)

        self.loadedPanels[panelName] = obj
        return obj
    else
        return self.loadedPanels[panelName]
    end
end

function PanelManager:ShowPanel(panelName)
    --先判断是不是从来没开过面板
    if self.nowPanel == nil then
        if panelName == nil then 
            Debug.LogError("未打开过任何面板 不能返回上一级")
            --直接跳出
            return
        else
            --加载面板 然后进栈
            self.nowPanel = self:GetPanel(panelName)
            table.insert(self.panelStack, self.nowPanel)
            --后面逻辑不执行
            return 
        end
    end

    --不管空不空 当前面板都先关了
    self.nowPanel:SetActive(false)
    if panelName == nil then
        --传空就返回上一级 退栈 然后调用末位面板
        table.remove(self.panelStack)
        self.nowPanel = self.panelStack[#self.panelStack]
        self.nowPanel:SetActive(true)
    else
        --非空就打开新面板 进栈
        self.nowPanel = self:GetPanel(panelName)
        table.insert(self.panelStack, self.nowPanel)
        self.nowPanel:SetActive(true)
    end 
end

