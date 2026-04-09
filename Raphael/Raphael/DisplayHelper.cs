using System;
using System.Threading;

namespace Raphael
{
    /// <summary>
    /// Handles all console visual output including colours,
    /// ASCII art, borders, and the typing effect.
    /// </summary>
    internal class DisplayHelper
    {
        // Optional configurable typing speed (helps with readability + future changes)
        public int TypingDelay { get; set; } = 30;

        /// <summary>
        /// Clears the console and displays the ASCII header/logo.
        /// </summary>
        public void ShowHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(@"
  _____            _____  _    _          ______ _      
 |  __ \     /\   |  __ \| |  | |   /\   |  ____| |     
 | |__) |   /  \  | |__) | |__| |  /  \  | |__  | |     
 |  _  /   / /\ \ |  ___/|  __  | / /\ \ |  __| | |     
 | | \ \  / ____ \| |    | |  | |/ ____ \| |____| |____ 
 |_|  \_\/_/    \_\_|    |_|  |_/_/    \_\______|______|
");

            Console.WriteLine("===== CYBERSECURITY AWARENESS ASSISTANT =====");
            DrawDivider();
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the welcome message introducing the chatbot.
        /// </summary>
        public void ShowWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeText("Welcome to Raphael - Cybersecurity Awareness Assistant");
            TypeText("Your guide to staying safe online in South Africa.");
            Console.ResetColor();
            DrawDivider();
        }

        /// <summary>
        /// Prompts the user to enter their name.
        /// </summary>
        public void ShowNamePrompt()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("What is your name? ");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an error when the user enters an empty name.
        /// </summary>
        public void ShowNameError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(!) Name cannot be empty. Please enter your name to continue.");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays a personalised greeting to the user.
        /// </summary>
        public void ShowPersonalGreeting(string name)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Hello, {name}! How can I help you today?");
            Console.ResetColor();
            DrawDivider();
        }

        /// <summary>
        /// Displays the chat prompt with the user's name.
        /// </summary>
        public void ShowChatPrompt(string name)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{name} > ");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the bot's response with typing effect.
        /// </summary>
        public void ShowBotResponse(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Raphael]: ");
            Console.ResetColor();

            TypeText(message);
            DrawDivider();
        }

        /// <summary>
        /// Shows an error message when the user enters invalid input.
        /// </summary>
        public void ShowInvalidInputMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please type a question or type 'exit' to leave.");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the goodbye message when the user exits.
        /// </summary>
        public void ShowGoodbyeMessage()
        {
            DrawDivider();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Thank you for using Raphael.");
            Console.WriteLine("Stay safe online!");
            Console.ResetColor();
        }

        /// <summary>
        /// Prints text character by character to simulate typing.
        /// </summary>
        public void TypeText(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(TypingDelay);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Draws a divider line across the console.
        /// </summary>
        public void DrawDivider()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('=', 60));
            Console.ResetColor();
        }
    }
}