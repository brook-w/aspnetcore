using System;

namespace PipelineAndMiddleware.Service
{
    public class Service : IService
    {
        public string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}