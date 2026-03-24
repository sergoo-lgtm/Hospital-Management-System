namespace HospitalManagementSystemAPIVersion.Model;

public class Patient
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    private Patient() { }

    
    List<Appointment> Appointments { get; set; }
    
    public Patient(string name, string phone)
    {
        SetName(name);
        SetPhone(phone);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new Exception("Name cannot be null or empty.");
        Name = name;
    }

    public void SetPhone(string phone)
    {
        if (string.IsNullOrEmpty(phone) || phone.Length != 11 || !phone.All(char.IsDigit))
            throw new Exception("Invalid phone number. Must be 11 digits.");

        Phone = phone;
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
}