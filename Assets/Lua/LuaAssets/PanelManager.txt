Object:subClass("PanelManager")

--加载进来的物体Dic Key是name Value是GameObject
PanelManager.loadedPanels = {}
--顺序缓存 栈 或者说实际是个List 缓存的只是个string
PanelManager.panelStack = {}
--当前面板
PanelManager.nowPanel = nil

--Lua实例Dic Key是name Value是lua对象
PanelManager.LuaEntity = {}

--如果有该面板 直接返回 没有就加载后再返回
function PanelManager:GetPanel(panelName)

    if self.loadedPanels[panelName] == nil then

        local obj = LoadPrefab(panelName)
        obj.transform:SetParent(Canvas, false)
        obj:SetActive(false)
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
            --加载面板
            self.nowPanel = self:GetPanel(panelName)
            --进栈
            table.insert(self.panelStack, panelName)
            --激活 添加监听
            self.nowPanel:SetActive(true)
            self:AddListener(panelName)

            --后面逻辑不执行
            return 
        end
    end

    --不管空不空 当前面板都先关了
    self.nowPanel:SetActive(false)
    if panelName == nil then
        --传空就返回上一级 退栈 然后调用末位面板
        table.remove(self.panelStack)
        local name = self.panelStack[#self.panelStack]
        print(name)
        self.nowPanel = self:GetPanel(name)
        self.nowPanel:SetActive(true)
        self:AddListener(name)
    else
        --非空就打开新面板 进栈
        self.nowPanel = self:GetPanel(panelName)
        table.insert(self.panelStack, panelName)
        self.nowPanel:SetActive(true)
        self:AddListener(panelName)
    end 
end

function PanelManager:AddListener(panelName)
    --拿到lua实例来做OnStart和OnEnable逻辑
    local panelEntity = self.LuaEntity[panelName]

    if panelEntity == nil then 
        Debug.LogError("没有拿到该面板的Lua实体，是否脚本没有执行基类的Init流程")
        return
    end

    if panelEntity.OnStartFinished == false then
        panelEntity:OnStart()
        panelEntity.OnStartFinished = true
    end
    panelEntity:OnEnable()
end

