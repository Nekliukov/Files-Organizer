using System;
using System.Collections.Generic;
using System.Text;

namespace FilesDistributor.Logger
{
    interface ILogger
    {
        void Success(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
    }
}
