namespace Hospital_Management_System.Model;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }

    public List<Appointment> Appointments { get; private set; } = new List<Appointment>();

    private Doctor() { }

    public Doctor(string name, string specialization)
    {
        SetProfile(name, specialization);
    }

    public void SetProfile(string name, string specialization)
    {
        if (string.IsNullOrEmpty(name)) throw new Exception("Name cannot be null or empty.");
        if (string.IsNullOrEmpty(specialization)) throw new Exception("Specialization cannot be null or empty.");
        Name = name;
        Specialization = specialization;
    }

    public void AddAppointment(Appointment appointment)
    {
        if (appointment == null) throw new Exception("Appointment cannot be null.");
        if (!Appointments.Contains(appointment)) Appointments.Add(appointment);
    }

    public void RemoveAppointment(Appointment appointment)
    {
        if (appointment == null) throw new Exception("Appointment cannot be null.");
        if (!Appointments.Contains(appointment)) throw new Exception("This appointment does not belong to this doctor.");
        Appointments.Remove(appointment);
    }
}