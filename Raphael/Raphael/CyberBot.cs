using System;

namespace Raphael
{
    /// <summary>
    /// The brain of the operation. Talks to the user, figures out what they're asking,
    /// and spits out cybersecurity advice with a South African flavour.
    /// </summary>
    internal class CyberBot
    {
        // Automatic properties – POE requirement ticked. Start with empty strings so no null drama.
        public string BotName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        // Helpers – marked readonly because we set 'em once and leave 'em alone
        private readonly DisplayHelper _display;
        private readonly AudioHelper _audio;

        public CyberBot()
        {
            BotName = "Raphael"; // The bot's name, obviously
            _display = new DisplayHelper();
            _audio = new AudioHelper();
        }

        public void Start()
        {
            // Audio might fail – that's fine, we handle it and keep moving
            _audio.PlayGreeting();
            _display.ShowHeader();
            _display.ShowWelcomeMessage();

            AskForUserName();
            RunChatLoop();

            _display.ShowGoodbyeMessage();
        }

        /// <summary>
        /// Keeps asking until they give us something that isn't blank.
        /// </summary>
        public void AskForUserName()
        {
            bool validName = false;

            while (!validName)
            {
                _display.ShowNamePrompt();
                string? input = Console.ReadLine(); // ReadLine can return null, so we use string?

                if (!string.IsNullOrWhiteSpace(input))
                {
                    UserName = input.Trim();
                    validName = true;
                }
                else
                {
                    // Let DisplayHelper handle the yelling – keeps CyberBot clean
                    _display.ShowNameError();
                }
            }

            _display.ShowPersonalGreeting(UserName);
        }

        /// <summary>
        /// The main chat loop. Runs until the user says exit, quit, or goodbye.
        /// </summary>
        public void RunChatLoop()
        {
            bool keepRunning = true;

            while (keepRunning)
            {
                _display.ShowChatPrompt(UserName);
                string? userInput = Console.ReadLine();

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
                    string response = GetResponse(userInput);
                    _display.ShowBotResponse(response);
                }
            }
        }

        /// <summary>
        /// Takes whatever the user typed, cleans it up, and tries to match it to a topic.
        /// Returns a helpful (and hopefully not boring) cybersecurity response.
        /// </summary>
        public string GetResponse(string input)
        {
            // Clean it ourselves so we don't have to trust the caller got it right
            string cleanInput = input?.ToLower().Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(cleanInput))
                return "I didn't catch that. Could you say it again?";

            // Basic chit-chat
            if (cleanInput.Contains("how are you"))
                return "I'm fully operational and ready to help you stay safe online. What can I do for you?";

            if (cleanInput.Contains("purpose") || cleanInput.Contains("who are you"))
                return "I'm Raphael, built for the South African Cybersecurity Awareness Campaign. My job is to help everyday South Africans spot scams and protect themselves online.";

            if (cleanInput.Contains("what can i ask") || cleanInput.Contains("topics"))
                return "You can ask me about passwords, phishing, safe browsing, malware – basically anything that helps you not get hacked. Where do you want to start?";

            // Cybersecurity topics – with SA context sprinkled in
            if (cleanInput.Contains("password"))
                return "Use strong, unique passwords for every account. Don't use your ID number, birthdate, or '123456'. A password manager is your best friend here.";

            if (cleanInput.Contains("phishing"))
                return "Phishing is huge in South Africa right now. Scammers pretend to be your bank, SARS, or even your boss. Never click links in weird SMSs or emails. If it feels off, it probably is.";

            if (cleanInput.Contains("safe browsing") || cleanInput.Contains("browsing"))
                return "Look for the little padlock and 'HTTPS' in the address bar. Don't do online banking on free mall WiFi – that's asking for trouble. Use your mobile data or a VPN if you must.";

            if (cleanInput.Contains("malware") || cleanInput.Contains("virus"))
                return "Malware can lock your files or steal your info. Keep your antivirus updated and never download attachments from people you don't know. Yes, even if it says 'Invoice'.";

            // If nothing matched, give a nudge toward topics we actually know
            return "I'm not sure about that one. Try asking me about 'passwords', 'phishing', or 'safe browsing' – those I can definitely help with.";
        }
    }
}
}
