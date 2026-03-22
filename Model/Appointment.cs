namespace Hospital_Management_System.Model;

public class Appointment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public Payment Payment { get; set; }
    public Prescription Prescription { get; set; }

    private Appointment() { }

    public Appointment(Patient patient, Doctor doctor, DateTime date, string status = "Scheduled")
    {
        AssignPatient(patient);
        AssignDoctor(doctor);
        UpdateDate(date);
        UpdateStatus(status);
    }

    public void AssignPatient(Patient patient)
    {
        if (patient == null) throw new ArgumentNullException(nameof(patient));
        Patient = patient;
        PatientId = patient.Id;
        patient.AddAppointment(this);
    }

    public void AssignDoctor(Doctor doctor)
    {
        if (doctor == null) throw new ArgumentNullException(nameof(doctor));
        Doctor = doctor;
        DoctorId = doctor.Id;
        doctor.AddAppointment(this);
    }

    public void UpdateDate(DateTime newDate) => Date = newDate;

    public void UpdateStatus(string newStatus)
    {
        if (string.IsNullOrEmpty(newStatus)) throw new ArgumentException("Status cannot be empty");
        Status = newStatus;
    }

    public void AssignPayment(Payment payment)
    {
        if (payment == null) throw new ArgumentNullException(nameof(payment));
        Payment = payment;
    }

    public void AssignPrescription(Prescription prescription)
    {
        if (prescription == null) throw new ArgumentNullException(nameof(prescription));
        Prescription = prescription;
    }
}