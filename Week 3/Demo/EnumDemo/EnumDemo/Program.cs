using System;

namespace EnumDemo
{
    
    public enum OrderStatus
    {
        Pending,    
        Processing, 
        Shipped,    
        Delivered,  
        Cancelled  
    }
    public enum ErrorCode
    {
        None = 0,
        NotFound = 404,
        Unauthorized = 401,
        InternalServerError = 500
    }
    public enum ByteEnum : byte
    {
        A = 1,
        B = 2,
        C = 3
    }

    [Flags]
    public enum FileAccess
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4,
        ReadWrite = Read | Write
    }

    public class Vehicle
    {
        public enum VehicleType
        {
            Car,
            Bike,
            Truck
        }
    }

    class Program
    {
        static void Main()
        {
          

     
            OrderStatus status = OrderStatus.Pending;
            Console.WriteLine($"Order status: {status} ({(int)status})");

            status = OrderStatus.Delivered;
            Console.WriteLine($"Updated Order status: {status}");


            ErrorCode code = ErrorCode.NotFound;
            Console.WriteLine($"Error: {code} ({(int)code})");

            
            ByteEnum be = ByteEnum.C;
            Console.WriteLine($"ByteEnum: {be}, Underlying Type: {Enum.GetUnderlyingType(typeof(ByteEnum))}");

         
            foreach (string name in Enum.GetNames(typeof(OrderStatus)))
                Console.WriteLine(name);

            Console.WriteLine("\nValues:");
            foreach (OrderStatus os in Enum.GetValues(typeof(OrderStatus)))
                Console.WriteLine($"{os} = {(int)os}");

   

            FileAccess fa = FileAccess.Read | FileAccess.Write;
            Console.WriteLine($"File Access: {fa}");

          
  
            switch (status)
            {
                case OrderStatus.Pending:
                    Console.WriteLine("Your order is pending.");
                    break;
                case OrderStatus.Delivered:
                    Console.WriteLine("Your order has been delivered.");
                    break;
                default:
                    Console.WriteLine("Other status.");
                    break;
            }

           
            Vehicle.VehicleType vt = Vehicle.VehicleType.Car;
            Console.WriteLine($"Vehicle Type: {vt}");
            enumWithDuplicatesDemo();
        }

        // Enum with Duplicate Values
        enum Color
        {
            Red = 1,
            Crimson = 1, // duplicate
            Blue = 2
        }

        static void enumWithDuplicatesDemo()
        {
            foreach (string name in Enum.GetNames(typeof(Color)))
                Console.WriteLine(name);

            Console.WriteLine($"Red == Crimson? {Color.Red == Color.Crimson}");
        }
    }
}
