using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace CyberSecurittChatBot
{
    public partial class TaskManagerWindow : Window
    {
        private int quizScore = 0;
        private int currentQuestionIndex = 0;
        private bool quizInProgress = false;
        private DispatcherTimer reminderTimer;
        private List<string> activeTasks = new List<string>();

        // Quiz questions and answers
        private List<QuizQuestion> quizQuestions = new List<QuizQuestion>
        {
            new QuizQuestion("What does CIA stand for in cybersecurity?",
                new string[] { "Central Intelligence Agency", "Confidentiality, Integrity, Availability", "Computer Information Access", "Cyber Information Authority" }, 1),
            new QuizQuestion("Which type of attack involves flooding a network with traffic?",
                new string[] { "Phishing", "Malware", "DDoS", "Social Engineering" }, 2),
            new QuizQuestion("What is the purpose of a firewall?",
                new string[] { "To speed up internet", "To block unauthorized access", "To store passwords", "To encrypt files" }, 1),
            new QuizQuestion("Which of these is a strong password characteristic?",
                new string[] { "Uses personal information", "Contains only letters", "Has 12+ characters with mixed case", "Is easy to remember" }, 2),
            new QuizQuestion("What does VPN stand for?",
                new string[] { "Virtual Private Network", "Very Personal Network", "Verified Protection Network", "Visual Privacy Network" }, 0),
            new QuizQuestion("Which protocol is used for secure web browsing?",
                new string[] { "HTTP", "HTTPS", "FTP", "SMTP" }, 1),
            new QuizQuestion("What is phishing?",
                new string[] { "A type of firewall", "Fraudulent attempt to obtain sensitive information", "A network protocol", "A password manager" }, 1),
            new QuizQuestion("Which of these is NOT a type of malware?",
                new string[] { "Virus", "Trojan", "Worm", "Router" }, 3),
            new QuizQuestion("What does two-factor authentication provide?",
                new string[] { "Faster login", "Additional security layer", "Password storage", "Network access" }, 1),
            new QuizQuestion("Which practice helps prevent social engineering attacks?",
                new string[] { "Sharing passwords", "Verifying requests independently", "Using public Wi-Fi", "Ignoring security updates" }, 1)
        };

        public TaskManagerWindow()
        {
            InitializeComponent();
            InitializeReminder();
        }

        private void InitializeReminder()
        {
            // Set up reminder timer to check every 30 seconds
            reminderTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(30)
            };
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Start();
        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            if (activeTasks.Count > 0)
            {
                string reminder = $"Reminder: You have {activeTasks.Count} pending task(s)";
                ActivityLogListBox.Items.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] {reminder}");

                // Auto-scroll to bottom
                if (ActivityLogListBox.Items.Count > 0)
                {
                    ActivityLogListBox.ScrollIntoView(ActivityLogListBox.Items[ActivityLogListBox.Items.Count - 1]);
                }
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskInput.Text))
            {
                string task = TaskInput.Text.Trim();
                TasksListBox.Items.Add(task);
                activeTasks.Add(task);
                ActivityLogListBox.Items.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Added Task: {task}");
                TaskInput.Clear();

                // Auto-scroll activity log
                if (ActivityLogListBox.Items.Count > 0)
                {
                    ActivityLogListBox.ScrollIntoView(ActivityLogListBox.Items[ActivityLogListBox.Items.Count - 1]);
                }
            }
            else
            {
                MessageBox.Show("Please enter a task before adding.", "Empty Task", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoveTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem != null)
            {
                string removedTask = TasksListBox.SelectedItem.ToString();
                TasksListBox.Items.Remove(TasksListBox.SelectedItem);
                activeTasks.Remove(removedTask);
                ActivityLogListBox.Items.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Removed Task: {removedTask}");

                // Auto-scroll activity log
                if (ActivityLogListBox.Items.Count > 0)
                {
                    ActivityLogListBox.ScrollIntoView(ActivityLogListBox.Items[ActivityLogListBox.Items.Count - 1]);
                }
            }
            else
            {
                MessageBox.Show("Please select a task to remove.", "No Task Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (!quizInProgress)
            {
                StartNewQuiz();
            }
            else
            {
                MessageBox.Show("Quiz is already in progress!", "Quiz Active", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void StartNewQuiz()
        {
            quizScore = 0;
            currentQuestionIndex = 0;
            quizInProgress = true;

            ActivityLogListBox.Items.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Quiz started");

            // Auto-scroll activity log
            if (ActivityLogListBox.Items.Count > 0)
            {
                ActivityLogListBox.ScrollIntoView(ActivityLogListBox.Items[ActivityLogListBox.Items.Count - 1]);
            }

            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            if (currentQuestionIndex < quizQuestions.Count)
            {
                QuizQuestion question = quizQuestions[currentQuestionIndex];

                string questionText = $"Question {currentQuestionIndex + 1}/10:\n\n{question.Question}\n\n";
                for (int i = 0; i < question.Options.Length; i++)
                {
                    questionText += $"{i + 1}. {question.Options[i]}\n";
                }

                string userInput = Microsoft.VisualBasic.Interaction.InputBox(
                    questionText + "\nEnter your answer (1-4):",
                    "Cybersecurity Quiz",
                    "1"
                );

                if (string.IsNullOrEmpty(userInput))
                {
                    // User cancelled
                    EndQuiz();
                    return;
                }

                if (int.TryParse(userInput, out int answer) && answer >= 1 && answer <= 4)
                {
                    if (answer - 1 == question.CorrectAnswerIndex)
                    {
                        quizScore++;
                        ActivityLogListBox.Items.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Question {currentQuestionIndex + 1}: Correct!");
                    }
                    else
                    {
                        ActivityLogListBox.Items.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Question {currentQuestionIndex + 1}: Incorrect. Correct answer: {question.Options[question.CorrectAnswerIndex]}");
                    }

                    currentQuestionIndex++;
                    UpdateQuizScore();

                    // Auto-scroll activity log
                    if (ActivityLogListBox.Items.Count > 0)
                    {
                        ActivityLogListBox.ScrollIntoView(ActivityLogListBox.Items[ActivityLogListBox.Items.Count - 1]);
                    }

                    // Small delay before next question
                    var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
                    timer.Tick += (s, e) =>
                    {
                        timer.Stop();
                        ShowNextQuestion();
                    };
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("Please enter a valid number between 1 and 4.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ShowNextQuestion(); // Ask the same question again
                }
            }
            else
            {
                EndQuiz();
            }
        }

        private void UpdateQuizScore()
        {
            QuizScoreTextBlock.Text = $"Your Quiz Score: {quizScore}/10";
        }

        private void EndQuiz()
        {
            quizInProgress = false;

            string performance = "";
            if (quizScore >= 9) performance = "Excellent!";
            else if (quizScore >= 7) performance = "Good job!";
            else if (quizScore >= 5) performance = "Not bad, keep studying!";
            else performance = "Need more practice!";

            string finalMessage = $"Quiz completed!\n\nFinal Score: {quizScore}/10\n{performance}";
            MessageBox.Show(finalMessage, "Quiz Results", MessageBoxButton.OK, MessageBoxImage.Information);

            ActivityLogListBox.Items.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] Quiz completed. Final Score: {quizScore}/10");

            // Auto-scroll activity log
            if (ActivityLogListBox.Items.Count > 0)
            {
                ActivityLogListBox.ScrollIntoView(ActivityLogListBox.Items[ActivityLogListBox.Items.Count - 1]);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            reminderTimer?.Stop();
            base.OnClosed(e);
        }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public QuizQuestion(string question, string[] options, int correctAnswerIndex)
        {
            Question = question;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}