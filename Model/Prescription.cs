namespace HospitalManagementSystemAPIVersion.Model;

public class Prescription
{
    public int Id { get;  private set; }  
    public string Notes { get; private set; }
    public string Medications { get; private set; }
    public int AppointmentId { get; private set; }
    public Appointment Appointment { get; private set; }

    private Prescription()
    {
        
    }
    
    public Prescription(Appointment appointment, string notes, string medications)
    {
        if (appointment == null)
            throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");

        Appointment = appointment;
        AppointmentId = appointment.Id;

        SetNotes(notes);
        SetMedications(medications);
    }
    public void AssignToAppointment(Appointment appointment)
    {
        if (appointment == null)
            throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");

        Appointment = appointment;
        AppointmentId = appointment.Id;
    }

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
}