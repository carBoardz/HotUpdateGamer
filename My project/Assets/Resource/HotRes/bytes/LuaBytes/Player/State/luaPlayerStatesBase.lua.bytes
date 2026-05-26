require("Object")

Object:subClass("luaPlayerStatesBase")

function LuaPlayerStateBase:Enter() end
function LuaPlayerStateBase:Exit() end
function LuaPlayerStateBase:OnUpdate() end
function LuaPlayerStateBase:OnFixedUpdate() end
function LuaPlayerStateBase:OnLateUpdate() end

function LuaPlayerStateBase:SwitchState(state)
	self.csharp:SwitchState(state);
end