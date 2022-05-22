using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management
{
    public partial class UI:Form {
        public static int activeHotelID = -1;
        public UI() {
            if(BLL.checkData()==false) MessageBox.Show("First time - Default data has been generated and written to datafile");
            InitializeComponent();
            initUI();
            //Cosmetic - disabled selection of column header when fullrowselect is enabled
            dgvBookings.EnableHeadersVisualStyles=false;
            dgvBookings.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvBookings.ColumnHeadersDefaultCellStyle.BackColor;
            dgvCustomers.EnableHeadersVisualStyles=false;
            dgvCustomers.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor;
        }

        public void initUI() {
            DateTime filterStart = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
            DateTime filterStop = filterStart;
            filterStop=filterStop.AddMonths(1);
            filterStop=filterStop.AddDays(-1);//set end filter to last day of current month. Can also use .DaysInMonth
            dtpFilterFrom.Value=filterStart;
            dtpFilterTo.Value=filterStop;
            BLL.fetchData();
            if(BLL.hotel!=null) {
                //Populate the Hotel-combobox
                cbHotels.SelectedIndexChanged-=cbHotels_SelectedIndexChanged; //unsubscribe from the change event
                cbHotels.Items.Clear();
                for(int i = 0;i<BLL.hotel.Length;i++) {
                    cbHotels.Items.Add(BLL.hotel[i].hotelDescription);
                }
                if(activeHotelID==-1) { //fresh program
                    cbHotels.SelectedIndex=0;
                    activeHotelID=BLL.hotel[0].hotelID;
                }
                cbHotels.SelectedIndexChanged+=cbHotels_SelectedIndexChanged; //resubscribe to the event
                lblHotelRooms.Text=BLL.hotel[cbHotels.SelectedIndex].hotelRooms.ToString();
                lblHotelLocation.Text=BLL.hotel[cbHotels.SelectedIndex].hotelLocation;
                lblHotelCost.Text=BLL.hotel[cbHotels.SelectedIndex].cost.ToString();
                updateBookingCombobox();
                dgvBookings.Rows.Clear();
                addFilteredBookings(activeHotelID);
                updateHotelStatistics();
            }
            if(BLL.customer!=null) {
                updateCustomers();
                if(BLL.customer.Length>0) {
                    tbSelectedCustomerFirstname.Text=dgvCustomers.Rows[0].Cells[0].Value.ToString();
                    tbSelectedCustomerLastname.Text=dgvCustomers.Rows[0].Cells[1].Value.ToString();
                    tbNewBookingCustomer.Text=dgvCustomers.Rows[0].Cells[1].Value.ToString()+", "+dgvCustomers.Rows[0].Cells[0].Value.ToString();
                }
            }
            
        }
        //private void updateHotelStatistics(int hotelID, DateTime dtFrom, DateTime dtTo) {
        private void updateHotelStatistics() {
            int cost = BLL.hotel[cbHotels.SelectedIndex].cost;
            int ID = BLL.hotel[cbHotels.SelectedIndex].hotelID;
            //Taking advantage of datagridview with bookings is already (always) showing current filters etc
            DateTime dtStart, dtStop;
            TimeSpan ts;
            int countDays=0;
            lblNightsCost.Text="";
            for(int i = 0;i<dgvBookings.Rows.Count;i++) {
                dtStart=Convert.ToDateTime(dgvBookings.Rows[i].Cells[1].Value);
                dtStop=Convert.ToDateTime(dgvBookings.Rows[i].Cells[2].Value);
                ts=dtStop-dtStart;
                countDays+=ts.Days;
            }
            lblNightsBooked.Text=countDays.ToString();
            lblNightsCost.Text=(countDays*cost).ToString();
            //Check available rooms in the time period
            bool[] availableRooms = new bool[BLL.hotel[cbHotels.SelectedIndex].hotelRooms];
            for(int i = 0;i<availableRooms.Length;i++) availableRooms[i]=false; //set all rooms unavailable
            string tooltipString = "";
            for (int i=0;i<availableRooms.Length;i++) {
                availableRooms[i]=BLL.checkAvailability(ID,(i+1),dtpFilterFrom.Value,dtpFilterTo.Value);
                if (availableRooms[i]) tooltipString+="Room #" + (i+1).ToString()+"\n";
            }
            toolTip1.SetToolTip(lblAvailableRooms,tooltipString);
            int count = 0;
            for (int i=0;i<availableRooms.Length;i++) {
                if(availableRooms[i]) count++;
            }
            lblAvailableRooms.Text=count.ToString();
        }
        public void updateCustomers() {
            //Populate the Customer-dgv
            dgvCustomers.SelectionChanged-=dgvCustomers_SelectionChanged;
            dgvCustomers.Rows.Clear();
            for(int i = 0;i<BLL.customer.Length;i++) {
                dgvCustomers.Rows.Add();
                dgvCustomers.Rows[i].Cells[0].Value=BLL.customer[i].firstName;
                dgvCustomers.Rows[i].Cells[1].Value=BLL.customer[i].lastName;
                dgvCustomers.Rows[i].Cells[2].Value=BLL.customer[i].customerID;
            }
            dgvCustomers.ScrollBars=ScrollBars.Vertical;
            dgvCustomers.SelectionChanged+=dgvCustomers_SelectionChanged;
        }
        public void updateBookingCombobox() {
            cbNewBookingRoom.Items.Clear();
            for(int i = 0;i<BLL.hotel[cbHotels.SelectedIndex].hotelRooms;i++) cbNewBookingRoom.Items.Add((i+1).ToString());
        }

        public void addFilteredBookings(int hotelID) {
            dgvBookings.Rows.Clear();
            int activeRow = 0;
            DateTime dtFilterFrom = dtpFilterFrom.Value;
            DateTime dtFilterTo = dtpFilterTo.Value;
            if(BLL.booking==null) return;
            for(int i = 0;i<BLL.booking.Length;i++) {
                if(BLL.booking[i].hotelID==activeHotelID) {
                    
                    DateTime dtFrom = Convert.ToDateTime(BLL.booking[i].fromDate.ToString("dd-MM-yyyy"));
                    DateTime dtTo = Convert.ToDateTime(BLL.booking[i].toDate.ToString("dd-MM-yyyy"));
                    if(dtFilterFrom<=dtFrom&&dtFilterTo>=dtTo) {
                        dgvBookings.Rows.Add();
                        dgvBookings.Rows[activeRow].Cells[0].Value=BLL.booking[i].room;
                        dgvBookings.Rows[activeRow].Cells[1].Value=BLL.booking[i].fromDate.ToString("dd-MM-yyyy");
                        dgvBookings.Rows[activeRow].Cells[2].Value=BLL.booking[i].toDate.ToString("dd-MM-yyyy");
                        dgvBookings.Rows[activeRow].Cells[3].Value=BLL.fetchFirstName(BLL.booking[i].customerID);
                        dgvBookings.Rows[activeRow].Cells[4].Value=BLL.fetchLastName(BLL.booking[i].customerID);
                        dgvBookings.Rows[activeRow].Cells[5].Value=BLL.booking[i].reference;
                        activeRow++;
                    }
                }
            }
            updateHotelStatistics(); 
        }

        private void cbHotels_SelectedIndexChanged(object sender,EventArgs e) {
            activeHotelID=BLL.hotel[cbHotels.SelectedIndex].hotelID;
            lblHotelRooms.Text=BLL.hotel[cbHotels.SelectedIndex].hotelRooms.ToString();
            lblHotelLocation.Text=BLL.hotel[cbHotels.SelectedIndex].hotelLocation;
            lblHotelCost.Text=BLL.hotel[cbHotels.SelectedIndex].cost.ToString();

            addFilteredBookings(activeHotelID);
            updateBookingCombobox();
        }
        private void btnAddHotel_Click_1(object sender,EventArgs e) {
            int i;
            if(tbNewDescription.Text==""||tbNewRooms.Text==""||!int.TryParse(tbNewRooms.Text,out i)||tbNewLocation.Text==""||!int.TryParse(tbNewCost.Text, out i)) return; //checking input data
            BLL.addhotel(tbNewDescription.Text,Convert.ToInt32(tbNewRooms.Text), tbNewLocation.Text, Convert.ToInt32(tbNewCost.Text));
            cbHotels.Items.Add(BLL.hotel[BLL.hotel.Length-1].hotelDescription);
            tbNewDescription.Text="";
            tbNewRooms.Text="";
            tbNewCost.Text="";
            tbNewLocation.Text="";
        }

        private void btnDeleteHotel_Click(object sender,EventArgs e) {
            if(cbHotels.Items.Count==0) return;
            //if(BLL.hotel.Length==1) return; 
            BLL.deleteHotel(activeHotelID);
            int oldIndex = cbHotels.SelectedIndex;
            activeHotelID=BLL.hotel[0].hotelID;
            cbHotels.Items.RemoveAt(oldIndex);
            cbHotels.SelectedIndex=oldIndex-1;
            addFilteredBookings(activeHotelID);
        }

        private void dgvCustomers_SelectionChanged(object sender,EventArgs e) {
            int selectedRow = dgvCustomers.CurrentRow.Index;
            tbSelectedCustomerFirstname.Text=dgvCustomers.Rows[selectedRow].Cells[0].Value.ToString();
            tbSelectedCustomerLastname.Text=dgvCustomers.Rows[selectedRow].Cells[1].Value.ToString();
            tbNewBookingCustomer.Text=dgvCustomers.Rows[selectedRow].Cells[1].Value.ToString()+", "+dgvCustomers.Rows[selectedRow].Cells[0].Value.ToString();
        }

        private void btnAddCustomer_Click(object sender,EventArgs e) {
            if(tbNewCustomerFirstname.Text==""||tbNewCustomerLastname.Text=="") return;
            BLL.addCustomer(tbNewCustomerFirstname.Text,tbNewCustomerLastname.Text);
            tbNewCustomerFirstname.Text="";
            tbNewCustomerLastname.Text="";
            updateCustomers();
            dgvCustomers.ClearSelection();
            tbSelectedCustomerFirstname.Text="";
            tbSelectedCustomerLastname.Text="";
        }

        private void btnDeleteUser_Click(object sender,EventArgs e) {
            int selectedRow = dgvCustomers.CurrentRow.Index;
            int customerID = Convert.ToInt32(dgvCustomers.Rows[selectedRow].Cells[2].Value);
            BLL.deleteCustomer(customerID);
            updateCustomers();
            dgvCustomers.ClearSelection();
            tbSelectedCustomerFirstname.Text="";
            tbSelectedCustomerLastname.Text="";
            addFilteredBookings(activeHotelID);
        }

        private void btnDeleteBooking_Click(object sender,EventArgs e) {
            if(dgvBookings.Rows.Count==0) return;
            int selectedRow = dgvBookings.CurrentRow.Index;
            int bref = Convert.ToInt32(dgvBookings.Rows[selectedRow].Cells[5].Value);
            BLL.deleteBooking(bref);
            addFilteredBookings(activeHotelID);
        }

        private void btnApplyDateFilter_Click(object sender,EventArgs e) {
            if(dtpFilterFrom.Value>dtpFilterTo.Value) return;
            addFilteredBookings(activeHotelID);
        }

        private void btnAddBooking_Click(object sender,EventArgs e) {
            if(dtpFrom.Value>=dtpTo.Value||cbNewBookingRoom.Text==""||tbNewBookingCustomer.Text=="") {
                MessageBox.Show("Incorrect input data");
                return;
            }
            bool isAvailable = BLL.checkAvailability(activeHotelID,Convert.ToInt32(cbNewBookingRoom.Text),dtpFrom.Value,dtpTo.Value);
            if (!isAvailable) {
                MessageBox.Show("The selected room is not available at the requested dates");
                return;
            }
            int customerID = Convert.ToInt32(dgvCustomers.Rows[dgvCustomers.CurrentRow.Index].Cells[2].Value);
            BLL.addBooking(activeHotelID,customerID,dtpFrom.Value.ToString("dd-MM-yyyy"),dtpTo.Value.ToString("dd-MM-yyyy"),Convert.ToInt32(cbNewBookingRoom.Text));
            addFilteredBookings(activeHotelID);
        }

        private void btnEditCustomer_Click(object sender,EventArgs e) {
            if (tbSelectedCustomerFirstname.Text==""||tbSelectedCustomerLastname.Text=="") {
                MessageBox.Show("Check input data");
                return;
            }
            int selectedRow = dgvCustomers.CurrentRow.Index;
            string customerFirstName = tbSelectedCustomerFirstname.Text; //Firstname
            string customerLastName = tbSelectedCustomerLastname.Text; //Lastname
            int customerID = Convert.ToInt32(dgvCustomers.Rows[selectedRow].Cells[2].Value); //ID
            if (BLL.updateCustomer(customerID,customerFirstName,customerLastName)==true) {
                BLL.fetchData();
                updateCustomers();
                tbSelectedCustomerFirstname.Text="";
                tbSelectedCustomerLastname.Text="";
            }
        }

        private void btnSearchCustomers_Click(object sender,EventArgs e) {
            if(tbSearchCustomers.Text=="") return;
            string results = BLL.searchCustomersByKeyword(tbSearchCustomers.Text);
            MessageBox.Show(results,"Search Results");
        }

        private void btnSearchHotels_Click(object sender,EventArgs e) {
            if(tbSearchHotels.Text=="") return;
            string results = BLL.searchHotelsByKeyword(tbSearchHotels.Text);
            MessageBox.Show(results,"Search Results");
        }
    }
}
