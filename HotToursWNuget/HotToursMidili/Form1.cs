using HotToursLogic;
using HotToursLogic.Models;
using Microsoft.VisualBasic.Logging;

namespace HotTours_Midili
{
    public partial class Form1 : Form
    {
        private readonly BindingSource bindingSource;
        Logic logic = new Logic();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bindingSource = new BindingSource();
            bindingSource.DataSource = logic.Tours;
            dataGridView1.DataSource = bindingSource;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            tsizm.Enabled = tsud.Enabled = dataGridView1.SelectedRows.Count > 0;
            удалитьToolStripMenuItem.Enabled = изменитьToolStripMenuItem.Enabled = dataGridView1.SelectedRows.Count > 0;
        }
        private void ЗакрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Данная программа позволяет вести реестр горячих туров",
            "Информация",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
        }

        private void Tsdob_Click(object sender, EventArgs e)
        {
            var infform = new Form2
            {
                Text = "Добавление товара"
            };
            if (infform.ShowDialog() == DialogResult.OK)
            {
                logic.Add(infform.tour);
                bindingSource.ResetBindings(false);
                Strips();
            }
        }

        private void Tsizm_Click(object sender, EventArgs e)
        {
            var id = (Tour)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            var infform = new Form2(id)
            {
                Text = "Изменение товара"
            };
            if (infform.ShowDialog() == DialogResult.OK)
            {
                id.Direction = infform.tour.Direction;
                id.Wifi = infform.tour.Wifi;
                id.Sum = infform.tour.Sum;
                id.Qty = infform.tour.Qty;
                id.Dop = infform.tour.Dop;
                id.Nights = infform.tour.Nights;
                id.Date = infform.tour.Date;
                logic.Edit(infform.tour,
                    dataGridView1.SelectedRows[0].Index);
                bindingSource.ResetBindings(false);
                Strips();
            }
        }

        private void Tsud_Click(object sender, EventArgs e)
        {
            var id = (Tour)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            logic.Delete(id);
            bindingSource.ResetBindings(false);
            Strips();
        }

        private void ДобавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tsdob_Click(sender, e);
        }

        private void ИзменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tsizm_Click(sender, e);
        }

        private void УдалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tsud_Click(sender, e);
        }
        public void Strips()
        {
            qtytours.Text = logic.Tours.Count().ToString();
            sumtours.Text = logic.Tours.Sum(x => x.Sum).ToString();
            qtydops.Text = logic.Tours.Count(x => x.Dop != 0).ToString();
            sumdop.Text = logic.Tours.Sum(x => x.Dop).ToString();
        }
    }
}
