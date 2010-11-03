﻿using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CuttingEdge.ServiceLocation.Tests.Unit
{
    /// <summary>
    /// Tests for the <see cref="SimpleServiceLocator.RegisterSingle{T}(Action{T})"/> overload.
    /// This overload allows registering concrete singleton instances to be returned and initialized using the
    /// supplied Action{T} delegate.
    /// </summary>
    [TestClass]
    public class RegisterSingleByActionTests
    {
        [TestMethod]
        public void RegisterSingleByAction_WithValidAction_Succeeds()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            // Act
            container.RegisterSingle<Samurai>(samurai => { });
        }

        [TestMethod]
        public void GetInstance_RegisterSingleByAction_CallsActionOnce()
        {
            // Arrange
            const int ExpectedNumberOfCalls = 1;
            int actualNumberOfCalls = 0;

            var container = new SimpleServiceLocator();

            // Samurai takes an IWeapon as a constructor argument.
            container.RegisterSingle<IWeapon>(new Katana());

            Action<Samurai> instanceInitializer = _ => { actualNumberOfCalls++; };

            container.RegisterSingle<Samurai>(instanceInitializer);

            // Act
            container.GetInstance<Samurai>();
            container.GetInstance<Samurai>();
            container.GetInstance<Samurai>();

            // Assert
            Assert.AreEqual(ExpectedNumberOfCalls, actualNumberOfCalls,
                "The Action<T> was expected to be called once.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterSingleByAction_WithNullArgument_ThrowsException()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            Action<Samurai> invalidInstanceInitializer = null;

            // Act
            container.RegisterSingle<Samurai>(invalidInstanceInitializer);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterSingleByAction_WithAbstractType_ThrowsException()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            Action<IWeapon> validInstanceInitializer = _ => { };

            // Act
            // IWeapon is not an concrete type.
            container.RegisterSingle<IWeapon>(validInstanceInitializer);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterSingleByAction_CalledAfterCallToGetInstance_ThrowsException()
        {
            // Arrange
            var container = new SimpleServiceLocator();
            container.Validate();

            Action<Samurai> validInstanceInitializer = _ => { };

            // Act
            container.RegisterSingle<Samurai>(validInstanceInitializer);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterByFunc_ThrowsException()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.Register<Samurai>(() => new Samurai(null));

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterSigle_ThrowsException()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.RegisterSingle<Samurai>();

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterSingleByFunc_ThrowsException()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.RegisterSingle<Samurai>(() => new Samurai(null));

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterSingleByAction_ThrowsException()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.RegisterSingle<Samurai>(_ => { });

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }

        [TestMethod]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterFuncByKey_Succeeds()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.RegisterByKey<Samurai>("sammy_the_samurai", () => new Samurai(null));

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }

        [TestMethod]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterByKeyedFunc_Succeeds()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.RegisterByKey<Samurai>(key => new Samurai(null));

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }

        [TestMethod]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterSingleInstanceByKey_Succeeds()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.RegisterSingleByKey<Samurai>("sammy", new Samurai(null));

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }

        [TestMethod]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterSingleFuncByKey_Succeeds()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.RegisterSingleByKey<Samurai>("sammy", () => new Samurai(null));

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }

        [TestMethod]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterSingleByKeyedFunc_Succeeds()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.RegisterSingleByKey<Samurai>(key => new Samurai(null));

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }

        [TestMethod]
        public void RegisterSingleByAction_CalledAfterTypeAlreadyRegisteredUsingRegisterAll_Succeeds()
        {
            // Arrange
            var container = new SimpleServiceLocator();

            container.RegisterAll<Samurai>(new[] { new Samurai(null) });

            // Act
            container.RegisterSingle<Samurai>(_ => { });
        }
    }
}