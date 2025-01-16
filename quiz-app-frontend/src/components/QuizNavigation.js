const QuizNavigation = ({
  currentQuestionIndex,
  totalQuestions,
  onNextQuestion,
  onPrevQuestion,
  onFinishQuiz,
}) => {
  const isFirstQuestion = currentQuestionIndex === 0;
  const isLastQuestion = currentQuestionIndex === totalQuestions - 1;

  const handleNextClick = () => {
    if (isLastQuestion) {
      onFinishQuiz();
    } else {
      onNextQuestion();
    }
  };

  return (
    <div className="quiz-navigation">
      <button onClick={onPrevQuestion} disabled={isFirstQuestion}>
        &lt; 
      </button>
      <button onClick={handleNextClick}>
       Next Question
      </button>
    </div>
  );
};

export default QuizNavigation;
