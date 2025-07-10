using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Config;

public static class AppConfig
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        return builder;
    }

    public static AppOptions AppOptionsConfigure(this WebApplicationBuilder builder)
    {
        IConfiguration appOptionsSection = builder.Configuration.GetSection(AppOptions.AppOptionsSection);
        AppOptions appOptions = appOptionsSection.Get<AppOptions>()!;
        builder.Services.Configure<AppOptions>(appOptionsSection);

        return appOptions;
    }
}
