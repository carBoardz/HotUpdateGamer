require("LuaUIMVCBase")
LuaUIMVCBase:subClass("UILoadingController")
local UIAutoBind = require("UILoading_AutoBind")

local util = require "xlua.util"
function UILoadingController:OnInit(view,userData)
	LuaUIMVCBase.OnInit(self, view, userData)

	UIAutoBind:AutoBind(view);
	self.bind = UIAutoBind
end

function UILoadingController:OnButtonClick(btnName)
	
end

function UILoadingController:OnShow(loadingText)
	self.bind.loadingText_TextMeshProUGUI.text = loadingText
	CS.LoadingManager.Instance:OnShowAnimFinished()
end

function UILoadingController:UpdateProgress(downloadedBytes,totalBytes,DownLoadProgress,msg)
	self.bind.uILoading_Slider.value = DownLoadProgress
	if DownLoadProgress == 1 then
		self.bind.loadingText_TextMeshProUGUI.text = "Waiting for resource initialization"
	end
	
end

function UILoadingController:OnHide(loadingText)
	self.bind.loadingText_TextMeshProUGUI.text = loadingText
	CS.LoadingManager.Instance:OnHideAnimFinished()
end

function UILoadingController:DisposeView()
	
end

function UILoadingController:OnDestroy()
    self.view = nil
    --self.model = nil
end

return UILoadingController