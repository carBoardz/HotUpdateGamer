-- AnimationController.lua
local AnimationController = {}

-- 缓存 hash
local hashSpeed = CS.UnityEngine.Animator.StringToHash("Speed")
local hashDirX = CS.UnityEngine.Animator.StringToHash("directionX")
local hashDirY = CS.UnityEngine.Animator.StringToHash("directionY")
local hashIsCrouch = CS.UnityEngine.Animator.StringToHash("IsCrouch")
local hashStopType = CS.UnityEngine.Animator.StringToHash("StopType")

-- 平滑系数：值越小越平滑，但反应会慢；建议 5~10，越大响应越快
local SMOOTH_SPEED = 8.0
-- 当前平滑后的方向值
local currentSpeed = 0.0
-- 当前平滑后的方向值
local currentDirX = 0.0
local currentDirY = 0.0
local currentStopType = 0

local function lerp(a, b, t)
    return a + (b - a) * t
end

function AnimationController.UpdateAnimation(controller, animator, worldDir)
    local hasMoveInput = controller.HasMoveInput
    local hasRunInput = controller.HasRunInput
    local hasCrouchInput = controller.HasCrouchInput

    -- 计算目标速度
    local targetSpeed = 0.0
    -- 计算目标方向
    local targetDirX = 0.0
    local targetDirY = 0.0

    if hasMoveInput and worldDir.magnitude > 0.01 then
        targetSpeed = hasRunInput and 2 or 1
        currentStopType = hasRunInput and 1 or 0

        local inverseRot = CS.UnityEngine.Quaternion.Inverse(controller.transform.rotation)
        local localDir = inverseRot * worldDir -- 四元数旋转方向向量
        targetDirX = localDir.x
        targetDirY = localDir.z
        
        else
        targetSpeed = 0
        targetDirX = 0.0
        targetDirY = 0.0
    end

    local dt = CS.UnityEngine.Time.fixedDeltaTime
    -- 平滑速度
    currentSpeed = lerp(currentSpeed, targetSpeed, dt * SMOOTH_SPEED)
    -- 平滑方向
    currentDirX = lerp(currentDirX, targetDirX, dt * SMOOTH_SPEED)
    currentDirY = lerp(currentDirY, targetDirY, dt * SMOOTH_SPEED)

    -- 设置 Animator 参数
    animator:SetFloat(hashSpeed, currentSpeed)
    animator:SetFloat(hashDirX, currentDirX)
    animator:SetFloat(hashDirY, currentDirY)
    animator:SetBool(hashIsCrouch, hasCrouchInput)
    animator:SetFloat(hashStopType, currentStopType)
end

return AnimationController