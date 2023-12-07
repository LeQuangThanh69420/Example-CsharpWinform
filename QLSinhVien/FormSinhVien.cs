using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSinhVien
{
    public partial class FormSinhVien : Form
    {
        QLSinhVienDataContext db = new QLSinhVienDataContext();
        private Form1 _form1;
        public FormSinhVien(Form1 form1, string username)
        {
            InitializeComponent();
            lbl_Username.Text = username;
            _form1 = form1;
        }

        private void LoadData()
        {
            dgv_SinhVien.DataSource = db.SVs.ToList();
            txt_MSSV.Text = "";
            txt_TenSV.Text = "";
            cbb_GioiTinh.Text = "";
            btn_Add.Enabled = true;
            btn_Edit.Enabled = false;
            btn_Delete.Enabled = false;
            txt_MSSV.ReadOnly = false;
        }

        private void FormSinhVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FormSinhVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form1.Show();
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MSSV.Text == "" || txt_TenSV.Text == "" || cbb_GioiTinh.Text == "")
                {
                    MessageBox.Show("Không được để trống MSSV, Họ tên, GioiTinh!");
                }
                else
                {
                    SV newSV = new SV();
                    newSV.MSSV = txt_MSSV.Text;
                    newSV.TenSV = txt_TenSV.Text;
                    if (cbb_GioiTinh.Text == "Nam") newSV.GioiTinh = true;
                    if (cbb_GioiTinh.Text == "Nu") newSV.GioiTinh = false;
                    db.SVs.InsertOnSubmit(newSV);
                    db.SubmitChanges();
                    MessageBox.Show("Add success!");
                    LoadData();
                }
            }
            catch
            {
                MessageBox.Show("Add fail!");
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                SV editSV = db.SVs.SingleOrDefault(o => o.MSSV.Equals(txt_MSSV.Text));
                editSV.MSSV = txt_MSSV.Text;
                editSV.TenSV = txt_TenSV.Text;
                if (cbb_GioiTinh.Text == "Nam") editSV.GioiTinh = true;
                if (cbb_GioiTinh.Text == "Nu") editSV.GioiTinh = false;
                MessageBox.Show("Edit succ");
                LoadData();
            }
            catch
            {
                MessageBox.Show("Edit fail!");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                var delSV = db.SVs.SingleOrDefault(s => s.MSSV == txt_MSSV.Text);
                db.SVs.DeleteOnSubmit(delSV);
                db.SubmitChanges();
                MessageBox.Show("Delete Succ");
                LoadData();
            }
            catch
            {
                MessageBox.Show("Delete fail!");
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgv_SinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txt_MSSV.ReadOnly = true;
                btn_Add.Enabled = false;
                btn_Edit.Enabled = true;
                btn_Delete.Enabled = true;
                DataGridViewRow row = dgv_SinhVien.Rows[e.RowIndex];
                txt_MSSV.Text = row.Cells[0].Value.ToString();
                txt_TenSV.Text = row.Cells[1].Value.ToString();
                if((bool)row.Cells[2].Value == true) cbb_GioiTinh.Text = "Nam";
                if((bool)row.Cells[2].Value == false) cbb_GioiTinh.Text = "Nu";
            }
        }
    }
}
