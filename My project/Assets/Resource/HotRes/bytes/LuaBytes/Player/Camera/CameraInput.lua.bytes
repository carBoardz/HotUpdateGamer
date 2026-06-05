-- CameraInput.lua

-- ========== 参数设置 ==========
local CameraManager = CS.CameraManager.Instance
local sensitivityX = 6    -- 水平旋转灵敏度
local sensitivityY = 4    -- 垂直旋转灵敏度
local invertY = true       -- 是否反转Y轴
local zoomSpeed = 2.0
local minDist = 2.5
local maxDist = 8.0
local currentRadius = 5.0   -- 初始距离

-- ========== 平滑相关 ==========
local smoothLookTarget = nil
local neckSmoothSpeed = 6.5          -- 平滑速度

-- ========== 锁定状态记忆 ==========
local wasLocked = false   -- 上一帧是否锁定，用于检测状态变化

-- ========== 工具函数 ==========
local function clamp(val, minVal, maxVal)
    if val < minVal then return minVal end
    if val > maxVal then return maxVal end
    return val
end

-- ========== LookAt的平滑更新 ==========
local function UpdateSmoothLookTarget()
    if not smoothLookTarget then
        -- 尝试从 PlayerCamera 获取平滑目标（通过全局变量）
        smoothLookTarget = _G.smoothLookTarget or smoothLookTarget -- _G.smoothLookTarget在PlayerCamera.lua里面设置
        if not smoothLookTarget then return end
    end

    local player = CS.PlayerManager.Instance and CS.PlayerManager.Instance.CurrentPlayer
    if not player then return end

    local neck = player.transform:Find("root/pelvis/spine_01/spine_02/spine_03/neck_01")
    if not neck then return end

    local targetPos = neck.position
    local currentPos = smoothLookTarget.position
    local newPos = CS.UnityEngine.Vector3.Lerp(
        currentPos,
        targetPos,
        CS.UnityEngine.Time.deltaTime * neckSmoothSpeed
    )
    smoothLookTarget.position = newPos
end

-- ========== 相机输入更新 ==========
function UpdateCameraInput()
    if not FreeLookVCam then return end

    local playerCtrl = CS.PlayerManager.Instance and CS.PlayerManager.Instance.CurrentController
    if not playerCtrl then return end

    UpdateSmoothLookTarget()

    -- 读取鼠标输入
    local lookInput = playerCtrl.LookInput
    local mouseX = lookInput.x
    local mouseY = lookInput.y
    local scroll = playerCtrl.ZoomInput
    local lockTarget = playerCtrl.HasLockTargetInput
    local targetEnemy = playerCtrl.LockTarget -- 锁定的目标 Transform

    -- 锁定状态切换逻辑
    if lockTarget and not wasLocked then
        -- 刚刚进入锁定：切换到锁定相机
        ThirdPersonVCam.Priority = 20
        FreeLookVCam.Priority = 0
        if targetEnemy then
            ThirdPersonVCam.LookAt = targetEnemy
        end
    elseif not lockTarget and wasLocked then
        -- 解除锁定时，同步自由相机的角度到锁定相机的当前视角
        local playerTransform = CS.PlayerManager.Instance.CurrentPlayer.transform
        local lockedCamPos = ThirdPersonVCam.transform.position
        local playerPos = playerTransform.position

        -- 世界空间的相机方向
        local camDir = lockedCamPos - playerPos
        camDir = camDir.normalized

        -- 水平方向（忽略垂直）
        local horizDir = CS.UnityEngine.Vector3(camDir.x, 0, camDir.z).normalized

        if horizDir.magnitude > 0.01 then
            -- 计算水平角度（相对于角色前方）
            local forward = playerTransform.forward
            local signedAngle = CS.CameraManager.GetSignedAngle(forward, horizDir, CS.UnityEngine.Vector3.up)

            -- 设置 FreeLook 的 X 轴值（度）
            CameraManager:SetFreeLookAxisX(FreeLookVCam, signedAngle)

            -- 计算俯仰角度
            local pitchRad = math.asin(camDir.y)
            local pitchDeg = pitchRad * 180.0 / 3.141592653589793
            -- 钳位到允许范围
            pitchDeg = clamp(pitchDeg, -20, 60)

            -- 设置 FreeLook 的 Y 轴值
            CameraManager:SetFreeLookAxisY(FreeLookVCam, pitchDeg)
        end

        -- 然后才切换优先级，让混合过渡平滑开始
        FreeLookVCam.Priority = 5
        ThirdPersonVCam.Priority = 0
    end


    if lockTarget then
        
        -- ========== 锁定状态下的操作 ==========
         -- 更新锁定目标（如果目标可能移动或切换）
        if targetEnemy then
            ThirdPersonVCam.LookAt = targetEnemy
        end

        -- 滚轮缩放仍然有效
        if math.abs(scroll) > 0.01 then
            currentRadius = clamp(currentRadius - scroll * zoomSpeed, minDist, maxDist)
            CameraManager:SetThirdPersonDistance(ThirdPersonVCam, currentRadius)
        end

    else
        
        -- ========== 自由视角下的操作 ==========
        -- 水平旋转
        if math.abs(mouseX) > 0.01 then
            local newX = CameraManager:GetFreeLookAxisX(FreeLookVCam) + mouseX * sensitivityX * 0.1
            CameraManager:SetFreeLookAxisX(FreeLookVCam, newX)
        end

        -- 垂直旋转
        if math.abs(mouseY) > 0.01 then
            local currentY = CameraManager:GetFreeLookAxisY(FreeLookVCam)
            local deltaY = mouseY * sensitivityY * 0.1 * (invertY and -1 or 1)
            local newY = clamp(currentY + deltaY, -20, 60)
            CameraManager:SetFreeLookAxisY(FreeLookVCam, newY)
        end

        -- 滚轮缩放
        if math.abs(scroll) > 0.01 then
            currentRadius = clamp(currentRadius - scroll * zoomSpeed, minDist, maxDist)
            CameraManager:SetFreeLookRadius(FreeLookVCam, currentRadius)
        end
    
    end

end

EventCenter:RegisterLua("PlayerLuaUpdate", UpdateCameraInput, 0, false, false)
