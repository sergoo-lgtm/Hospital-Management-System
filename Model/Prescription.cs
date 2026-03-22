namespace Hospital_Management_System.Model;

public class Prescription
{
    public int Id { get;  set; }  // Private setter عشان ماحدش يعدل الـ Id من بره
    public string Notes { get; set; }
    public string Medications { get; set; }

    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }

    // ===== Private constructor للـ EF Core =====
    private Prescription() { }

    // ===== Public constructor =====
    public Prescription(Appointment appointment, string notes, string medications)
    {
        if (appointment == null)
            throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");

        Appointment = appointment;
        AppointmentId = appointment.Id;

        SetNotes(notes);
        SetMedications(medications);
    }

    // ===== Methods to control the data (Rich Model behavior) =====
    public void SetNotes(string notes)
    {
        if (string.IsNullOrWhiteSpace(notes))
            throw new ArgumentException("Notes cannot be empty or null.");
        Notes = notes;
    }

    public void SetMedications(string medications)
    {
        if (string.IsNullOrWhiteSpace(medications))
            throw new ArgumentException("Medications cannot be empty or null.");
        Medications = medications;
    }

    public void AssignToAppointment(Appointment appointment)
    {
        if (appointment == null)
            throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");

        Appointment = appointment;
        AppointmentId = appointment.Id;
    }
}