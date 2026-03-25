
using HospitalManagementSystemAPIVersion.CustomExceptions;

namespace HospitalManagementSystemAPIVersion.Model;

public class Patient
{
    
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }
    private Patient() { }

    
    public List<Appointment> Appointments { get; private set; }
    
    public Patient(string name, string phone)
    {
        SetName(name);
        SetPhone(phone);
        Appointments = new List<Appointment>(); 

    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty.");
        Name = name;
    }

    public void SetPhone(string phone)
    {
        if (string.IsNullOrEmpty(phone) || phone.Length != 11 || !phone.All(char.IsDigit))
            throw new ArgumentException("Invalid phone number. Must be 11 digits.");

        Phone = phone;
    }

   
    
    public void AddAppointment(Appointment appointment)
    {
        if (Appointments == null)
            throw  new BusinessException("Appointments cannot be null.");
        if (!Appointments.Contains(appointment)) 
            Appointments.Add(appointment);
    }

    public void RemoveAppointment(Appointment appointment)
    {
        if (Appointments == null)
            throw new BusinessException("Appointments cannot be null.");

        if (!Appointments.Contains(appointment))
            throw new NotFoundException("Appointment not found.");

        Appointments.Remove(appointment);
    }
}