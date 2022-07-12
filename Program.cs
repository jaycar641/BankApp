using System;
using System.Threading;


    Banking bank = new Banking ();
    bank.start();

public class Banking
{
    public int currentBalance = 200000;
    public int currentUserId;
    public Profile [] accountPeople;
    
    public Banking() //current user id, sets up default settings
    {
      
        Profile[] accountList = new Profile[10];
        accountPeople = accountList;
        Console.WriteLine("Checking current user..." + currentUserId);
        Console.WriteLine("Checking current balance..." + currentBalance);
        Console.WriteLine("Adjusting the balance..");  
    }

    /////START
   public void start() //start the banking applicatiom
    {
        Console.WriteLine("Welcome to the Cool bank. What is your bank account number? Enter R to register.");
        string accountResponse = Console.ReadLine();
      
           if(accountResponse == "R" || accountResponse == "r") 
             { 
                int registerAccountNumber = Int32.Parse(register(accountResponse));      
                setAccount(registerAccountNumber);//sets current user id
             }
        else { 
                Console.WriteLine("Loading account...");    
                Thread.Sleep(3000); 
       
               try{
                     int accountNumber = Int32.Parse(accountResponse);
                     setAccount(accountNumber);
                  }
               catch(FormatException e)
                  {
                     Console.WriteLine("That is not a valid account number.  Please try again");
                     start();
                  }          
             }
        theMenu menu = new theMenu(currentUserId, accountPeople);
        accountPeople = menu.menuResponse();
        start();
    }
    
   private string register(string response) 
    {
       Profile temp = new Profile ();
       Console.WriteLine("What is your First Name?");
       temp.firstName = Console.ReadLine();
       Console.WriteLine("What is your Last Name?");
       temp.lastName = Console.ReadLine();
       int age = 0;
           do{
               Console.WriteLine("What is your Age?");
                   try {
                            age = Int32.Parse(Console.ReadLine());
                            temp.age = age;
                        }
                   catch (FormatException e) 
                       {
                              Console.WriteLine("Please enter your age in the correct format.");
                       }
             } while(age == 0);
       Console.WriteLine("What is your Phone Number?");
       temp.phoneNumber = Console.ReadLine();
       Console.WriteLine("Thank You " + temp.firstName +  " You have set up a new account!  Here is your account number " + temp.accountNumber);
       accountPeople[0] = temp;// should eventually be a list to add,or database
      return temp.accountNumber.ToString();
    }
     
    public void setAccount (int account)
    {
        
     
        try{
              Profile file = new Profile ();
              file =  accountPeople.Where(s => s.accountNumber == account).FirstOrDefault<Profile>();
              currentUserId = file.userId;

           }
        catch (NullReferenceException e)
           {
              Console.WriteLine("This is not a valid account.");
              start();
           }
    }
  
}
    ///////THE MENU
   public class theMenu
{
     
        Profile file = new Profile ();
        Profile [] menuList = new Profile [10];
        public int currentUserId;
       
     public theMenu(int account, Profile [] accountPeople )
     {
        file =  accountPeople.Where(s => s.userId == account).FirstOrDefault<Profile>();
        currentUserId = file.userId;
        menuList = accountPeople;
       
     }
       
    public Profile [] menuResponse ()
    {
        string request = "";
         do{
                Console.WriteLine("Hello " + file.firstName + " how can we help you today?" );
                 Thread.Sleep(3000);
                 Console.WriteLine("Here's our menu");
                 Console.WriteLine("Enter: 1 or W/Withdrawal, 2 or D/Deposit, 3 or B/Check Balance, 4 or E/Exit");
                 request = Console.ReadLine();
                 Thread.Sleep(3000);
                switch(request) 
                {
                    case "W":
                    case "w":
                    case "1":
                    case "one":
                    withdraw();
                    break;
                    case "D":
                    case "d":
                    case "2":
                    case "two":
                   deposit();
                    break;
                    case "b":
                    case "B":
                    case "3":
                    case "three":
                    balance();
                    break;  
                    case "e":
                    case "E":
                    case "4":
                    case "four":
                    break;  
               };
          }
       while(request != "E" || request != "e" || request != "4" || request != "four");
       return menuList;
    }

         private void withdraw()
   {
     if (file.balance > 0) 
        {   
            Console.WriteLine("How much would you like to withdraw?  For example $20.50");
            string response = Console.ReadLine();
            double amount = Convert.ToDouble(response);
       
         if ( amount > file.balance)
            { 
                Console.WriteLine("You only have " + file.balance);
            }
       else {
                Console.WriteLine("Transaction is successful. Here is your money");
                file.balance -= amount;
                Console.WriteLine("Your balance is " + file.balance + " Have a great day");
                menuList[0] = file;
    //can say anything else, and then do bank end function that will save it to database or save on compute
             }
         }
    else {
    Console.WriteLine("You don't have any money.  Please deposit some.");
         }
   }
       
  private void deposit() 
   {
    Console.WriteLine("How much would you like to deposit?  For example $20.50");
      try{
            file.balance += Convert.ToDouble(Console.ReadLine());
         }
     catch (ArgumentException e)
         {
             Console.WriteLine("Not valid. Try again");
             deposit();
         }
      Console.WriteLine("Your balance is " + file.balance + " Have a great day");
      menuList[0] = file;
   }
private void balance() 
   {
    Console.WriteLine("Your balance is: " + file.balance);
   }

}


public class Profile 
{
//while registering
public int userId; //should be automatically generate but for now a set id number
public int accountNumber;
public double balance;
public string firstName;
public string lastName;
public int age;
public string phoneNumber;
    
    public Profile() 
    {
     Random ran = new Random();
        userId = ran.Next(3000, 9000);
        accountNumber = ran.Next(200000, 900000);
        balance = 0.00;
    }    
}

