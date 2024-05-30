using System;
using System.IO;

class Settings
{
    public static bool PlayerChoosePlayers()
    {
        string? answer = null;
        Console.Write("\nDo you want to play against the other player? if not you will play against a computer (y/n): ");
        do
        {
            try
            {
                answer = Console.ReadLine()?.ToLower();
                if(answer == "y")
                {
                    return true;
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'");
            }
            if(answer != "y" && answer != "n")
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'");
                Console.WriteLine("Do you want to play against the other player? (y/n)");
            }

        } while(answer != "y" && answer != "n");

        return false;
    }

    public static void Save(Board board, bool playerOneTurn) 
    {
        string? answer = null;
        Console.WriteLine("Do you want to save the game? (y/n)");
        do
        {
            try
            {
                answer = Console.ReadLine()?.ToLower();
                if(answer == "y")
                {
                    ShowSaveFiles();
                }
                else if(answer == "n")
                {
                    return;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            if(answer != "y" && answer != "n")
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'");
                Console.WriteLine("Do you want to save the game? (y/n)");
            }

        } while(answer != "y" && answer != "n");

        do
        {
            try
            {
                Console.WriteLine("Enter the name of the save file:");
                string? saveName = Console.ReadLine();
                if(saveName == "exit")
                {
                    return;
                }
                if (saveName == null || saveName == "" || saveName == " ")
                {
                    saveName = "ChessSave";
                }
                string path = "saves/" + saveName + ".txt";

                using StreamWriter writer = new(path);
                writer.WriteLine(playerOneTurn ? "1" : "0");
                writer.WriteLine(board.GetBoard());

                Console.WriteLine("\n\nGame saved successfully\n");

                return;
                
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
            }
        }while(true);
    }

    public static void Load(ref Board board, ref bool playerOneTurn)
    {
        string? answer = null;
        Console.WriteLine("Do you want to load a game? (y/n)");
        do
        {
            try
            {
                answer = Console.ReadLine()?.ToLower();
                if(answer == "y")
                {
                    ShowSaveFiles();
                }
                else if(answer == "n")
                {
                    return;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            if(answer != "y" && answer != "n")
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'");
                Console.WriteLine("Do you want to load a game? (y/n)");
            }
        }while(answer != "y" && answer != "n");
        
        do
        {
            try
            {
                Console.WriteLine("Enter the name of the save file or type exit:");
                string? saveName = Console.ReadLine()?.ToLower();
                if(saveName == "exit")
                {
                    return;
                }
                else if (saveName == null || saveName == "" || saveName == " ")
                {
                    saveName = "ChessSave";
                }
                string path = "saves/" + saveName + ".txt";

                if(File.Exists(path))
                {
                    using StreamReader reader = new(path);
                    playerOneTurn = reader.ReadLine() == "1";
                    board = Board.SetBoard(reader.ReadToEnd());   
                    Console.WriteLine("\n\nGame loaded successfully\n");
                    return;
                }
                else
                {
                    Console.WriteLine("Save file not found. Please enter a valid save file name\n");
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
        }while(true);  

    }

    public static void DeleteSave()
    {
        do
        {
            try
            {
                ShowSaveFiles();
                Console.Write("Enter the name of the save file: ");
                string? saveName = Console.ReadLine();

                string folder = Directory.GetCurrentDirectory() + "/saves/" + saveName + ".txt";

                if(File.Exists(folder))
                {
                    File.Delete(folder);
                    Console.WriteLine("Save Successfully Deleted!");
                    return;
                }
                else if(saveName?.ToUpper() == "EXIT")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("File Not Found");
                }

            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
            }

        }while(true);
    }

    static void ShowSaveFiles()
    {
        string folder = Directory.GetCurrentDirectory() + "/saves";
        if(Directory.Exists(folder))
        {
            Console.WriteLine("\nSaves:");
            string[] files = Directory.GetFiles(folder);
            foreach(string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("No save files found");
        }    
    }
}