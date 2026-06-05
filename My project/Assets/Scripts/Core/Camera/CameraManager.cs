using Cinemachine;
using MySinleton;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[XLua.LuaCallCSharp]
public class CameraManager : SingletonMono<CameraManager>
{
    public Camera MainCamera { get; private set; }
    public CinemachineBrain Brain { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        GameObject cameraGo = new GameObject("MainCamera");
        cameraGo.transform.SetParent(transform);
        MainCamera = cameraGo.AddComponent<Camera>();
        Brain = cameraGo.AddComponent<CinemachineBrain>();
        Brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        cameraGo.AddComponent<AudioListener>();
    }

    [XLua.LuaCallCSharp]
    public CinemachineVirtualCamera CreateVirtualCamera(string name)
    {
        GameObject vcamGo = new GameObject(name);
        vcamGo.transform.SetParent(transform);
        return vcamGo.AddComponent<CinemachineVirtualCamera>();
    }

    [XLua.LuaCallCSharp]
    public void SetupThirdPersonFollow(
        CinemachineVirtualCamera vcam,
        float distance = 4.5f,
        float shoulderX = 0.6f,
        float shoulderY = 1.2f,
        float shoulderZ = 0.0f,
        float dampingX = 0.5f,
        float dampingY = 0.4f,
        float dampingZ = 0.5f,
        float verticalArmLength = 0.4f,
        int cameraSide = 1,
        float cameraRadius = 0.2f,
        float dampingIntoCollision = 0.6f,
        float dampingFromCollision = 1.2f
    )
    {
        // 1. 添加并配置第三人称组件
        var thirdPerson = vcam.gameObject.AddComponent<Cinemachine3rdPersonFollow>();
        thirdPerson.CameraDistance = distance;
        thirdPerson.ShoulderOffset = new Vector3(shoulderX, shoulderY, shoulderZ);
        thirdPerson.Damping = new Vector3(dampingX, dampingY, dampingZ);
        thirdPerson.VerticalArmLength = verticalArmLength;
        thirdPerson.CameraSide = cameraSide;
        thirdPerson.CameraRadius = cameraRadius;
        thirdPerson.DampingIntoCollision = dampingIntoCollision;
        thirdPerson.DampingFromCollision = dampingFromCollision;

        // 2. 碰撞避免：忽略玩家自身的Layer（需要你在项目中定义"Player" Layer）
        thirdPerson.CameraCollisionFilter = LayerMask.GetMask("Default");
        thirdPerson.IgnoreTag = "Player"; // 假设玩家模型Layer设置为"Player"
    }

    [XLua.LuaCallCSharp]
    public CinemachineFreeLook CreateFreeLookCamera(string name)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform);
        return go.AddComponent<CinemachineFreeLook>();
    }

    [XLua.LuaCallCSharp]
    public void SetupFreeLookCamera(
    CinemachineFreeLook freeLook,
    Transform follow, Transform lookAt,
    float topHeight = 2.0f, float middleHeight = 1.0f, float bottomHeight = 0.0f,
    float orbitRadius = 5.0f,
    float xSpeed = 150f, float ySpeed = 2f,
    float yMin = -30f, float yMax = 60f,
    bool invertY = false)
    {
        freeLook.Follow = follow;
        freeLook.LookAt = lookAt;

        // 设置三层环绕轨道高度
        freeLook.m_Orbits[0].m_Height = topHeight;
        freeLook.m_Orbits[1].m_Height = middleHeight;
        freeLook.m_Orbits[2].m_Height = bottomHeight;
        freeLook.m_Orbits[0].m_Radius = orbitRadius;
        freeLook.m_Orbits[1].m_Radius = orbitRadius;
        freeLook.m_Orbits[2].m_Radius = orbitRadius;

        // 输入速度
        freeLook.m_XAxis.m_MaxSpeed = xSpeed;
        freeLook.m_YAxis.m_MaxSpeed = ySpeed;
        freeLook.m_YAxis.m_MinValue = yMin;
        freeLook.m_YAxis.m_MaxValue = yMax;
        freeLook.m_YAxis.m_InvertInput = invertY;
    }
    /// <summary>
    /// 动态修改第三人称跟随相机的距离（用于锁定视角下的滚轮缩放）
    /// </summary>
    /// <param name="vcam">目标虚拟相机</param>
    /// <param name="distance">新的相机距离</param>
    public void SetThirdPersonDistance(CinemachineVirtualCamera vcam, float distance)
    {
        if (vcam == null) return;
        var thirdPerson = vcam.gameObject.GetComponent<Cinemachine3rdPersonFollow>();
        if (thirdPerson != null)
        {
            thirdPerson.CameraDistance = distance;
        }
    }

    /// <summary>
    /// 计算有符号角度（用于解除锁定时同步自由相机视角）
    /// </summary>
    /// <param name="from">起始方向</param>
    /// <param name="to">目标方向</param>
    /// <param name="axis">旋转轴</param>
    /// <returns>有符号角度（度）</returns>
    public static float GetSignedAngle(Vector3 from, Vector3 to, Vector3 axis)
    {
        return Vector3.SignedAngle(from, to, axis);
    }
    // 获取/设置 FreeLook 的轴值
    public void SetFreeLookAxisX(CinemachineFreeLook freeLook, float value)
    {
        freeLook.m_XAxis.Value = value;
    }
    public void SetFreeLookAxisY(CinemachineFreeLook freeLook, float value)
    {
        freeLook.m_YAxis.Value = value;
    }
    public float GetFreeLookAxisX(CinemachineFreeLook freeLook) => freeLook.m_XAxis.Value;
    public float GetFreeLookAxisY(CinemachineFreeLook freeLook) => freeLook.m_YAxis.Value;

    // 设置轨道半径（用于滚轮缩放）
    public void SetFreeLookRadius(CinemachineFreeLook freeLook, float radius)
    {
        freeLook.m_Orbits[0].m_Radius = radius;
        freeLook.m_Orbits[1].m_Radius = radius;
        freeLook.m_Orbits[2].m_Radius = radius;
    }
}