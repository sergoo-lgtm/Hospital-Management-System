namespace HospitalManagementSystemAPIVersion.DTO;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new List<T>(); // الصفحة اللي هتبعتها
    public int TotalCount { get; set; } // عدد كل المرضى بعد البحث
}