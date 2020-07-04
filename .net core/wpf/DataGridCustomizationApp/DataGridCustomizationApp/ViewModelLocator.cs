using Autofac;
using DataGridCustomizationApp.ViewModels;

namespace DataGridCustomizationApp
{
    public class ViewModelLocator
    {
        private IContainer _container;

        public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();

        public ViewModelLocator()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterServices(containerBuilder);
            RegisterModules(containerBuilder);

            _container = containerBuilder.Build();
        }

        private void RegisterServices(ContainerBuilder containerBuilder)
        {
            
        }

        private void RegisterModules(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterMainModule();
        }
    }

    public static class MainModuleExtension
    {
        public static ContainerBuilder RegisterMainModule(this ContainerBuilder containerBuilder)
        {

            containerBuilder.RegisterModule<MainModule>();

            return containerBuilder;
        }
    }

    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<DataGridViewModel>().AsSelf().SingleInstance();
        }
    }
}
