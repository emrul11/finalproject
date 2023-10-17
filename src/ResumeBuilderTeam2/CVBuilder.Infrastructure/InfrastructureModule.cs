using Autofac;
using CVBuilder.Application.features.Services;
using CVBuilder.Domain.Services;
using CVBuilder.Infrastructure.Service;
using CVBuilder.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Infrastructure
{
    public class InfrastructureModule : Module
    {
        public InfrastructureModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ResumeService>().As<IResumeService>().InstancePerLifetimeScope();
            builder.RegisterType<HtmlEmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailMessageService>().As<IEmailMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailServiceTest>().InstancePerLifetimeScope();
            builder.RegisterType<UserEmailOptions>().AsSelf().InstancePerLifetimeScope();
            
            base.Load(builder);
        }
    }
}
