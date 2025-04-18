﻿using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace HandheldCompanion.ADLX
{
    public static class ADLXBackend
    {
        public const string ADLX_Wrapper = @"ADLX_Wrapper.dll";
        public const string ADLX_Probe = @"ADLX_Probe.exe";

        public enum ADLX_RESULT
        {
            ADLX_OK = 0,                    /**< @ENG_START_DOX This result indicates success. @ENG_END_DOX */
            ADLX_ALREADY_ENABLED,           /**< @ENG_START_DOX This result indicates that the asked action is already enabled. @ENG_END_DOX */
            ADLX_ALREADY_INITIALIZED,       /**< @ENG_START_DOX This result indicates that ADLX has a unspecified type of initialization. @ENG_END_DOX */
            ADLX_FAIL,                      /**< @ENG_START_DOX This result indicates an unspecified failure. @ENG_END_DOX */
            ADLX_INVALID_ARGS,              /**< @ENG_START_DOX This result indicates that the arguments are invalid. @ENG_END_DOX */
            ADLX_BAD_VER,                   /**< @ENG_START_DOX This result indicates that the asked version is incompatible with the current version. @ENG_END_DOX */
            ADLX_UNKNOWN_INTERFACE,         /**< @ENG_START_DOX This result indicates that an unknown interface was asked. @ENG_END_DOX */
            ADLX_TERMINATED,                /**< @ENG_START_DOX This result indicates that the calls were made in an interface after ADLX was terminated. @ENG_END_DOX */
            ADLX_ADL_INIT_ERROR,            /**< @ENG_START_DOX This result indicates that the ADL initialization failed. @ENG_END_DOX */
            ADLX_NOT_FOUND,                 /**< @ENG_START_DOX This result indicates that the item is not found. @ENG_END_DOX */
            ADLX_INVALID_OBJECT,            /**< @ENG_START_DOX This result indicates that the method was called into an invalid object. @ENG_END_DOX */
            ADLX_ORPHAN_OBJECTS,            /**< @ENG_START_DOX This result indicates that ADLX was terminated with outstanding ADLX objects. Any interface obtained from ADLX points to invalid memory and calls in their methods will result in unexpected behavior. @ENG_END_DOX */
            ADLX_NOT_SUPPORTED,             /**< @ENG_START_DOX This result indicates that the asked feature is not supported. @ENG_END_DOX */
            ADLX_PENDING_OPERATION,         /**< @ENG_START_DOX This result indicates a failure due to an operation currently in progress. @ENG_END_DOX */
            ADLX_GPU_INACTIVE               /**< @ENG_START_DOX This result indicates that the GPU is inactive. @ENG_END_DOX */
        }

        [HandleProcessCorruptedStateExceptions]
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)] public static extern bool IntializeAdlx(StringBuilder dispName, int nameLength);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool InitializeAdlxWithIncompatibleDriver();
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool CloseAdlx();

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern ADLX_RESULT GetNumberOfDisplays(ref int displayNum);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)] public static extern ADLX_RESULT GetDisplayName(int idx, StringBuilder dispName, int nameLength);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern ADLX_RESULT GetDisplayGPU(int idx, ref int UniqueId);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern ADLX_RESULT GetGPUIndex(int UniqueId, ref int idx);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool HasRSRSupport();
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool GetRSR();
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetRSR(bool enable);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern int GetRSRSharpness();
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetRSRSharpness(int sharpness);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool HasAFMFSupport();
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool GetAFMF();
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetAFMF(bool enable);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool GetAntiLag(int GPU);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetAntiLag(int GPU, bool enable);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool GetBoost(int GPU);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetBoost(int GPU, bool enable);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern int GetBoostResolution(int GPU);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetBoostResolution(int GPU, int minRes);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool GetChill(int GPU);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetChill(int GPU, bool enable);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern int GetChillMinFPS(int GPU);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetChillMinFPS(int GPU, int minFPS);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern int GetChillMaxFPS(int GPU);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetChillMaxFPS(int GPU, int maxFPS);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool GetImageSharpening(int GPU);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetImageSharpening(int GPU, bool enable);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern int GetImageSharpeningSharpness(int GPU);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetImageSharpeningSharpness(int GPU, int sharpness);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool HasIntegerScalingSupport(int displayIdx);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool GetIntegerScaling(int displayIdx);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetIntegerScaling(int displayIdx, bool enabled);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool HasGPUScalingSupport(int displayIdx);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool GetGPUScaling(int displayIdx);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetGPUScaling(int displayIdx, bool enabled);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool HasScalingModeSupport(int displayIdx);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern int GetScalingMode(int displayIdx);
        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool SetScalingMode(int displayIdx, int mode);

        [DllImport(ADLX_Wrapper, CallingConvention = CallingConvention.Cdecl)] public static extern bool GetAdlxTelemetry(int GPU, ref AdlxTelemetryData adlxTelemetryData);

        private static StringBuilder displayName = new StringBuilder(256);

        static ADLXBackend() { }

        [SecurityCritical]
        public static bool SafeIntializeAdlx()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = ADLX_Probe;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = Path.Combine(System.Environment.CurrentDirectory, "Resources"); // Explicit working directory
                process.Start();
                process.WaitForExit();

                int exitCode = process.ExitCode;
                switch (process.ExitCode)
                {
                    case 2:
                        // Assume display name won't exceed 255 characters
                        bool success = IntializeAdlx(displayName, displayName.Capacity);
                        return success;
                    default:
                        return InitializeAdlxWithIncompatibleDriver();
                }
            }
            catch { }

            return false;
        }

        public static string GetVersion()
        {
            return displayName.ToString();
        }
    }
}
