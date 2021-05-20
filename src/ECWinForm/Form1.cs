using System.Windows.Forms;
using Akka.Actor;
using ECLab.Model.Messages;
using ECWinForm.ViewModels;

namespace ECWinForm
{
    public partial class Form1 : Form
    {
        private readonly Form1ViewModel viewModel;

        public Form1()
        {
            this.InitializeComponent();

            this.viewModel = new Form1ViewModel();

            this.textBox1.DataBindings.Add("Text", this.viewModel, nameof(this.viewModel.Products));
            this.textBox2.DataBindings.Add("Text", this.viewModel, nameof(this.viewModel.Order));
            this.textBox3.DataBindings.Add("Text", this.viewModel, nameof(this.viewModel.Delivery));

            this.button1.Click += (sender, args) => { this.viewModel.SearchProducts(); };
            this.button2.Click += (sender, args) => { this.viewModel.PlaceAnOrder(); };
            this.button3.Click += (sender, args) => { this.viewModel.PayForOrder(); };
        }
    }
}