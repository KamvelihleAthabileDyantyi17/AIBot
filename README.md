# CyberSecurityAIBot

## Overview
This project implements a Cybersecurity Chatbot available both as a Command Line Interface (CLI) and an interactive Graphical User Interface (GUI) using WPF. The bot assists users with cybersecurity awareness through Q&A, interactive quizzes, password strength checking, phishing URL analysis, and task reminders. The application is designed to be both educational and user-friendly.

## Features

### AI-Powered Q&A
- **Pre-Defined and Dynamic Answers:** Users can select from categorized cybersecurity questions (beginner, intermediate, advanced), or ask custom questions. If the question is not recognized, AI generates a relevant response.
- **Sentiment Analysis:** The bot provides basic sentiment detection for user messages.

### Cybersecurity Quiz
- **Interactive Quiz:** Users can start a quiz to test their cybersecurity knowledge. Questions cover strong passwords, phishing, HTTPS, malware, and authentication. The system tracks the score and explains each answer.
- **Score Tracking:** At the end of the quiz, users receive their score and explanations for learning.

### Task and Reminder System
- **Task Management:** Add tasks or reminders (e.g., "Enable 2FA", "Review Privacy Settings") directly from the GUI.
- **Reminders:** Tasks can have scheduled reminders.

### Activity Logging
- **Action Log:** All key user and bot actions (e.g., adding tasks, quiz results) are logged for review.

### Security Tools
- **Password Strength Checking:** Users can check the strength of a password.
- **Phishing Link Checking:** Enter a URL to analyze it for phishing indicators.

### Enhanced User Interfaces
- **GUI (WPF):** Modern, interactive desktop experience with chat, tasks, quizzes, and activity log.
- **CLI:** Classic terminal-based chat and quiz interface, complete with ASCII art and improved formatting.

## Architecture

- **Main Logic:** Handles user input, determines if it’s a command, quiz interaction, or general question.
- **Chatbot Engine:** Processes natural language input and provides context-aware responses.
- **Quiz Engine:** Modular system to load and present quiz questions, check answers, and provide feedback.
- **Models:** Unified models for messages, tasks, quiz questions, and activity log entries.
- **Observable Collections (GUI):** Real-time updates for chat, tasks, and logs in the WPF interface.
- **CI/CD:** GitHub Actions workflow for automated testing, linting, and deployment.

## Getting Started

### Requirements
- .NET 6.0+ (for both CLI and GUI)
- Visual Studio 2022+ recommended for GUI (WPF) development

### Running the CLI

```bash
cd CyberSecurittChatBot
dotnet run
```

### Running the GUI

```bash
cd GUI
dotnet run
```

## Usage

- Type `help` to see available commands.
- Enter "start quiz" or "quiz" to begin an interactive security quiz.
- Add a task by typing phrases like "remind me to update my password".
- Type "password" to check password strength.
- Type "checklink" to analyze a URL for phishing.

## Example CLI Session

```
Welcome to the Cybersecurity Chatbot!
Type 'help' for a list of commands.
> quiz
Starting cybersecurity quiz! Answer the questions that appear.
Q1: What's a strong password characteristic?
  1: password123
  2: 123456
  3: J#f9!2qLp8
  4: mypetname
> 3
Correct! Strong passwords contain uppercase, lowercase, numbers, and special characters.
...
```

## Example GUI Features

- Chat panel for Q&A and bot responses
- Task/reminder panel with due dates
- Quiz panel with multiple-choice questions and explanations
- Activity log for tracking interactions

## Contributing

Pull requests are welcome! Please ensure your code passes all tests and linting checks. See [CONTRIBUTING.md](CONTRIBUTING.md) for more details.

## License

[MIT](LICENSE)
