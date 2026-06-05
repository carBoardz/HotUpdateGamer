require("Object")
require("SplitTools")
Json = require("JsonUtility")
util = require("xlua.util")

GameObject = CS.UnityEngine.GameObject
Resources = CS.UnityEngine.Resources
Transform = CS.UnityEngine.Transform
RectTransform = CS.UnityEngine.RectTransform
SpriteAtlas = CS.UnityEngine.U2D.SpriteAtlas
TextAsset = CS.UnityEngine.TextAsset 
Vector3 = CS.UnityEngine.Vector3
Vector2 = CS.UnityEngine.Vector2
Quaternion = CS.UnityEngine.Quaternion

UI = CS.UnityEngine.UI
Image = UI.Image
Text = UI.Text
TextMeshProUGUI = CS.TMPro.TextMeshProUGUI
Button = UI.Button
Toggle = UI.Toggle
ScrollRect = UI.ScrollRect
UIBehaviour = CS.UnityEngine.EventSystems.UIBehaviour
--Canvas = GameObject.Find("Canvas").transform

-- 全局唯一配置表	
Config = Config or {}

--ABManager、EventCenter、LuaMgr、

--状态机中各种状态初始化
require("PlayerStateRegister")
--玩家相机初始化注册事件
require("PlayerCamera")