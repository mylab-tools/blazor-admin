using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using MyLab.BlazorAdmin.Tools;
using Xunit.Abstractions;

namespace UnitTests
{
    public class InitParametersDictionaryFromExpressionBehavior
    {
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Initializes a new instance of <see cref="InitParametersDictionaryFromExpressionBehavior"/>
        /// </summary>
        public InitParametersDictionaryFromExpressionBehavior(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ShouldInitFromExpression()
        {
            //Arrange
            Expression<Func<TestModel>> expr = () => new TestModel
            {
                Value = "foo"
            };

            //Act
            var parameters = InitParametersDictionary.FromExpression(expr);

            //Assert
            Assert.NotNull(parameters);
            Assert.Single(parameters);
            Assert.Contains(parameters, p => p.Key == nameof(TestModel.Value) && p.Value == "foo");
        }

        [Fact]
        public void ShouldNotPassWhenNotInitMemberExpression()
        {
            //Arrange
            Expression<Func<TestModel>> expr = () => new TestModel();

            //Act & Assert
            var e = Assert.Throws<NotSupportedException>(() => InitParametersDictionary.FromExpression(expr));
            
            _output.WriteLine("Exception: " + e.Message);
        }

        [Fact]
        public void ShouldNotPassWhenNotDefaultConstructor()
        {
            //Arrange
            Expression<Func<TestModel>> expr = () => new TestModel("foo"){ Value = "bar"};

            //Act & Assert
            var e = Assert.Throws<NotSupportedException>(() => InitParametersDictionary.FromExpression(expr));

            _output.WriteLine("Exception: " + e.Message);
        }

        [Fact]
        public void ShouldNotPassWhenWrongBinding()
        {
            //Arrange
            Expression<Func<TestModel>> expr = () => new TestModel
            {
                List =
                {
                    "foo"
                }
            };

            //Act & Assert
            var e = Assert.Throws<NotSupportedException>(() => InitParametersDictionary.FromExpression(expr));

            _output.WriteLine("Exception: " + e.Message);
        }

        [Fact]
        public void ShouldNotPassWhenNotParameterAssignment()
        {
            //Arrange
            Expression<Func<TestModel>> expr = () => new TestModel { NotParameter = "foo"};

            //Act & Assert
            var e = Assert.Throws<NotSupportedException>(() => InitParametersDictionary.FromExpression(expr));

            _output.WriteLine("Exception: " + e.Message);
        }

        class TestModel
        {
            [Parameter]
            public string? Value { get; set; }

            public string? NotParameter{ get; set; }

            [Parameter]
            public List<string>? List { get; set; }

            public TestModel()
            {
                
            }
            
            public TestModel(string value)
            {
                Value = value;
            }
        }
    }
}