﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Faker;
using Test;

namespace FakerTest
{
    [TestClass]
    public class FakerTest
    {
        private static FakerInstance _fakerInstance;

        private class TestAllType
        {
            public int Int;
            public float Float;
            public long Long;
            public double Double;
            public char Char;
            public bool Bool;
            public string String;
            public DateTime DateTime;
        }

        private class TestConfig
        {
            public string ConfigString;
            public int ConfigInt;
            public string String;
            public int Int;
            public int PropInt { get; }
            public int PropIntConfig { get; }

            public TestConfig(int PropInt, int PropIntConfig)
            {
                this.PropInt = PropInt;
                this.PropIntConfig = PropIntConfig;
            }
        }

        private class TestConstructor
        {
            public int Int { get; }
            public string String { get; }
            public bool Bool { get; }

            public TestConstructor(int i)
            {
            }

            public TestConstructor(int i, int j)
            {
                Int = -10;
                Bool = false;
                String = "done";
            }
        }

        [TestInitialize]
        public void Init()
        {
            FakerConfiguration configuration = new FakerConfiguration();
            configuration.Add<TestConfig, string, NewStringGenerator>(TestConfig => TestConfig.ConfigString);
            configuration.Add<TestConfig, int, NewIntGenerator>(TestConfig => TestConfig.ConfigInt);
            configuration.Add<TestConfig, int, NewIntGenerator>(TestConfig => TestConfig.PropIntConfig);
            _fakerInstance = new FakerInstance(configuration);
        }

        [TestMethod]
        public void ConfigTest()
        {
            string[] songWords =
            {
                "flower", "bumper", "electricity", "atom", "auditor", "athlete"
            };
            TestConfig testConfig = _fakerInstance.Create<TestConfig>();
            Assert.IsNotNull(testConfig);
            Assert.AreEqual(testConfig.ConfigInt, 322);
            Assert.AreEqual(testConfig.PropIntConfig, 322);
            CollectionAssert.Contains(songWords, testConfig.ConfigString);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            TestConstructor testConstructor = _fakerInstance.Create<TestConstructor>();
            Assert.IsNotNull(testConstructor);
            Assert.AreEqual(testConstructor.Int, -10);
            Assert.AreEqual(testConstructor.String, "done");
            Assert.AreEqual(testConstructor.Bool, false);
        }

        [TestMethod]
        public void ListTest()
        {
            List<TestAllType> testConstructor = _fakerInstance.Create<List<TestAllType>>();
            Assert.IsNotNull(testConstructor);
            CollectionAssert.AllItemsAreNotNull(testConstructor);
        }

        [TestMethod]
        public void DoubleListTest()
        {
            List<List<TestAllType>> testConstructor = _fakerInstance.Create<List<List<TestAllType>>>();
            Assert.IsNotNull(testConstructor);
            CollectionAssert.AllItemsAreNotNull(testConstructor);
        }
    }
}