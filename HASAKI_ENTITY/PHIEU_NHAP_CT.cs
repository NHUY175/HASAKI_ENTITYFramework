//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HASAKI_ENTITY
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHIEU_NHAP_CT
    {
        public string MaNhap { get; set; }
        public string MaSP { get; set; }
        public string MaNV { get; set; }
        public int SoLuongNhap { get; set; }
        public string TrangThai { get; set; }
        public decimal DonGiaNhap { get; set; }
        public decimal TongNhap { get; set; }
    
        public virtual NHAN_VIEN NHAN_VIEN { get; set; }
        public virtual PHIEU_NHAP PHIEU_NHAP { get; set; }
    }
}
