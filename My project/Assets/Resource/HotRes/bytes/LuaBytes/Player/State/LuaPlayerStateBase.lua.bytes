-- LuaPlayerStateBase.lua
require("Object")
Object:subClass("LuaPlayerStateBase")

local AnimCtrl = require("AnimationController")

function LuaPlayerStateBase:Enter() end
function LuaPlayerStateBase:Exit() end
function LuaPlayerStateBase:OnUpdate() end

function LuaPlayerStateBase:OnFixedUpdate()
    local csharp = self.csharp
    if not csharp then return end

    local controller = csharp.controller
    local anim = csharp:GetAnimator()
    if not anim then return end

    local worldDir = CS.UnityEngine.Vector3.zero

    if controller.HasMoveInput then
        local moveInput = controller._inputDirection   -- Vector2
        local camForward = controller:GetCameraForward()
        local camRight = controller:GetCameraRight()
        -- 计算世界空间移动方向
        local dir = camRight * moveInput.x + camForward * moveInput.y
        dir.y = 0
        if dir.magnitude > 0.01 then
            worldDir = dir.normalized

            -- 平滑旋转角色（视角转向）
            local targetRot = CS.UnityEngine.Quaternion.LookRotation(worldDir)
            local currentRot = controller.transform.rotation
            local smoothRot = CS.UnityEngine.Quaternion.Slerp(
                currentRot,
                targetRot,
                10.0 * CS.UnityEngine.Time.fixedDeltaTime
            )
            controller.transform.rotation = smoothRot
        end
    end

    -- 统一更新动画参数（包括方向）
    AnimCtrl.UpdateAnimation(controller, anim, worldDir)
end

function LuaPlayerStateBase:OnLateUpdate() end

function LuaPlayerStateBase:SwitchState(state)
    self.csharp.stateMachine:SwitchState(state)
end

return LuaPlayerStateBase