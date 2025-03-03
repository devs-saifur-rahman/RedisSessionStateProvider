﻿using System;
using System.Web.SessionState;

namespace RedisSessionProvider
{
    internal interface ICacheConnection
    {
        KeyGenerator Keys { get; set; }
        void Set(ISessionStateItemCollection data, int sessionTimeout);
        void UpdateExpiryTime(int timeToExpireInSeconds);
        bool TryTakeWriteLockAndGetData(DateTime lockTime, int lockTimeout, out object lockId, out ISessionStateItemCollection data, out int sessionTimeout);
        bool TryCheckWriteLockAndGetData(out object lockId, out ISessionStateItemCollection data, out int sessionTimeout);
        void TryReleaseLockIfLockIdMatch(object lockId, int sessionTimeout);
        void TryRemoveAndReleaseLockIfLockIdMatch(object lockId);
        void TryUpdateAndReleaseLockIfLockIdMatch(object lockId, ISessionStateItemCollection data, int sessionTimeout);
        TimeSpan GetLockAge(object lockId);
    }
}
