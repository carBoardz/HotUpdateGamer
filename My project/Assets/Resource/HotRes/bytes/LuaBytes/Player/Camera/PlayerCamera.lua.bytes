-- PlayerCamera.lua
local CameraManager = CS.CameraManager.Instance

-- 全局相机引用，供输入控制使用
FreeLookVCam = nil
ThirdPersonVCam = nil

-- 自由视角相机参数（FreeLook）
local FreeLook_TopHeight = 2.0         -- 顶部轨道高度（向上看时）
local FreeLook_MiddleHeight = 1.0      -- 中部轨道高度（平视时）
local FreeLook_BottomHeight = 0.5      -- 底部轨道高度（向下看时）
local FreeLook_OrbitRadius = 5.0       -- 轨道半径（默认距离）
local FreeLook_XSpeed = 150            -- 水平旋转速度（越大越快）
local FreeLook_YSpeed = 2              -- 垂直旋转速度（越小越稳）
local FreeLook_YMin = -20              -- 俯仰最低角度（防止穿地）
local FreeLook_YMax = 60               -- 俯仰最高角度
local FreeLook_InvertY = true          -- 是否反转 Y 轴（true=飞机摇杆模式）

-- 锁定视角相机参数（第三人称跟随，战斗/瞄准用）
local ThirdPerson_Distance = 4.5       -- 相机距离
local ThirdPerson_ShoulderX = 0.6      -- 肩膀偏移 X（右为正）
local ThirdPerson_ShoulderY = 1.2      -- 肩膀偏移 Y（上为正）
local ThirdPerson_ShoulderZ = 0.0      -- 肩膀偏移 Z（前为正）
local ThirdPerson_DampingX = 0.5       -- 水平阻尼
local ThirdPerson_DampingY = 0.4       -- 垂直阻尼
local ThirdPerson_DampingZ = 0.5       -- 距离阻尼
local ThirdPerson_VerticalArm = 0.4    -- 垂直臂长
local ThirdPerson_CameraSide = 1       -- 1=右肩视角，0=居中，-1=左肩
local BlendTime = 0.3                  -- 相机切换混合时间（秒）

-- ============================================

-- 初始化逻辑
EventCenter:RegisterLua("OnPlayerSpawned", function(playerTransform)
    if not playerTransform then return end

    local neck = playerTransform:Find("root/pelvis/spine_01/spine_02/spine_03/neck_01")
    local lookTarget = neck or playerTransform

    -- 创建平滑目标（空物体，挂在玩家根节点下，跟随玩家）
    local smoothGo = CS.UnityEngine.GameObject("LookAt")
    smoothGo.transform.parent = playerTransform
    smoothGo.transform.position = lookTarget.position
    smoothLookTarget = smoothGo.transform

    -- 1. 创建自由环绕相机（日常移动用）
    FreeLookVCam = CameraManager:CreateFreeLookCamera("FreeLook_Player")
    CameraManager:SetupFreeLookCamera(
        FreeLookVCam,
        playerTransform,
        smoothLookTarget,
        FreeLook_TopHeight,
        FreeLook_MiddleHeight,
        FreeLook_BottomHeight,
        FreeLook_OrbitRadius,
        FreeLook_XSpeed,
        FreeLook_YSpeed,
        FreeLook_YMin,
        FreeLook_YMax,
        FreeLook_InvertY
    )
    FreeLookVCam.Priority = 5

    -- 2. 创建锁定视角相机（战斗/瞄准用）
    ThirdPersonVCam = CameraManager:CreateVirtualCamera("VCam_Follow_Player")
    ThirdPersonVCam.Follow = playerTransform
    ThirdPersonVCam.LookAt = smoothLookTarget
    CameraManager:SetupThirdPersonFollow(
        ThirdPersonVCam,
        ThirdPerson_Distance,
        ThirdPerson_ShoulderX,
        ThirdPerson_ShoulderY,
        ThirdPerson_ShoulderZ,
        ThirdPerson_DampingX,
        ThirdPerson_DampingY,
        ThirdPerson_DampingZ,
        ThirdPerson_VerticalArm,
        ThirdPerson_CameraSide
    )
    ThirdPersonVCam.Priority = 0  -- 初始不激活

    -- 设置默认混合时间
    CS.CameraManager.Instance.Brain.m_DefaultBlend.m_Time = BlendTime

    print("[PlayerCamera] 相机系统就绪")
 
    require("CameraInput")
end)