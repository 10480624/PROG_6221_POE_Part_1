using System;

namespace Raphael
{
    /// <summary>
    /// Core chatbot class. Handles conversation logic,
    /// user interaction, and response generation.
    /// </summary>
    internal class CyberBot
    {
        // POE REQUIREMENT: Automatic properties
        public string BotName { get; set; }
        public string? UserName { get; set; }

        // Private helpers – readonly for discipline
        private readonly DisplayHelper _display;
        private readonly AudioHelper _audio;

        /// <summary>
        /// Constructor: initialises bot name and helper objects.
        /// </summary>
        public CyberBot()
        {
            BotName = "Raphael";
            _display = new DisplayHelper();
            _audio = new AudioHelper();
        }

        /// <summary>
        /// Starts the chatbot application.
        /// </summary>
        public void Start()
        {
            _audio.PlayGreeting();
            _display.ShowHeader();
            _display.ShowWelcomeMessage();

            AskForUserName();
            RunChatLoop();

            _display.ShowGoodbyeMessage();
        }

        /// <summary>
        /// Prompts the user for their name with a validation loop.
        /// </summary>
        public void AskForUserName()
        {
            bool validName = false;

            while (!validName)
            {
                _display.ShowNamePrompt();
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    UserName = input.Trim();
                    validName = true;
                }
                else
                {
                    // Delegates error display to DisplayHelper – keeps CyberBot clean
                    _display.ShowNameError();
                }
            }

            _display.ShowPersonalGreeting(UserName);
        }

        /// <summary>
        /// Main conversation loop. Runs until the user types 'exit', 'quit', or 'goodbye'.
        /// </summary>
        public void RunChatLoop()
        {
            bool keepRunning = true;

            while (keepRunning)
            {
                _display.ShowChatPrompt(UserName);
                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    _display.ShowInvalidInputMessage();
                    continue;
                }

                string cleanInput = userInput.ToLower().Trim();

                if (cleanInput == "exit" || cleanInput == "quit" || cleanInput == "goodbye")
                {
                    keepRunning = false;
                }
                else
                {
                    // Pass raw input – GetResponse does its own cleaning
                    string response = GetResponse(userInput);
                    _display.ShowBotResponse(response);
                }
            }
        }

        /// <summary>
        /// Returns an appropriate cybersecurity response based on user input.
        /// Cleaning is done inside the method for self-contained safety.
        /// </summary>
        /// <param name="input">Raw user input.</param>
        public string GetResponse(string input)
        {
            // Defensive cleaning – ensures method works even if called incorrectly
            string cleanInput = input?.ToLower().Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(cleanInput))
                return "I didn't catch that. Could you say it again?";

            // Conversational / required prompts
            if (cleanInput.Contains("how are you"))
                return "I'm fully operational and ready to assist with your online safety.";

            if (cleanInput.Contains("purpose") || cleanInput.Contains("who are you"))
                return "I am Raphael, an assistant developed for the South African Cybersecurity Awareness Campaign. My purpose is to help citizens recognise and avoid cyber threats.";

            if (cleanInput.Contains("what can i ask") || cleanInput.Contains("topics"))
                return "You can ask me about passwords, phishing, safe browsing, malware, and more. Where would you like to start?";

            // Cybersecurity topics – South African context included
            if (cleanInput.Contains("password"))
                return "Use strong, unique passwords for each account. Avoid personal info like your birthdate. A password manager can help keep your credentials secure.";

            if (cleanInput.Contains("phishing"))
                return "Phishing is a common threat in South Africa. Never click links in unexpected emails or SMSs. Banks and SARS will never ask for your PIN or password via a link.";

            if (cleanInput.Contains("safe browsing") || cleanInput.Contains("browsing"))
                return "Stick to websites with HTTPS and the padlock icon. Avoid banking or shopping on public Wi-Fi unless you use a VPN.";

            if (cleanInput.Contains("malware") || cleanInput.Contains("virus"))
                return "Malware can steal your data or lock your files. Keep your antivirus updated and never download attachments from unknown senders.";

            // Fallback
            return "I'm not sure about that. Try asking about 'passwords', 'phishing', or 'safe browsing'.";
        }
    }
}