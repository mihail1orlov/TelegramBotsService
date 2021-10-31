﻿using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace ServiceTests
{
    [TestClass]
    public class BootstrapperTests
    {
        private Bootstrapper _target;

        [TestInitialize]
        public void Init()
        {
            _target = new Bootstrapper();
        }

        [TestMethod]
        public void GetDependencyInjectionContainerTest()
        {
            _target.BootStrap(new ContainerBuilder());
        }
    }
}