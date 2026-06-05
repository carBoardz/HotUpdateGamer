#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class CameraManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CameraManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 9, 2, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateVirtualCamera", _m_CreateVirtualCamera);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetupThirdPersonFollow", _m_SetupThirdPersonFollow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateFreeLookCamera", _m_CreateFreeLookCamera);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetupFreeLookCamera", _m_SetupFreeLookCamera);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFreeLookAxisX", _m_SetFreeLookAxisX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFreeLookAxisY", _m_SetFreeLookAxisY);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetFreeLookAxisX", _m_GetFreeLookAxisX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetFreeLookAxisY", _m_GetFreeLookAxisY);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFreeLookRadius", _m_SetFreeLookRadius);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "MainCamera", _g_get_MainCamera);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Brain", _g_get_Brain);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new CameraManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CameraManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateVirtualCamera(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.CreateVirtualCamera( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetupThirdPersonFollow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 14&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 11)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 12)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 13)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 14)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _shoulderZ = (float)LuaAPI.lua_tonumber(L, 6);
                    float _dampingX = (float)LuaAPI.lua_tonumber(L, 7);
                    float _dampingY = (float)LuaAPI.lua_tonumber(L, 8);
                    float _dampingZ = (float)LuaAPI.lua_tonumber(L, 9);
                    float _verticalArmLength = (float)LuaAPI.lua_tonumber(L, 10);
                    int _cameraSide = LuaAPI.xlua_tointeger(L, 11);
                    float _cameraRadius = (float)LuaAPI.lua_tonumber(L, 12);
                    float _dampingIntoCollision = (float)LuaAPI.lua_tonumber(L, 13);
                    float _dampingFromCollision = (float)LuaAPI.lua_tonumber(L, 14);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY, _shoulderZ, _dampingX, _dampingY, _dampingZ, _verticalArmLength, _cameraSide, _cameraRadius, _dampingIntoCollision, _dampingFromCollision );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 13&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 11)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 12)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 13)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _shoulderZ = (float)LuaAPI.lua_tonumber(L, 6);
                    float _dampingX = (float)LuaAPI.lua_tonumber(L, 7);
                    float _dampingY = (float)LuaAPI.lua_tonumber(L, 8);
                    float _dampingZ = (float)LuaAPI.lua_tonumber(L, 9);
                    float _verticalArmLength = (float)LuaAPI.lua_tonumber(L, 10);
                    int _cameraSide = LuaAPI.xlua_tointeger(L, 11);
                    float _cameraRadius = (float)LuaAPI.lua_tonumber(L, 12);
                    float _dampingIntoCollision = (float)LuaAPI.lua_tonumber(L, 13);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY, _shoulderZ, _dampingX, _dampingY, _dampingZ, _verticalArmLength, _cameraSide, _cameraRadius, _dampingIntoCollision );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 12&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 11)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 12)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _shoulderZ = (float)LuaAPI.lua_tonumber(L, 6);
                    float _dampingX = (float)LuaAPI.lua_tonumber(L, 7);
                    float _dampingY = (float)LuaAPI.lua_tonumber(L, 8);
                    float _dampingZ = (float)LuaAPI.lua_tonumber(L, 9);
                    float _verticalArmLength = (float)LuaAPI.lua_tonumber(L, 10);
                    int _cameraSide = LuaAPI.xlua_tointeger(L, 11);
                    float _cameraRadius = (float)LuaAPI.lua_tonumber(L, 12);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY, _shoulderZ, _dampingX, _dampingY, _dampingZ, _verticalArmLength, _cameraSide, _cameraRadius );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 11&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 11)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _shoulderZ = (float)LuaAPI.lua_tonumber(L, 6);
                    float _dampingX = (float)LuaAPI.lua_tonumber(L, 7);
                    float _dampingY = (float)LuaAPI.lua_tonumber(L, 8);
                    float _dampingZ = (float)LuaAPI.lua_tonumber(L, 9);
                    float _verticalArmLength = (float)LuaAPI.lua_tonumber(L, 10);
                    int _cameraSide = LuaAPI.xlua_tointeger(L, 11);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY, _shoulderZ, _dampingX, _dampingY, _dampingZ, _verticalArmLength, _cameraSide );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 10&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _shoulderZ = (float)LuaAPI.lua_tonumber(L, 6);
                    float _dampingX = (float)LuaAPI.lua_tonumber(L, 7);
                    float _dampingY = (float)LuaAPI.lua_tonumber(L, 8);
                    float _dampingZ = (float)LuaAPI.lua_tonumber(L, 9);
                    float _verticalArmLength = (float)LuaAPI.lua_tonumber(L, 10);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY, _shoulderZ, _dampingX, _dampingY, _dampingZ, _verticalArmLength );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 9&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _shoulderZ = (float)LuaAPI.lua_tonumber(L, 6);
                    float _dampingX = (float)LuaAPI.lua_tonumber(L, 7);
                    float _dampingY = (float)LuaAPI.lua_tonumber(L, 8);
                    float _dampingZ = (float)LuaAPI.lua_tonumber(L, 9);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY, _shoulderZ, _dampingX, _dampingY, _dampingZ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 8&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _shoulderZ = (float)LuaAPI.lua_tonumber(L, 6);
                    float _dampingX = (float)LuaAPI.lua_tonumber(L, 7);
                    float _dampingY = (float)LuaAPI.lua_tonumber(L, 8);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY, _shoulderZ, _dampingX, _dampingY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 7&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _shoulderZ = (float)LuaAPI.lua_tonumber(L, 6);
                    float _dampingX = (float)LuaAPI.lua_tonumber(L, 7);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY, _shoulderZ, _dampingX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _shoulderZ = (float)LuaAPI.lua_tonumber(L, 6);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY, _shoulderZ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _shoulderY = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX, _shoulderY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _shoulderX = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance, _shoulderX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam, _distance );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<Cinemachine.CinemachineVirtualCamera>(L, 2)) 
                {
                    Cinemachine.CinemachineVirtualCamera _vcam = (Cinemachine.CinemachineVirtualCamera)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineVirtualCamera));
                    
                    gen_to_be_invoked.SetupThirdPersonFollow( _vcam );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CameraManager.SetupThirdPersonFollow!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateFreeLookCamera(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.CreateFreeLookCamera( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetupFreeLookCamera(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 13&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 11)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 12)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 13)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    float _topHeight = (float)LuaAPI.lua_tonumber(L, 5);
                    float _middleHeight = (float)LuaAPI.lua_tonumber(L, 6);
                    float _bottomHeight = (float)LuaAPI.lua_tonumber(L, 7);
                    float _orbitRadius = (float)LuaAPI.lua_tonumber(L, 8);
                    float _xSpeed = (float)LuaAPI.lua_tonumber(L, 9);
                    float _ySpeed = (float)LuaAPI.lua_tonumber(L, 10);
                    float _yMin = (float)LuaAPI.lua_tonumber(L, 11);
                    float _yMax = (float)LuaAPI.lua_tonumber(L, 12);
                    bool _invertY = LuaAPI.lua_toboolean(L, 13);
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt, _topHeight, _middleHeight, _bottomHeight, _orbitRadius, _xSpeed, _ySpeed, _yMin, _yMax, _invertY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 12&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 11)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 12)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    float _topHeight = (float)LuaAPI.lua_tonumber(L, 5);
                    float _middleHeight = (float)LuaAPI.lua_tonumber(L, 6);
                    float _bottomHeight = (float)LuaAPI.lua_tonumber(L, 7);
                    float _orbitRadius = (float)LuaAPI.lua_tonumber(L, 8);
                    float _xSpeed = (float)LuaAPI.lua_tonumber(L, 9);
                    float _ySpeed = (float)LuaAPI.lua_tonumber(L, 10);
                    float _yMin = (float)LuaAPI.lua_tonumber(L, 11);
                    float _yMax = (float)LuaAPI.lua_tonumber(L, 12);
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt, _topHeight, _middleHeight, _bottomHeight, _orbitRadius, _xSpeed, _ySpeed, _yMin, _yMax );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 11&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 11)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    float _topHeight = (float)LuaAPI.lua_tonumber(L, 5);
                    float _middleHeight = (float)LuaAPI.lua_tonumber(L, 6);
                    float _bottomHeight = (float)LuaAPI.lua_tonumber(L, 7);
                    float _orbitRadius = (float)LuaAPI.lua_tonumber(L, 8);
                    float _xSpeed = (float)LuaAPI.lua_tonumber(L, 9);
                    float _ySpeed = (float)LuaAPI.lua_tonumber(L, 10);
                    float _yMin = (float)LuaAPI.lua_tonumber(L, 11);
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt, _topHeight, _middleHeight, _bottomHeight, _orbitRadius, _xSpeed, _ySpeed, _yMin );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 10&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    float _topHeight = (float)LuaAPI.lua_tonumber(L, 5);
                    float _middleHeight = (float)LuaAPI.lua_tonumber(L, 6);
                    float _bottomHeight = (float)LuaAPI.lua_tonumber(L, 7);
                    float _orbitRadius = (float)LuaAPI.lua_tonumber(L, 8);
                    float _xSpeed = (float)LuaAPI.lua_tonumber(L, 9);
                    float _ySpeed = (float)LuaAPI.lua_tonumber(L, 10);
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt, _topHeight, _middleHeight, _bottomHeight, _orbitRadius, _xSpeed, _ySpeed );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 9&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    float _topHeight = (float)LuaAPI.lua_tonumber(L, 5);
                    float _middleHeight = (float)LuaAPI.lua_tonumber(L, 6);
                    float _bottomHeight = (float)LuaAPI.lua_tonumber(L, 7);
                    float _orbitRadius = (float)LuaAPI.lua_tonumber(L, 8);
                    float _xSpeed = (float)LuaAPI.lua_tonumber(L, 9);
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt, _topHeight, _middleHeight, _bottomHeight, _orbitRadius, _xSpeed );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 8&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    float _topHeight = (float)LuaAPI.lua_tonumber(L, 5);
                    float _middleHeight = (float)LuaAPI.lua_tonumber(L, 6);
                    float _bottomHeight = (float)LuaAPI.lua_tonumber(L, 7);
                    float _orbitRadius = (float)LuaAPI.lua_tonumber(L, 8);
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt, _topHeight, _middleHeight, _bottomHeight, _orbitRadius );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 7&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    float _topHeight = (float)LuaAPI.lua_tonumber(L, 5);
                    float _middleHeight = (float)LuaAPI.lua_tonumber(L, 6);
                    float _bottomHeight = (float)LuaAPI.lua_tonumber(L, 7);
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt, _topHeight, _middleHeight, _bottomHeight );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    float _topHeight = (float)LuaAPI.lua_tonumber(L, 5);
                    float _middleHeight = (float)LuaAPI.lua_tonumber(L, 6);
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt, _topHeight, _middleHeight );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    float _topHeight = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt, _topHeight );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<Cinemachine.CinemachineFreeLook>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& translator.Assignable<UnityEngine.Transform>(L, 4)) 
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    UnityEngine.Transform _follow = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _lookAt = (UnityEngine.Transform)translator.GetObject(L, 4, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.SetupFreeLookCamera( _freeLook, _follow, _lookAt );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CameraManager.SetupFreeLookCamera!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFreeLookAxisX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    float _value = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetFreeLookAxisX( _freeLook, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFreeLookAxisY(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    float _value = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetFreeLookAxisY( _freeLook, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFreeLookAxisX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    
                        var gen_ret = gen_to_be_invoked.GetFreeLookAxisX( _freeLook );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFreeLookAxisY(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    
                        var gen_ret = gen_to_be_invoked.GetFreeLookAxisY( _freeLook );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFreeLookRadius(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Cinemachine.CinemachineFreeLook _freeLook = (Cinemachine.CinemachineFreeLook)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineFreeLook));
                    float _radius = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetFreeLookRadius( _freeLook, _radius );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MainCamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MainCamera);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Brain(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CameraManager gen_to_be_invoked = (CameraManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Brain);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
