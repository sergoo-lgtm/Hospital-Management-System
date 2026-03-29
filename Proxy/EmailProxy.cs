// Proxy/EmailProxy.cs
using MimeKit;
using MailKit.Net.Smtp;
using HospitalManagementSystemAPIVersion.Model;

namespace HospitalManagementSystemAPIVersion.Proxy
{
    public static class EmailProxy
    {
        public static async Task SendAppointmentEmail(Patient patient, Appointment appointment)
        {
            var msg = new MimeMessage();

            msg.From.Add(new MailboxAddress("Hospital App", "no-reply@hospital.com"));
            msg.To.Add(new MailboxAddress(patient.Name, $"{patient.Name}@example.com"));
            msg.Subject = "Appointment Confirmation";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
                <h2>Hello {patient.Name}</h2>
                <p>Your appointment with Dr. {appointment.Doctor.Name} has been successfully scheduled.</p>
                <p>Appointment Date & Time: {appointment.Date:dddd, dd MMMM yyyy HH:mm}</p>
                <p>Thank you for using our service ❤️</p>
            ";

            msg.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("localhost", 1025, false);
            await smtp.SendAsync(msg);
            await smtp.DisconnectAsync(true);
        }
    }
}