﻿using Dependency_Injection_Container;

namespace Tests
{
    public class SomeAnotherService : IService
    {
        private IRepository Repository;

        public SomeAnotherService([DependencyKey(DependencyInjectionContainerLib.Key.Second)] IRepository repository)
        {
            Repository = repository;
        }
        
        public object GetTestFieldValue()
        {
            return Repository;
        }
    }
}