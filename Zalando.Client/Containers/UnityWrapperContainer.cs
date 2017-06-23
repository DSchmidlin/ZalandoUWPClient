using Microsoft.Practices.Unity;

namespace Zalando.Client.Containers
{
    public class UnityWrapperContainer : IContainer
    {
        private IUnityContainer mContainer;

        public UnityWrapperContainer()
        {
            mContainer = new UnityContainer();
        }

        public void Register<Tinterface, UConcreteClass>() where UConcreteClass : Tinterface
        {
            mContainer.RegisterType<Tinterface, UConcreteClass>();
        }

        public void Register<Tinterface, UConcreteClass>(UConcreteClass instance) where UConcreteClass : Tinterface
        {
            mContainer.RegisterInstance<Tinterface>(instance);
        }

        public Tinterface Resolve<Tinterface>()
        {
            return mContainer.Resolve<Tinterface>();
        }
    }
}
