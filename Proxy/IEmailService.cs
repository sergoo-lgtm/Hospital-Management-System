namespace HospitalManagementSystemAPIVersion.Proxy;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);

}