require("LuaPlayerStateBase")
LuaPlayerStateBase:subClass("PlayerIdleState")
local AnimCtrl = require("AnimationController")

function PlayerIdleState:new()
	local obj = self.base.new(self)
	return obj
end

function PlayerIdleState:Enter( )
	print("玩家进入PlayerIdleState")
	self.base.Enter(self)
	local csharp = self.csharp
	csharp:OnBufferComplete()
end

function PlayerIdleState:Exit( )
	self.base.Exit(self)
end

function PlayerIdleState:OnUpdate( )
	self.base.OnUpdate(self)
	local csharp = self.csharp
    -- 对应 C# 的 base.OnBufferComplete();
	csharp:OnBufferComplete()
end

function PlayerIdleState:OnFixedUpdate( )
	self.base.OnFixedUpdate(self)
	local csharp = self.csharp
    
	-- 对应 C# 的 if (controller.HasMoveInput)
	if csharp.controller.HasMoveInput then
	    -- 有蹲伏输入
	    if csharp.controller.HasCrouchInput then
	            csharp.stateMachine:SwitchState("PlayerCrouchState")
	    else
	        csharp.stateMachine:SwitchState("PlayerWalk_RunState")
	    end
	end

end

function PlayerIdleState:OnLateUpdate( )
	self.base.OnLateUpdate(self)
end

return PlayerIdleState