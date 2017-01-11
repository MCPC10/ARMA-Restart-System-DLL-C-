using RGiesecke.DllExport;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System;

namespace ARMARestartSystem
{
    public class DllEntry
    {
        private static Stopwatch watch = null;

        [DllExport("_RVExtension@12", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
        public static void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function)
        {
           outputSize--;

           switch (function)
            {
                case "init":
                    watch = Stopwatch.StartNew();
                    break;

                case "seconds":
                    if(watch == null) { goto GiveNull; }
                    output.Append((long) watch.Elapsed.TotalSeconds);

                    break;

                case "minutes":
                    if (watch == null) { goto GiveNull; }
                    output.Append((long) watch.Elapsed.TotalMinutes);

                    break;

                case "hours":
                    if (watch == null) { goto GiveNull; }
                    output.Append((long) watch.Elapsed.TotalHours);

                    break;

                case "realtime":  
                    output.Append(DateTime.Now.ToString("HH:mm"));

                    break;

                case "realtimeUtc":
                    output.Append(DateTime.UtcNow.ToString("HH:mm"));

                    break;

                case "version":
                    output.Append("v1.1.0");

                    break;

                default:
                    output.Append("Fehler");

                    break;

                GiveNull:
                    output.Append(0);

                    break;
            }
        }

    }
}
