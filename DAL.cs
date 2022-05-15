using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hotel_Management.DataAccessLayer {
    public static class DAL {
        public static int numberOfHotels() {
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            while((s=sr.ReadLine())!=null) {
                if(s.Contains("[Hotels]=")) {
                    sr.Close();
                    return Convert.ToInt32(s.Substring(9));
                }
            }
            sr.Close();
            return 0;
        }

        public static int numberOfCustomers() {
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            while((s=sr.ReadLine())!=null) {
                if(s.Contains("[Customers]=")) {
                    sr.Close();
                    return Convert.ToInt32(s.Substring(12));
                }
            }
            sr.Close();
            return 0;
        }

        public static int numberOfBookings() {
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            while((s=sr.ReadLine())!=null) {
                if(s.Contains("[Bookings]=")) {
                    sr.Close();
                    return Convert.ToInt32(s.Substring(11));
                }
            }
            sr.Close();
            return 0;
        }

        public static void addHotel(string description,int rooms,string location,int cost) {
            List<String> oldData = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            int insertPosition = 0;
            int numHotels = numberOfHotels();
            string s;
            while((s=sr.ReadLine())!=null) {
                oldData.Add(s);
                if(s.Contains("[Hotels]=")) {
                    insertPosition=oldData.Count+numHotels;
                }
            }
            sr.Close();
            string newID = getNewHotelID().ToString();
            string newHotelString = newID+"|"+description+"|"+rooms.ToString()+"|"+location+"|"+cost.ToString();
            List<String> newData = oldData;
            newData.Insert(insertPosition,newHotelString);
            StreamWriter sw = new StreamWriter("Data.dat");
            foreach(string str in newData) {
                if(str=="[Hotels]="+numHotels.ToString()) {
                    sw.WriteLine("[Hotels]="+(numHotels+1).ToString());
                } else {
                    sw.WriteLine(str);
                }
            }
            sw.Close();
        }

        public static void addCustomer(string firstname,string lastname) {
            List<String> oldData = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            int insertPosition = 0;
            int numCustomers = numberOfCustomers();
            string s;
            while((s=sr.ReadLine())!=null) {
                oldData.Add(s);
                if(s.Contains("[Customers]=")) {
                    insertPosition=oldData.Count+numCustomers;
                }
            }
            sr.Close();
            string newID = getNewCustomerID().ToString();
            string newCustomerString = newID+"|"+firstname+"|"+lastname;
            List<String> newData = oldData;
            newData.Insert(insertPosition,newCustomerString);
            StreamWriter sw = new StreamWriter("Data.dat");
            foreach(string str in newData) {
                if(str=="[Customers]="+numCustomers.ToString()) {
                    sw.WriteLine("[Customers]="+(numCustomers+1).ToString());
                } else {
                    sw.WriteLine(str);
                }
            }
            sw.Close();
        }

        public static void addBooking(int hotelID,int customerID,string fromDate,string toDate,int room,int bookingReference) {
            List<String> oldData = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            int insertPosition = 0;
            int numBookings = numberOfBookings();
            string s;
            while((s=sr.ReadLine())!=null) {
                oldData.Add(s);
                if(s.Contains("[Bookings]")) {
                    insertPosition=oldData.Count+numBookings;
                }
            }
            sr.Close();
            string newBookingString = hotelID.ToString()+"|"+customerID.ToString()+"|"+fromDate+"|"+toDate+"|"+room.ToString()+"|"+bookingReference.ToString();
            List<String> newData = oldData;
            newData.Insert(insertPosition,newBookingString);
            StreamWriter sw = new StreamWriter("Data.dat");
            foreach(string str in newData) {
                if(str=="[Bookings]="+numBookings.ToString()) {
                    sw.WriteLine("[Bookings]="+(numBookings+1).ToString());
                } else {
                    sw.WriteLine(str);
                }
            }
            sw.Close();
        }

        public static int getNewHotelID() {
            int numHotels = numberOfHotels();
            int[] hotelID = new int[numHotels];
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            while((s=sr.ReadLine())!=null) {
                if(s.Contains("[Hotels]=")) {
                    string str;
                    for(int i = 0;i<numHotels;i++) {
                        str=sr.ReadLine();
                        string[] splitstring = str.Split('|');
                        hotelID[i]=Convert.ToInt32(splitstring[0]);
                    }
                }
            }
            sr.Close();
            int maxID = 0;
            for(int i = 0;i<hotelID.Length;i++) {
                if(hotelID[i]>maxID) maxID=hotelID[i];
            }
            return maxID+1;
        }
        private static int getNewCustomerID() {
            int numCustomers = numberOfCustomers();
            int[] customerID = new int[numCustomers];
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            while((s=sr.ReadLine())!=null) {
                if(s.Contains("[Customers]=")) {
                    string str;
                    for(int i = 0;i<numCustomers;i++) {
                        str=sr.ReadLine();
                        string[] splitstring = str.Split('|');
                        customerID[i]=Convert.ToInt32(splitstring[0]);
                    }
                }
            }
            sr.Close();
            int maxID = 0;
            for(int i = 0;i<customerID.Length;i++) {
                if(customerID[i]>maxID) maxID=customerID[i];
            }
            return maxID+1;
        }
        public static int getNewBookingReference() {
            int numBookings = numberOfBookings();
            int[] bookingReference = new int[numBookings];
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            while((s=sr.ReadLine())!=null) {
                if(s.Contains("[Bookings]")) {
                    string str;
                    for(int i = 0;i<numBookings;i++) {
                        str=sr.ReadLine();
                        string[] splitstring = str.Split('|');
                        bookingReference[i]=Convert.ToInt32(splitstring[5]);
                    }
                }
            }
            sr.Close();
            int maxRef = 0;
            for(int i = 0;i<bookingReference.Length;i++) {
                if(bookingReference[i]>maxRef) maxRef=bookingReference[i];
            }
            return maxRef+1;
        }
        public static List<string> fetchHotels() {
            List<string> hotelList = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            int numHotels = numberOfHotels();
            string s;
            while((s=sr.ReadLine())!=null) {
                if(s.Contains("[Hotels]")) {
                    for(int i = 0;i<numHotels;i++) {
                        hotelList.Add(sr.ReadLine());
                    }
                    break;
                }
            }
            sr.Close();
            return hotelList;
        }

        public static List<string> fetchCustomers() {
            List<string> customerList = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            int numCustomers = numberOfCustomers();
            string s;
            while((s=sr.ReadLine())!=null) {
                if(s.Contains("[Customers]")) {
                    for(int i = 0;i<numCustomers;i++) {
                        customerList.Add(sr.ReadLine());
                    }
                    break;
                }
            }
            sr.Close();
            return customerList;
        }

        public static List<string> fetchBookings() {
            List<string> bookingList = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            int numBookings = numberOfBookings();
            string s;
            while((s=sr.ReadLine())!=null) {
                if(s.Contains("[Bookings]")) {
                    for(int i = 0;i<numBookings;i++) {
                        bookingList.Add(sr.ReadLine());
                    }
                    break;
                }
            }
            sr.Close();
            return bookingList;
        }

        public static bool deleteCustomer(int customerID) {
            List<string> customerList = fetchCustomers();
            string removeString = null;
            bool foundCustomer = false;
            int numCustomers = numberOfCustomers();
            for(int i = 0;i<numCustomers;i++) {
                string[] splitString = customerList[i].Split('|');
                if(splitString[0]==customerID.ToString()) {
                    removeString=customerList[i];
                    foundCustomer=true;
                }
            }
            if(!foundCustomer) return false;
            List<string> oldData = new List<string>();
            List<string> newData = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            int deletePos = -1;
            int line = 0;
            int headerPos = 0;
            while((s=sr.ReadLine())!=null) {
                oldData.Add(s);
                if(s.Contains("[Customers]")) headerPos=line;
                if(s==removeString) deletePos=line;
                line++;
            }
            sr.Close();
            newData=oldData;
            newData[headerPos]="[Customers]="+(numCustomers-1).ToString();
            newData.RemoveAt(deletePos);
            StreamWriter sw = new StreamWriter("Data.dat");
            foreach(string str in newData) {
                sw.WriteLine(str);
            }
            sw.Close();
            while(deleteBooking(customerID,"CUSTOMER")==true) deleteBooking(customerID,"CUSTOMER"); //remove associated bookings
            return true;
        }
        public static bool deleteHotel(int hotelID) {
            List<string> hotelList = fetchHotels();
            string removeString = null;
            bool foundHotel = false;
            int numHotels = numberOfHotels();
            for(int i = 0;i<numHotels;i++) {
                string[] splitString = hotelList[i].Split('|');
                if(splitString[0]==hotelID.ToString()) {
                    removeString=hotelList[i];
                    foundHotel=true;
                }
            }
            if(!foundHotel) return false;
            List<string> oldData = new List<string>();
            List<string> newData = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            int deletePos = -1;
            int line = 0;
            int headerPos = -1;
            while((s=sr.ReadLine())!=null) {
                oldData.Add(s);
                if(s==removeString) deletePos=line;
                if(s.Contains("[Hotels]")) headerPos=line;
                line++;
            }
            sr.Close();
            newData=oldData;
            newData[headerPos]="[Hotels]="+(numHotels-1).ToString();
            newData.RemoveAt(deletePos);
            StreamWriter sw = new StreamWriter("Data.dat");
            foreach(string str in newData) {
                sw.WriteLine(str);
            }
            sw.Close();
            while(deleteBooking(hotelID,"HOTEL")==true) deleteBooking(hotelID,"HOTEL"); //remove associated bookings
            return true;
        }
        public static bool deleteBooking(int deleteID,string deleteType) {
            int splitPos = 0;
            if(deleteType=="CUSTOMER") splitPos=1;
            if(deleteType=="HOTEL") splitPos=0;
            if(deleteType=="REF") splitPos=5;
            List<string> bookingList = fetchBookings();
            string removeString = null;
            bool foundBooking = false;
            int numBookings = numberOfBookings();
            for(int i = 0;i<numBookings;i++) {
                string[] splitString = bookingList[i].Split('|');
                if(splitString[splitPos]==deleteID.ToString()) {
                    removeString=bookingList[i];
                    foundBooking=true;
                }
            }
            if(!foundBooking) return false;
            List<string> oldData = new List<string>();
            List<string> newData = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            int deletePos = -1;
            int line = 0;
            int headerPos = -1;
            while((s=sr.ReadLine())!=null) {
                oldData.Add(s);
                if(s.Contains("[Bookings]")) headerPos=line;
                if(s==removeString) deletePos=line;
                line++;
            }
            sr.Close();
            newData=oldData;
            newData[headerPos]="[Bookings]="+(numBookings-1).ToString();
            newData.RemoveAt(deletePos);
            StreamWriter sw = new StreamWriter("Data.dat");
            foreach(string str in newData) {
                sw.WriteLine(str);
            }
            sw.Close();
            return true;
        }
        public static bool checkData() {
            if(!File.Exists("Data.dat")) {
                //generate a Data.dat file with seed data, for first-time use/demonstration
                StreamWriter sw = new StreamWriter("Data.dat");
                String s = "[Hotels]=2\n"+
                    "1|Grand Hotel|10|New York|150\n"+
                    "2|Ritz Hotel|5|California|175\n"+
                    "\n"+
                    "[Customers]=10\n"+
                    "2|George|Clooney\n"+
                    "3|Bruce|Willis\n"+
                    "4|Quentin|Tarantino\n"+
                    "5|Ronald|Mcdonald\n"+
                    "6|Arnold|Schwarzenegger\n"+
                    "7|Bill|Clinton\n"+
                    "8|George|Bush Jr.\n"+
                    "9|Donald|Trump\n"+
                    "10|Barak|Obama\n"+
                    "12|George|Bush|Sr.\n"+
                    "\n"+
                    "[Bookings]=7\n"+
                    "1|2|01-12-2021|10-12-2021|1|1\n"+
                    "1|3|10-12-2021|17-12-2021|1|2\n"+
                    "1|4|17-12-2021|24-12-2021|1|3\n"+
                    "1|5|03-12-2021|24-12-2021|2|4\n"+
                    "1|6|03-12-2021|24-12-2021|3|5\n"+
                    "2|7|03-12-2021|24-12-2021|1|6\n"+
                    "2|9|03-12-2021|24-12-2021|2|7\n";
                sw.WriteLine(s);
                sw.Close();
                return false;
            }
            return true;
        }

        public static bool updateCustomer(int customerID, string customerFirstName, string customerLastName) {
            int numCustomers = numberOfCustomers();
            List<string> oldData = new List<string>();
            StreamReader sr = new StreamReader("Data.dat");
            string s;
            while((s=sr.ReadLine())!=null) {
                oldData.Add(s);
            }
            sr.Close();
            int insertPos = 0;
            string[] splitString;
            bool foundCustomer = false;
            for (int i=0;i<oldData.Count;i++) {
                if (oldData[i].Contains("[Customers]")) {
                    for (int n=0;n<numCustomers+1;n++) {
                        splitString = oldData[i+n].Split('|');
                        if (splitString[0]==customerID.ToString()) {
                            insertPos=(i+n);
                            foundCustomer=true;
                        }
                        if(foundCustomer) break;
                    }
                    if(foundCustomer) break;
                }
            }
            if(!foundCustomer) return false;
            List<string> newData = new List<string>();
            foreach (string str in oldData) {
                if((newData.Count)==insertPos) {
                    newData.Add(customerID.ToString()+"|"+customerFirstName+"|"+customerLastName);
                } else {
                    newData.Add(str);
                }
            }
            StreamWriter sw = new StreamWriter("Data.dat");
            foreach (string str in newData) {
                sw.WriteLine(str);
            }
            sw.Close();
            return foundCustomer;
        }
    } // end class DAL
} //namespace
