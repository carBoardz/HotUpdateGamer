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
    public class PlayerMovementStateMachineWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(PlayerMovementStateMachine);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 1, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetInitialState", _m_SetInitialState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LuaRisterState", _m_LuaRisterState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SwitchState", _m_SwitchState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnUpdate", _m_OnUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnFixedUpdate", _m_OnFixedUpdate);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "_stateDict", _g_get__stateDict);
            
			
			
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
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<PlayerController>(L, 2))
				{
					PlayerController _controller = (PlayerController)translator.GetObject(L, 2, typeof(PlayerController));
					
					var gen_ret = new PlayerMovementStateMachine(_controller);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to PlayerMovementStateMachine constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetInitialState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PlayerMovementStateMachine gen_to_be_invoked = (PlayerMovementStateMachine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _stateName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetInitialState( _stateName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PlayerMovementStateMachine gen_to_be_invoked = (PlayerMovementStateMachine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _stateName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.Initialize( _stateName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LuaRisterState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PlayerMovementStateMachine gen_to_be_invoked = (PlayerMovementStateMachine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _StateName = LuaAPI.lua_tostring(L, 2);
                    XLua.LuaTable _luaState = (XLua.LuaTable)translator.GetObject(L, 3, typeof(XLua.LuaTable));
                    
                    gen_to_be_invoked.LuaRisterState( _StateName, _luaState );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SwitchState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PlayerMovementStateMachine gen_to_be_invoked = (PlayerMovementStateMachine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _stateName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SwitchState( _stateName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PlayerMovementStateMachine gen_to_be_invoked = (PlayerMovementStateMachine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnFixedUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                PlayerMovementStateMachine gen_to_be_invoked = (PlayerMovementStateMachine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnFixedUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get__stateDict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerMovementStateMachine gen_to_be_invoked = (PlayerMovementStateMachine)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked._stateDict);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
