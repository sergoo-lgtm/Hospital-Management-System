namespace HospitalManagementSystemAPIVersion.Model;

public class Payment
{
    public int Id { get; set; }
    public bool IsPaid { get; set; }
    public int Amount { get; set; }

    public Appointment Appointment { get; set; }
    public int AppointmentId { get; set; }
    public Payment(int amount, int appointmentId)
    {
        SetAmount(amount);
        AppointmentId = appointmentId;
        IsPaid = false;
    }
    private Payment()
    {
    }

    public int SetAmount(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException("amount");
        }
        Amount = amount;
        return Amount;
    }

    public bool Pay(int checkedAppointmentId)
    {
        
        if (checkedAppointmentId != AppointmentId)
            return false;

        if (!IsPaid)
            IsPaid = true;

        return IsPaid;
    }

    public void UpdateAmount(int Newmount)
    {
        if(!IsPaid){Amount = SetAmount(Newmount); }
    }

}