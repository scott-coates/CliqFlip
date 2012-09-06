using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using MassTransit.Diagnostics.Introspection;
using MassTransit.Pipeline;

namespace CliqFlip.Infrastructure.ServiceBus
{
    public class FakeServiceBus : IServiceBus
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Inspect(DiagnosticsProbe probe)
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(T message) where T : class
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(T message, Action<IPublishContext<T>> contextCallback) where T : class
        {
            throw new NotImplementedException();
        }

        public void Publish(object message)
        {
            throw new NotImplementedException();
        }

        public void Publish(object message, Type messageType)
        {
            throw new NotImplementedException();
        }

        public void Publish(object message, Action<IPublishContext> contextCallback)
        {
            throw new NotImplementedException();
        }

        public void Publish(object message, Type messageType, Action<IPublishContext> contextCallback)
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(object values) where T : class
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(object values, Action<IPublishContext<T>> contextCallback) where T : class
        {
            throw new NotImplementedException();
        }

        public IEndpoint GetEndpoint(Uri address)
        {
            throw new NotImplementedException();
        }

        public UnsubscribeAction Configure(Func<IInboundPipelineConfigurator, UnsubscribeAction> configure)
        {
            throw new NotImplementedException();
        }

        public IBusService GetService(Type type)
        {
            throw new NotImplementedException();
        }

        public bool TryGetService(Type type, out IBusService result)
        {
            throw new NotImplementedException();
        }

        public IEndpoint Endpoint
        {
            get { throw new NotImplementedException(); }
        }

        public IInboundMessagePipeline InboundPipeline
        {
            get { throw new NotImplementedException(); }
        }

        public IOutboundMessagePipeline OutboundPipeline
        {
            get { throw new NotImplementedException(); }
        }

        public IServiceBus ControlBus
        {
            get { throw new NotImplementedException(); }
        }

        public IEndpointCache EndpointCache
        {
            get { throw new NotImplementedException(); }
        }
    }
}
