using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace bluechatbot
{
    public class user_name
    {
        // ==================== MAIN METHOD ====================
        public string submit_name(TextBox usernames_input, ListView chats)
        {
            if (usernames_input == null || chats == null)
            {
                MessageBox.Show("Error: Input controls are missing.");
                return string.Empty;
            }

            string filename = "user_names.txt";
            string name = usernames_input.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                error_method("ChatBot", "Please enter a valid username.", chats);
                return string.Empty;
            }

            // Create file if it doesn't exist
            if (!File.Exists(filename))
            {
                File.AppendAllText(filename, "auto_create\n");
            }

            bool found = check_name(name);

            if (!found)
            {
                File.AppendAllText(filename, name + "\n");
                error_method("ChatBot", $"Hey {name}, welcome to AI Cybersecurity!", chats);
            }
            else
            {
                error_method("ChatBot", $"Hey {name}, welcome back! How can I help you today?", chats);
            }

            return name;
        }

        // ==================== CHECK NAME METHOD ====================
        private bool check_name(string name)
        {
            string filename = "user_names.txt";

            if (!File.Exists(filename))
                return false;

            string[] names = File.ReadAllLines(filename);

            foreach (string existingName in names)
            {
                if (string.Equals(existingName.Trim(), name.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        // ==================== ERROR METHOD ====================
        private void error_method(string name, string message, ListView chats)
        {
            Border messageBorder = new Border
            {
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(5, 3, 5, 3),
                CornerRadius = new CornerRadius(5),
                BorderThickness = new Thickness(1)
            };

            // Set background color
            if (name.ToLower().Contains("chatbot") || name.ToLower().Contains("chat"))
            {
                messageBorder.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
                messageBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(173, 216, 230));
            }
            else
            {
                messageBorder.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
                messageBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            }

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(2)
            };

            Brush nameColor = (name.ToLower().Contains("chatbot") || name.ToLower().Contains("chat")) ?
                              Brushes.DarkBlue : Brushes.DarkGreen;

            messageText.Inlines.Add(new Run
            {
                Text = name + ": ",
                Foreground = nameColor,
                FontWeight = FontWeights.Bold
            });

            messageText.Inlines.Add(new Run
            {
                Text = message,
                Foreground = Brushes.Black
            });

            messageBorder.Child = messageText;
            chats.Items.Add(messageBorder);
            chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
        }
    }
}