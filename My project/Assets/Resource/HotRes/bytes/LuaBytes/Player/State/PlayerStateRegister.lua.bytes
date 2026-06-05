-- PlayerStateRegister.lua
require("PlayerIdleState")
require("PlayerWalk_RunState")
require("PlayerCrouchState")

-- 在玩家生成后执行注册
EventCenter:RegisterLua("OnPlayerSpawned", function(playerTransform)
    -- 通过 PlayerManager 拿到状态机（确保此时 CurrentController 已存在）
    local playerCtrl = CS.PlayerManager.Instance.CurrentController
    if not playerCtrl then return end

    local stateMachine = playerCtrl.playerMovementStateMachine

    -- 创建状态实例并注册
    local idleState = PlayerIdleState:new()
    stateMachine:LuaRisterState("PlayerIdleState", idleState)

    local walk_RunState = PlayerWalk_RunState:new()
    stateMachine:LuaRisterState("PlayerWalk_RunState", walk_RunState)

    local crouchState = PlayerCrouchState:new()
    stateMachine:LuaRisterState("PlayerCrouchState", crouchState)

    -- 设置初始状态
    stateMachine:SetInitialState("PlayerIdleState")

    print("所有状态注册完成")
end, 1, false, true)  -- once = true，只执行一次