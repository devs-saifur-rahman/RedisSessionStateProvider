﻿using System;

namespace RedisSessionProvider
{
    internal class RedisSharedConnection
    {

        internal IRedisClientConnection connection;
        object lockObject;
        Func<IRedisClientConnection> factory;
        ProviderConfiguration configuration;

        public RedisSharedConnection(ProviderConfiguration configuration, Func<IRedisClientConnection> factory)
        {
            this.configuration = configuration;
            lockObject = new object();
            this.factory = factory;
        }

        public IRedisClientConnection TryGetConnection()
        {
            if (connection != null)
            {
                //case 1: already available connection
                return connection;
            }
            else
            {
                //case 2: we are allowed to create first connection
                lock (lockObject)
                {
                    // make suer it is not created by other request in between
                    if (connection == null)
                    {
                        connection = factory.Invoke();
                        connection.Open();
                    }
                }
                return connection;
            }
        }
    }

}
