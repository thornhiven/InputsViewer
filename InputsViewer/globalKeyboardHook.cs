﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace InputsViewer
{
    class globalKeyboardHook
    {
        public struct keyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;
        public List<Keys> HookedKeys = new List<Keys>();
        IntPtr hhook = IntPtr.Zero;
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        public globalKeyboardHook()
        {
            hook();
        }

        ~globalKeyboardHook()
        {
            unhook();
        }

        public void hook()
        {

            IntPtr hInstance = LoadLibrary("User32");
            //hhook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, hInstance, 0);
            delegateHookProc = hookProc;
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, delegateHookProc, hInstance, 0);
        }
        public delegate int keyboardHookProc(int code, int wParam, ref keyboardHookStruct lParam);
        keyboardHookProc delegateHookProc;

        public void unhook()
        {
            UnhookWindowsHookEx(hhook);
        }
        public int hookProc(int code, int wParam, ref keyboardHookStruct lParam)
        {
            if (code >= 0)
            {
                Keys key = (Keys)lParam.vkCode;
                if (HookedKeys.Contains(key))
                {
                    KeyEventArgs kea = new KeyEventArgs(key);
                    if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
                    {
                        KeyDown(this, kea);
                    }
                    else
                        if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
                    {
                        KeyUp(this, kea);
                    }
                    if (kea.Handled)
                        return 1;
                }
            }
            return CallNextHookEx(hhook, code, wParam, ref lParam);
        }

        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref keyboardHookStruct lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);
    }
}
