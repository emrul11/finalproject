using Autofac;
using CVBuilder.Web.Areas.Admin.Models;
using CVBuilder.Web.Areas.Users.Factory;
using CVBuilder.Web.Areas.Users.Models;
using CVBuilder.Web.Models;

namespace CVBuilder.Web
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<RegisterModelVM>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoginVM>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ConfirmEmailModelVM>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ForgotPasswordModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ResetPasswordModel>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<RoleCreateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoleEditModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoleDeleteModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<IdentityUserListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ManageClaimModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AssignClaimModel>().AsSelf().InstancePerLifetimeScope();       
            builder.RegisterType<ResumeFactory>().As<IResumeFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ResumeUpdateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ResumeCreateModel>().AsSelf()
                   .InstancePerLifetimeScope();
        }
    }
}
