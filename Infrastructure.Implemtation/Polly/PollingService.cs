﻿using Infrastructure.Interfaces.Logger;
using Infrastructure.Interfaces.Poll;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Implemtation.Polly
{
    public class PollingService : IPollService
    {
        private int _attemptCount;
        private ILoggerService _logger;
        public PollingService(
            int attempCount,
            ILoggerService loggerService)
        {
            _attemptCount = attempCount;
            _logger = loggerService;
        }


        public async Task<T> Execute<T>(Func<Task<PollResult<T>>> action,
            Func<Task> retryAction)
        {
            var result = await action();

            if (result.IsSuccess)
                return result.Data;

            for (int i = 0; i < _attemptCount; i++)
            {
                _logger.Info($"Попытка {i}");
                var actResult = await action();

                if (actResult.IsSuccess)
                    return result.Data;

                if (i != _attemptCount)
                    retryAction();

                if (i == _attemptCount)
                    _logger.Error("Error in polling");
            }

            return default;
        }

        public async Task<T> Execute<T,V>(Func<V,Task<PollResult<T>>> action, V arg,
            Func<Task> retryAction)
        {
            var result = await action(arg);

            if (result.IsSuccess)
                return result.Data;

            for (int i = 0; i < _attemptCount; i++)
            {
                _logger.Info($"Попытка {i}");
                var actResult = await action(arg);

                if (actResult.IsSuccess)
                    return result.Data;

                if (i != _attemptCount)
                    retryAction();

                if (i == _attemptCount)
                    _logger.Error("Error in polling");
            }

            return default;
        }
    }
}