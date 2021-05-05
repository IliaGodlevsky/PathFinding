using AssembleClassesLib.Attributes;
using AssembleClassesLib.Interface;
using AssembleClassesLib.Realizations;
using AssembleClassesLib.Realizations.AssembleClassesImpl;
using AssembleClassesLib.Realizations.LoadMethods;
using AssembleClassesLib.Tests.Infrastructure;
using AssembleClassesLib.Tests.TestObjects;
using NUnit.Framework;
using System.Linq;

namespace AssembleClassesLib.Tests
{
    [NotLoadable]
    internal class AssembleClassesTests
    {
        private readonly IAssembleLoadPath loadPath;
        private readonly IAssembleSearchOption searchOption;
        private readonly IAssembleClasses assembleClasses;
        private readonly ILoadMethod loadMethod;

        public AssembleClassesTests()
        {
            loadPath = new AssempleLoadPath();
            searchOption = new TopDirectoryOnly();
            loadMethod = new LoadFrom();
            assembleClasses = new AssembleClasses(loadPath, searchOption, loadMethod);
        }

        [Test]
        public void ClassesNames_ReturnsNotZeroNumberOfClassesNames()
        {
            assembleClasses.LoadClasses();

            Assert.IsTrue(assembleClasses.ClassesNames.Count > 0);
        }

        [Test]
        public void LoadClasses_IgnoresClassesWithNotLoadableAttribute()
        {
            assembleClasses.LoadClasses();
            var classes = assembleClasses.ClassesNames;

            Assert.IsFalse(classes.Contains(nameof(AssembleClassesTests)));
            Assert.IsFalse(classes.Contains(nameof(AssempleLoadPath)));
        }

        [Test]
        public void Get_NameFromClassNameAttribute_ReturnsNotNull()
        {
            assembleClasses.LoadClasses();

            var testObject1 = assembleClasses.Get(Constants.TestObject1Name, 0);
            var testObject2 = assembleClasses.Get(Constants.TestObject2Name, 0);

            Assert.IsTrue(testObject1 != null);
            Assert.IsTrue(testObject2 != null);
        }

        [Test]
        public void Get_NameIsClassName_ReturnsNotNull()
        {
            assembleClasses.LoadClasses();

            var testObject = assembleClasses.Get(typeof(TestObject3).FullName, 0);

            Assert.IsTrue(testObject != null);
        }
    }
}