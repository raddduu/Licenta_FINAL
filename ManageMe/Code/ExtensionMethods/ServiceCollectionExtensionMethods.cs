using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ManageMe.BusinessLogic;
using ManageMe.Common.DTOs;
using ManageMe.WebApp.Code;
using System;
using System.Linq;
using ManageMe.BusinessLogic.Implementation.Subject;
using ManageMe.Common;
using ManageMe.Services;

namespace ManageMe.WebApp.Code.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {

        public static IServiceCollection AddManageMeBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<ChannelService>();
            services.AddScoped<GeneralAlgorithm>();
            services.AddScoped<SubjectService>();
            services.AddScoped<MessageService>();
            services.AddScoped<UserService>();
            services.AddScoped<StudyDomainService>();
            services.AddScoped<StudyPlanService>();
            services.AddScoped<TeacherPermissionService>();
            services.AddScoped<GroupService>();
            services.AddScoped<GradingCriterionService>();
            //services.AddScoped<SubjectActivityFrequencyService>();
            services.AddScoped<GradeService>();
            services.AddScoped<FinalGradeService>();
            services.AddScoped<BuildingService>();
            services.AddScoped<HallService>();
            services.AddScoped<ScheduleService>();
            services.AddScoped<BatchService>();
            services.AddScoped<MethodologyService>();
            services.AddScoped<ChapterService>();
            services.AddScoped<ParagraphService>();
            services.AddScoped<DetailService>();
            services.AddScoped<SectionService>();
            services.AddScoped<ArticleService>();
            services.AddScoped<ProvisionService>();

            return services;
        }
    }
}
