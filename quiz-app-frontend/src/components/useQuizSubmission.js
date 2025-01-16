import { useState } from 'react';

const useQuizSubmission = (apiBaseUrl, userEmailId) => {
  const [score, setScore] = useState(null);

  const handleFinishQuiz = async (quizzes, selectedAnswers) => {
    if (!userEmailId) {
      alert("Please submit your email first.");
      return;
    }

    const answerSubmission = {
      EmailSubmissionId: userEmailId,
      Answers: selectedAnswers.map((answer, index) => ({
        QuizId: quizzes[index].id,
        AnswerText: Array.isArray(answer) ? answer.join(', ') : answer || '',
      })),
    };

    try {
      const response = await fetch(`${apiBaseUrl}/submit-answers`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(answerSubmission),
      });

      if (response.ok) {
        const data = await response.json();
        setScore(data.totalScore);
        alert("Quiz submitted successfully!");
      } else {
        const errorMessage = await response.text();
        alert(`Failed to submit answers: ${errorMessage}`);
      }
    } catch (error) {
      console.error("Error submitting answers:", error);
      alert("An error occurred while submitting your answers.");
    }
  };

  return { score, handleFinishQuiz };
};

export default useQuizSubmission;
