require("LuaPlayerStateBase")
LuaPlayerStateBase:subClass("PlayerCrouchState")
local AnimCtrl = require("AnimationController")

function PlayerCrouchState:new()
	local obj = self.base.new(self)
	return obj
end

function PlayerCrouchState:Enter( )
	print("玩家进入PlayerCrouchState")
	self.base.Enter(self)
	local csharp = self.csharp
	csharp:OnBufferComplete()
end

function PlayerCrouchState:Exit( )
	self.base.Exit(self)
end

function PlayerCrouchState:OnUpdate( )
	self.base.OnUpdate(self)
	local csharp = self.csharp
    -- 对应 C# 的 base.OnBufferComplete();
	csharp:OnBufferComplete()
end

function PlayerCrouchState:OnFixedUpdate()
    self.base.OnFixedUpdate(self)
    local csharp = self.csharp

    if csharp.controller.HasMoveInput then
        if csharp.controller.HasCrouchInput then
            -- 仍在蹲伏移动中，保持自身
        else
            -- 松开蹲伏但还在移动，回到 Walk
            csharp.stateMachine:SwitchState("PlayerWalk_RunState")
        end
    else
        -- 没有移动输入，回到 Idle
        csharp.stateMachine:SwitchState("PlayerIdleState")
    end
end

function PlayerCrouchState:OnLateUpdate( )
	self.base.OnLateUpdate(self)
end

return PlayerCrouchState