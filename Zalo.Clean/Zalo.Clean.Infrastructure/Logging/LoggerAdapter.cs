﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Logging;

namespace Zalo.Clean.Infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> logger;
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<T>();
        }

        public void info(string message, params object[] args)
        {
            logger.LogInformation(message, args);
        }

        public void warn(string message, params object[] args)
        {
            logger.LogWarning(message, args);
        }
    }
}
