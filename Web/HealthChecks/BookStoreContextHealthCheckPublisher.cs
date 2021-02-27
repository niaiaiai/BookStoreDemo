using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Web.HealthChecks
{
    public class BookStoreContextHealthCheckPublisher : IHealthCheckPublisher
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public BookStoreContextHealthCheckPublisher(IEmailSender emailSender, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _configuration = configuration;
        }
        public async Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            string message = report.Entries["BookStoreContext"].Status.ToString();
            await _emailSender.SendEmailAsync(_configuration.GetValue<string>("Email:ToEmail"), "test", message);
        }
    }
}
