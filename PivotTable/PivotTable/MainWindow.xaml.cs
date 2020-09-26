using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PivotTable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //data static
            dsDiem.Items.Add(new ItemdsDiem { MASV = "1712801", Mon = "Toan", Diem = 9 });
            dsDiem.Items.Add(new ItemdsDiem { MASV = "1712802", Mon = "Ly", Diem = 10 });
            dsDiem.Items.Add(new ItemdsDiem { MASV = "1712803", Mon = "Ly", Diem = 8 });
            dsDiem.Items.Add(new ItemdsDiem { MASV = "1712802", Mon = "Hoa", Diem = 7 });
            dsDiem.Items.Add(new ItemdsDiem { MASV = "1712802", Mon = "Hoa", Diem = 9 });
        }
        //Item add listview diem
        public class ItemdsDiem
        {
            public string MASV { get; set; }
            public string Mon { get; set; }
            public float Diem { get; set; }
        }
        //Item add listview diem cao nhat (pivot)
        public class ItemdsDiemCaoNhat
        {
            public string MASV { get; set; }
            public float Toan { get; set; }
            public float Ly { get; set; }
            public float Hoa { get; set; }
        }
        //function get diem cao nhat cua sinh vien tai mon hoc
        public float getMaxSubject(string mssv, string mon)
        {
            float result = 0;
            for (int i = 0; i < dsDiem.Items.Count; i++)
            {
                ItemdsDiem item = dsDiem.Items[i] as ItemdsDiem;
                if(item.MASV == mssv && item.Mon == mon)
                {
                    if(item.Diem > result)
                    {
                        result = item.Diem;
                    }
                    else
                    {
                        //do noting
                    }
                } 

                else
                {
                    //do nothing;
                }    
            }

            return result;
        }
        //them diem mon hoc cua mot sinh vien
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (float.Parse(txt_diem.Text) < 0 || float.Parse(txt_diem.Text) > 10)
                {
                    MessageBox.Show("Invalid value at Diem", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        dsDiem.Items.Add(new ItemdsDiem { MASV = txt_masv.Text, Mon = txt_mon.Text, Diem = float.Parse(txt_diem.Text) });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //refesh du lieu listview pivot
        private void btn_refesh_Click(object sender, RoutedEventArgs e)
        {
            dsDiemcaonhat.Items.Clear();
            //lay danh sach ma sinh vien
            List<string> list_mssv  = new List<string>();
            for (int i = 0; i < dsDiem.Items.Count; i++)
            {
                ItemdsDiem item = dsDiem.Items[i] as ItemdsDiem;
                list_mssv.Add(item.MASV);
            }
            //bo di cac ma sinhvien bi trung lap
            List<string> list_mssv_distinct = list_mssv.Distinct().ToList();
            //lay diem cao nhat tai tung mon hoc cua tat ca sinh vien
            //sau do add vao listview pivot
            for (int i = 0; i < list_mssv_distinct.Count; i++)
            {
                string masv = list_mssv_distinct[i];
                float maxToan = getMaxSubject(masv, "Toan");
                float maxLy = getMaxSubject(masv, "Ly");
                float maxHoa = getMaxSubject(masv, "Hoa");
                dsDiemcaonhat.Items.Add(new ItemdsDiemCaoNhat { MASV = masv, Toan = maxToan, Ly = maxLy, Hoa = maxHoa });          
            }
        }
    }
}
