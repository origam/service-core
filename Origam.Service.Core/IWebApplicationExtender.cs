using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Origam.Service.Core;

public interface IWebApplicationExtender
{
    void Extend(IApplicationBuilder app, IConfiguration configuration);
}