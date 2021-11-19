﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace DeliverySystem.API.Helpers.Logging
{
    public class LoggerProvider : ILoggerProvider
    {
        public IHostingEnvironment _hostingEnvironment;
        public LoggerProvider(IHostingEnvironment hostingEnvironment) => _hostingEnvironment = hostingEnvironment;
        public ILogger CreateLogger(string categoryName) => new Logger(_hostingEnvironment);
        public void Dispose() => throw new NotImplementedException();

    }
}
