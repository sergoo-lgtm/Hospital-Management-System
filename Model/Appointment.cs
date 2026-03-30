namespace HospitalManagementSystemAPIVersion.Model;

public class Appointment
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Status { get; private set; }
    

    public int PatientId { get; private set; }
    public Patient Patient { get; private set; }

    public int DoctorId { get; private set; }
    public Doctor Doctor { get; private set; }

    public Payment Payment { get; private set; }
    public Prescription Prescription { get;private  set; }

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
    }}