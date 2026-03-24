namespace HospitalManagementSystemAPIVersion.Model;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }

    List<Appointment> Appointments { get; set; }
    
    public Doctor(string name, string specialization)
    {
        SetProfile(name, specialization);
    }

    private Doctor()
    {
        
    }
    public void AddAppointment(Appointment appointment)
    {
        if (Appointments == null)
            throw  new Exception("Appointments cannot be null.");
        if (!Appointments.Contains(appointment)) 
            Appointments.Add(appointment);
    }

    public void RemoveAppointment(Appointment appointment)
    {
        if (Appointments == null)
            throw  new Exception("Appointments cannot be null.");
        if (Appointments.Contains(appointment))
            if(!Appointments.Contains(appointment)) throw  new Exception("Appointments not Found.");
        Appointments.Remove(appointment);
    }
    public void SetProfile(string name, string specialization)
    {
        if (string.IsNullOrEmpty(name)) throw new Exception("Name cannot be null or empty.");
        if (string.IsNullOrEmpty(specialization)) throw new Exception("Specialization cannot be null or empty.");
        Name = name;
        Specialization = specialization;
    }

}