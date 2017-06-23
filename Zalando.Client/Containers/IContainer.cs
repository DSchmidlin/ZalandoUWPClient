namespace Zalando.Client.Containers
{
    public interface IContainer
    {
        void Register<Tinterface, UConcreteClass>() where UConcreteClass : Tinterface;
        void Register<Tinterface, UConcreteClass>(UConcreteClass instance) where UConcreteClass : Tinterface;
        Tinterface Resolve<Tinterface>();
    }
}
