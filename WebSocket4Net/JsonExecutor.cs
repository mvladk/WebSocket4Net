﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSocket4Net
{
    abstract class JsonExecutorBase<T> : IJsonExecutor
    {
        public Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public abstract void Execute(JsonWebSocket websocket, string token, object param);
    }

    class JsonExecutor<T> : JsonExecutorBase<T>
    {
        private Action<T> m_ExecutorAction;

        public JsonExecutor(Action<T> action)
        {
            m_ExecutorAction = action;
        }

        public override void Execute(JsonWebSocket websocket, string token, object param)
        {
            m_ExecutorAction.Method.Invoke(m_ExecutorAction.Target, new object[] { param });
        }
    }

    class JsonExecutorWithToken<T> : JsonExecutorBase<T>
    {
        private Action<string, T> m_ExecutorActionWithToken;

        public JsonExecutorWithToken(Action<string, T> action)
        {
            m_ExecutorActionWithToken = action;
        }

        public override void Execute(JsonWebSocket websocket, string token, object param)
        {
            m_ExecutorActionWithToken.Method.Invoke(m_ExecutorActionWithToken.Target, new object[] { token, param });
        }
    }

    class JsonExecutorWithSender<T> : JsonExecutorBase<T>
    {
        private Action<JsonWebSocket, T> m_ExecutorActionWithSender;

        public JsonExecutorWithSender(Action<JsonWebSocket, T> action)
        {
            m_ExecutorActionWithSender = action;
        }

        public override void Execute(JsonWebSocket websocket, string token, object param)
        {
            m_ExecutorActionWithSender.Method.Invoke(m_ExecutorActionWithSender.Target, new object[] { websocket, param });
        }
    }

    class JsonExecutorFull<T> : JsonExecutorBase<T>
    {
        private Action<JsonWebSocket, string, T> m_ExecutorActionFull;

        public JsonExecutorFull(Action<JsonWebSocket, string, T> action)
        {
            m_ExecutorActionFull = action;
        }

        public override void Execute(JsonWebSocket websocket, string token, object param)
        {
            m_ExecutorActionFull.Method.Invoke(m_ExecutorActionFull.Target, new object[] { websocket, token, param });
        }
    }
}
