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
    
    public partial class CHUC_VU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUC_VU()
        {
            this.NHAN_VIEN = new HashSet<NHAN_VIEN>();
        }
    
        public string MaChucVu { get; set; }
        public string TenChucVu { get; set; }
        public int SoLuongNV { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAN_VIEN> NHAN_VIEN { get; set; }
    }
}
