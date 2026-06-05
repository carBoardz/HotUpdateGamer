require("LuaPlayerStateBase")
LuaPlayerStateBase:subClass("PlayerWalk_RunState")
local AnimCtrl = require("AnimationController")

function PlayerWalk_RunState:new()
	local obj = self.base.new(self)
	return obj
end

function PlayerWalk_RunState:Enter( )
	print("玩家进入PlayerWalk_RunState")
	self.base.Enter(self)
	local csharp = self.csharp
	csharp:OnBufferComplete()
end

function PlayerWalk_RunState:Exit( )
	self.base.Exit(self)
end

function PlayerWalk_RunState:OnUpdate( )
	self.base.OnUpdate(self)
	local csharp = self.csharp
	csharp:OnBufferComplete()
end

function PlayerWalk_RunState:OnFixedUpdate()
    self.base.OnFixedUpdate(self)
    local csharp = self.csharp
    local controller = csharp.controller
    local anim = csharp:GetAnimator()

    if not controller.HasMoveInput then
        csharp.stateMachine:SwitchState("PlayerIdleState")
    elseif controller.HasCrouchInput then
        csharp.stateMachine:SwitchState("PlayerCrouchState")
    end
end

function PlayerWalk_RunState:OnLateUpdate( )
	self.base.OnLateUpdate(self)
end

return PlayerWalk_RunState