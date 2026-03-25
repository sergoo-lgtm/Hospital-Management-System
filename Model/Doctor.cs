using HospitalManagementSystemAPIVersion.CustomExceptions;

namespace HospitalManagementSystemAPIVersion.Model;

public class Doctor
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Specialization { get; private set; }

    public List<Appointment> Appointments { get; private set; }
    
    public Doctor(string name, string specialization)
    {
        SetProfile(name, specialization);
        Appointments = new List<Appointment>(); 

    }

    private Doctor()
    {
        
    }
    public void AddAppointment(Appointment appointment)
    {
        if (Appointments == null)
            throw  new BusinessException("Appointments cannot be null.");
        if (!Appointments.Contains(appointment)) 
            Appointments.Add(appointment);
    }

    public void RemoveAppointment(Appointment appointment)
    {if (Appointments == null)
            throw new BusinessException("Appointments cannot be null.");

        if (!Appointments.Contains(appointment))
            throw new NotFoundException("Appointment not found.");

        Appointments.Remove(appointment);
    }
    public void SetProfile(string name, string specialization)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty.");
        if (string.IsNullOrEmpty(specialization)) throw new ArgumentException("Specialization cannot be null or empty.");
        Name = name;
        Specialization = specialization;
    }

}