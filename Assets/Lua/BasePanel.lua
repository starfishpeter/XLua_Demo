--利用面向对象
Object:subClass("BasePanel")

--面板物体
BasePanel.panelObj = nil

--相当于模拟一个字典 键为 控件名 值为控件本身
BasePanel.controls = {}

--OnStart标识 用于确保事件只执行一次
BasePanel.OnStartFinished = false

--默认面板用的Init函数
function BasePanel:Init(name)
    if self.panelObj == nil then

        --从面板管理器直接拿到Obj
        self.panelObj = PanelManager:GetPanel(name)

        --Init的时候要把lua实体传出来 方便调用
        PanelManager.LuaEntity[name] = self

        self:CollectControls()
    end
end

--设计上的缺陷吧。。实际应该分开的
function BasePanel:InitGrid(obj)
    if obj ~= nil then
        self.panelObj = obj
        self:CollectControls()
    end
end

function BasePanel:CollectControls()
    --GetComponentsInChildren
    --找到所有UI控件 存起来 但这里有个问题
    --TextMeshPro的基类不是UIBehaviour
    --这里先得到除了文字以外其它的控件 然后再拿文字组件
    local allControls = self.panelObj:GetComponentsInChildren(typeof(UIBehaviour))
    local allTMPText = self.panelObj:GetComponentsInChildren(typeof(TMPro.TMP_Text))
    
    for i = 0, allControls.Length-1 do
        --这里还可以细分 只添加自己需要的类型进来
        local typeName = allControls[i]:GetType().Name
        if typeName == "Text" or
            typeName == "Image" or
            typeName == "Button" or
            typeName == "ScrollRect" then
                local controlName = allControls[i].name
                --print(controlName, typeName)
                self:AddControl(controlName, allControls[i])
            end
    end

    for i = 0, allTMPText.Length - 1 do
        local controlName = allTMPText[i].name
        self:AddControl(controlName, allTMPText[i])
    end
end

--用于添加组件的函数
function BasePanel:AddControl(name, control)
    --这里要加一个类型名来判断 否则同物体下不同组件会混淆
    local typeName = control:GetType().Name
    if self.controls[name] ~= nil then
        self.controls[name][typeName] = control
    else
        self.controls[name] = {[typeName] = control}
    end
end

--得到控件 根据 控件依附对象的名字 和 控件的类型名
function BasePanel:GetControl(name, typeName)
    if self.controls[name] ~= nil then
        local sameNameControls = self.controls[name]
        if sameNameControls[typeName] ~= nil then
            return sameNameControls[typeName]
        end
    end
    return nil
end

-- OnStart接口
function BasePanel:OnStart()
    --子类可以覆盖此方法来实现自定义的 OnStart 逻辑
end

-- OnEnable接口
function BasePanel:OnEnable()
    --子类可以覆盖此方法来实现自定义的 OnEnable 逻辑
end