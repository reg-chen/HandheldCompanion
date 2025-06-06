﻿using hidapi;
using System;

namespace controller_hidapi.net
{
    public class GenericController
    {
        // device data
        protected ushort _vid, _pid;
        protected short _index;
        // subclass is responsible for opening the device
        protected HidDevice _hidDevice;

        public bool Reading => _hidDevice.Reading;
        public bool IsDeviceValid => _hidDevice.IsDeviceValid;

        public event OnControllerInputReceivedEventHandler OnControllerInputReceived;
        public delegate void OnControllerInputReceivedEventHandler(byte[] Data);

        public GenericController(ushort vid, ushort pid)
        {
            _vid = vid;
            _pid = pid;
        }

        internal virtual void OnInputReceived(HidDeviceInputReceivedEventArgs e)
        {
            OnControllerInputReceived?.Invoke(e.Buffer);
        }

        public virtual void Open()
        {
            if (!_hidDevice.OpenDevice())
                throw new Exception("Could not open device!");
            _hidDevice.BeginRead();
        }

        public virtual void Close()
        {
            EndRead();
            _hidDevice.Close();
        }

        public virtual void EndRead()
        {
            if (_hidDevice.IsDeviceValid)
                _hidDevice.EndRead();
        }
    }
}
