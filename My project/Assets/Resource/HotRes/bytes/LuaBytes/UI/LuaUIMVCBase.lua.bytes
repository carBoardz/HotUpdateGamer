require("Object")

Object:subClass("LuaUIMVCBase")

function LuaUIMVCBase:New()
	local o = {}
	setmetatable(o,self)
	return o
end

function LuaUIMVCBase:OnInit(view, userData)
    self.view = view
    self.userData = userData
end

function OpenUIAsync(uiName)
	UIManager.Instance.OpenUIAsync(uiName)
end
function ClearAll()
	UIManager.Instance.ClearAll()
end
function CloseUI(uiName)
	UIManager.Instance.CloseUI(uiName)
end