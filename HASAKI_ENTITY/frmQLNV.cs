using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using HASAKI_ENTITY;

namespace QLKH
{
    public partial class frmQLNV : Form
    {
        QLBH_HASAKI db = new QLBH_HASAKI();
        public frmQLNV()
        {
            InitializeComponent();
            LoadData(); 
        }
        void LoadData()
        {
            var listNV = from nv in db.NHAN_VIEN
                         select new
                         {
                             nv.MaNV,
                             nv.TenNV,
                             nv.NgSinhNV,
                             nv.GioiTinh,
                             nv.CMND,
                             nv.DiaChi,
                             nv.SDT,
                             nv.MaChucVu
                         };
            dataGridView2.DataSource = listNV.ToList();
            var Tencv = from cv in db.CHUC_VU select new { cv.MaChucVu, cv.TenChucVu };
            cboMaCV.DataSource = Tencv.ToList();
            cboMaCV.DisplayMember = "MaChucVu";
            cboMaCV.ValueMember = "TenChucVu";
        }
        void Lammoi()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            cboGioiTinh.Text = "";
            txtCMND.Text = "";
            txtDiaChiNV.Text = "";
            mstxt_SDT.Text = "";
            txtTenCV.Text = "";
            cboMaCV.Text = "";
        }
        void Them()//
        {
            try
            {
                NHAN_VIEN nv = new NHAN_VIEN
                {
                    MaNV = txtMaNV.Text,
                    TenNV = txtTenNV.Text,
                    NgSinhNV = dtp_NgaySinhNV.Value,
                    GioiTinh = cboGioiTinh.Text,
                    CMND = txtCMND.Text,
                    DiaChi = txtDiaChiNV.Text,
                    SDT = mstxt_SDT.Text,
                    MaChucVu = cboMaCV.Text
                };
                if (txtMaNV.TextLength > 5)
                {
                    MessageBox.Show($"Chỉ được nhập tối đa 5 ký tự ở MÃ NHÂN VIÊN!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaNV.Focus();
                }
                else if (txtMaNV.Text == "")
                {
                    MessageBox.Show("MÃ NHÂN VIÊN còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaNV.Focus();
                }
                else if (txtTenNV.Text == "")
                {
                    MessageBox.Show("TÊN NHÂN VIÊN còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenNV.Focus();
                }
                else if (cboGioiTinh.Text == "")
                {
                    MessageBox.Show("GIỚI TÍNH NHÂN VIÊN còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboGioiTinh.Focus();
                }
                else if (txtCMND.Text == "")
                {
                    MessageBox.Show("CMND NHÂN VIÊN còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCMND.Focus();
                }
                else if (txtDiaChiNV.Text == "")
                {
                    MessageBox.Show("ĐỊA CHỈ NHÂN VIÊN còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiaChiNV.Focus();
                }
                else if (mstxt_SDT.Text == "")
                {
                    MessageBox.Show("SỐ ĐIỆN THOẠI NHÂN VIÊN còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mstxt_SDT.Focus();
                }
                else if (cboMaCV.Text == "")
                {
                    MessageBox.Show("MÃ CHỨC VỤ NHÂN VIÊN còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mstxt_SDT.Focus();
                }
                else
                {
                    db.NHAN_VIEN.Add(nv);
                    db.SaveChanges(); //lưu thay đổi
                    LoadData();
                    MessageBox.Show($"Thêm NHÂN VIÊN thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                //Hiển thị thông báo lỗi
                MessageBox.Show($"Lỗi khi thêm dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Xoa()
        {
            try
            {
                if (dataGridView2.SelectedRows != null)
                {
                    NHAN_VIEN nv = (from n in db.NHAN_VIEN
                                     where n.MaNV == txtMaNV.Text
                                     select n).Single<NHAN_VIEN>();

                    db.NHAN_VIEN.Remove(nv);
                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show($"Xóa thông tin NHÂN VIÊN thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show($"Lỗi khi xóa dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Sua()
        {
            try
            {
                if (dataGridView2.SelectedRows != null)
                {
                    NHAN_VIEN nv = (from n in db.NHAN_VIEN
                                    where n.MaNV == txtMaNV.Text
                                    select n).Single<NHAN_VIEN>();
                    nv.TenNV = txtTenNV.Text.Trim();
                    nv.NgSinhNV = dtp_NgaySinhNV.Value;
                    nv.GioiTinh = cboGioiTinh.Text;
                    nv.CMND = txtCMND.Text;
                    nv.DiaChi = txtDiaChiNV.Text.Trim();
                    nv.SDT = mstxt_SDT.Text;
                    nv.MaChucVu = cboMaCV.Text;
                    db.SaveChanges();
                    MessageBox.Show($"Sửa thông tin NHÂN VIÊN thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show($"Lỗi khi sửa dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void TimKiem() //done
        {
            try
            {
                //Kiểm tra xem đã nhập thông tin vào ô tìm kiếm chưa
                if (string.IsNullOrWhiteSpace(txtTimKiemNV.Text))
                {
                    MessageBox.Show($"Vui lòng nhập thông tin cần tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // Tìm NHÂN VIÊN theo điều kiện
                var ketQuaTimKiem = db.NHAN_VIEN.Where(nv => nv.MaNV.ToString().Contains(txtTimKiemNV.Text) || nv.TenNV.Contains(txtTimKiemNV.Text));
                // Hiển thị kết quả tìm kiếm
                dataGridView2.DataSource = ketQuaTimKiem.ToList();
                int SLKQ = ketQuaTimKiem.Count();
                if (SLKQ > 0)
                {
                    MessageBox.Show($"Đã tìm thấy {SLKQ} NHÂN VIÊN.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy NHÂN VIÊN.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show($"Lỗi khi tìm kiếm NHÂN VIÊN", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Thêm dữ liệu
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            Them();
        }
        // Xóa dữ liệu
        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            Xoa();
        }
        // Sửa dữ liệu
        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            Sua();
        }
        //Làm mới dữ liệu
        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            Lammoi();
            LoadData();
        }
        //Tìm kiếm dữ liệu
        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtTenNV.Text = row.Cells["TenNV"].Value.ToString();
                dtp_NgaySinhNV.Text = row.Cells["NgsinhNV"].Value.ToString();
                cboGioiTinh.Text = row.Cells["GioiTinh"].Value.ToString();
                txtCMND.Text = row.Cells["CMND"].Value.ToString();
                txtDiaChiNV.Text = row.Cells["DiaChi"].Value.ToString();
                mstxt_SDT.Text = row.Cells["SDT"].Value.ToString();
                txtTenCV.Text = row.Cells["MaChucVu"].Value.ToString();
                cboMaCV.Text = row.Cells["MaChucVu"].Value.ToString();
                string chuyendoi = row.Cells["MaChucVu"].Value.ToString();
            }
        }

        private void cboMaCV_SelectedValueChanged(object sender, EventArgs e)
        {
            txtTenCV.Text = cboMaCV.SelectedValue.ToString();
        }
    }
}
