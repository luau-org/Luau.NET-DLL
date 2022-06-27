using System.Text;
using System.Runtime.InteropServices;

namespace Luau.Net
{
    public class LuauRuntime
    {
        [DllImport(@".\luau.net.dll", CharSet = CharSet.Ansi)]
        static extern void InternalCompile(string code, StringBuilder result);
        
        [DllImport(@".\luau.net.dll", CharSet = CharSet.Ansi)]
        static extern bool InternalRun(ulong stateId, string bytecode, StringBuilder result);
        
        [DllImport(@".\luau.net.dll", CharSet = CharSet.Ansi)]
        static extern ulong InternalNewState();

        public static string Compile(string code)
        {
            StringBuilder sb = new StringBuilder();
            InternalCompile(code, sb);
            return sb.ToString();
        }

        public class LuaState
        {
            private ulong stateId;

            public LuaState()
            {
                stateId = InternalNewState();
            }

            public bool RunBytecode(string bytecode, string output)
            {
                StringBuilder sb = new StringBuilder();
                bool success = InternalRun(stateId, bytecode, sb);
                output = sb.ToString();
                return success;
            }
            
            public string RunBytecode(string bytecode)
            {
                StringBuilder sb = new StringBuilder();
                InternalRun(stateId, bytecode, sb);
                return sb.ToString();
            }
            public ulong GetStateId()
            {
                return stateId;
            }

            public string Run(string code)
            {
                return RunBytecode(Compile(code));
            }
            
            public bool Run(string code, string output)
            {
                return RunBytecode(Compile(code), output);
            }
        }
    }
}