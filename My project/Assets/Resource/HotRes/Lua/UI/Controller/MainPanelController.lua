require("LuaUIMVCBase")
LuaUIMVCBase:subClass("MainPanelController")

local UIAutoBind = require("MainPanel_AutoBind")
local util = require "xlua.util"

--for k, v in pairs(util) do
--    print(k)
--end

print("MainPanelController util loaded: " .. tostring(util))

function MainPanelController:OnInit(view,userData)
	LuaUIMVCBase.OnInit(self, view, userData)

	UIAutoBind:AutoBind(view);
	self.bind = UIAutoBind
end

function MainPanelController:OnButtonClick(btnName)
	if btnName == "StartButton_Button" then
        print("开始游戏按钮被点击")
        LoadSceneMgr:NextLevelAsync(function()
		    print("场景加载完成")
		end)

    elseif btnName == "SettingButton_Button" then
    	print("设置按钮被点击")
        UIManager:OpenUIAsync("SettingsPanel")
        
    elseif btnName == "ExitButton_Button" then
        print("退出游戏按钮被点击")
    end
end

function MainPanelController:OnToggleValueChanged(togName,isOn)

end

--初始化图片等可热更资源
--function MainPanelController:Init()
	--self.mainPanel_Image = view:GetWidget("MainPanel_Image")
	--self.startButton_Image = view:GetWidget("StartButton_Image")
	--self.settingButton_Image = view:GetWidget("SettingButton_Image")
	--self.exitButton_Image = view:GetWidget("ExitButton_Image")

	--self.startButton_Button = view:GetWidget("StartButton_Button")
	--self.settingButton_Button = view:GetWidget("SettingButton_Button")
	--self.exitButton_Button = view:GetWidget("ExitButton_Button")

	--self.startButtonText_TextMeshProUGUI = view:GetWidget("StartButtonText_TextMeshProUGUI")
	--self.settingButtonText_TextMeshProUGUI = view:GetWidget("SettingButtonText_TextMeshProUGUI")
	--self.exitButtonText_TextMeshProUGUI = view:GetWidget("ExitButtonText_TextMeshProUGUI")
--end

return MainPanelController