namespace Hospital_Management_System.Model;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }

    public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    private Patient() { }

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
        if (appointment == null) throw new Exception("Appointment cannot be null.");
        if (!Appointments.Contains(appointment)) Appointments.Add(appointment);
    }

    public void RemoveAppointment(Appointment appointment)
    {
        if (appointment == null) throw new Exception("Appointment cannot be null.");
        if (!Appointments.Contains(appointment)) throw new Exception("This appointment does not belong to this patient.");
        Appointments.Remove(appointment);
    }
}