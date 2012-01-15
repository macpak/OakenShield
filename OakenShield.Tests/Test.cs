using Castle.Windsor;
using NUnit.Framework;

namespace OakenShield.Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        [ExpectedException(typeof(PredicatesNotSatisfiedException))]
        public void PredicateNotSatisfied_ShouldThrowException()
        {
            using (var container = new WindsorContainer())
            {
                container.Register(new[]
                                       {
                                           Component.For<A>().Method(a => a.Fun(ArgsHelper<int>.Arg, ArgsHelper<string>.Arg)).Arg<int>("arg1",
                                                                                                               arg =>
                                                                                                               arg > 10)
                                       });

                var instance = container.Resolve<A>();
                instance.Fun(2,"qwe");
            }

        }
    }

    public class A
    {
         public virtual int Fun(int arg1, string s)
         {
             return 0;
         }
    }
}   