namespace ParentalSight.Keylogger.Mappers
{
    using System.Windows.Forms;

    internal class KeyboardMapper
    {
        protected KeyboardMapper()
        {
        }

        public string ParseKey(int vKey, bool shift, bool caps)
        {
            if (vKey > 64 && vKey < 91)
            {
                var key = ((Keys)vKey).ToString();
                return shift | caps ? key : key.ToLower();
            }

            if (vKey >= 96 && vKey <= 111)
            {
                switch (vKey)
                {
                    case 96:
                        return "0";

                    case 97:
                        return "1";

                    case 98:
                        return "2";

                    case 99:
                        return "3";

                    case 100:
                        return "4";

                    case 101:
                        return "5";

                    case 102:
                        return "6";

                    case 103:
                        return "7";

                    case 104:
                        return "8";

                    case 105:
                        return "9";

                    case 106:
                        return "*";

                    case 107:
                        return "+";

                    case 108:
                        return "|";

                    case 109:
                        return "-";

                    case 110:
                        return ".";

                    case 111:
                        return "/";
                }
            }
            else if (vKey >= 48 && vKey <= 57 || vKey >= 186 && vKey <= 192)
            {
                if (shift)
                {
                    switch (vKey)
                    {
                        case 48:
                            return ")";

                        case 49:
                            return "!";

                        case 50:
                            return "@";

                        case 51:
                            return "#";

                        case 52:
                            return "$";

                        case 53:
                            return "%";

                        case 54:
                            return "^";

                        case 55:
                            return "&";

                        case 56:
                            return "*";

                        case 57:
                            return "(";

                        case 186:
                            return ":";

                        case 187:
                            return "+";

                        case 188:
                            return "<";

                        case 189:
                            return "_";

                        case 190:
                            return ">";

                        case 191:
                            return "?";

                        case 192:
                            return "~";

                        case 219:
                            return "{";

                        case 220:
                            return "|";

                        case 221:
                            return "}";

                        case 222:
                            return "<Double Quotes>";
                    }
                }
                else
                {
                    switch (vKey)
                    {
                        case 48:
                            return "0";

                        case 49:
                            return "1";

                        case 50:
                            return "2";

                        case 51:
                            return "3";

                        case 52:
                            return "4";

                        case 53:
                            return "5";

                        case 54:
                            return "6";

                        case 55:
                            return "7";

                        case 56:
                            return "8";

                        case 57:
                            return "9";

                        case 186:
                            return ";";

                        case 187:
                            return "=";

                        case 188:
                            return ",";

                        case 189:
                            return "-";

                        case 190:
                            return ".";

                        case 191:
                            return "/";

                        case 192:
                            return "`";

                        case 219:
                            return "[";

                        case 220:
                            return "\\";

                        case 221:
                            return "]";

                        case 222:
                            return "<Single Quote>";
                    }
                }
            }
            else
            {
                switch ((Keys)vKey)
                {
                    case Keys.F1:
                        return "<F1>";

                    case Keys.F2:
                        return "<F2>";

                    case Keys.F3:
                        return "<F3>";

                    case Keys.F4:
                        return "<F4>";

                    case Keys.F5:
                        return "<F5>";

                    case Keys.F6:
                        return "<F6>";

                    case Keys.F7:
                        return "<F7>";

                    case Keys.F8:
                        return "<F8>";

                    case Keys.F9:
                        return "<F9>";

                    case Keys.F10:
                        return "<F10>";

                    case Keys.F11:
                        return "<F11>";

                    case Keys.F12:
                        return "<F12>";

                    case Keys.Tab:
                        return "\t";

                    case Keys.Enter:
                        return "\n";

                    case Keys.Space:
                        return " ";

                        #region Keys removed 1/14/2023
                        /*
                        case Keys.Back:
                            return "<Backspace>";

                        case Keys.Left:
                            return "<Left>";

                        case Keys.Up:
                            return "<Up>";

                        case Keys.Right:
                            return "<Right>";

                        case Keys.Down:
                            return "<Down>";

                        case Keys.LMenu:
                            return "<Alt>";

                        case Keys.RMenu:
                            return "<Alt>";

                        case Keys.LWin:
                            return "<WIN>";

                        case Keys.RWin:
                            return "<WIN>";

                        //case Keys.LShiftKey:
                        //    return "<Shift>";
                        //
                        //case Keys.RShiftKey:
                        //    return "<Shift>";
                        
                        case Keys.LControlKey:
                            return "<Ctrl>";

                        case Keys.RControlKey:
                            return "<Ctrl>";

                        //case Keys.Snapshot:
                        //    return "<Print Screen>";
                        //
                        //case Keys.Scroll:
                        //    return "<Scroll Lock>";
                        //
                        //case Keys.Pause:
                        //    return "<Pause/Break>";

                        case Keys.Insert:
                            return "<Insert>";

                        //case Keys.Home:
                        //    return "<Home>";

                        case Keys.Delete:
                            return "<Delete>";

                         //case Keys.End:
                         //    return "<End>";
                         //
                         //case Keys.Prior:
                         //    return "<Page Up>";
                         //
                         //case Keys.Next:
                         //    return "<Page Down>";
                         //
                         //case Keys.Escape:
                         //    return "<Esc>";
                         //
                         //case Keys.NumLock:
                         //    return "<Num Lock>";
                         //
                         //case Keys.Capital:
                         //    return "<Caps Lock>";
                         //
                         */

                        #endregion Keys removed 1/14/2023
                }
            }

            return string.Empty;
        }

        private static KeyboardMapper _instance;
        public static KeyboardMapper Instance =>
            _instance = _instance ?? new KeyboardMapper();
    }
}