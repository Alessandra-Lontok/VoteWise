using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteWise
{
    internal class Program
    {
        static void File_Verification()
        {
            if (!File.Exists("UserInfo.txt"))
            {
                File.WriteAllText("UserInfo.txt", "");
            }
        }
        static void Main(string[] args)
        {
            File_Verification();

            string[] Source_Info = File.ReadAllLines(@"UserInfo.txt");
            List<string> User = new List<string>();
            List<string> Password = new List<string>();

            List<string> Current_Login = new List<string> { "", ""};

            UserInfo(Source_Info, User, Password);

            Printing(Password);

            while (true)
            {
                Header_Printer();
                Menu_Printer("Main Menu");
                Console.WriteLine("Welcome to VOTEWISE: ELECTION COMPILER");
                Console.WriteLine("Please enter the option you would like to do: ");
                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Register");
                Console.WriteLine("[3] Login as Guest");
                Console.WriteLine("[X] Exit");
                string UserInput = GetInput();

                if (UserInput == null)
                {
                    return;
                }

                switch (UserInput)
                {
                    case "1":
                        Login(Current_Login, User, Password);
                        break;
                    case "2":
                        Register(Current_Login, User, Password);    
                        break;
                    case "3":
                        Guest(Current_Login);
                        break;
                    default:
                        Console.WriteLine("Please enter valid input...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //====================[Printing/UI]=====================
        static void Header_Printer()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 63));
            Console.WriteLine($"- - - - - - - ++ VOTEWISE: ELECTION COMPILER   ++ - - - - - - -");
            Console.WriteLine(new string('=', 63));
        }
        static void Menu_Printer(string Title)
        {
            Console.WriteLine($"                         [" + Title + "]                      ");
        }
        static void Printing(List<string> Password)
        {
            foreach (string var in Password)
            {
                Console.WriteLine(var);
            }
        }

        //====================[Functions]=====================
        static string GetInput()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (input == null || input == "")
                {
                    Console.WriteLine("Field cannot be empty...");
                }
                else if (input.ToUpper() == "X")
                {
                    return null;
                }
                else
                {
                    return input.Trim();
                }
            }
        }

        //====================[Info Splitting Page]=====================
        static void UserInfo(string[] Source_File, List<string> User, List<string> Password)
        {
            for (int i = 0; i < Source_File.Length; i++)
            {
                string[] temp = Source_File[i].Split(',');

                User.Add(temp[0]);
                Password.Add(temp[1]);
            }
        }

        //====================[LOGIN Sub-Menus]=====================
        static void Login(List<string> Current_Login, List<string> User, List<string> Password)
        {
            while (true)
            {
                Header_Printer();
                Menu_Printer("Login Menu");
                Console.WriteLine("Enter [X] to return to return to previous page");
                Console.WriteLine("Please enter your account details");
                Console.WriteLine("[Username]");
                string InputUsername = GetInput();
                if (InputUsername == null)
                {
                    return;
                }
                Console.WriteLine("[Password]");
                string InputPassword = GetInput();
                if (InputPassword == null)
                {
                    return;
                }

                bool verify = false;

                for (int i = 0; i < User.Count; i++)
                {
                    if (InputUsername == User[i] && InputPassword == Password[i])
                    {
                        Current_Login[0] = InputUsername;
                        Current_Login[1] = InputPassword;
                        verify = true;
                        break;
                    }
                }

                if (verify == false)
                {
                    Console.WriteLine("Please check if username or role entered is correct");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Login Successful!");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadKey();
                    break;
                }
            }
        }

        static void Register(List<string> Current_Login, List<string> User, List<string> Password)
        {
            string temp = "";

            Header_Printer();
            Menu_Printer("Login Menu");
            Console.WriteLine("Enter [X] to return to previous page");
            Console.WriteLine("Please enter your account details");
            while(true)
            {
                bool exists = false;

                Console.WriteLine("[Username]");
                string InputUsername = GetInput();
                if (InputUsername == null)
                {
                    return;
                }

                for (int i = 0; i < User.Count; i++)
                {
                    if (InputUsername == User[i])
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists == true)
                {
                    Console.WriteLine("Invalid input. User already exists. Please try again...");
                }
                else
                {
                    Current_Login[0] = InputUsername;
                    break;
                }
            }

            while (true)
            {
                bool confirm_exit = false;

                Console.WriteLine("[Password]");
                string InputPassword = GetInput();
                if (InputPassword == null)
                {
                    return;
                }
                temp = InputPassword;

                Console.WriteLine("[Confirm Password]");
                Console.WriteLine("Enter [X] to retype password");
                while (confirm_exit == false)
                {
                    string confirm = GetInput();

                    if (confirm == null)
                    {
                        break;
                    }

                    if (confirm == temp)
                    {
                        Current_Login[1] = confirm;
                        confirm_exit = true;
                    }
                    else
                    {
                        Console.WriteLine("Password incorrect. Please re-enter password...");
                    }
                }

                if (confirm_exit)
                {
                    break;
                }
            }

            File.AppendAllText("UserInfo.txt", "" + Current_Login[0] + "," + Current_Login[1]);

            Console.WriteLine("Sucessfully registered!");
            Console.ReadKey();
        }

        static void Guest(List<string> Current_Login)
        {
            Current_Login[0] = "Guest";
            Current_Login[1] = "000";
        }
        //====================[Account/Guest Sub-Menus]=====================

    }
}
