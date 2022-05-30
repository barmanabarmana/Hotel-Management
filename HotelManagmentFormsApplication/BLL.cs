using System;
using System.Collections.Generic;

namespace Hotel_Management
{
    public static class BLL
    {
        public static Hotel[] hotel;
        public static Customer[] customer;
        public static Booking[] booking;
        public class Hotel
        {
            //#hotel_ID|Description|Rooms
            public int hotelID { get; set; }
            public string hotelDescription { get; set; }
            public int hotelRooms { get; set; }
            public int cost { get; set; }
            public string hotelLocation { get; set; }
        }
        public class Customer
        {
            //customer_ID|Firstname|Lastname
            public int customerID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
        }
        public class Booking
        {
            //#hotel_ID|customer_ID|fromDate|toDate|room
            public int hotelID { get; set; }
            public int customerID { get; set; }
            public DateTime fromDate { get; set; }
            public DateTime toDate { get; set; }
            public int room { get; set; }
            public int reference { get; set; }
        }
        public static void addQuestRoom(string description, int rooms, string location, int cost)
        {
            DAL.addHotel(description, rooms, location, cost);
            fetchData(); //updates objects of hotels,customers, bookings etc
        }
        public static void addCustomer(string firstname, string lastname)
        {
            DAL.addCustomer(firstname, lastname);
            fetchData();
        }
        public static void addBooking(int hotelID, int userID, string from, string to, int room)
        {
            int bookingReference = DAL.getNewBookingReference();
            DAL.addBooking(hotelID, userID, from, to, room, bookingReference);
            fetchData();
        }

        public static void deleteHotel(int hotelID)
        {
            DAL.deleteHotel(hotelID);
            fetchData(); //updates objects of hotels,customers, bookings etc
        }
        public static void deleteCustomer(int customerID)
        {
            DAL.deleteCustomer(customerID);
            fetchData();
        }
        public static void deleteBooking(int bookingReference)
        {
            DAL.deleteBooking(bookingReference, "REF");
            fetchData();

        }
        public static void fetchData()
        {
            // Create hotel-objects //
            int numHotels = DAL.numberOfHotels();
            if (numHotels > 0)
            {
                hotel = new Hotel[numHotels];
                for (int i = 0; i < numHotels; i++) hotel[i] = new Hotel();
                List<string> hotelData;
                hotelData = DAL.fetchHotels();
                for (int i = 0; i < numHotels; i++)
                {
                    string[] rawHotelData = hotelData[i].Split('|');
                    hotel[i].hotelID = Convert.ToInt32(rawHotelData[0]);
                    hotel[i].hotelDescription = rawHotelData[1];
                    hotel[i].hotelRooms = Convert.ToInt32(rawHotelData[2]);
                    hotel[i].hotelLocation = rawHotelData[3];
                    hotel[i].cost = Convert.ToInt32(rawHotelData[4]);
                }
            }

            // Create Customer-objects //
            int numCustomers = DAL.numberOfCustomers();
            customer = new Customer[numCustomers];
            if (numCustomers > 0)
            {
                for (int i = 0; i < numCustomers; i++) customer[i] = new Customer();
                List<string> customerData;
                customerData = DAL.fetchCustomers();
                for (int i = 0; i < numCustomers; i++)
                {
                    string[] rawCustomerData = customerData[i].Split('|');
                    customer[i].customerID = Convert.ToInt32(rawCustomerData[0]);
                    customer[i].firstName = rawCustomerData[1];
                    customer[i].lastName = rawCustomerData[2];
                }
            }

            // Create Booking-objects //
            int numBookings = DAL.numberOfBookings();
            booking = new Booking[numBookings];
            if (numBookings > 0)
            {
                for (int i = 0; i < numBookings; i++) booking[i] = new Booking();
                List<string> bookingData;
                bookingData = DAL.fetchBookings();
                for (int i = 0; i < numBookings; i++)
                {
                    string[] rawbookingData = bookingData[i].Split('|');
                    booking[i].hotelID = Convert.ToInt32(rawbookingData[0]);
                    booking[i].customerID = Convert.ToInt32(rawbookingData[1]);
                    booking[i].fromDate = Convert.ToDateTime(rawbookingData[2]);
                    booking[i].toDate = Convert.ToDateTime(rawbookingData[3]);
                    booking[i].room = Convert.ToInt32(rawbookingData[4]);
                    booking[i].reference = Convert.ToInt32(rawbookingData[5]);
                }
            }
        }
        public static string fetchFirstName(int userID)
        {
            for (int i = 0; i < customer.Length; i++)
            {
                if (customer[i].customerID == userID)
                {
                    return customer[i].firstName;
                }
            }
            return "";
        }
        public static string fetchLastName(int userID)
        {
            for (int i = 0; i < customer.Length; i++)
            {
                if (customer[i].customerID == userID)
                {
                    return customer[i].lastName;
                }
            }
            return "";
        }

        public static bool checkAvailability(int hotelID, int room, DateTime fromDate, DateTime toDate)
        {
            int numBookings = booking.Length; //all bookings
            for (int i = 0; i < numBookings; i++)
            {
                if (booking[i].hotelID != hotelID) continue;
                if (booking[i].room != room) continue;
                DateTime compareFromDate = Convert.ToDateTime(booking[i].fromDate);
                DateTime compareToDate = Convert.ToDateTime(booking[i].toDate);
                compareFromDate = compareFromDate.AddDays(1.0);
                if (fromDate >= compareFromDate && fromDate <= compareToDate || toDate >= compareFromDate && toDate <= compareToDate || fromDate < compareFromDate && toDate > compareFromDate || fromDate < compareToDate && toDate > compareToDate) return false;

            }
            return true;
        }
        static public bool checkData()
        {
            bool result = DAL.checkData();
            return result;
        }

        public static bool updateCustomer(int customerID, string customerFirstName, string customerLastName)
        {
            return DAL.updateCustomer(customerID, customerFirstName, customerLastName);
        }
        public static int nightsBooked(DateTime startDate, DateTime stopDate, int hotelID)
        {
            //returns total nights booked for a given hotel within a given time frame
            int count = 0;
            if (booking == null || booking.Length == 0) return 0;
            for (int i = 0; i < booking.Length; i++)
            {
                if (booking[i].hotelID != hotelID) continue;
                DateTime bookingStart = booking[i].fromDate;
                DateTime bookingEnd = booking[i].toDate;
                TimeSpan ts = bookingEnd - bookingStart;
                count += ts.Days;
            }
            return count;
        }
        public static string searchCustomersByKeyword(string searchKey)
        {
            string searchResult = "";
            for (int i = 0; i < customer.Length; i++)
            {
                if (customer[i].firstName.Contains(searchKey) || customer[i].customerID.ToString().Contains(searchKey) || customer[i].lastName.Contains(searchKey))
                {
                    searchResult += "(" + customer[i].customerID.ToString() + ") " + customer[i].firstName + ", " + customer[i].lastName + "\n";
                }
            }
            if (searchResult == "") searchResult = "No Results";
            return searchResult;
        }
        public static string searchHotelsByKeyword(string searchKey)
        {
            string searchResult = "";
            for (int i = 0; i < hotel.Length; i++)
            {
                if (hotel[i].hotelID.ToString().Contains(searchKey) || hotel[i].cost.ToString().Contains(searchKey) || hotel[i].hotelDescription.Contains(searchKey) || hotel[i].hotelLocation.Contains(searchKey) || hotel[i].hotelRooms.ToString().Contains(searchKey))
                {
                    searchResult += "(" + hotel[i].hotelID.ToString() + ") " + hotel[i].hotelDescription + ", " + hotel[i].hotelLocation + ", " + hotel[i].hotelRooms.ToString() + " rooms, " + hotel[i].cost.ToString() + " EUR/night\n";
                }
            }
            if (searchResult == "") searchResult = "No Results";
            return searchResult;
        }
        public static int addPeople(int a, int b)
        {
            return a + b;
        }
    }
}
