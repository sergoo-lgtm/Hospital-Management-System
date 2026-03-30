
using HospitalManagementSystemAPIVersion.CustomExceptions;
using System.Text.RegularExpressions;

namespace HospitalManagementSystemAPIVersion.Model;

public class Patient
{
    
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }
    
    public string Email { get; private set; }
    private Patient() { }


    public List<Appointment> Appointments { get; private set; } 
    
    public Patient(string name, string phone,  string email)
    {
        SetName(name);
        SetPhone(phone);
        SetEmail(email);
        Appointments = new List<Appointment>(); 

    }


    private void SetEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new BusinessException("Email cannot be null or empty.");

        var pattern = @"^[^@\s]+@[^@\s]+\.com$";

        if (!Regex.IsMatch(email, pattern))
            throw new BusinessException("Invalid email format.");

        Email = email;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new BusinessException("Name cannot be null or empty.");
        Name = name;
    }

    public void SetPhone(string phone)
    {
        if (string.IsNullOrEmpty(phone) || phone.Length != 11 || !phone.All(char.IsDigit))
            throw new BusinessException("Invalid phone number. Must be 11 digits.");

        Phone = phone;
    }

   
    
    public void AddAppointment(Appointment appointment)
    {
        if (Appointments == null)
            Appointments = new List<Appointment>();

        if (!Appointments.Contains(appointment)) 
            Appointments.Add(appointment);
    }

    public void RemoveAppointment(Appointment appointment)
    {
        if (Appointments == null)
            throw new ArgumentNullException("Appointments cannot be null.");

        if (!Appointments.Contains(appointment))
            throw new NotFoundException("Appointment not found.");

        Appointments.Remove(appointment);
    }
}