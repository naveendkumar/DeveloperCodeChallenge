using System;
using System.Collections.Generic;
using System.Text;

namespace AGL_Logger
{
    interface ILogger
    {
        void WriteErrorConsole();
        void WriteErrorLogFile();
        void WriteErrorLogDatabase();
        void SendErrorEmail();
    }
}
