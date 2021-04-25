using Infrastructure.Services;
using Infrastructure.Services.ItemMasterService;
using Model.DTO;
using POSkabaPOSako.MainForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSkabaPOSako
{
    public partial class MainForm :BaseController
    {
        ItemMasterService _itemMasterService = new ItemMasterService();
        CategoryService _categoryService = new CategoryService();
        ParkService _parkService = new ParkService();
        public MainForm()
        {
            InitializeComponent();
        }

        BindingList<ItemMasterListboxMainPOS> items = new BindingList<ItemMasterListboxMainPOS>();
        int orderCount = 0;
        #region private methods
        private void MainForm_Load(object sender, EventArgs e)
        {
            DatetimeLabel.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            CurrentUsername.Text = AppuserData.FullName;
            InitTable();
            PopulateCategoryButtons();
            PopulateParkedTable();
            // ItemListbox.Items.Add(String.Format(headers, "Description", "Quantity"));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DatetimeLabel.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
        }
        private void BarcodeTextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (BarcodeTextbox.Text.Length == 13)
                    AddItems(true);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }
        private void PopulateCategoryButtons()
        {
            var categories = _categoryService.GetAllCategoriesButton();
            var counter = 0;
            foreach(var category in categories)
            {
                counter++;
                Button categoryButton = new Button();
                categoryButton.Name = category.Id.ToString();
                categoryButton.Text = category.LongDescription;
                categoryButton.Location = Point.Add(new Point(4 + counter * 307, 4), new Size(100, 100));
                categoryButton.Width = 100;
                categoryButton.Height = 75;
                categoryButton.Click += CategoryClick;
                categoryButton.FlatStyle = FlatStyle.Flat;
                categoryButton.BackColor = Color.Gold;
                categoryButton.ForeColor = Color.White;
                categoryButton.Cursor = Cursors.Hand;
                categoryButton.Font = new Font("Arial", 14, FontStyle.Bold);
                categoryButton.FlatAppearance.BorderSize = 0;
                flowLayoutPanel1.Controls.Add(categoryButton);
            }
        }
        private void AddItems(bool fromBarcode, int id=0)
        {
            var itemMaster = fromBarcode ? _itemMasterService.GetItemMasterByBarcode(BarcodeTextbox.Text) : 
                _itemMasterService.GetItemMasterById(id); 
            if (itemMaster != null)
            {
                orderCount++;
                var item = items.FirstOrDefault(x => x.Id == itemMaster.Id);
                if (item == null)
                {
                    items.Add(new ItemMasterListboxMainPOS
                    {
                        Id = itemMaster.Id,
                        LongDescription = itemMaster.LongDescription,
                        Quantity = 1,
                        Index = orderCount
                    });
                }
                else
                {
                    item.Quantity++;
                    item.Index = orderCount;
                }
                items.OrderByDescending(x => x.Index);
                items.Add(new ItemMasterListboxMainPOS
                {
                    Id = 0,
                    LongDescription = "",
                    Quantity = 1,
                    Index = 0
                });
                items.RemoveAt(items.Count - 1);
            }
            else
                MessageBox.Show("No item found.");
            if(fromBarcode)
                BarcodeTextbox.Clear();

        }
        public void ParkItems(string tableNo, ParkTableInput pti)
        {
            RunMethod(()=> {
                if (items.Count != 0)
                {
                    _parkService.ParkItems(items, AppuserData.Id, Convert.ToInt32(tableNo));
                    items.Clear();
                    orderCount = 0;
                    PopulateParkedTable();
                    ShowPopup(new PopupBox(false, "Sucessfully parked items."));
                }
                pti.Close();
            },"Failed to park item");
        }
        private void PopulateParkedTable()
        {
            var parks = _parkService.GetAllUnProcessedPark();
            var counter = 0;
            flowLayoutPanel2.Controls.Clear();
            foreach (var park in parks)
            {
                counter++;
                Button parkbutton = new Button();
                parkbutton.Name = park.Id.ToString();
                parkbutton.Text = "Table No: "+park.TableNo;
                parkbutton.Location = Point.Add(new Point(4 + counter * 307, 4), new Size(100, 100));
                parkbutton.Width = 100;
                parkbutton.Height = 75;
                parkbutton.Click += ParkClick;
                parkbutton.FlatStyle = FlatStyle.Flat;
                parkbutton.BackColor = Color.Violet;
                parkbutton.ForeColor = Color.White;
                parkbutton.Cursor = Cursors.Hand;
                parkbutton.Font = new Font("Arial", 14, FontStyle.Bold);
                parkbutton.FlatAppearance.BorderSize = 0;
                flowLayoutPanel2.Controls.Add(parkbutton);
            }
        }

        #region handlers
        private void CategoryClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var items = _itemMasterService.GetAllItemMasterButtons(int.Parse(button.Name));
            flowLayoutPanel1.Controls.Clear();
            var counter = 0;
            foreach(var item in items)
            {
                counter++;
                Button itemButton = new Button();
                itemButton.Name = item.Id.ToString();
                itemButton.Text = item.LongDescription;
                itemButton.Location = System.Drawing.Point.Add(new Point(4 + counter * 307, 4), new Size(100, 100));
                itemButton.Width = 120;
                itemButton.Height = 75;
                itemButton.FlatStyle = FlatStyle.Flat;
                itemButton.BackColor = Color.Green;
                itemButton.ForeColor = Color.White;
                itemButton.Cursor = Cursors.Hand;
                itemButton.Font = new Font("Arial", 10, FontStyle.Bold);
                itemButton.FlatAppearance.BorderSize = 0;
                itemButton.Click += ItemClick;
                flowLayoutPanel1.Controls.Add(itemButton);
            }
        }
        private void ItemClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            AddItems(false, int.Parse(button.Name));
        }
        private void ParkClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            items.Clear();
            items = _parkService.RetrievePark(int.Parse(button.Name));
            InitTable();
        }
        #endregion

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //  ParkItems();
            ShowDialogForm(new ParkTableInput(this));
        }
        private void InitTable()
        {
            ItemListbox.DataSource = items;
            ItemListbox.DisplayMember = "LongDescription";
            QuantityListbox.DataSource = items;
            QuantityListbox.DisplayMember = "Quantity";
        }
    }
}
