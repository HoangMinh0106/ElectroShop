namespace DoAnWebNC.Models;
//Được dùng để hiển thị mã lỗi trên View khi có lỗi xảy ra.
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
