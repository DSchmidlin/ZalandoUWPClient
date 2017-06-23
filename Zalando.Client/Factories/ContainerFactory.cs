using Zalando.Client.Containers;
using Zalando.Client.Extensions;

namespace Zalando.Client.Factories
{
    public static class ContainerFactory
    {
        private static IContainer mContainer;

        public static IContainer GetContainer()
        {
            if (mContainer == null)
            {
                mContainer = new UnityWrapperContainer();
                mContainer.RegisterDefaults();
            }

            return mContainer;
        }
    }
}
