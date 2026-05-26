
require("InitClass")
require("PlayerDate")

EventCenter:RegisterLua("LuaEnv_Ready", function()
    print("Lua 收到事件，开始后续逻辑")
    -- ...
end)