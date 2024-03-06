--利用面向对象
Object:subClass("BasePanel")

--面板物体
BasePanel.panelObj = nil

--相当于模拟一个字典 键为 控件名 值为控件本身
BasePanel.controls = {}

--事件监听标识
BasePanel.isInitEvent = false

function BasePanel:Init(name)
    if self.panelObj == nil then
        --公共的实例化对象的方法

        --加载进来 并且设置父级
        --self.panelObj = ABMgr:LoadRes("ui", name, typeof(GameObject))
        self.panelObj = LoadPrefab(name)
        self.panelObj.transform:SetParent(Canvas, false)

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
                typeName == "ScrollView" then
                    local controlName = allControls[i].name
                    --print(controlName, typeName)
                    self:AddControl(controlName, allControls[i])
                end
        end

        for i = 0, allTMPText.Length - 1 do
            local controlName = allTMPText[i].name
            --print(controlName, "TMP")
            self:AddControl(controlName, allTMPText[i])
        end
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

--得到控件 根据 控件依附对象的名字 和 控件的类型字符串名字 Button Image Toggle
function BasePanel:GetControl(name, typeName)
    if self.controls[name] ~= nil then
        local sameNameControls = self.controls[name]
        if sameNameControls[typeName] ~= nil then
            return sameNameControls[typeName]
        end
    end
    return nil
end

function BasePanel:ShowMe(name)
    self:Init(name)
    self.panelObj:SetActive(true)
end

function BasePanel:HideMe()
    self.panelObj:SetActive(false)
end
