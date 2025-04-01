using System;
using NAudio.Wave; // Required for audio playback
using System.Threading;

namespace CybersecurityBot
{
    class Program
    {
        // Define colors for a consistent theme
        private static readonly ConsoleColor BotColor = ConsoleColor.Cyan;
        private static readonly ConsoleColor HighlightColor = ConsoleColor.Yellow;
        private static readonly ConsoleColor ErrorColor = ConsoleColor.Red;

        static void Main(string[] args)
        {
            DisplayAsciiLogo(); // Display ASCII art
            PlayGreeting();     // Play voice greeting
            string userName = GetUserName();      // Get the user's name
            StartInteraction(userName); // Begin the main interaction loop
        }

        static void DisplayAsciiLogo()
        {
            Console.ForegroundColor = BotColor;
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║       .--.                      ║");
            Console.WriteLine("║      | ^ ^|                     ║");
            Console.WriteLine("║      | >-<|                     ║");
            Console.WriteLine("║      '----'                     ║");
            Console.WriteLine("║     /_____\\                    ║");
            Console.WriteLine("║    |_______|                    ║");
            Console.WriteLine("║      || ||                      ║");
            Console.WriteLine("║      || ||                      ║");
            Console.WriteLine("║     --  --                      ║");
            Console.WriteLine("╚═════════════════════════════════╝");
            Console.WriteLine($"\n  [{ColorText("Happy Cyber Bot", BotColor)}]");
            Console.WriteLine($"  Keeping You Safe");
            Console.ResetColor();
            Console.WriteLine($"\n  A friendly guide to staying secure online.");
        }

        static void PlayGreeting()
        {
            string audioFilePath = "greeting.wav"; // Ensure file exists

            try
            {
                using (var audioFile = new AudioFileReader(audioFilePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    Console.WriteLine($"\n[{ColorText("Bot", BotColor)}]: Playing greeting...");
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100); // Wait for playback to finish
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ErrorColor;
                Console.WriteLine($"Error playing the audio file: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
            }
            Console.WriteLine($"[{ColorText("Bot", BotColor)}]: Hello there! Welcome to the Cybersecurity Bot...");
            Thread.Sleep(500); // Slight delay after greeting
        }

        static string GetUserName()
        {
            Console.Write($"[{ColorText("Bot", BotColor)}]: What's your name? ");
            string userName = Console.ReadLine();
            Console.WriteLine($"[{ColorText("Bot", BotColor)}]: Nice to meet you, {ColorText(userName, HighlightColor)}!");
            return userName;
        }

        static void StartInteraction(string userName)
        {
            Console.WriteLine("\n" + SectionHeader("How Can I Help You?", '='));
            Console.WriteLine("You can ask me about:");
            Console.WriteLine($"  {Symbol("►")} {ColorText("Password safety", HighlightColor)}");
            Console.WriteLine($"  {Symbol("►")} {ColorText("Phishing", HighlightColor)}");
            Console.WriteLine($"  {Symbol("►")} {ColorText("Safe browsing", HighlightColor)}");
            Console.WriteLine($"  {Symbol("►")} {ColorText("General questions", HighlightColor)} (e.g., How are you?)");
            Console.WriteLine($"\nType your question or '{ColorText("exit", ErrorColor)}' to quit.");

            string input;
            while ((input = Console.ReadLine()) != null && input.ToLower() != "exit")
            {
                Console.Write($"[{ColorText(userName, HighlightColor)}]: ");
                string response = GetResponse(input);
                Console.Write($"[{ColorText("Bot", BotColor)}]: ");
                TypeText(response); // Simulate typing effect
                Console.WriteLine("\nAsk another question or type 'exit' to quit.");
            }

            Console.WriteLine("\n" + SectionHeader("Thank You!", '='));
            Console.WriteLine($"[{ColorText("Bot", BotColor)}]: Thank you for chatting! Stay safe online!");
        }

        static string GetResponse(string query)
        {
            query = query.ToLower();

            if (query.Contains("how are you"))
            {
                return "I'm doing well, thank you for asking! Ready to help you with cybersecurity.";
            }
            else if (query.Contains("what's your purpose"))
            {
                return "My purpose is to raise awareness about cybersecurity and help you stay safe online.";
            }
            else if (query.Contains("what can i ask you about"))
            {
                return "You can ask me about topics like password safety, phishing, and safe browsing. Feel free to ask!";
            }
            else if (query.Contains("password safety"))
            {
                return $"{ColorText("Password Safety:", HighlightColor)} Strong passwords are crucial! Use a mix of uppercase and lowercase letters, numbers, and symbols. Avoid using personal information and consider using a password manager.";
            }
            else if (query.Contains("phishing"))
            {
                return $"{ColorText("Phishing:", HighlightColor)} Phishing attempts try to trick you into revealing sensitive information. Be wary of suspicious emails, links, and requests for personal data. Always verify the source.";
            }
            else if (query.Contains("safe browsing"))
            {
                return $"{ColorText("Safe Browsing:", HighlightColor)} Practice safe browsing by keeping your browser updated, avoiding suspicious websites, and being cautious about downloading files. Look for the HTTPS in the URL.";
            }
            else if (string.IsNullOrWhiteSpace(query))
            {
                return "I didn't quite understand that. Could you please rephrase?";
            }
            else
            {
                return "I'm still learning about that topic. Could you ask something else?";
            }
        }

        // Helper methods for UI enhancements
        static string ColorText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            string coloredText = text;
            Console.ResetColor();
            return coloredText;
        }

        static string SectionHeader(string title, char borderChar)
        {
            int width = Console.WindowWidth - 1; // Adjust for console width
            int titleLength = title.Length + 4; // Add padding
            int padding = Math.Max(0, (width - titleLength) / 2); // Ensure padding is not negative
            string border = new string(borderChar, padding);
            return $"{border} {title} {border}";
        }

        static string Symbol(string symbol)
        {
            return symbol;
        }

        static void TypeText(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(50); // Adjust typing speed
            }
            Console.WriteLine();
        }
    }
}