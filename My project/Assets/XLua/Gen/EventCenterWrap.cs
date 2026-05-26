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
    public class EventCenterWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(EventCenter);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Register", _m_Register);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterLua", _m_RegisterLua);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnRegister", _m_UnRegister);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Trigger", _m_Trigger);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clear", _m_Clear);
			
			
			
			
			
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
					
					var gen_ret = new EventCenter();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to EventCenter constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Register(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                EventCenter gen_to_be_invoked = (EventCenter)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 7&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Delegate>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<UnityEngine.Object>(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    System.Delegate _callback = translator.GetDelegate<System.Delegate>(L, 3);
                    int _Priority = LuaAPI.xlua_tointeger(L, 4);
                    UnityEngine.Object _owner = (UnityEngine.Object)translator.GetObject(L, 5, typeof(UnityEngine.Object));
                    bool _MainThreadOnly = LuaAPI.lua_toboolean(L, 6);
                    bool _once = LuaAPI.lua_toboolean(L, 7);
                    
                    gen_to_be_invoked.Register( _eventName, _callback, _Priority, _owner, _MainThreadOnly, _once );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Delegate>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<UnityEngine.Object>(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    System.Delegate _callback = translator.GetDelegate<System.Delegate>(L, 3);
                    int _Priority = LuaAPI.xlua_tointeger(L, 4);
                    UnityEngine.Object _owner = (UnityEngine.Object)translator.GetObject(L, 5, typeof(UnityEngine.Object));
                    bool _MainThreadOnly = LuaAPI.lua_toboolean(L, 6);
                    
                    gen_to_be_invoked.Register( _eventName, _callback, _Priority, _owner, _MainThreadOnly );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Delegate>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<UnityEngine.Object>(L, 5)) 
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    System.Delegate _callback = translator.GetDelegate<System.Delegate>(L, 3);
                    int _Priority = LuaAPI.xlua_tointeger(L, 4);
                    UnityEngine.Object _owner = (UnityEngine.Object)translator.GetObject(L, 5, typeof(UnityEngine.Object));
                    
                    gen_to_be_invoked.Register( _eventName, _callback, _Priority, _owner );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Delegate>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    System.Delegate _callback = translator.GetDelegate<System.Delegate>(L, 3);
                    int _Priority = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.Register( _eventName, _callback, _Priority );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Delegate>(L, 3)) 
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    System.Delegate _callback = translator.GetDelegate<System.Delegate>(L, 3);
                    
                    gen_to_be_invoked.Register( _eventName, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to EventCenter.Register!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterLua(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                EventCenter gen_to_be_invoked = (EventCenter)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TFUNCTION)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    XLua.LuaFunction _LuaCallback = (XLua.LuaFunction)translator.GetObject(L, 3, typeof(XLua.LuaFunction));
                    int _Priority = LuaAPI.xlua_tointeger(L, 4);
                    bool _MainThreadOnly = LuaAPI.lua_toboolean(L, 5);
                    bool _once = LuaAPI.lua_toboolean(L, 6);
                    
                    gen_to_be_invoked.RegisterLua( _eventName, _LuaCallback, _Priority, _MainThreadOnly, _once );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TFUNCTION)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    XLua.LuaFunction _LuaCallback = (XLua.LuaFunction)translator.GetObject(L, 3, typeof(XLua.LuaFunction));
                    int _Priority = LuaAPI.xlua_tointeger(L, 4);
                    bool _MainThreadOnly = LuaAPI.lua_toboolean(L, 5);
                    
                    gen_to_be_invoked.RegisterLua( _eventName, _LuaCallback, _Priority, _MainThreadOnly );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TFUNCTION)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    XLua.LuaFunction _LuaCallback = (XLua.LuaFunction)translator.GetObject(L, 3, typeof(XLua.LuaFunction));
                    int _Priority = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.RegisterLua( _eventName, _LuaCallback, _Priority );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TFUNCTION)) 
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    XLua.LuaFunction _LuaCallback = (XLua.LuaFunction)translator.GetObject(L, 3, typeof(XLua.LuaFunction));
                    
                    gen_to_be_invoked.RegisterLua( _eventName, _LuaCallback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to EventCenter.RegisterLua!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnRegister(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                EventCenter gen_to_be_invoked = (EventCenter)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Object>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<EventCenter.EventCallbackWrapper>(L, 4)) 
                {
                    UnityEngine.Object _owner = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    string _eventName = LuaAPI.lua_tostring(L, 3);
                    EventCenter.EventCallbackWrapper _targetWrapper = (EventCenter.EventCallbackWrapper)translator.GetObject(L, 4, typeof(EventCenter.EventCallbackWrapper));
                    
                    gen_to_be_invoked.UnRegister( _owner, _eventName, _targetWrapper );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Object>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityEngine.Object _owner = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    string _eventName = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.UnRegister( _owner, _eventName );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Object>(L, 2)) 
                {
                    UnityEngine.Object _owner = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                    gen_to_be_invoked.UnRegister( _owner );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.UnRegister(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to EventCenter.UnRegister!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Trigger(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                EventCenter gen_to_be_invoked = (EventCenter)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _eventName = LuaAPI.lua_tostring(L, 2);
                    object[] _args = translator.GetParams<object>(L, 3);
                    
                    gen_to_be_invoked.Trigger( _eventName, _args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                EventCenter gen_to_be_invoked = (EventCenter)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.Clear(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
