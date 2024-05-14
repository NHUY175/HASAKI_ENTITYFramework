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
    public partial class frmQLKH : Form
    {
        QLBH_HASAKI db = new QLBH_HASAKI();
        public frmQLKH()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            var listKH = from kh in db.KHACH_HANG
                         select new
                         {
                             kh.MaKH,
                             kh.TenKH,
                             kh.NgSinhKH,
                             kh.GioiTinh,
                             kh.DiaChi,
                             kh.SDT,
                         };
            dataGridView1.DataSource = listKH.ToList();
        }
        void Lammoi()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            cboGioiTinh.Text = "";
            txtDiaChi.Text = "";
            mstxt_SDTKH.Text = "";
        }
        void Them()
        {
            try
            {
                KHACH_HANG KH = new KHACH_HANG
                {
                    MaKH = txtMaKH.Text,
                    TenKH = txtTenKH.Text,
                    NgSinhKH = dtp_NgaySinh.Value,
                    GioiTinh = cboGioiTinh.Text,
                    DiaChi = txtDiaChi.Text,
                    SDT = mstxt_SDTKH.Text
                };
                if (txtMaKH.TextLength > 5)
                {
                    MessageBox.Show($"Chỉ được nhập tối đa 5 ký tự ở MÃ KHÁCH HÀNG!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaKH.Focus();
                }
                else if (txtMaKH.Text == "")
                {
                    MessageBox.Show("MÃ KHÁCH HÀNG còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaKH.Focus();
                }
                else if (txtTenKH.Text == "")
                {
                    MessageBox.Show("TÊN KHÁCH HÀNG còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenKH.Focus();
                }
                else if (cboGioiTinh.Text == "")
                {
                    MessageBox.Show("GIỚI TÍNH KHÁCH HÀNG còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboGioiTinh.Focus();
                }
                else if (txtDiaChi.Text == "")
                {
                    MessageBox.Show("ĐỊA CHỈ KHÁCH HÀNG còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiaChi.Focus();
                }
                else if (mstxt_SDTKH.Text == "")
                {
                    MessageBox.Show("SỐ ĐIỆN THOẠI KHÁCH HÀNG còn trống", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mstxt_SDTKH.Focus();
                }
                else
                {
                    db.KHACH_HANG.Add(KH);
                    db.SaveChanges(); //lưu thay đổi
                    LoadData();
                    MessageBox.Show($"Thêm khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (dataGridView1.SelectedRows != null)
                {
                    KHACH_HANG kh = (from k in db.KHACH_HANG
                                     where k.MaKH == txtMaKH.Text
                                     select k).Single<KHACH_HANG>();

                    db.KHACH_HANG.Remove(kh);
                    db.SaveChanges();
                    LoadData();
                    MessageBox.Show($"Xóa thông tin khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (dataGridView1.SelectedRows != null)
                {
                    KHACH_HANG kh = (from k in db.KHACH_HANG
                                     where k.MaKH == txtMaKH.Text.Trim()
                                     select k).Single<KHACH_HANG>();

                    kh.TenKH = txtTenKH.Text.Trim();
                    kh.NgSinhKH = dtp_NgaySinh.Value;
                    kh.GioiTinh = cboGioiTinh.Text;
                    kh.DiaChi = txtDiaChi.Text.Trim();
                    kh.SDT = mstxt_SDTKH.Text;
                    db.SaveChanges();
                    MessageBox.Show($"Sửa thông tin khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show($"Lỗi khi sửa dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void TimKiem()
        {
            try
            {
                //Kiểm tra xem đã nhập thông tin vào ô tìm kiếm chưa
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    MessageBox.Show($"Vui lòng nhập thông tin cần tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // Tìm khách hàng theo điều kiện
                var ketQuaTimKiem = db.KHACH_HANG.Where(kh => kh.MaKH.ToString().Contains(txtTimKiem.Text) || kh.TenKH.Contains(txtTimKiem.Text));
                // Hiển thị kết quả tìm kiếm
                dataGridView1.DataSource = ketQuaTimKiem.ToList();
                int SLKQ = ketQuaTimKiem.Count();
                if (SLKQ > 0)
                {
                    MessageBox.Show($"Đã tìm thấy {SLKQ} khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show($"Lỗi khi tìm kiếm khách hàng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm dữ liệu
        private void btnThem_Click(object sender, EventArgs e)
        {
            Them();
        }
        // Xóa dữ liệu
        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }
        // Sửa dữ liệu
        private void btnSua_Click(object sender, EventArgs e)
        {
            Sua();
        }
        //Làm mới dữ liệu
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Lammoi();
        }
        //Tìm kiếm dữ liệu
        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtTenKH.Text = row.Cells["TenKH"].Value.ToString();
                dtp_NgaySinh.Text = row.Cells["NgsinhKH"].Value.ToString();
                cboGioiTinh.Text = row.Cells["Gioitinh"].Value.ToString();
                txtDiaChi.Text = row.Cells["Diachi"].Value.ToString();
                mstxt_SDTKH.Text = row.Cells["Sdt"].Value.ToString();
            }
        }
    }
}
